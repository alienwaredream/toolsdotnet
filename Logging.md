# Introduction #

Specifics of logging in ASP.NET web sites and applications [ASPNETLogging](ASPNETLogging.md)


# Setting up logging programaticaly #

When setting source programaticaly pay attention to setting the Switch.Level to the required value. Failure to do so will leave Switch at default value of Off, which means no log entries!

```
 source.Listeners.Add(new ConsoleTraceListener());
 source.Switch.Level = SourceLevels.All; // Set required level here
```

# Set up logging for windows workflow #
```
<configuration>
  <system.diagnostics>
    <sources>
      <source name="System.Workflow.Runtime" >
        <listeners>
          <add name = "System.Workflow"/>
        </listeners>
      </source>
      <source name="System.Workflow.Runtime.Hosting">
        <listeners>
          <add name="System.Workflow"/>
        </listeners>
      </source>
      <source name="System.Workflow.Activities">
        <listeners>
          <add name="System.Workflow"/>
        </listeners>
      </source>
    </sources>
    <sharedListeners>
      <add name="System.Workflow"
           type="System.Diagnostics.TextWriterTraceListener"
           initializeData="c:\Logs\WFTrace.log"
           traceOutputOptions="DateTime,ProcessId"/>
    </sharedListeners>
    <switches>
      <add name="System.Workflow.LogToTraceListeners" value="1"/>
      <add name="System.Workflow.Runtime" value="All" />
      <add name="System.Workflow.Runtime.Hosting" value="All" />
      <add name="System.Workflow.Runtime.Tracking" value="All" />
      <add name="System.Workflow.Activities" value="All" />
      <add name="System.Workflow.Activities.Rules" value="All" />
    </switches>
  </system.diagnostics>


</configuration>
```

# References #
John Robbins on .NET 2.0 logging http://msdn.microsoft.com/en-us/magazine/cc163767.aspx