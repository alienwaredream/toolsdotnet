
```
public class ResolveKeySource : TaskExtension
{
    // Fields
    private int autoClosePasswordPromptShow = 15;
    private int autoClosePasswordPromptTimeout = 20;
    private string certificateFile;
    private string certificateThumbprint;
    private string keyFile;
    private const string pfxFileContainerPrefix = "VS_KEY_";
    private const string pfxFileExtension = ".pfx";
    private static Hashtable pfxKeysToIgnore = new Hashtable(StringComparer.OrdinalIgnoreCase);
    private string resolvedKeyContainer = string.Empty;
    private string resolvedKeyFile = string.Empty;
    private string resolvedThumbprint = string.Empty;
    private bool showImportDialogDespitePreviousFailures;
    private bool suppressAutoClosePasswordPrompt;

    // Methods
    public override bool Execute()
    {
        return (this.ResolveAssemblyKey() && this.ResolveManifestKey());
    }

    private bool ResolveAssemblyKey()
    {
        bool successfullyImported = true;
        if ((this.KeyFile != null) && (this.KeyFile.Length > 0))
        {
            string strA = string.Empty;
            try
            {
                strA = Path.GetExtension(this.KeyFile);
            }
            catch (ArgumentException exception)
            {
                base.Log.LogErrorWithCodeFromResources("ResolveKeySource.InvalidKeyName", new object[] { this.KeyFile, exception.Message });
                successfullyImported = false;
            }
            if (!successfullyImported)
            {
                return successfullyImported;
            }
            if (string.Compare(strA, ".pfx", StringComparison.OrdinalIgnoreCase) != 0)
            {
                this.ResolvedKeyFile = this.KeyFile;
                return successfullyImported;
            }
            successfullyImported = false;
            FileStream stream = null;
            try
            {
                string wszKeyContainer = string.Empty;
                string str3 = Environment.UserDomainName + @"\" + Environment.UserName;
                byte[] bytes = Encoding.Unicode.GetBytes(str3.ToLower(CultureInfo.InvariantCulture));
                stream = File.OpenRead(this.KeyFile);
                int length = (int) stream.Length;
                byte[] buffer = new byte[length + bytes.Length];
                stream.Read(buffer, 0, length);
                Array.Copy(bytes, 0, buffer, length, bytes.Length);
                byte[] buffer3 = new MD5CryptoServiceProvider().ComputeHash(buffer);
                StringBuilder builder = new StringBuilder("VS_KEY_");
                foreach (byte num2 in buffer3)
                {
                    builder.Append(num2.ToString("X02", CultureInfo.InvariantCulture));
                }
                wszKeyContainer = builder.ToString();
                IntPtr zero = IntPtr.Zero;
                uint publicKeyBlobSize = 0;
                if (NativeMethods.StrongNameGetPublicKey(wszKeyContainer, IntPtr.Zero, 0, out zero, out publicKeyBlobSize) && (zero != IntPtr.Zero))
                {
                    NativeMethods.StrongNameFreeBuffer(zero);
                    successfullyImported = true;
                }
                else
                {
                    if (this.ShowImportDialogDespitePreviousFailures || !pfxKeysToIgnore.Contains(wszKeyContainer))
                    {
                        Array.Resize<byte>(ref buffer, length);
                        ImportPFXKeyDlg dlg = new ImportPFXKeyDlg(wszKeyContainer, ref buffer, this.KeyFile);
                        dlg.SuppressCountdown = this.SuppressAutoClosePasswordPrompt;
                        dlg.CountDownTimer = this.AutoClosePasswordPromptTimeout;
                        dlg.CountDownTimerShow = this.AutoClosePasswordPromptShow;
                        dlg.ShowDialog();
                        successfullyImported = dlg.SuccessfullyImported;
                        if (!successfullyImported)
                        {
                            pfxKeysToIgnore[wszKeyContainer] = string.Empty;
                        }
                    }
                    if (!successfullyImported)
                    {
                        base.Log.LogErrorWithCodeFromResources("ResolveKeySource.KeyImportError", new object[] { this.KeyFile });
                    }
                }
                if (successfullyImported)
                {
                    this.ResolvedKeyContainer = wszKeyContainer;
                }
            }
            catch (Exception exception2)
            {
                ExceptionHandling.RethrowUnlessFileIO(exception2);
                base.Log.LogErrorWithCodeFromResources("ResolveKeySource.KeyMD5SumError", new object[] { this.KeyFile, exception2.Message });
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }
        return successfullyImported;
    }

    private bool ResolveManifestKey()
    {
        bool successfullyImported = false;
        bool flag2 = false;
        if (!string.IsNullOrEmpty(this.CertificateThumbprint))
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadWrite);
                if (store.Certificates.Find(X509FindType.FindByThumbprint, this.CertificateThumbprint, false).Count == 1)
                {
                    flag2 = true;
                    this.ResolvedThumbprint = this.CertificateThumbprint;
                    successfullyImported = true;
                }
            }
            finally
            {
                store.Close();
            }
        }
        if (!string.IsNullOrEmpty(this.CertificateFile) && !flag2)
        {
            if (!File.Exists(this.CertificateFile))
            {
                base.Log.LogErrorWithCodeFromResources("ResolveKeySource.CertificateNotInStore", new object[0]);
                return successfullyImported;
            }
            if (X509Certificate2.GetCertContentType(this.CertificateFile) == X509ContentType.Pfx)
            {
                bool flag3 = false;
                X509Certificate2 certificate = new X509Certificate2();
                X509Store store2 = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                try
                {
                    store2.Open(OpenFlags.ReadWrite);
                    certificate.Import(this.CertificateFile, (string) null, X509KeyStorageFlags.PersistKeySet);
                    store2.Add(certificate);
                    this.ResolvedThumbprint = certificate.Thumbprint;
                    flag3 = true;
                    successfullyImported = true;
                }
                catch (CryptographicException)
                {
                }
                finally
                {
                    store2.Close();
                }
                if (!flag3 && this.ShowImportDialogDespitePreviousFailures)
                {
                    ImportPFXKeyDlg dlg = new ImportPFXKeyDlg(this.CertificateFile);
                    dlg.SuppressCountdown = this.SuppressAutoClosePasswordPrompt;
                    dlg.CountDownTimer = this.AutoClosePasswordPromptTimeout;
                    dlg.CountDownTimerShow = this.AutoClosePasswordPromptShow;
                    dlg.ShowDialog();
                    successfullyImported = dlg.SuccessfullyImported;
                    this.ResolvedThumbprint = dlg.Thumbprint;
                }
                if (!successfullyImported)
                {
                    base.Log.LogErrorWithCodeFromResources("ResolveKeySource.KeyImportError", new object[] { this.CertificateFile });
                }
                return successfullyImported;
            }
            X509Store store3 = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                X509Certificate2 certificate2 = new X509Certificate2(this.CertificateFile);
                store3.Open(OpenFlags.ReadWrite);
                store3.Add(certificate2);
                this.ResolvedThumbprint = certificate2.Thumbprint;
                return true;
            }
            catch (CryptographicException)
            {
                base.Log.LogErrorWithCodeFromResources("ResolveKeySource.KeyImportError", new object[] { this.CertificateFile });
                return successfullyImported;
            }
            finally
            {
                store3.Close();
            }
        }
        if ((!flag2 && !string.IsNullOrEmpty(this.CertificateFile)) && !string.IsNullOrEmpty(this.CertificateThumbprint))
        {
            base.Log.LogErrorWithCodeFromResources("ResolveKeySource.CertificateNotInStore", new object[0]);
            return false;
        }
        return true;
    }

    // Properties
    public int AutoClosePasswordPromptShow
    {
        get
        {
            return this.autoClosePasswordPromptShow;
        }
        set
        {
            this.autoClosePasswordPromptShow = value;
        }
    }

    public int AutoClosePasswordPromptTimeout
    {
        get
        {
            return this.autoClosePasswordPromptTimeout;
        }
        set
        {
            this.autoClosePasswordPromptTimeout = value;
        }
    }

    public string CertificateFile
    {
        get
        {
            return this.certificateFile;
        }
        set
        {
            this.certificateFile = value;
        }
    }

    public string CertificateThumbprint
    {
        get
        {
            return this.certificateThumbprint;
        }
        set
        {
            this.certificateThumbprint = value;
        }
    }

    public string KeyFile
    {
        get
        {
            return this.keyFile;
        }
        set
        {
            this.keyFile = value;
        }
    }

    [Output]
    public string ResolvedKeyContainer
    {
        get
        {
            return this.resolvedKeyContainer;
        }
        set
        {
            this.resolvedKeyContainer = value;
        }
    }

    [Output]
    public string ResolvedKeyFile
    {
        get
        {
            return this.resolvedKeyFile;
        }
        set
        {
            this.resolvedKeyFile = value;
        }
    }

    [Output]
    public string ResolvedThumbprint
    {
        get
        {
            return this.resolvedThumbprint;
        }
        set
        {
            this.resolvedThumbprint = value;
        }
    }

    public bool ShowImportDialogDespitePreviousFailures
    {
        get
        {
            return this.showImportDialogDespitePreviousFailures;
        }
        set
        {
            this.showImportDialogDespitePreviousFailures = value;
        }
    }

    public bool SuppressAutoClosePasswordPrompt
    {
        get
        {
            return this.suppressAutoClosePasswordPrompt;
        }
        set
        {
            this.suppressAutoClosePasswordPrompt = value;
        }
    }
}
 

```