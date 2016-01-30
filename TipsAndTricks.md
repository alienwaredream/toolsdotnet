## Helper attributes ##
```
     DebuggerDisplay("{ToString()}")
```

## How to register vs 2008/9 environment variables ##
So you can execute then gacutil, ildasm whatever without providing a path. I guess this is just what VS Command Line does, but never had a time to look. Sample from the Tess's blog:

---

CALL "%VS90COMNTOOLS%\vsvars32.bat" > NULL
gacutil.exe /if "$(TargetPath)"


this will add the assembly to the GAC when you build.  Note that on 2005 you would have to change the path to be %VS80COMNTOOLS% rather than %VS90COMNTOOLS%

---

## Using forfiles ##
Don't look for it on you XP machine. By default is present though on server systems.

forfiles /p [Path](Path.md) /s /m **.** /d -3 /c "cmd /c del @file : date >= 3 days"

official help:

---

FORFILES [/P pathname] [/M searchmask] [/S]
> [/C command] [/D [+ | -] {MM/dd/yyyy | dd}]

Description:
> Selects a file (or set of files) and executes a
> command on that file. This is helpful for batch jobs.

Parameter List:
> /P    pathname      Indicates the path to start searching.
> > The default folder is the current working directory (.).


> /M    searchmask    Searches files according to a searchmask. The default searchmask is ''.

> /S                  Instructs forfiles to recurse into subdirectories. Like "DIR /S".

> /C    command       Indicates the command to execute for each file.
> > Command strings should be wrapped in double
> > quotes.


> The default command is "cmd /c echo @file".

> The following variables can be used in the command string:
> @file    - returns the name of the file.
> @fname   - returns the file name without extension.
> @ext     - returns only the extension of the file.
> @path    - returns the full path of the file.
> @relpath - returns the relative path of the file.
> @isdir   - returns "TRUE" if a file type is
> > a directory, and "FALSE" for files.

> @fsize   - returns the size of the file in bytes.
> @fdate   - returns the last modified date of the file.
> @ftime   - returns the last modified time of the file.

> To include special characters in the command
> line, use the hexadecimal code for the character
> in 0xHH format (ex. 0x09 for tab). Internal
> CMD.exe commands should be preceded with
> "cmd /c".

> /D    date          Selects files with a last modified date greater
> > than or equal to (+), or less than or equal to
> > (-), the specified date using the
> > "MM/dd/yyyy" format; or selects files with a
> > last modified date greater than or equal to (+)
> > the current date plus "dd" days, or less than or
> > equal to (-) the current date minus "dd" days. A
> > valid "dd" number of days can be any number in
> > the range of 0 - 32768.
> > "+" is taken as default sign if not specified.


> /?                  Displays this help message.

Examples:
> FORFILES /?
> FORFILES
> FORFILES /P C:\WINDOWS /S /M DNS**.**
> FORFILES /S /M **.txt /C "cmd /c type @file | more"
> FORFILES /P C:\ /S /M**.bat
> FORFILES /D -30 /M **.exe
> > /C "cmd /c echo @path 0x09 was changed 30 days ago"

> FORFILES /D 01/01/2001
> > /C "cmd /c echo @fname is new since Jan 1st 2001"

> FORFILES /D +9/10/2008 /C "cmd /c echo @fname is new today"
> FORFILES /M**.exe /D +1
> FORFILES /S /M **.doc /C "cmd /c echo @fsize"
> FORFILES /M**.txt /C "cmd /c if @isdir==FALSE notepad.exe @file"

---


## Using telnet to verify if port is open, host is available ##
If connection succeeded a black window is open to send data on.

---

telnet host port

---


## Runing 3.5 builds on the team build 2005 ##
Sample program:
namespace MsBuild
{
> public class Program
> {
> > public static int Main(string[.md](.md) args)
> > {
> > > for (int argIndex = 0; argIndex < args.Length; argIndex++)
> > > {
> > > > if (args[argIndex](argIndex.md).Contains(" "))
> > > > {
> > > > > string quotedArg = string.Format("\"{0}\"", args[argIndex](argIndex.md));
> > > > > args[argIndex](argIndex.md) = quotedArg;

> > > > }

> > > }
> > > string arguments = string.Join(" ", args);
> > > Process process = Process.Start(
> > > > @"C:\WINDOWS\Microsoft.NET\Framework\v3.5\MSBuild.exe",
> > > > arguments);

> > > process.WaitForExit();
> > > return process.ExitCode;

> > }

> }
}
Author: Mitch Denny http://tfsnow.wordpress.com/2007/08/31/building-net-35-applications-with-team-build-2005/

## Using cacls ##
Displays or modifies access control lists (ACLs) of files

---

CACLS filename [/T] [/E] [/C] [/G user:perm] [/R user [...]]
> [/P user:perm [...]] [/D user [...]]
> filename      Displays ACLs.
> /T            Changes ACLs of specified files in
> > the current directory and all subdirectories.

> /E            Edit ACL instead of replacing it.
> /C            Continue on access denied errors.
> /G user:perm  Grant specified user access rights.
> > Perm can be: R  Read
> > > W  Write
> > > C  Change (write)
> > > F  Full control

> /R user       Revoke specified user's access rights (only valid with /E).
> /P user:perm  Replace specified user's access rights.
> > Perm can be: N  None
> > > R  Read
> > > W  Write
> > > C  Change (write)
> > > F  Full control

> /D user       Deny specified user access.
Wildcards can be used to specify more that one file in a command.
You can specify more than one user in a command.

Abbreviations:
> CI - Container Inherit.
> > The ACE will be inherited by directories.

> OI - Object Inherit.
> > The ACE will be inherited by files.

> IO - Inherit Only.
> > The ACE does not apply to the current file/directory.

---

Sample: CACLS %WINDIR%\assembly /e /t /p [DOMAIN|MACHINENAME]\useraccount:R



## Configuring HTTP and HTTPS (namespace reservations, listen ips, etc) ##
http://msdn.microsoft.com/en-us/library/ms733768.aspx

## Enabling more than 2 concurrent connections for the HttpManager - Registry ##
Source: Tess

---

Windows Registry Editor Version 5.00

[HKEY\_CURRENT\_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings]

"MaxConnectionsPerServer"=dword:0000000a

"MaxConnectionsPer1\_0Server"=dword:0000000a

---

## ADPlus config file ##
Sample (for the stackoverflow exception):
```
<ADPlus>
    <Settings>
        <RunMode> CRASH </RunMode>
    </Settings>

    <!-- defining and configuring exceptions -->
    <Exceptions>
        <!-- First we redefine all exceptions -->
        <Config>
            <Code>AllExceptions</Code>
            <Actions1>Log</Actions1>
            <Actions2>MiniDump;Log;EventLog</Actions2>
        </Config>

        <NewException>
            <Code> 0xe053534f </Code>
            <Name> Unknown_Exception </Name>
        </NewException>
        <!-- Configuring the custom exception -->
        <Config>
            <Code> 0xe053534f </Code>
            <Actions1>FullDump;Log;EventLog</Actions1>
            <Actions2>FullDump;Log;EventLog</Actions2>
        </Config>
    </Exceptions>
</ADPlus>
```

## A nasty thing about SC.EXE ##
There is a space after every parameter, e.g. binPath=[space](space.md)xyz!!

## Troubleshooting msi installer issues ##
msiexec /i product.msi /L\*v c:\log.txt

## Changing IIS virtual directory ##

### IIS 5/6 ###
```
cscript.exe adsutil.vbs SET W3SVC/1/root/MyVirtualDir/path path

Sample: cscript.exe adsutil.vbs SET W3SVC/1/root/MyWeb/path E:\web\myweb\
```
### IIS 7 ###
```
c:\Windows\System32\inetsrv\appcmd.exe set app /app.name "Default Web Site/MyVirtualDir"  /[path='/'].physicalPath:path

Sample: c:\Windows\System32\inetsrv\appcmd.exe set app /app.name "Default Web Site/MyWeb"  /[path='/'].physicalPath:E:\web\myweb\
```

## Stop/Start MSDTC ##

```

net stop msdtc
net start msdtc

```

## finding and deleting embedded objects in Excel ##


ALT-F11 - Open macro editor

Ctrl-G - Open immediate window

Then in immediate window:


> ?Activesheet.Shapes(1).delete

step with this through all sheets until all Shapes are deleted.

## Windows 7 wifi hotspot ##

http://www.techradar.com/news/computing/pc/how-to-turn-your-windows-7-laptop-into-a-wireless-hotspot-657138