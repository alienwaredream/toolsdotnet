# Introduction #

The intent behind is to learn WinDbg debugging techniques for .Net and beyond.

# Where to start #
John Robbins's book http://www.amazon.com/Debugging-Microsoft-NET-2-0-Applications/dp/0735622027

Tess's blog http://blogs.msdn.com/tess/

# Stage 1 links #
Tess's debugging labs starting point: http://blogs.msdn.com/tess/pages/net-debugging-demos-information-and-setup-instructions.aspx

Johan's blog: http://blogs.msdn.com/johan/archive/2007/11/13/getting-started-with-windbg-part-i.aspx

# Loading SOS #

Two ways:

- .load sos

- .loadby sos mscorwks

The first requires to copy the corresponding version of sos.dll into the windbg folder.

The second is suggested by John Robbins in his book, though in the comments to the Johan's entry, it is rightly noticed that second option requires the .net runtime to be loaded already.


---

_Saturday, November 24, 2007 1:15 PM by Cory Foy
@Aaron - Just to add - you can only do that if the framework has already been loaded. For example, you can't do that at the beginning of a process because mscorwks hasn't been loaded yet._

If you need to get SOS loaded as soon as mscorwks is loaded, you can do:

sxe ld mscorwks

g

.loadby sos mscorwks

Generally most of us do .loadby sos mscorwks, but there are circumstances where that won't work.

Cory_---_

# Memory #

## Garbage collection ##

### Mid-life crisis ###
http://blogs.msdn.com/ricom/archive/2003/12/04/41281.aspx

### Collection modes ###
http://blogs.msdn.com/clyon/archive/2004/09/08/226981.aspx

### Latency modes ###
http://blogs.msdn.com/clyon/archive/2007/03/12/new-in-orcas-part-3-gc-latency-modes.aspx

# Misc Links #
MS Jeff Stucky's debugging webcast: http://msevents.microsoft.com/CUI/WebCastEventDetails.aspx?culture=en-US&EventID=1032290859&CountryCode=US

A web chat after Jeff's debugging demo: http://blogs.technet.com/mscom/archive/2006/03/31/mscom-ops-march-debug-madness-2nd-session-q-amp-a-debugging-clr-internals.aspx

# Ideas for extra labs #

## Identify loader heap leaks ##
Although can be handled by Lab 6 of Tess, will see when I'm there

## Mid-life crisis analysis ##
http://blogs.msdn.com/ricom/archive/2003/12/04/41281.aspx

Interesting point to visualize would be John Robbin's sample of having a StringBuilder inside or outside the long loop.