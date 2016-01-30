# Introduction #

Tools.Tracing is dedicated to the idea of building enterprise level log/tracing solution.


# Areas #

## UI ##
Virtual trace listview. Extensible configurable columns/views.

## Server ##
Runs Remoting, WCF proprietary or HTTP/REST/json

## Backing storage ##
File, MSMQ, Sql Broker

# Backlog #

## Cleanup current UI and infrastructure ##

## Move to more interoperable medium ##
> Currently it is proprietary TraceEvent that is passed around.  It should be changed to either XElement or just string. Although, some challenges with those primitive types will be:

- where to keep extra classification information like Processed, context, etc. Can be done in the "convention" way.

## Introduce extensibility and configurability ##

- Apply SOLID and analyze if Spring.NET or Add.In or MEF would be the best for the modularity/plugability/DI required.

## Add support for WCF ##
- That should be for all options : tcp/binary, http/datacontractserializer, http/rest, http/json
## Apply async events/trace handling ##
- Current priority is lowest, but is one of the most important elements to achieve. No risk predicted to implement at a later stage.

## Reanimate Sql Broker events/trace distribution ##

## Support routing of events/trace between servers ##

## Reanimate filters for server and UI ##

## Introduce active correlation monitor for trace/events ##

# Subjects to analyze in regard to this project #

## ACID vs. BASE ideas ##
In the trace and logging area can be relevant given the density of the trace/events stream
## CAP theorem ##
For the scalability and grow analysis
## Event stream processing ##
Review in order not to invent the own wheel in some areas.
Find ESPER project.
## Complex event processing ##
Correlation of events and patterns search.

# Definitions #
## Event ##
An event is an immutable record of a past occurrence of an action or state change. Event properties capture the state information for an event.