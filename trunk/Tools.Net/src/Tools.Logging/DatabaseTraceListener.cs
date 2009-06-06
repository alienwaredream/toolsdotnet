using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Tools.Core.Configuration;
using Tools.Core;

namespace Tools.Logging
{
    //TODO:(SD) Introduce configuration for max message size, mapping to params, etc.
    /// <summary>
    /// Provides logging to the database
    /// </summary>
    public class DatabaseTraceListener : TraceListener
    {
        private readonly string appDomainName;
        private readonly string connectionStringName;
        private readonly IExtraDataTransformer extraLogDataProvider;
        private readonly TraceListener fallbackTraceListener;
        private readonly object initSyncObject = new object();
        private readonly string modulePath;
        private readonly string storedProcedureName;
        protected string connectionString;

        protected IConfigurationValueProvider connectionStringProvider =
            new ConnectionStringConfigurationProvider();

        protected DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");

        private string initializationFailureString;
        private bool initialized;
        private bool initializedInFailedMode;
        private string machineName;

        /// <summary>
        /// Initalizes a new instance of <see cref="DatabaseTraceListener"/>.
        /// </summary>
        public DatabaseTraceListener()
        {
            fallbackTraceListener = new XmlWriterRollingTraceListener(2000000, "dblogfallback");
            machineName = Environment.MachineName;
            modulePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            appDomainName = AppDomain.CurrentDomain.FriendlyName;
        }

        public DatabaseTraceListener(string storedProcedureName, string connectionStringName,
                                     TraceListener fallbackListener, IExtraDataTransformer extraLogDataTransformer
            )
            : this()
        {
            this.storedProcedureName = storedProcedureName;
            this.connectionStringName = connectionStringName;
            fallbackTraceListener = fallbackListener;
            extraLogDataProvider = extraLogDataTransformer;
        }

        #region Trace listener methods

        #region calling overloads

        public override void Fail(string message, string detailMessage)
        {
            var builder = new StringBuilder(message);
            if (detailMessage != null)
            {
                builder.Append(" ");
                builder.Append(detailMessage);
            }
            TraceEvent(null, Log.Source.Name, TraceEventType.Error, 0, builder.ToString());
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id,
                                        string format, params object[] args)
        {
            if ((base.Filter == null) ||
                base.Filter.ShouldTrace(eventCache, source, eventType, id, format, args, null, null))
            {
                if (args != null)
                {
                    TraceEvent(eventCache, source, eventType, id,
                               String.Format(CultureInfo.InvariantCulture, format, args));
                }
                else
                {
                    TraceEvent(eventCache, source, eventType, id, format);
                }
            }
        }

        #endregion

        public override void TraceData(
            TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if ((base.Filter == null) ||
                base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                WriteInternal(() => WriteInternal(eventCache, source, eventType, id, data));
            }
        }

        public override void TraceData(
            TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            if ((base.Filter == null) ||
                base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
            {
                WriteInternal(() => WriteInternal(eventCache, source, eventType, id, data));
            }
        }

        public override void TraceEvent(
            TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            if ((base.Filter == null) ||
                base.Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
            {
                WriteInternal(() => WriteInternal(eventCache, source, eventType, id, message));
            }
        }

        public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message,
                                           Guid relatedActivityId)
        {
            WriteInternal(
                () => WriteInternal(eventCache, source, TraceEventType.Transfer, id, message, relatedActivityId));
        }

        public override void Write(string message)
        {
            WriteLine(message);
        }

        public override void WriteLine(string message)
        {
            TraceEvent(null, Log.Source.Name, TraceEventType.Information, 0, message);
        }

        #endregion Trace listener methods

        #region Private implementation methods

        private void WriteInternal(VoidDelegate write)
        {
            write();
            //throw new NotImplementedException("Method WriteInternal(Action write) is not implemented!");
        }

        private void WriteInternal(
            TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if (!initialized)
            {
                Initialize();
            }
            if (!initializedInFailedMode)
            {
                try
                {
                    using (IDbConnection conn =
                        factory.CreateConnection())
                    {
                        conn.ConnectionString = connectionString;

                        using (IDbCommand command = factory.CreateCommand())
                        {

                            command.CommandText = storedProcedureName;
                            command.CommandType = CommandType.StoredProcedure;
                            command.Connection = conn as DbConnection;

                            AddContextParameters(eventCache, eventType, id, command);

                            AddTransformerParameters(data, command);

                            if (!command.Parameters.Contains("Message") && data != null)
                            {
                                DbParameter param = factory.CreateParameter();


                                param.DbType = DbType.String;
                                param.Value = data.ToString();
                                param.ParameterName = "Message";

                                command.Parameters.Add(param);
                            }

                            conn.Open();

                            int n = command.ExecuteNonQuery();
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (fallbackTraceListener != null)
                    {
                        fallbackTraceListener.TraceData(null, Log.Source.Name, eventType, id, ex);
                        fallbackTraceListener.TraceData(null, Log.Source.Name, eventType, id, data);
                        return;
                    }
                    throw;
                }
            }

            if (fallbackTraceListener != null)
            {
                if (initializedInFailedMode)
                {
                    fallbackTraceListener.TraceData(eventCache, source, TraceEventType.Error, 301,
                                                    initializationFailureString);
                }
                fallbackTraceListener.TraceData(eventCache, source, eventType, id, data);
            }
        }

        private void WriteInternal(
            TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message,
            Guid correlationId)
        {
            if (!initialized)
            {
                Initialize();
            }
            if (!initializedInFailedMode)
            {
                try
                {
                    using (IDbConnection conn =
                        factory.CreateConnection())
                    {
                        conn.ConnectionString = connectionString;

                        using (IDbCommand command = factory.CreateCommand())
                        {

                            command.CommandText = storedProcedureName;
                            command.CommandType = CommandType.StoredProcedure;
                            command.Connection = conn as DbConnection;

                            AddContextParameters(eventCache, eventType, id, command);

                            AddTransformerParameters(message, command);

                            if (!command.Parameters.Contains("Message") && message != null)
                            {
                                DbParameter param = factory.CreateParameter();


                                param.DbType = DbType.String;
                                param.Value = message;
                                param.ParameterName = "Message";

                                command.Parameters.Add(param);
                            }

                            DbParameter corrParam = factory.CreateParameter();

                            corrParam.DbType = DbType.Guid;
                            corrParam.Value = correlationId;
                            corrParam.ParameterName = "CorrelationId";

                            command.Parameters.Add(corrParam);

                            conn.Open();

                            int n = command.ExecuteNonQuery();
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (fallbackTraceListener != null)
                    {
                        fallbackTraceListener.TraceData(eventCache, Log.Source.Name, eventType, id, ex);
                        fallbackTraceListener.TraceTransfer(eventCache, Log.Source.Name, id, message, correlationId);
                        return;
                    }
                    throw;
                }
            }
            if (fallbackTraceListener != null)
            {
                if (initializedInFailedMode)
                {
                    fallbackTraceListener.TraceData(eventCache, source, TraceEventType.Error, 301,
                                                    initializationFailureString);
                }
                fallbackTraceListener.TraceTransfer(eventCache, Log.Source.Name, id, message, correlationId);
            }
        }

        private void AddTransformerParameters(object data, IDbCommand command)
        {
            if (extraLogDataProvider != null)
            {
                Dictionary<string, object> extraLogParameters =
                    extraLogDataProvider.TransformToDictionary(data);

                if (extraLogParameters != null && extraLogParameters.Count > 0)
                {
                    foreach (string paramName in extraLogParameters.Keys)
                    {
                        // Introducing extra var, just in order not use indexer twice
                        object objValue = extraLogParameters[paramName];

                        if (objValue != null)
                        {
                            // Normalizing parameters to the string type here and
                            // normalizing values to be .ToString().
                            DbParameter param = factory.CreateParameter();

                            param.DbType = DbType.String;
                            param.Value = objValue.ToString();
                            param.ParameterName = paramName;

                            command.Parameters.Add(param);
                            //? Why to normalize - another option would be to use
                            // extensive mapping or DbType.Object, the latter one
                            // would lead to the sql_variant on the sql server
                            // which has got contraints by itself (8000 size, indexing)
                            // DbType.Object may not be supported by some odbcs.
                        }
                    }
                }
            }
        }

        private void AddContextParameters(TraceEventCache eventCache, TraceEventType eventType, int id,
                                          IDbCommand command)
        {
            //**
            //command.Parameters.Add(factory.CreateParameter(
            //                           (p) =>
            //                           {
            //                               p.DbType = DbType.DateTime;
            //                               p.Value = (eventCache != null) ? eventCache.DateTime : DateTime.UtcNow;
            //                               p.ParameterName = "Date";
            //                           }));

            //command.Parameters.Add(factory.CreateParameter(
            //                           (p) =>
            //                           {
            //                               p.DbType = DbType.Int32;
            //                               p.Value = id;
            //                               p.ParameterName = "MessageId";
            //                           }));
            //command.Parameters.Add(factory.CreateParameter(
            //                           (p) =>
            //                           {
            //                               p.DbType = DbType.Int32;
            //                               p.Value = Convert.ToInt32(eventType);
            //                               p.ParameterName = "TypeId";
            //                           }));
            //command.Parameters.Add(factory.CreateParameter(
            //                           (p) =>
            //                           {
            //                               p.DbType = DbType.String;
            //                               p.Value = eventType.ToString();
            //                               p.ParameterName = "TypeName";
            //                           }));
            //command.Parameters.Add(factory.CreateParameter(
            //                           (p) =>
            //                           {
            //                               p.DbType = DbType.String;
            //                               p.Value = Environment.MachineName;
            //                               p.ParameterName = "MachineName";
            //                           }));
            //command.Parameters.Add(factory.CreateParameter(
            //                           (p) =>
            //                           {
            //                               p.DbType = DbType.String;
            //                               p.Value = modulePath;
            //                               p.ParameterName = "ModulePath";
            //                           }));
            //command.Parameters.Add(factory.CreateParameter(
            //                           (p) =>
            //                           {
            //                               p.DbType = DbType.String;
            //                               p.Value = appDomainName;
            //                               p.ParameterName = "ModuleName";
            //                           }));
            //command.Parameters.Add(factory.CreateParameter(
            //                           (p) =>
            //                           {
            //                               p.DbType = DbType.String;
            //                               p.Value = Thread.CurrentThread.Name;
            //                               p.ParameterName = "ThreadName";
            //                           }));

            //if (Thread.CurrentPrincipal != null && Thread.CurrentPrincipal.Identity != null)
            //{
            //    command.Parameters.Add(factory.CreateParameter(
            //                               (p) =>
            //                               {
            //                                   p.DbType = DbType.String;
            //                                   p.Value = Thread.CurrentPrincipal.Identity.Name;
            //                                   p.ParameterName = "ThreadIdentity";
            //                               }));
            //}
            //// (SD) question is if there should be a factory method for getting the identity, 
            //// what is on Mono/Linux for this?
            //IIdentity identity = WindowsIdentity.GetCurrent();
            //if (identity != null)
            //{
            //    command.Parameters.Add(factory.CreateParameter(
            //                               (p) =>
            //                               {
            //                                   p.DbType = DbType.String;
            //                                   p.Value = identity.Name;
            //                                   p.ParameterName = "OSIdentity";
            //                               }));
            //}

            //command.Parameters.Add(factory.CreateParameter(
            //                           (p) =>
            //                           {
            //                               p.DbType = DbType.Guid;
            //                               p.Value = Trace.CorrelationManager.ActivityId;
            //                               p.ParameterName = "ActivityId";
            //                           }));
        }

        #endregion

        private void Initialize()
        {
            if (!initialized)
            {
                lock (initSyncObject)
                {
                    initialized = true;
                    initializedInFailedMode = true;

                    if (connectionStringProvider == null ||
                        String.IsNullOrEmpty(connectionStringProvider[connectionStringName]))
                    {
                        initializationFailureString += String.Format(CultureInfo.InvariantCulture,
                                                                     "Non empty connection string with name {0} is required for logging purposes!" +
                                                                     " Review configuration settings. Logging will be rerouted to the fallback listener.",
                                                                     connectionStringName);
                        return;
                    }

                    connectionString = connectionStringProvider[connectionStringName];
                    initializedInFailedMode = false;
                }
            }
        }
    }
}