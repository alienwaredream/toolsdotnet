## On identifying the x64 or xOS ##
All praise to http://www.koders.com/csharp/fid495E7D0ED07BA35F5F0CE0EEFE87D7375BF1CF55.aspx

and extracted code is:
```
        internal const ushort PROCESSOR_ARCHITECTURE_INTEL = 0;
        internal const ushort PROCESSOR_ARCHITECTURE_IA64 = 6;
        internal const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9;
        internal const ushort PROCESSOR_ARCHITECTURE_UNKNOWN = 0xFFFF;

[StructLayout(LayoutKind.Sequential)]
        internal struct SYSTEM_INFO
        {
            public ushort wProcessorArchitecture;
            public ushort wReserved;
            public uint dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public UIntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public ushort wProcessorLevel;
            public ushort wProcessorRevision;
        };

        [DllImport("kernel32.dll")]
        internal static extern void GetNativeSystemInfo(ref SYSTEM_INFO lpSystemInfo);

        private enum Platform
        {
            X86,
            X64,
            Unknown
        }

        private static Platform GetPlatform()
        {
            SYSTEM_INFO sysInfo = new SYSTEM_INFO();
            GetNativeSystemInfo(ref sysInfo);

            switch (sysInfo.wProcessorArchitecture)
            {
                case PROCESSOR_ARCHITECTURE_AMD64:
                    return Platform.X64;

                case PROCESSOR_ARCHITECTURE_INTEL:
                    return Platform.X86;

                default:
                    return Platform.Unknown;
            }
        }
```

and sample of usage would be:
```
            var platform = GetPlatform();
 
            if (platform == Platform.Unknown)
                throw new InvalidOperationException("Registering generator tools on this platform is not supported!");

            if (platform == Platform.X64)
                wowPrefix = WOW_NODE_PATTERN;
```

## Classic causes of problems with invoices/billing in telcos ##

### Problems with delivery of all records to network usage db ###

### Problems with correctness of rating ###

#### Quite often synchronization e.g. VPN platform and CM/Rating rules ####
#### Speed and reliability of provisioning ####

### Correlation between ordering and provisioning ###

## WiX do's a and don'ts ##
Do setup project dependencies for wix project, so it depends projects producing binaries that it requires (primary output plus custom tools).

Do use variables

Use some mass tool to produce initial wix fragment (parafin or heat)


## On updating file attributes in wix and beyond ##
http://www.microsoft.com/downloads/en/details.aspx?FamilyId=9BA6FAC6-520B-4A0A-878A-53EC8300C4C2&displaylang=en

On RegFree COM usage

http://neilsleightholm.blogspot.com/2009/08/registration-free-com.html


## On mixing multiple types of UIAlertView with single delegate ##

- (BOOL)alertViewShouldEnableFirstOtherButton:(UIAlertView **)alertView
{
> if (alertView.alertViewStyle == UIAlertViewStyleDefault) {
> > return YES;

> }
> UITextField**textField = [textFieldAtIndex:0](alertView.md);
> if ([textField.text length] == 0)
> {
> > return NO;

> }

> return YES;
}

textFieldIndex (0) is outside of the bounds of the array of text fields


## On multiple waypoints in gpx files - xcode ##
http://stackoverflow.com/questions/9439495/when-using-gpx-in-xcode-to-simulate-location-changes-is-there-a-way-to-control
```
<?xml version="1.0"?>
<gpx version="1.1" creator="Xcode"> 
    <wpt lat="37.331705" lon="-122.030237"></wpt>
    <wpt lat="37.331705" lon="-122.030337"></wpt>
    <wpt lat="37.331705" lon="-122.030437"></wpt>
    <wpt lat="37.331705" lon="-122.030537"></wpt>
</gpx>
<?xml version="1.0"?>
<gpx version="1.1" creator="Xcode"> 
    <wpt lat="35.641043" lon="139.609592">
        <name>63.265614</name>
    </wpt>
    <wpt lat="35.641043" lon="139.609592">
        <name>63.265614</name>
    </wpt>
    <wpt lat="35.640779" lon="139.609641">
        <name>45.113590</name>
    </wpt>
    <wpt lat="35.640771" lon="139.609642">
        <name>45.052517</name>
    </wpt>
    <wpt lat="35.640770" lon="139.609680">
        <name>45.798065</name>
    </wpt>
    ...
</gpx>
```

## Emoji icons ##

http://barrow.io/posts/iphone-emoji/

## Xcode shortcuts  & my tricks ##

Alt click on the file in navigator opens the file in the assistant panel, even if panel was not open before!

Use Alt-Run and then Analyze instead of Product->Analyze. As first will only analyze my target and latter will go though all of the libs included. Which is getting really stuck on GData with its 200+ files in my case.

# iphone sqlite helpers #

# oracle #
== 		Message	"Unable to load DLL 'OraOps11w.dll': The specified module could not be found. (Exception from HRESULT: 0x8007007E)"	string
##  ##
http://stackoverflow.com/questions/1596167/where-to-download-microsoft-visual-c-2003-redistributable