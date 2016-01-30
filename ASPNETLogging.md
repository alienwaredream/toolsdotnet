# Introduction #

Either you create a web project as a "web site" or "web application" logging in it has got its own specifics.
That is mostly affected by understanding the security account your application runs under and conditional compilation.


# Details #
## Conditional Compilation ##
Do you use System.Diagnostics.TraceSource or System.Diagnostics.Trace in your web application or web site, but don't see it really logging?
Does it skip over the trace lines in the debugger?
Then you are most probably facing the conditional compilation issues. Trace methods are attributed with the [Conditional("TRACE")], so unless this conditional compilation constant is set, the code calling those conditional methods will not be compiled.

Setting the conditional compilation constants in web site project can be a tricky task by itself. Read K. Scott Allen's introductory entry on it http://odetocode.com/Blogs/scott/archive/2005/12/01/2559.aspx. See how easily one can get confused as Phil Haack has an excellent information onto conditional compilation specifics for web apps http://haacked.com/archive/2007/09/16/conditional-compilation-constants-and-asp.net.aspx and read K. Scott Allen's another entry as it demystifies some of the Phil's observations http://odetocode.com/Blogs/scott/archive/2007/09/24/11413.aspx.

Main deduction from the above links is:
The compiler options on the @Page directive and in the web.config are not additive. Setting in the @Page directive overrides the one in the web config.

Lets re-analyze it from the perspective of using System.Diagnostics.Trace and System.Diagnostics.TraceSource from your web app.... //TODO: (SD) Complete

So rather minimalistic configuration for using trace to log into some non-default listener is:
```
  <system.diagnostics>
   <sources>
      <source name="Test" switchValue="All">
        <listeners>
          <add name="XmlLogger" type="Tools.Logging.XmlWriterRollingTraceListener, Tools.Logging"
               initializeData="c:\logs\singlefile.xml" />
        </listeners>
      </source>
    </sources>
  </system.diagnostics>
  <system.codedom>
    <compilers>
      <compiler language="c#;cs;csharp" extension=".cs" warningLevel="4" compilerOptions="/d:TRACE"
                type="Microsoft.CSharp.CSharpCodeProvider, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
        <providerOption name="CompilerVersion" value="v3.5"/>
        <providerOption name="WarnAsError" value="false"/>
      </compiler>
    </compilers>
  </system.codedom>
```

If you use VB.NET you'll have to have vb compiler configured inside the system.codedom/compilers element:
```
<compiler language="vb;vbs;visualbasic;vbscript" extension=".vb" warningLevel="4" compilerOptions="/d:TRACE"
                  type="Microsoft.VisualBasic.VBCodeProvider, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
          <providerOption name="CompilerVersion" value="v3.5"/>
          <providerOption name="OptionInfer" value="true"/>
          <providerOption name="WarnAsError" value="false"/>
        </compiler>
```

# References #
ASP.NET trace, core and customizations
http://msdn.microsoft.com/en-us/magazine/cc163927.aspx