# Introduction #

[Wcf](http://msdn.microsoft.com/en-us/netframework/aa663324.aspx) team did a good job in leveraging the .NET 2.0 logging. This is both in generating correlated log messages that make it possible to relate together events from different activities across different servers and providing a trace parser tool in the SDK, which is [ServiceTraceViewer](http://msdn.microsoft.com/en-us/library/ms732023.aspx).

A sample of a log view for wcf events itself would be:

![http://msdn.microsoft.com/en-us/library/Aa751795.065e702e-2848-4769-8b04-dad9bb411398(en-us,VS.90).gif](http://msdn.microsoft.com/en-us/library/Aa751795.065e702e-2848-4769-8b04-dad9bb411398(en-us,VS.90).gif)


# Leverage ServiceTraceViewer for your own logging #

Nothing can really stop you from creating your own events in a way that ServiceTraceViewer can be really usable for parsing them. As an extra benefit you can correlate your events with wcf activities, which gives you an integrated picture of your system actions then.

//TODO: (SD) TBC