# Introduction #

Original lists are taken from here: http://www.informit.com/articles/article.aspx?p=417090 but they are defined in the [| IEEE Standard 1471](http://standards.ieee.org/reading/ieee/std_public/description/se/1471-2000_desc.html)

# Views and Viewpoints #

Fundamental organization of the system:

## Functional Viewpoint ##
Describes the system's functional elements as well as their responsibilities, interfaces, and primary interactions.
## Information Viewpoint ##
Describes the way that the architecture stores, manipulates, manages, and distributes information.
## Concurrency Viewpoint ##
Describes the concurrency structure of the system, mapping functional elements to concurrency units to clearly identify the parts of the system that can execute concurrently and how this execution is coordinated and controlled.
## Development Viewpoint ##
Exists to support the system's construction. It describes how the architecture supports the software development process.

_Characterize the system once it's in its live environment:_

## Deployment Viewpoint ##
Describes the environment into which the system will be deployed, including capturing the dependencies the system has on its runtime environment.
## Operational Viewpoint ##
Describes how the system will be operated, administered, and supported when it's running in its production environment.



# Perspective Catalog #

A perspective catalog defines a number of perspectives, which the following table briefly describes. The perspective catalog aims to identify all of the quality properties that may be important to a modern information system. Some will be more relevant than others, depending on the goals of the system, the context in which it's being built, and the concerns and priorities of the stakeholders.


## Accessibility ##
> Ability of the system to be used by people with disabilities

## Availability and Resilience ##
> Ability of the system to be fully or partly operational as and when required and to effectively handle failures that could affect system availability

## Development Resource ##
> Ability of the system to be designed, built, deployed, and operated within known constraints around people, budget, time, and materials

## Evolution ##
> Ability of the system to be flexible in the face of the inevitable change that all systems experience after deployment, balanced against the costs of providing such flexibility

## Internationalization ##
> Ability of the system to be independent from any particular language, country, or cultural group

## Location ##
> Ability of the system to overcome problems brought about by the absolute location of the system's elements and the distances between them

## Performance and Scalability ##
> Ability of the system to execute predictably within its mandated performance profile and to handle increased processing volumes

## Regulation ##
> Ability of the system to conform to local and international laws, quasi-legal regulations, company policies, and other rules and standards

## Security ##
> Ability of the system to reliably control, monitor, and audit who can perform what actions on which resources and to detect and recover from failures in security mechanisms

## Usability ##
> Ease with which people who interact with the system can work effectively


# Perspectives: Key Tasks #

**Familiarize yourself with the nonfunctional requirements of the system.**

**Identify the most important perspectives for your architectural evaluation.**

**Apply perspectives to the architectural models to help ensure that the architecture exhibits the required quality properties.**

**Validate the architectural models against the nonfunctional requirements.**

**Revise or improve your architecture based on the results of applying the perspectives.**

**Iterate until you produce an acceptable architecture.**

# Aspects #
We have views and perpectives. We have levels as well.
Here I just want to provide some extra aspects I consider to be important when I do architectural analysis.

## Latency ##
[| Wikipedia](http://en.wikipedia.org/wiki/Latency_(engineering))
Latency should be considered in Availability, Resilience, Performance, Location, Usability perspectives. That is actually almost all of the perspectives and that is why I consider it to one of the most important aspects to take into account.

## Upgrades ##
Aspect definitely belongs to the Operational viewpoint. Main impact are on the following perspectives: Availability and Resilience, Evolution, Location, Performance and Scalability, Usability. We can say that Upgrades aspect facilitates Evolution (one of facilitators).
Again we can see that Upgrades have impact in almost all perspectives.