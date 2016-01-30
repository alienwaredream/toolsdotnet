# Resources #
Better resource file generator. Support for public properties and resource name constants:
http://www.codeproject.com/KB/dotnet/ResXFileCodeGeneratorEx.aspx

# Localizing xaml elements #

http://esmithy.net/2009/07/08/silverlight-localization/

# Synchronous calls #

http://www.codeproject.com/KB/silverlight/SynchronousSilverlight.aspx

# Using System.Reactive #

```

            customizationContext.Load(loadResult1 =>
            {
                if (loadResult1.Error != null)
                {
                    MessageBox.Show(String.Format(loadResult1.Error.ToString()));
                    return;
                }
                siteContext.Load(loadResult2 => dispatcher.Dispatch(() =>
                {
                    if (loadResult2.Error != null)
                    {
                        MessageBox.Show(String.Format(loadResult2.Error.ToString()));
                        return;
                    }

                    LoadResources();
                    RegisterRegions();
                }));
            });

```

I still had to cope with Dispatcher in the odd way as the tip of ObserveOnDispatcher provided here http://stackoverflow.com/questions/1951823/using-observable-fromevent-when-calling-a-wcf-service-in-silverlight is not available in System.Reactive version I have.

```

            var dispatcher = _ioc.Resolve<IDispatcherService>();

            var customizationLoad =
                Observable.FromEvent<LoadCompletedEventArgs>(h => customizationContext.LoadCompleted += h, h => customizationContext.LoadCompleted -= h).Take(1);
            var siteContextLoad =
                Observable.FromEvent<LoadCompletedEventArgs>(h => siteContext.LoadCompleted += h, h => siteContext.LoadCompleted -= h).Take(1);

            customizationLoad.Subscribe(siteContext.Load,
                (ex) => MessageBox.Show(String.Format(ex.ToString())));

            siteContextLoad.Subscribe(
                () => dispatcher.Dispatch(()=>{LoadResources();RegisterRegions();}),
                (ex) => MessageBox.Show(String.Format(ex.ToString())));

            customizationContext.Load();

```

A good starting point among few filtered out was: http://themechanicalbride.blogspot.com/2009/07/developing-with-rx-part-1-extension.html

I wanted then to move async wcf service call to be asynchronuous, but ran into silverlight proxy as it generates not having events actually for the call completion.

```

            _client.BeginGetTerminologyList(ar =>
            {
                _terminology = new List<Terminology>(from t in _client.EndGetTerminologyList(ar) select new Terminology { ReplacementKey = t.ReplacementKeyField, Value = t.ValueField });

                OnLoadCompleted(new LoadCompletedEventArgs { });

            }, null);

```

Unfortunately events are not generated in the interface but only in the client class itself. Ok, after fixing from:

```

        private ICommonService _client = new CommonServiceClient();

```

to:

```

        private CommonServiceClient _client = new CommonServiceClient();

```

I'm able to get to to events:

```

            Observable.FromEvent<GetTerminologyListCompletedEventArgs>(
                h => _client.GetTerminologyListCompleted += h, h => _client.GetTerminologyListCompleted -= h).
                Subscribe((e) =>
                {
                    _terminology =
                        new List<Terminology>(from t in e.EventArgs.Result
                                              select
                                                  new Terminology
                                                  {
                                                      ReplacementKey = t.ReplacementKeyField, 
                                                      Value = t.ValueField
                                                  });
                    OnLoadCompleted(new LoadCompletedEventArgs());
                },
                    (ex) => { throw ex; }
                    );
            _client.GetTerminologyListAsync();

```

So what about exception handling paths?

# Debugging #

Different experience in different browsers. Regardless the page title you should attach to iexplorer process running Silverlight.
Debugging of data binding didn't work for me in FF, but worked ok in IE.
Overall, debugging is quite fragile.

# Working with VS2008 and Silverlight 4 #

This is a lame story of MS pushing forward vs2010, but you can still debug SL4 from VS2008 with this: http://forums.silverlight.net/forums/t/178873.aspx
Link to SL4 Developer Runtime: http://go.microsoft.com/fwlink/?LinkID=188039

### Uninstall Silverlight 3 toolkit and SDK ###

### Install Silverlight 4 Toolkit ###


### Install Silverlight 4 SDK ###


http://www.microsoft.com/downloads/details.aspx?familyid=55B44DA3-E5DE-4D2A-8EAF-1155CA6B3207&displaylang=en

### Redirect project imports ###

redirect project imports to C:\Program Files\MSBuild\Microsoft\Silverlight\v4.0\ instead of C:\Program Files\MSBuild\Microsoft\Silverlight\v3.0\Microsoft.Silverlight.CSharp.targets


# Troubleshooting data binding errors #

As this page suggests, look into output window of vs while debugging:
http://stackoverflow.com/questions/2724932/checkbox-command-behaviors-for-silverlight-mvvm-pattern

## Order of bindings ##

Order of binding is given by order of bindings in your markup:
```
                    <sr:ComboBox IsEnabled="{Binding IsMilestoneSelected}" ItemsSource="{Binding MilestoneIntervalTypes}" SelectedIndex="{Binding Path=SelectedMilestoneIntervalTypeIndex, Mode=TwoWay}" DisplayMemberPath="Value" />

```

As opposed to:

```

                    <sr:ComboBox IsEnabled="{Binding IsMilestoneSelected}"  SelectedIndex="{Binding Path=SelectedMilestoneIntervalTypeIndex, Mode=TwoWay}" ItemsSource="{Binding MilestoneIntervalTypes}" DisplayMemberPath="Value" />

```

For former, MilestoneIntervalTypes property on view model will be called first and for latter, it is SelectedMilestoneIntervalTypeIndex view model property.

# Misc #
## Silverlight 4 MessageBox high CPU ##
https://connect.microsoft.com/VisualStudio/feedback/details/552718/system-windows-messagebox-takes-100-cpu-usage?wa=wsignin1.0
For me it is only 50%, probably only one core is really 100% busy

# Unit testing #

Sample unit test, showing as well how to write to output:

```
    [TestClass]
    public class CommonServiceSessionAdapterTest : SilverlightTest
    {
        [TestMethod]
        public void Should_HaveProxyStrategyWhenSet()
        {
            // Arrange
            const SessionCase testCase = SessionCase.StandardCase;
            var testStrategy = new TestClientProxyStrategy(testCase);
            var client = new CommonServiceClient(testStrategy);
            // Assert
            Assert.AreEqual(testStrategy, client.ProxyStrategy);

            UnitTestHarness.LogWriter.DebugWriteLine(String.Format("Not null instance of [{0}] is setup as a client proxy strategy", client.ProxyStrategy.GetType()));

        }
        [TestMethod]
        public void Should_HaveNullStrategyWhenNotSet()
        {
            // Arrange
            var client = new CommonServiceClient();
            // Assert
            Assert.IsNull(client.ProxyStrategy);
        }
    }

```