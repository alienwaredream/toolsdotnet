# What is weak #

## Paradigm shift requirement ##

Educate yourself:
http://10rem.net/blog/2010/08/26/10-things-beginning-silverlight-and-wpf-developers-need-to-know?utm_source=feedburner&utm_medium=feed&utm_campaign=Feed%3A+PeteBrown+%28Pete+Brown%27s+Blog%29&utm_content=Google+Reader

## Testing ##
IsolatedStorageFile - IDisposable, no other interface
No own, first class citizen testing framework
No own CI (point to Statlight)

## Blend programming model ##
-- Association with TIBCO programming model, point on testability/verification again
Often we can't see design view as of "errors" that are not really errors

## Cooperation between designer and coder ##

## Scalability ##

## Binding hurdles ##
Very hard to verify, troubleshoot when binding is not working properly

## Exception handling ##
Swallowing exceptions

## Readiness and support for MVVM ##

 Visual state manager for MVVM as attached property: http://tdanemar.wordpress.com/2009/11/15/using-the-visualstatemanager-with-the-model-view-viewmodel-pattern-in-wpf-or-silverlight/

 Usage pf EventTrigger for conversion between events and commands:

```
        <telerikRibbonBar:RadRibbonBar HelpButtonVisibility="Visible"  Grid.Row="0"  HorizontalAlignment="Stretch" x:Name="MenuRibbon" Title="Action Plan" ApplicationName="Valtera" ApplicationButtonImageSource ="../../Images/Logo.png" >
        	    <i:Interaction.Triggers>
                    <i:EventTrigger EventName="HelpRequested">
                        <Events:ExecuteTriggerAction Command="HelpCommand" />
                    </i:EventTrigger>
                </i:Interaction.Triggers>
...
```
### Blend no support for MVVM ###

```
\                        	<StackPanel Orientation="Vertical" VerticalAlignment="Stretch">
                                <ItemsControl
                                    rgn:RegionManager.RegionName="{Binding Path=ReportWarehouseModule.ReportsListRegion, Source={StaticResource Regions}}" 
                                    Margin="1" d:DesignWidth="200" />
									</StackPanel>
```

### Telerik support for MVVM ###
As an example we can take the need for your viewmodel to be "injected" as a static resource only when you want to use Command on a Button inside Telerik's RadGridView (and pointing to your parent ElementName DataContext for a command) http://www.telerik.com/community/forums/silverlight/gridview/button-command-not-working-in-gridviewcolumn.aspx. This skews your MVVM design a "bit" if you use a different viewmodel injection techinique.

http://blogs.telerik.com/vladimirenchev/posts/10-05-31/how_to_synchronize_your_ui_selected_items_with_your_data_context_using_mvvm_and_blend_behaviors_for_silverlight_and_wpf.aspx

## Speed of verification ##
Need to build to see the result. Tiniest change in your xaml and you have to rebuid before refresh. You might say, Blend shows it instantly - but once you use MVVM you will not see actually that layout changes for pages with regions as they are not "inflated".

## Design decisions ##

## Versioning and backward support ##
The story of SL3 to SL4. Moving System.Action from System to System.Core

## Tooling ##
VS 2010 is quite flaky on Silverlight. And more debugging and builds you more unstable it becomes, causing need to restart and thus loosing time.

## Complexity of asynchronous programmming ##
Provide examples of bugs in well-known framework like CLog for example
Mix of async and sync API (like old one Isolated storage api).
Chaining multiple events:
http://mtaulty.com/CommunityServer/blogs/mike_taultys_blog/archive/2008/09/08/10730.aspx
And critics of eventing in SL:
http://www.atrevido.net/blog/2008/08/28/Tsk+Tsk+Silverlight+Events+Are+Not+Asyncs+Friend.aspx



## Overlooked bugs caused by async programming non-readiness ##
Sometimes it is just very complex to synchronize all async stuff

Sometimes it works but you see buginness by the fact that things begin to happen twice/multiple times.

Unregistering for events is another biggy. E.g. it is not straightforward to unregister from anonymous delegate/lamda expression

Observable collection bindings can "buffer"/hide some of the async issues in the code, but they declare themselves more clearly when you need to compose more async actions.

## You can't show/convert html ##
This kind of a tough thing. Quite often your customers are giving you those chunks of html they crafted and want to you for the LOB app customization. You/they become pretty limited here.
See: http://blogs.msdn.com/b/delay/archive/2007/09/10/bringing-a-bit-of-html-to-silverlight-htmltextblock-makes-rich-text-display-easy.aspx for at least some remediation. BUT THIS IS NOT A SOLUTION!
Looks like there is an ability to integrate webbrowser control into your application in Windows Phone 7 though. Webbrowser control is available in Silverlight 4, but only functions in out-of-browser applications.

http://forums.silverlight.net/forums/p/9179/29021.aspx

Another take on the MS sample (http://msdn.microsoft.com/en-us/library/aa972129.aspx) for Html to Xaml (WPF) conversion, this time adapted to Silverlight:
http://jacob4u2.blogspot.com/2009/09/bind-html-to-textblock-with-html-to.html

## No MVVM support for javascript integration ##

It would be good we we had Javascript Command binding javascript events with ViewModel commands.

## Loose ends of many frameworks ##
Take Prism for example and how it copes with disposing events in event aggregator.
Though it has ability to work with Weak references, in silverlight you can't have a weak reference to anonymous delegate. This is definitely a loose end. How do you unsubscribe the anonymous delegate without the need to keep a reference to it?

## Quickly changing API and implementations of 3rd party frameworks and components ##
As silverlight itself is developing with a quite fast pace, so do 3rd party frameworks and component for it.
Take Silverlight Toolkit or Telerik UI components for SL. Over months you can see API and internals to be quite changed. And quite often again you had to delve into their guts relying on some internal details.

## Very hard to find a designer or place a designer into the team ##
Either there is no work for a full time designer, but very hard to get any decent design from regular devs.
You'll have to buy Blend if you mean the design anyhow seriously.
Designer should prepare to be a bit of a data modeler, as they will need to do some binding of sample data to really see what design is going to be like.

## Easier to do += then -= ##

Current Prism's event aggregator design makes it easy to subscribe, harder to unsubscribe

Some ideas how to simplify that task with use of System.Reactive can be found here: http://richarddingwall.name/2010/03/30/reactive-extensions-and-prism-linq-to-your-event-aggregator/

# What is strong #