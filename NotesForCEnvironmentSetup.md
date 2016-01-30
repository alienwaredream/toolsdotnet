## Logging configuration ##

Be sure to review and configure at least following in Your configuration log4net.template:

```
    <appender name="AdoNetAppender" 
              xdt:Locator="Match(name)">
      <connectionString value="Data source=.\sqlexpress;Initial Catalog=ELMAH_c;Integrated Security=SSPI" 
                        xdt:Transform="Replace"/>
    </appender>
```

Otherwise, performance of the app is seriously hindered.

You'll need elmah database creation script, it can be found here:
http://code.google.com/p/elmah/wiki/Downloads