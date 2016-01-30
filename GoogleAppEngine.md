## Uploading app from mac os bash ##

```
bash /Users/stanislavdvoychenko/eclipse/plugins/com.google.appengine.eclipse.sdkbundle_1.5.0.r36v201105191508/appengine-java-sdk-1.5.0.1/bin/appcfg.sh update .
```

## Finding out if running in the cloud or local ##
from: http://code.google.com/appengine/docs/java/runtime.html


> com.google.appengine.runtime.environment is "Production" when running on App Engine, and "Development" when running in the development server.

> In addition to using System.getProperty(), you can access system properties using our type-safe API. For example:
```
    if (SystemProperty.environment.value() ==
        SystemProperty.Environment.Value.Production) {
        // The app is running on App Engine...
    }
```
> com.google.appengine.runtime.version is the version ID of the runtime environment, such as "1.3.0". You can get the version by invoking the following: String version = SystemProperty.version.get();
> com.google.appengine.application.id is the application's ID. You can get the ID by invoking the following: String ID = SystemProperty.applicationId.get();

## About static and resource files ##
http://code.google.com/appengine/docs/java/config/appconfig.html


Many web applications have files that are served directly to the user's browser, such as images, CSS style sheets, or browser JavaScript code. These are known as static files because they do not change, and can benefit from web servers dedicated just to static content. App Engine serves static files from dedicated servers and caches that are separate from the application servers.

Files that are accessible by the application code using the filesystem are called resource files. These files are stored on the application servers with the app.

By default, all files in the WAR are treated as both static files and resource files, except for JSP files, which are compiled into servlet classes and mapped to URL paths, and files in the WEB-INF/ directory, which are never served as static files and always available to the app as resource files.

You can adjust which files are considered static files and which are considered resource files using elements in the appengine-web.xml file. The 

&lt;static-files&gt;

 element specifies patterns that match file paths to include and exclude from the list of static files, overriding or amending the default behavior. Similarly, the 

&lt;resource-files&gt;

 element specifies which files are considered resource files.

## local admin page ##
http://localhost:8888/_ah/admin

## Profiling java app locally and in the cloud ##
http://googleappengine.blogspot.com/2010/03/easy-performance-profiling-with.html

The bellow only works in the cloud and shows you what you can already see from the log as cpu time per request (can be a bit more granular though):
```
		QuotaService qs = QuotaServiceFactory.getQuotaService();
        long start = qs.getCpuTimeInMegaCycles();

        double cpuSeconds = qs.convertMegacyclesToCpuSeconds(end - start);
		
		resp.setHeader("gaecost", String.valueOf(cpuSeconds));
```