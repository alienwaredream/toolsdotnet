# Interesting samples encountered #

## EventArgs.Result in .NET throwing exception when there is an error during service invocation ##

## Silverlight unit test framework ##

### Don't use private fields in public virtual methods, use protected or public properties instead ###

```

        public override void RestartRunDispatcher()
        {
            //if (this._harnessTasks == null)
            //{
            //    this.CreateHarnessTasks();
            //}
            this.RunDispatcher = new CustomFastRunDispatcher(new Func<bool>(this.RunNextStep), this.Dispatcher);
            this.RunDispatcher.Complete += new EventHandler(this.RunDispatcherComplete);
            this.RunDispatcher.Run();
        }

```

_Target was to override this.RunDispatcher with a custom one. But as method is designed we can't really just call base. as it has side effects and we can't use this.**harnessTasks**_
