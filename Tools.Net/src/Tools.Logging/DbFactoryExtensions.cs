using System;
using System.Data;
using System.Data.Common;

namespace Tools.Logging
{
    internal static class DbFactoryExtensions
    {
        internal static IDbConnection CreateConnection(this DbProviderFactory factory,
                                                       Action<IDbConnection> setup)
        {
            IDbConnection connection = factory.CreateConnection();
            setup(connection);
            return connection;
        }

        internal static DbParameter CreateParameter(this DbProviderFactory factory,
                                                    Action<DbParameter> setup)
        {
            DbParameter parameter = factory.CreateParameter();
            setup(parameter);
            return parameter;
        }

        internal static DbCommand CreateCommand(this DbProviderFactory factory,
                                                Action<DbCommand> setup)
        {
            DbCommand command = factory.CreateCommand();
            setup(command);
            return command;
        }
    }
}