using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
//using System.Transactions;

namespace Tools.Core.Data
{
    public abstract class CommonDB
    {
        private ConnectionStringSettings _settings;

        protected ConnectionStringSettings Settings
        {
            get { return this._settings; }
        }

        #region Construct/Destruct
        
        protected CommonDB()
        {
            // Read default connection name from config
            string connectionName = ConfigurationManager.AppSettings["DefaultConnectionName"];
            if (string.IsNullOrEmpty(connectionName))
            {
                throw new ConfigurationErrorsException("Default connection string name not found.");
            }
            InitConnectionStringSettings(connectionName);
        }
        
        protected CommonDB(string connectionName)
        {
            if (string.IsNullOrEmpty(connectionName))
            {
                throw new ArgumentOutOfRangeException("connectionName");
            }
            InitConnectionStringSettings(connectionName);
        }
        
        private void InitConnectionStringSettings(string connectionName)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[connectionName];
            if (settings == null)
            {
                throw new ConfigurationErrorsException("Connection string settings not found for connection name " + connectionName + ".");
            }
            this._settings = settings;
        }

        #endregion


        protected virtual void ExecuteCommand(Action<IDbCommand> execute)
        {
            using (IDbConnection connection = this.CreateConnection())
            {
                this.ExecuteCommand(connection, execute);
            }
        }

        protected virtual void ExecuteCommand(IDbConnection connection, Action<IDbCommand> execute)
        {
            DbHelper.EnsureConnectionOpen(connection);
            using (IDbCommand command = connection.CreateCommand())
            {
                execute(command);
            }
        }

        protected virtual IDataReader ExecuteReader(IDbCommand command, CommandBehavior behavior)
        {
            DbHelper.EnsureConnectionOpen(command.Connection);
            IDataReader reader = command.ExecuteReader(behavior);
            return reader;
        }

        protected virtual object ExecuteScalar(IDbCommand command)
        {
            DbHelper.EnsureConnectionOpen(command.Connection);
            return command.ExecuteScalar();
        }
        
        //protected virtual IDbConnection CreateConnection()
        //{
        //    ConnectionStringSettings csSettings = this.Settings;
        //    DbProviderFactory factory = DbProviderFactories.GetFactory(csSettings.ProviderName);
        //    IDbConnection connection = factory.CreateConnection((c => c.ConnectionString = csSettings.ConnectionString));
        //    return connection;
        //}
    }
}