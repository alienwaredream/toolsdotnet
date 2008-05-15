using System;
using System.IO;
using System.Configuration;
using System.Threading;
using System.Xml.Serialization;

using Tools.Common.Change;
using Tools.Common.Utils;

namespace Tools.Common.Cache
{

    using EventArgsType = GenericChangeEventArgs<NumericValidityToken>;
    using Tools.Common.Exceptions;
    using Tools.Common.Logging;
    using System.Diagnostics;

    /// <summary>
    /// Provides cache invalidation/expiration services
    /// </summary>
    public class CacheManager : Descriptor, IEnabled
    {
        private string ConfigurationSectionPath = 
            CommonCacheResource.DataCacheGroupCacheManagerConfigSection;
        /// <summary>
        /// If true, then even without any configuration it will be serving for all
        /// cache validation requests and watching the cache refresh event (only file watch
        /// currently).
        /// </summary>
        public const bool EnableByDefault = true;
        
        #region Declarations

        private int                                 _validityToken      = 0;

        /// <summary>
        /// Not being used at the moment. Subject to comment out/erase soon (SD).
        /// </summary>
        private int                                 _validityTokenAux   = 0;
        private CacheManagerConfigSection    _configuration      = null;
        private ReaderWriterLock                    _readerWriterLock = 
            new ReaderWriterLock();
        private event EventHandler<EventArgsType> _invalidated;
        private bool isSetup = false;
        /// <summary>
        /// Full path of the file that is being monitored for changes.
        /// </summary>
        private string monitoredFilePath;
        #endregion Declarations

        #region Properties

        public event EventHandler<EventArgsType> Invalidated
        {
            add
            {
                _invalidated += value;
            }
            remove
            {
                _invalidated -= value;
            }
        }

        public int ValidityToken
        {
            get
            {
                return _validityToken;
            }
        }

        public ReaderWriterLock ReaderWriterLock
        {
            get
            {
                return _readerWriterLock;
            }
        }
    
        #endregion Properties

        #region IEnabled Implementation

        private bool _enabled = true;

        public event System.EventHandler EnabledChanged = null;

        [XmlElement()]
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    OnEnabledChanged();
                }

            }
        }

        protected virtual void OnEnabledChanged()
        {
            if (EnabledChanged != null)
            {
                EnabledChanged(this, System.EventArgs.Empty);
            }
        }

        #endregion

        #region Singleton Declarations

        private static object syncRoot = new object();

        private static CacheManager _instance = null;

        #endregion Singleton Declarations

        #region Singleton Instance Property

        /// <summary>
        /// Gets the instance of a <see cref="CacheManager"/>.
        /// </summary>
        /// <value>The instance.</value>
        public static CacheManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new CacheManager();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion Singleton Instance Property

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheManager"/> class.
        /// </summary>
        protected CacheManager()
        {
            try
            {
                Name = "CacheManager" + AppDomainUtility.GetCurrentAppDomainDescriptor();
                Description = "CacheManager serves for local management of cached objects";

                Setup();
            }
            catch (Exception ex)
            {
                Enabled = false;

                Log.Source.TraceData(TraceEventType.Error, 2001,
                    "Cache manager setup failed!" + ex.ToString());

                    throw;
            }
        }



        #endregion Constructors

        #region Static Methods
        // Static interface is only provided as an easier to access wrapper
        // onto the single instance.

        /// <summary>
        /// Validates the cache. If cache entry is stale, sets it to null, so
        /// the client can recache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="readerWriterLock">The reader writer lock.</param>
        /// <param name="validityToken">The validity token.</param>
        /// <param name="cachedItem">The cached item.</param>
        public static void ValidateCache<T>
        (
        ReaderWriterLock    readerWriterLock,
        ref int             validityToken,
        ref T               cachedItem
        )
        {
            // If not enabled, just return
            // But it also hits the instance, which is important in order to
            // setup monitoring.
            if (!CacheManager.Instance.Enabled) return;

            if (cachedItem == null) return;

            #region Cache Reset implementation

            readerWriterLock.AcquireReaderLock(-1);

            try
            {
                CacheManager.Instance.ReaderWriterLock.AcquireReaderLock(-1);
                try
                {
                    if (CacheManager.Instance.ValidityToken != validityToken)
                    {
                        Log.Source.TraceData(TraceEventType.Verbose, 2002, 
                            String.Format(
                             "CacheManager identifed the need to update the cache item of type {0}" +
                             ". Item will be reset to null so caller can refresh it.", 
                             cachedItem.GetType().FullName));

                        LockCookie lockCookie =
                            readerWriterLock.UpgradeToWriterLock(-1);
                        try
                        {
                            if (CacheManager.Instance.ValidityToken != validityToken)
                            {
                                validityToken = CacheManager.Instance.ValidityToken;
                                cachedItem = default(T);
                            }
                        }
                        finally
                        {
                            // Ensure that the lock is released.
                            readerWriterLock.DowngradeFromWriterLock(ref lockCookie);
                        }
                    }
                }
                finally
                {
                    CacheManager.Instance.ReaderWriterLock.ReleaseReaderLock();
                }
            }
            finally
            {
                readerWriterLock.ReleaseReaderLock();
            }

            #endregion Cache Reset implementation
        }

        /// <summary>
        /// Invalidates the cache.
        /// </summary>
        /// <param name="raiseEvent">if set to <c>true</c> [raise event].</param>
        public static void InvalidateCache(bool raiseEvent)
        {
            CacheManager.Instance.invalidateCache(raiseEvent);
        }
        /// <summary>
        /// Setups the monitoring
        /// </summary>
        public static void Activate()
        {

            Instance.Setup();
        }
        #endregion

        #region Functions

        /// <summary>
        /// Gets a value indicating whether this instance is configuration empty.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is configuration less; otherwise, <c>false</c>.
        /// </value>
        public bool IsConfigurationless
        {
         
            get
            {
                return _configuration == null;
            }
        }


        /// <summary>
        /// Setups the monitoring
        /// </summary>
        public void Setup()
        {
            Initialize();
        }

        private void Initialize()
        {
            lock (syncRoot)
            {
                // (SD) That is added to enable reentrance to this method
                if (isSetup) return;

                try
                {
                    _configuration = getConfiguration();

                    if (IsConfigurationless && !EnableByDefault)
                    {
                        Log.Source.TraceData(TraceEventType.Information, 2003,
                            String.Format(
                                         "Cache refresh will be disabled due to the following condition: {0}" +
                                         "To enable cache refresh enable the configuration and restart application.",
                                         "Configuration object is empty"));

                        Enabled = false;
                        return;
                    }


                    //TODO: (SD) Come up with an approach that would not have a dependency
                    // on having a config file.
                    string monitoredFile = string.Format
                        (
                        "{0}.cache",
                        AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".vshost", "").Replace(
                            ".config", "")
                        );
                    // if no configuration then driven by a constant on top, otherwise
                    // takes the value for Enabled from the configuration section.
                    Enabled = (IsConfigurationless) ? EnableByDefault : _configuration.Enabled;

                    monitoredFilePath = Path.Combine
                        (
                        AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                        monitoredFile
                        );

                    if (!Enabled)
                    {
                        Log.Source.TraceData(TraceEventType.Information, 2005,
                            "Cache manager for this application will not be enabled!");
                        return;
                    }

                    if (
                        !File.Exists(monitoredFilePath) && !IsConfigurationless &&
                        _configuration.RequiresFileWatch
                        )
                    {
                        Log.Source.TraceData(TraceEventType.Information, 2006,
                            "Cache manager for this application will not be enabled as " +
                                     " file to monitor doesn't exist! File path is: " + monitoredFilePath);
                        Enabled = false;
                        return;
                    }

                    if (!File.Exists(monitoredFilePath) && IsConfigurationless)
                    {
                        using (FileStream fs = File.Create(monitoredFilePath, 1000, FileOptions.None))
                        {
                            fs.Close();
                        }
                    }

                    SetupFileMonitoring(monitoredFile, monitoredFilePath);

                    isSetup = true;
                }
                catch (Exception ex)
                {
                    Log.Source.TraceData(TraceEventType.Error, 2007, ex);
                        throw;
                }
            }
        }

        private void SetupFileMonitoring(string monitoredFile, string monitoredFilePath)
        {

            FileSystemWatcher watcher = new FileSystemWatcher();

            watcher.Path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            watcher.Filter = Path.GetFileName(monitoredFile);
            watcher.NotifyFilter = NotifyFilters.LastWrite;

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(watcher_Changed);
            watcher.Error += new ErrorEventHandler(watcher_Error);

            // Begin watching.
            watcher.EnableRaisingEvents = true;

            Log.Source.TraceData(TraceEventType.Information, 2008,
                string.Format("Cache manager is enabled with monitored file path: {0}." +
                "If you change//edit this file, cache will be refreshed. Currently either use Notepad to " +
                " edit the file, or update it twice if single-update tool is used.",
                                       monitoredFilePath));
        }
        private void TouchMonitoredFile()
        {
            lock (syncRoot)
            {
                IOUtility.TouchFile(monitoredFilePath);
            }
        }


        private CacheManagerConfigSection getConfiguration()
        {
             return
                    ConfigurationManager.GetSection
                    (
                    ConfigurationSectionPath
                    ) as CacheManagerConfigSection;

        }

        void watcher_Error(object sender, ErrorEventArgs e)
        {
            lock (syncRoot)
            {
                isSetup = false;
            }
            //TODO: (SD) Create ActivityContext.Empty
            Log.Source.TraceData(TraceEventType.Error, 2009,
                string.Format(
                "Error while listenning for cache refresh file changes. " +
                "CacheManager will attempt to restart the monitoring. Error text: {0}.",
                e.GetException()));
        }
        private void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

            //TODO: (SD) Create fixed ActivityContext
            Log.Source.TraceData(TraceEventType.Information, 2010,
                string.Format
                (
                "Change to the application cache file {0} detected. There might be two or more notifications" +
                " for the file change, whilst only every second notification will lead to the cache " +
                " refresh as Notepad file update scenario is assumed now.",
                e.FullPath
                ));

            // In the case that update request is initiated from file change
            // event, skip every second request, as it is fired twice (SD).
            if (
                updateToken
                (
                delegate() // The call to delegate will be protected internally by the updateToken method (SD)
                {
                    return true;
                    // The bellow code is commented out now and was used when update was mainly done via 
                    // Notepad which is issueing two updates per each save.
                    //Interlocked.Increment(ref _validityTokenAux); // May not be required then (SD)
                    //return _validityTokenAux % 2 == 0;
                }
                ))
            {
                //TODO: (SD) Create fixed ActivityContext
                Log.Source.TraceData(TraceEventType.Information, 2011, string.Format
                    (
                    "ValidityToken value updated to {0}.",
                    _validityToken
                    ));
            }

        }

        private void invalidateCache
            (
            EventArgsType eventArgs,
            bool raiseEvent
            )
        {
            try
            {
                _readerWriterLock.AcquireWriterLock(-1);

                if (
                    updateToken(null)
                    )
                {

                    //TODO: (SD) Create fixed ActivityContext
                    Log.Source.TraceData(TraceEventType.Information, 2012, string.Format
                        (
                        "ValidityToken value updated to {0}.",
                        CacheManager._instance._validityToken
                        ));

                    // TODO: raise event option to be resolved in better way (SD)
                    if (raiseEvent)
                    {
                        OnCacheInvalidated(eventArgs);
                    }
                }
                // TODO: Provide for else (SD)
            }
            finally
            {
                _readerWriterLock.ReleaseWriterLock();
            }
        }

        private void OnCacheInvalidated(GenericChangeEventArgs<NumericValidityToken> eventArgs)
        {
            if (_invalidated != null)
            {
                _invalidated(this, eventArgs);
            }
        }

        private void invalidateCache(bool raiseEvent)
        {
            invalidateCache
                (
                new EventArgsType
                (
                null,
                null
                ),
                raiseEvent);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateResolver"></param>
        /// <returns>True if token has been updated</returns>
        private bool updateToken(ParameterlessPredicate updateResolver)
        {
            _readerWriterLock.AcquireWriterLock(-1);

            try
            {
                if (updateResolver==null||updateResolver())
                {
                    _validityToken++;
                    return true;
                }
            }
            finally
            {
                _readerWriterLock.ReleaseWriterLock();
            }
            return false;
        }


        #endregion Functions
    }
}
