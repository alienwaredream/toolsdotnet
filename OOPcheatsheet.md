# Introduction #

Over time I see interpretation of concepts and idioms to become quite overloaded.
I tend to forget subtlities of the definitions and yet I want to have them handy.
Disclaimer: The contents of the cheat sheet is built in the way to ring mine bells, yours might need a different frequency :).

# Books #
**Fundamentals of object oriented design and UML** (Meilir Page-Jones)
http://www.amazon.com/Fundamentals-Object-Oriented-Design-Addison-Wesley-Technology/dp/020169946X
_I put this book on top as I believe it gave me at the time the best overview of OO principles. I used to be based on Booch's and Bertrand Meyer's books, but this one was a breath of a fresh air, trully._

**Object Oriented Analisys and Desing with Applications** (Grady Booch)
http://www.amazon.com/Object-Oriented-Analysis-Applications-Addison-Wesley-Technology/dp/020189551X/ref=pd_bbs_sr_1?ie=UTF8&s=books&qid=1218393901&sr=1-1

**Object Oriented Software Construction** (Bertrand Meyer)
http://www.amazon.com/Object-Oriented-Software-Construction-Prentice-Hall-International/dp/0136291554/ref=pd_bbs_sr_1?ie=UTF8&s=books&qid=1218394084&sr=1-1

**Agile Principles, Patterns and Practices in C#** (Robert C. Martin)
http://www.amazon.com/Principles-Patterns-Practices-Robert-Martin/dp/0131857258/ref=pd_bbs_sr_3?ie=UTF8&s=books&qid=1218394574&sr=1-3

**Applying UML and Patterns** (Graig Larman)
http://www.amazon.com/Applying-UML-Patterns-Introduction-Object-Oriented/dp/0131489062/ref=pd_bbs_sr_1?ie=UTF8&s=books&qid=1218394650&sr=1-1

**Design patterns** (Erich Gamma)
http://www.amazon.com/Design-Patterns-Object-Oriented-Addison-Wesley-Professional/dp/0201633612/ref=pd_bbs_sr_1?ie=UTF8&s=books&qid=1218394843&sr=1-1

**Patterns oriented software architecture** (Douglas Schmidt)
http://www.amazon.com/Pattern-Oriented-Software-Architecture-System-Patterns/dp/0471958697/ref=sr_1_1?ie=UTF8&s=books&qid=1218394916&sr=1-1
http://www.amazon.com/Pattern-Oriented-Software-Architecture-Concurrent-Networked/dp/0471606952/ref=pd_bxgy_b_text_b

# Most important concepts of OO #
For this one I don't like [Wikipedia's](http://en.wikipedia.org/wiki/Object-oriented_programming) definition at all. Meilir Page-Jones is my trusted reference source for this:

## Encapsulation ##

## Polymorphism ##

## Identity ##

## Combining state and behaviour ##


I believe that what is provided by [Wikipedia](http://en.wikipedia.org/wiki/Object-oriented_programming) is quite a wild mix of concepts (class, object, instance), mechanisms (method, inheritance) and design principles (abstraction, decoupling) flushed onto one list.

# Principles #

## The Single Responsibility Principle (SRP) ##
[Objectmentor](http://www.objectmentor.com/resources/articles/srp.pdf):
THERE SHOULD NEVER BE MORE THAN ONE REASON FOR A CLASS TO CHANGE.

"... each responsibility is an axis of change. When the requirements change, that
change will be manifest through a change in responsibility amongst the classes. If a class
assumes more than one responsibility, then there will be more than one reason for it to
change.
If a class has more then one responsibility, then the responsibilities become coupled.
Changes to one responsibility may impair or inhibit the class’ ability to meet the others.
This kind of coupling leads to fragile designs that break in unexpected ways when
changed."

## The Open/Closed Principle (OCP) ##
[Objectmentor](http://www.objectmentor.com/resources/articles/ocp.pdf):
SOFTWARE ENTITIES (CLASSES, MODULES, FUNCTIONS, ETC.) SHOULD BE OPEN FOR EXTENSION, BUT CLOSED FOR MODIFICATION.

"... When a single change to a program results in a cascade of changes to dependent modules,
that program exhibits the undesirable attributes that we have come to associate with “bad”
design. The program becomes fragile, rigid, unpredictable and unreusable. The openclosed
principle attacks this in a very straightforward way. It says that you should design
modules that never change. When requirements change, you extend the behavior of such
modules by adding new code, not by changing old code that already works."

## The Liskov Substitution principle (LSP) ##
[Objectmentor](http://www.objectmentor.com/resources/articles/lsp.pdf): FUNCTIONS THAT USE POINTERS OR REFERENCES TO BASE CLASSES MUST BE ABLE TO USE OBJECTS OF DERIVED CLASSES WITHOUT KNOWING IT.

"... The above is a paraphrase of the Liskov Substitution Principle (LSP). Barbara Liskov first
wrote it as follows nearly 8 years ago

_What is wanted here is something like the following substitution property: If for each object o1 of type S there is an object o2 of type T such that for all programs P defined in terms of T, the behavior of P is unchanged when o1 is substituted for o2 then S is a subtype of T._"

The classic example used in object mentor document is a rectangle and a square. One should be able to answer if square is a correct subtype of a rectangle or not (same can be ellipse and circle).

## The Interface Segregation Principle ##

[Objectmentor](http://www.objectmentor.com/resources/articles/isp.pdf): CLIENTS SHOULD NOT BE FORCED TO DEPEND UPON INTERFACES THAT THEY DO NOT USE.

## The Dependency Inversion Principle ##
[Objectmentor](http://www.objectmentor.com/resources/articles/dip.pdf):
A. HIGH LEVEL MODULES SHOULD NOT DEPEND UPON LOW LEVEL MODULES. BOTH SHOULD DEPEND UPON ABSTRACTIONS.

B. ABSTRACTIONS SHOULD NOT DEPEND UPON DETAILS. DETAILS SHOULD DEPEND UPON ABSTRACTIONS.

**A well known helper to remember for the above five principles is SOLID**

# Idioms #
## Law of Demeter ##
Wikipedia: http://en.wikipedia.org/wiki/Law_of_Demeter

The Law of Demeter (LoD), or Principle of Least Knowledge, is a design guideline for developing software, particularly object-oriented programs. The guideline was invented at Northeastern University towards the end of 1987, and can be succinctly summarized as “Only talk to your immediate friends.” The fundamental notion is that a given object should assume as little as possible about the structure or properties of anything else (including its subcomponents).

More formally, the Law of Demeter for functions requires that a method M of an object O may only invoke the methods of the following kinds of objects:

O itself
M's parameters
any objects created/instantiated within M
O's direct component objects
In particular, an object should avoid invoking methods of a member object returned by another method. For many modern object oriented languages that use a dot as field identifier, the law can be stated simply as "use only one dot". That is, the code "a.b.Method()" breaks the law where "a.Method()" does not.

# Methods/Approaches #

## Dependency Injection (Inversion of control) ##

[Martin Fowler](http://martinfowler.com/articles/injection.html): _In the Java community there's been a rush of lightweight containers that help to assemble components from different projects into a cohesive application. Underlying these containers is a common pattern to how they perform the wiring, a concept they refer under the very generic name of "Inversion of Control". In this article I dig into how this pattern works, under the more specific name of "Dependency Injection", and contrast it with the Service Locator alternative. The choice between them is less important than the principle of separating configuration from use._

Between points to note is that application of dependency inversion preceeds very often dependency injection, preparing the right ground for dependency injection application.

## Assigning responsibilities ##
GRASP - General Responsibility Assignment Software Patterns, as per the Graig Larman's book and copied from wikipedia http://en.wikipedia.org/wiki/GRASP_%28Object_Oriented_Design%29.
### Information Expert ###
The Information Expert pattern provides the general principles associated with the assignment of responsibilities to objects. The information expert pattern states that responsibility should be assigned to the information expert—the class that has all the essential information. Systems which appropriately utilize the information expert pattern are easier to understand, maintain and expand as well as increase the possibility that an element can be reused in future development.

### Creator ###
The Creator pattern solves the problem of who should be responsible for the creation of a new instance of a class. The creator pattern is important because creation of objects is one of the most ubiquitous activities in an object-oriented system. A system that effectively utilizes the creator pattern can also support low coupling, increased understandability, encapsulation and the likelihood that the object in question will be capable of sustaining reuse. Given two classes, class B and Class A, class B should be responsible for the creation of A if class B contains or compositely aggregates, records, closely uses or contains the initializing information for class A. It could then be stated that B is natural object to be a creator of A objects.

The Factory pattern is a common alternative to Creator when there are special considerations, such as complex creation logic. This is achieved by creating a Pure Fabrication object (see below), called Factory that handles the creation.

### Controller ###
The Controller pattern assigns the responsibility of dealing with system events to a non-UI class that represent the overall system or a use case scenario. A use case controller should be used to deal with all system events of a use case, and may be used for more than one use case (for instance, for use cases Create User and Delete User, one can have one UserController, instead of two separate use case controllers). It is defined as the first object beyond the UI layer that receives and coordinates ("controls") a system operation. The controller should delegate to other objects the work that needs to be done; it coordinates or controls the activity. It should not do much work itself. The GRASP Controller can be thought of as being a part of the Application/Service layer [3](3.md) (assuming that the application has made an explicit distinction between the App/Service layer and the Domain layer) in an object-oriented system with common layers.

### Low Coupling ###
Low Coupling is an evaluative pattern, which dictates how to assign responsibilities to support:

low dependency between classes;
low impact in a class of changes in other classes;
high reuse potential;

### High Cohesion ###
High Cohesion is an evaluative pattern that attempts to keep objects appropriately focused, manageable and understandable. High cohesion is generally used in support of Low Coupling. High cohesion means that the responsibilities of a given element are strongly related and highly focused. Breaking programs into classes and subsystems is an example of activities that increase the cohesive properties of a system. Alternatively, low cohesion is a situation in which a given element has too many unrelated responsibilities. Elements with low cohesion often suffer from being hard to comprehend, hard to reuse, hard to maintain and adverse to change.

### Polymorphism ###
According to the Polymorphism pattern, responsibility of defining the variation of behaviors based on type is assigned to the types for which this variation happens. This is achieved using polymorphic operations.

### Pure Fabrication ###
A pure fabrication is a class that does not represent a concept in the problem domain, specially made up to achieve low coupling, high cohesion, and the reuse potential thereof derived (when a solution presented by the Information Expert pattern does not). This kind of class is called "Service" in Domain-driven design.

### Indirection ###
The Indirection pattern supports low coupling (and reuse potential) between two elements by assigning the responsibility of mediation between them to an intermediate object. An example of this is the introduction of a controller component for mediation between data (model) and its representation (view) in the Model-view-controller pattern.

### Protected Variations ###
The Protected Variations pattern protects elements from the variations on other elements (objects, systems, subsystems) by wrapping the focus of instability with an interface and using polymorphism to create various implementations of this interface.


## Design for testability ##

## Design/Programming by contract ##
**Not to be interchanged with the "Contract First Design"** _Read [here](http://static.springframework.org/spring-ws/sites/1.5/reference/html/why-contract-first.html) on the "Contract First"_

An approach to designing computer software. It prescribes that software designers should define precise verifiable interface specifications for software components based upon the theory of abstract data types and the conceptual metaphor of a business contract.

One could summarize design by contract by the "three questions" that the designer must repeatedly ask:

What does it expect?
What does it guarantee?
What does it maintain?

The notion of a contract extends down to the method/procedure level; the contract for each method will normally contain the following pieces of information:

**Acceptable and unacceptable input values or types, and their meanings** Return values or types, and their meanings
**Error and exception conditions values or types, that can occur, and their meanings** Side effects
**Preconditions, which subclasses may weaken (but not strengthen)** Postconditions, which subclasses may strengthen (but not weaken)
**Invariants, which subclasses may strengthen (but not weaken)** (more rarely) Performance guarantees, e.g. for time or space used

Source - [Wikipedia](http://en.wikipedia.org/wiki/Design_by_contract)

# Jargon and terms #
For the full list of terms refer to [Wikipedia](http://en.wikipedia.org/wiki/List_of_object-oriented_programming_terms). I only provide here "jargon" terms that are used quite often in the community.

A helper to remember at this stage would be KYD (K[iss](iss.md) Y[agni](agni.md) D[ry](ry.md)).

## KISS ##
"Keep It Simple, Stupid!" [Wikipedia](http://en.wikipedia.org/wiki/KISS_principle)

## YAGNI ##
"You Ain't Gonna Need It" [Wikipedia](http://en.wikipedia.org/wiki/You_Ain't_Gonna_Need_It)

## DRY ##
"Don't Repeat Youself" [Wikipedia](http://en.wikipedia.org/wiki/Don%27t_repeat_yourself)

This is the one where again I'm pretty puzzled by the weakness of the Wikipedia's defintion. The guy saying that DRY is only about the artifacts other than code and OAOO (Once And Only Once)is about the code, did he/she read the book carefuly?
DRY is about any ill minded repetion, read the interview with the "Pragmatic programmer" author if you don't have his book on a bookshelf to judge for yourself http://www.artima.com/intv/dry.html

# Bad smells #
## Connascence of algorithm ##

# Miscelaneous #
One of the important things here will be to provide good examples for covariance and contravariance. With covariance there might be a need to mention the term overload in connection with generics (type

&lt;t&gt;

 not being _covariant_ on t). But otherwise for co(ntra)variance the Page-Jones's definition to be provided
One of the interesting samples can be to use not the Pages-Jones's sample but .NET List as a return, IEnumerable as a parameter, and then see what a subtype can do.
## Covariance ##
## Contravariance ##