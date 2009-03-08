using System;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using ILMerging;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace BuildTasks
{
    public class ILMerge : Task
    {
        #region Fields
        ILMerging.ILMerge mergeSession;

        private ILMerging.ILMerge.Kind targetKind; 
        #endregion

        #region Properties that are mapped indirectly to ILMerge
        [Required()]
        public virtual ITaskItem[] InputAssemblies { get; set; }

        public virtual ITaskItem[] SearchPaths { get; set; }

        public virtual string TargetKind
        {
            get
            {
                return targetKind.ToString();
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    if (Enum.IsDefined(typeof(ILMerging.ILMerge.Kind), value))
                    {
                        targetKind = (ILMerging.ILMerge.Kind)Enum.Parse(typeof(ILMerging.ILMerge.Kind), value);
                    }
                    else
                    {
                        Log.LogError(String.Format(CultureInfo.InvariantCulture,
                            "Invalid value of TargetKind {0}. Possible values {1}",
                            value, Enum.GetNames(typeof(ILMerging.ILMerge.Kind))));
                        throw new Exception(String.Format(CultureInfo.InvariantCulture,
                            "Invalid value of TargetKind {0}. Possible values {1}",
                            value, Enum.GetNames(typeof(ILMerging.ILMerge.Kind))));

                    }
                }
                else
                {
                    targetKind = ILMerging.ILMerge.Kind.SameAsPrimaryAssembly;
                    Log.LogMessage(MessageImportance.High, String.Format(CultureInfo.InvariantCulture,
                        "TargetKind property is not set to any of the legitimate values: {0}. Default value of {1} is applied.",
                        Enum.GetNames(typeof(ILMerging.ILMerge.Kind)), ILMerging.ILMerge.Kind.SameAsPrimaryAssembly));
                }
            }
        } 
        #endregion

        #region Properties that belong directly to ILMerge

        public bool AllowMultipleAssemblyLevelAttributes { get; set; }
        public bool AllowWildCards { get; set; }
        public bool AllowZeroPeKind { get; set; }
        public string AttributeFile { get; set; }
        public bool Closed { get; set; }
        public bool CopyAttributes { get; set; }
        public bool DebugInfo { get; set; }
        public bool DelaySign { get; set; }
        public string ExcludeFile { get; set; }
        public int FileAlignment { get; set; }
        public bool Internalize { get; set; }
        public string KeyFile { get; set; }
        public bool EnableLog { get; set; }
        public string LogFile { get; set; }
        public string OutputFile { get; set; }
        public bool PreserveShortBranches { get; set; }
        public bool PublicKeyTokens { get; set; }

        [Output()]
        public bool StrongNameLost { get { return mergeSession.StrongNameLost; } }
        public bool UnionMerge { get; set; }
        public Version Version { get; set; }
        public bool XmlDocumentation { get; set; }

        #endregion

        #region Extra properties

        public bool ApplyPrimaryAssemblyDate { get; set; }

        #endregion

        public ILMerge()
        {
            mergeSession = new ILMerging.ILMerge();
        }

        #region Implementation of Task methods
        
        public override bool Execute()
        {

            try
            {
                if (mergeSession == null)
                    mergeSession = new ILMerging.ILMerge();

                mergeSession.AllowMultipleAssemblyLevelAttributes = this.AllowMultipleAssemblyLevelAttributes;
                mergeSession.AllowWildCards = this.AllowWildCards;
                mergeSession.AllowZeroPeKind = this.AllowZeroPeKind;
                mergeSession.AttributeFile = this.AttributeFile;
                mergeSession.Closed = this.Closed;
                mergeSession.CopyAttributes = this.CopyAttributes;
                mergeSession.DebugInfo = this.DebugInfo;
                mergeSession.DelaySign = this.DelaySign;
                mergeSession.ExcludeFile = this.ExcludeFile;
                mergeSession.FileAlignment = this.FileAlignment;
                mergeSession.Internalize = this.Internalize;
                mergeSession.KeyFile = this.KeyFile;
                mergeSession.Log = this.EnableLog;
                mergeSession.LogFile = this.LogFile;
                mergeSession.OutputFile = this.OutputFile;
                mergeSession.PreserveShortBranches = this.PreserveShortBranches;
                mergeSession.PublicKeyTokens = this.PublicKeyTokens;
                mergeSession.UnionMerge = this.UnionMerge;
                mergeSession.Version = this.Version;
                mergeSession.XmlDocumentation = this.XmlDocumentation;

                List<string> assembliesToMerge = new List<string>();

                if (InputAssemblies == null || InputAssemblies.Length == 0)
                {
                    throw new ArgumentException("InputAssemblies parameter can't be null and should be non-zero length!",
                        "InputAssemblies");
                }

                foreach (ITaskItem item in InputAssemblies)
                {
                    assembliesToMerge.Add(item.ItemSpec);
                }

                mergeSession.SetInputAssemblies(assembliesToMerge.ToArray());

                if (SearchPaths != null && SearchPaths.Length > 0)
                {

                    var assemblySearchPaths = new List<string>() { "." };

                    foreach (ITaskItem item in SearchPaths)
                    {
                        if (!String.IsNullOrEmpty(item.ItemSpec))
                        {
                            assemblySearchPaths.Add(Path.Combine(BuildEngine.ProjectFileOfTaskNode, item.ItemSpec));
                        }
                    }

                    mergeSession.SetSearchDirectories(assemblySearchPaths.ToArray());
                }

                Log.LogMessage(MessageImportance.Normal, "Merging {0} assemblies to '{1}'.", assembliesToMerge.Count, OutputFile);
                mergeSession.Merge();
            }
            catch (Exception e)
            {
                Log.LogErrorFromException(e);
                return false;
            }

            return true;
        } 
        #endregion

    }
}
