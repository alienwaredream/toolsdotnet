## Assemblies and packaging ##
  * How to pack multiple modules in the single assembly?
  * What is the assembly manifest?
  * What is weakly named assembly and what is a strong named assembly?
  * What is part of the assembly's strong name?
  * Can the strong named assembly refer to the weakly named assembly?
  * What if two assemblies have the same public key token?
  * Describe the sequence in which strong named assembly is probed for? Weakly named?
  * Why and when publisher policy is used? How to override a publisher policy for a selected assembly?


## Working with types ##
### Boxing/Unboxing ###
Given the following code:
```
public static Main(){
Int32 v = 5;
Object o = v;
v = 123;

Console.WriteLine(v + "," + (Int32)o);
```

What is the output? How many boxing operations occurred in this code?

### Versioning of types ###
What is the of const field on versioning?
What one should be aware of when changing non-virtual method to be virtual?

### Virtual methods ###
  * What is the difference between IL's **call** and **callvirt**?
  * What is the overhead involved in calling IL's **callvirt**?
  * What does it mean to call a virtual method non-virtually?

## Interfaces and Contracts ##
  * Explain design by contract
  * Explain Dependency Inversion principle
  * What explicit interface implementation is used for?

## C# compiler tricks ##
  * What compiler does behind the **params** and what is the overhead involved in using of params idiom?

## OOP ##
  * What are main principles of OOP?
  * Explain SOLID

## Patterns ##
  * Why to use a singleton instead of a static class? _(Hint: e.g. the interface)_
  * Enumerate patterns you are using most often.
  * What is a difference between the abstract and concrete factories?

## Threading and Synchronization ##
  * Explain **volatile** and MemoryBarrier.
  * List synchronization primitives.
  * What **mutex** is used for?
  * Explain ThreadStaticAttribute.
  * What are low locking techniques and why to use them?

## Reliable execution ##
  * Explain the specifics of ThreadAbortException, StackOverflowException.
  * What is the syntax and elements of constrained execution setup?
  * What is the contract of the code inside the constrained execution sections?
  * What are changes in this area in .NET 2.0 compared to .NET 1.x?

http://msdn.microsoft.com/en-us/library/ms228973.aspx

## Exception handling ##
  * What is 1st chance exception
  * What is the difference between throw ex and throw;
  * 

# Different tests #

## Casting between different nullable enums ##
```

        [Test, Description("")]
        public void CastingBetweenDifferentEnums_Should_Cast()
        {
            MilestoneIntervalType? bcType = MilestoneIntervalType.Annually;
            Dto.MilestoneIntervalType? dtoType = (bcType.HasValue) ?
                            (Dto.MilestoneIntervalType?)bcType.Value : null;
            Console.WriteLine(String.Format("dto type: {0}", dtoType));
            Assert.AreEqual(Dto.MilestoneIntervalType.Annually, dtoType);

            bcType = null;
            dtoType = (bcType.HasValue) ?
                            (Dto.MilestoneIntervalType?)bcType.Value : null;
            Console.WriteLine(String.Format("dto type: {0}", dtoType.HasValue ? dtoType.ToString() : "null"));
            Assert.IsNull(dtoType);
        }

```

# Linq #

## Joins and orderby ##

```

           var sortedResources = from res in resources
                                  from cat in categories
                                  from scat in subCategories
                                  where res.ResourceSubCategoryId == scat.Id &&
                                        cat.Id == scat.ResourceCategoryId
                                  orderby cat.DisplaySeq , scat.DisplaySeq
                                  select res;


            sortedResources.ToList().ForEach(resourceSlot =>
            {

```

## Aggregate ##

```

                string dump = terminologies.Aggregate(String.Empty,
                    ( acc, t ) => acc += String.Format("<{0}:{1}>\r\n", t.ReplacementKey, t.Value));

```

## Usage of System.Ling.Observable with delegate conversion ##

```

                ObservableObject<object> context = RegionContext.GetObservableContext(view);

                Observable.FromEvent<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                    (h) => (s, e) => h(s, e),
                    h => context.PropertyChanged += h,
                    h => context.PropertyChanged -= h).Where(e => e.EventArgs.PropertyName == CodeHelper.GetMemberName(() => context.Value)).Take(1).Subscribe(eh => contextReaderModel.AssignContext(
                        context));

```

```

         <ItemsControl VerticalAlignment="Stretch"  
                                      rgn:RegionManager.RegionName="{Binding Path=ActionPlanModule.HelpRegion, Source={StaticResource RegionNames}}" 
									  rgn:RegionManager.RegionContext="{Binding Source={StaticResource ApplicationName}}"
                                      Margin="0" d:DesignWidth="200" />

```

```

public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);

```

= System.Expression vs. System.Reflection

## Get a property name with no reflection ##
```
        public List<string> fieldsWhiteListOnRenewal = new List<string>
        {
            GetNakedPropName((BidVariant v) => {return v.AcquisitionCosts;})
        };

        public static string GetNakedPropName<TType, TValue>(Expression<Func<TType, TValue>> prop)
        {
            return ((MemberExpression)((Expression<Func<TType, TValue>>)(prop)).Body).Member.Name;
        }
```