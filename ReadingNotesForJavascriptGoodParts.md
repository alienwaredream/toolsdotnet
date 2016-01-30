# The Book #

http://oreilly.com/catalog/9780596517748

# Book review #



# Notes #
## Chapter 1 ##

A simple testing ground didn't work for me in all browsers, here is ther version that worked in all major (IE, Chrome, FF)

```
<html>
<head><script src="test.js"></script></head>
<body><pre><script>hello();</script></pre>
</body>
</html>
```

and java script:

```

// add hello to the document
function hello()
{
	document.writeln('Hello, world!');
}

```

## Chapter 2. Grammer ##

Infinity represents all values greater than 1.79769313486231570e+308.

False values: false, null, undefined, the empty string '', the number 0, the number NaN

## Chapter 3. Objects ##

Everything is object except for numbers, strings, booleans (true and false), null, and undefined.
Objects are mutable keyed collections of properties. Objects are class free.

Prototype linkage feature - so one object can inherit properties from another.

### Objects Literal ###
A notation for creating new object values from literals.

Example:

```

// playing with object literals
function objectLiterals()
{
	var framework = {"title": ".NET", author: "Microsoft"}
	
	for (prop in framework)
	{
		if(framework.hasOwnProperty(prop))
		{
			document.writeln(prop);
		}
	}
}

```

Output:

```

title
author

```

More complex example, showing the mix of literal property definitions:

```

// playing with object literals
function objectLiterals()
{
	var dotnetframework = {"title": ".NET", author: "Microsoft"};
	var solution = {
		framework: dotnetframework,
		resources: {
			developerCount: 3,
			hours: 1020,
			},
		startDate: '01-Jun-2010',
		"endDate": "06-Oct-2010"
		};
	
	
	for (prop in solution)
	{
		if(solution.hasOwnProperty(prop))
		{
			document.writeln(prop);
		}
	}
}
```

and the output:

```

framework
resources
startDate
endDate

```

### Retrieving object values ###

["name"] or using . notation (preferred).

```

// retrieving object properties
function objectOperations()
{
	var dotnetframework = {"title": ".NET", author: "Microsoft"};
	var solution = {
		framework: dotnetframework,
		resources: {
			developerCount: 3,
			hours: 1020,
			},
		startDate: '01-Jun-2010',
		"endDate": "06-Oct-2010"
		};
		
	document.writeln("solution:", solution);	
	document.writeln("solution.framework:", solution.framework);
	document.writeln("solution.framework.title:", solution.framework.title);	
	document.writeln("solution.duration:", solution.duration);		
	document.writeln("solution.duration && solution.duration.hours:", solution.duration && solution.duration.hours);	// guarding against TypeError exception
	document.writeln("solution.duration || 3:", solution.duration || 3); // providing default value	
	document.writeln("solution.endDate:", solution.endDate);
}

```

Output:

```

solution:[object Object]
solution.framework:[object Object]
solution.framework.title:.NET
solution.duration:undefined
solution.duration && solution.duration.hours:undefined
solution.duration || 3:3
solution.endDate:06-Oct-2010

```

### Automatic augmentation of object in case of assignment to non-existing property ###

```

// retrieving object properties
function objectAutomaticAugmentation()
{
	var dotnetframework = {"title": ".NET", author: "Microsoft"};
	
	dotnetframework.title = ".NET 2.0";		
	document.writeln("1: dotnetframework.title:", dotnetframework.title);
	dotnetframework.Title = ".NET 3.0";	// Watch out!
	document.writeln("2: dotnetframework.title:", dotnetframework.title);	
	document.writeln("3: dotnetframework.Title:", dotnetframework.Title);	
	// Lets change the object behind the property
	dotnetframework.title = {name:".NET", description: "CLR based framework"};
	document.writeln("4: dotnetframework.title:", dotnetframework.title);
	document.writeln("5: dotnetframework.title.description:", dotnetframework.title.description);	
	// Can we remove the property?
	dotnetframework.title = undefined;
	document.writeln("6: dotnetframework.title:", dotnetframework.title);
}

```

Output:

```

1: dotnetframework.title:.NET 2.0
2: dotnetframework.title:.NET 2.0
3: dotnetframework.Title:.NET 3.0
4: dotnetframework.title:[object Object]
5: dotnetframework.title.description:CLR based framework
6: dotnetframework.title:undefined

```

### Prototype "inheritance" ###

Key concepts:
Custom helper function **"beget"** is helping to create a prototypical inheritance.
**Delegation** - probing for the property via the chain of self -> self.prototype ->self.prototype.prototype (see 5:)
**Dynamic relationship** prototype -> children. Property added to the prototype will be immediately visible in all the objects based on that prototype.

```

// properties helper
function listProperties(description, source)
{
	document.writeln(description);
	
	for (prop in source)
	{
		if(source.hasOwnProperty(prop))
		{
			document.writeln("\town:" + prop + "=" + source[prop]);
			continue;
		}
		document.writeln("\tinherited:" + prop + "=" + source[prop]);
	}
}
// playing with inheritance via prototype
function objectPrototype()
{
	var framework = {"title": ".NET", author: "Microsoft"};
	// The bellow is only made to provide a link to the "o" object to make it a prototype for the new object
	if (typeof Object.beget !== 'function')
	{
		Object.beget = function(o) {
			var F = function(){};
			F.prototype = o;
			return new F();
			};
	}
	// Now lets prototype from our framework
	var spring = Object.beget(framework);
	
	document.writeln("1: spring.title:", spring.title);
	listProperties("2: spring:", spring);
	spring.description = "DI, AOP and utility framework";
	listProperties("3: spring:", spring);
	listProperties("4: framework", framework); // To see that auto-augmenting spring had no effect on framework.
	framework.title = ".NET 2.0"; // Changing the prop value in the base object, WATCH OUT for delegation!!!!
	document.writeln("5: spring.title:", spring.title); // Showing the nasty delegation at work.
	spring.title = "Spring";
	document.writeln("6: spring.title:", spring.title); // Now we should be ok, as property is evaluated from our current object, no delegation.
	framework.age = 10; // New property in prototype is visible to objects that are based on this prototype.
	listProperties("7: spring:", spring);
	listProperties("8: framework", framework);
	delete spring.title // Removig property title from the prototype.
	listProperties("9: spring:", spring); // we can see the effect of delegation again. Value is taken from the prototype.
}

```

Output:

```

1: spring.title:.NET
2: spring:
	inherited:title=.NET
	inherited:author=Microsoft
3: spring:
	own:description=DI, AOP and utility framework
	inherited:title=.NET
	inherited:author=Microsoft
4: framework
	own:title=.NET
	own:author=Microsoft
5: spring.title:.NET 2.0
6: spring.title:Spring
7: spring:
	own:description=DI, AOP and utility framework
	own:title=Spring
	inherited:author=Microsoft
	inherited:age=10
8: framework
	own:title=.NET 2.0
	own:author=Microsoft
	own:age=10
9: spring:
	own:description=DI, AOP and utility framework
	inherited:title=.NET 2.0
	inherited:author=Microsoft
	inherited:age=10
```
### A-la namespaces ###
Mimicked via global variables.
```

var Tools = {}; // "namespace"
Tools.Js = {}; // "nested namespace"

// defining function within the namespace
Tools.Js.namespacedFunction = function()
{
	document.writeln("Printed out from the namespaced function: Tools.Js.namespacedFunction()");
}

```

Usage:

```

<script>
Tools.Js.namespacedFunction();
</script>

```
## Chapter 4. Functions ##

function has it prototype pointing to Function.prototype that in turn points to Object.prototype.

### Invocation patterns ###

Method, function, constructor, apply invocation patterns.

```

// playing with different types of invocation
function InvocationPatterns()
{
	// method invocation
	var counter = { 
		value: 0, 
		increment: function(desc, inc){
			document.writeln(desc + "this.value:" + this.value);
			this.value += typeof inc === 'number' ? inc : 1;}};
			
	counter.increment("1:"); // method invocation
	counter.increment("2:", 2); // method invocation
	document.writeln("3:counter.value:", counter.value); 
	
	// function invocation - Example that doesn't work properly
	
	counter.square = function(desc)
	{
		var helper = function() 
			{
				document.writeln(desc + "this.value:" + this.value);			
				this.value *= this.value;
			}
		helper(); // function invocation
	};
	counter.square("4:");	
	document.writeln("5:counter.value:", counter.value);
	
	// function invocation - Example with "that" trick - that works
	
	counter.square = function(desc)
	{
		var that = this; // Getting reference to this
		var helper = function() 
			{
				// inside the function, using an outer scope variable that
				document.writeln(desc + "that.value:" + that.value);			
				that.value *= that.value;
			}
		helper(); // function invocation, that and this are the same reference.
	};
	counter.square("6:");
	document.writeln("7:counter.value:", counter.value);
	
	// constructor invocation
	
	var Quo = function(status){this.status = status;};
	quo = new Quo("start");
	
	document.writeln("8:quo.status:", quo.status);
	
	Quo.prototype.setStatus = function(status){this.status = status;};
	Quo.prototype.getStatus = function(){return this.status};
	
	quo.setStatus("inProgress");
	document.writeln("9:quo.status:", quo.status);
	document.writeln("10:quo.getStatus():", quo.getStatus());
	
	// apply invocation pattern
	var args = ["11:"];
	counter.square.apply({value:20}, args); // first parameter is used as "this" inside the function
	document.writeln("12:counter.value:", counter.value); // inside the function "that" is assigned to "this", sp real "this" counter stays the same.
}

```

Output:

```

1:this.value:0
2:this.value:1
3:counter.value:3
4:this.value:undefined
5:counter.value:3
6:that.value:3
7:counter.value:9
8:quo.status:start
9:quo.status:inProgress
10:quo.getStatus():inProgress
11:that.value:20
12:counter.value:9

```

function always returns value, if there is no explicit return value then it is undefined.

### Augmenting type with functions ###

```

// augmenting types
function augmentingTypes()
{
	String.prototype.trim = function(){return this.replace(/^\s+|\s+$/g, '');};
	
	document.writeln("1:", '    test      '.trim(), ',', '    test      '.trim().length);
}

```

Output:

```

1:test,4

```

### Closures ###

```
// uses of closure
function closure()
{

	var state = '10';
	// value becomes a private variable
	var concat = function ( ) 
	{
		var value = state;

		return {
			add: function (inc) { 
				value += inc;},
			value: function () { return value;}
		}
	}( );	
		
	concat.add(10);
	document.writeln("1:concat.value:", concat.value());	
	// Change the state of an outer variable to see if closure holds right.
	state = 30;
	concat.add(10);
	document.writeln("2:concat.value:", concat.value());
	
	
}

```

Output:

```

1:concat.value:1010
2:concat.value:101010

```

### Module ###
by the usage of closure for eliminating access to the closure variables we can find place where to store otherwise "global" variables and hide/secure the implementation.

### Cascade aka Fluent Interfaces ###
returning some object instead of void/undefined to allow fluent interfaces.

```

// cascade example
function cascade()
{

	var state = '10';
	// value becomes a private variable
	var concat = function ( ) 
	{
		var value = state;

		return {
			append: function (inc) { 
				value += inc;
				return this; // returning "this", so we can use cascade expression
				},
			value: function () { return value;}
		}
	}( );	
		
	concat.append(60).append(30);
	document.writeln("1:concat.value:", concat.value());	
}

```

Output:

```

1:concat.value:106030

```

### Curry ###

```

// curry example
function curry()
{

	var state = '10';
	// having a closure here to try out a curry over a closure
	var concat = function ( ) 
	{
		var value = state;

		return {
			append: function (inc) { 
				value += inc;
				return this; // returning "this", so we can use cascade expression
				},
			value: function () { return value;},
			merge: function (inc, inc2, desc)
			{
				document.writeln(desc,value, ",", inc, ",", inc2);
				value += inc2 + inc;
				return value;
			}
		}
	}( );
	// having just a regular function
	var add = function add(a, b) { return a+b;};
	// Introducing the curry extension
	Function.prototype.curry = function (  ) 
	{
		var slice = Array.prototype.slice,
			args = slice.apply(arguments), // this is how we just extracted the function from array
			that = this;
			return function (  ) {
				return that.apply(null, args.concat(slice.apply(arguments)));
			}
	};
	// lets create a curry
	var appendA = concat.merge.curry('A');
	var add1 = add.curry(1);
	
	document.writeln("2:appendA('123'):", appendA('123', "1:")); // currying on the closure
	document.writeln("3:add1(5):", add1(5)); // Works on the regular functions
}

```

Output:

```

1:10,A,123
2:appendA('123'):10123A
3:add1(5):6

```