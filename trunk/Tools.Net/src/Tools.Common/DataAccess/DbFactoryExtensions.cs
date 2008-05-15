using System;
using System.Collections.Generic;

using System.Text;
using System.Data.Common;
using System.Data;

namespace Tools.Common.DataAccess
{
    public static class DbFactoryExtensions
    {
        public static IDbConnection CreateConnection(this DbProviderFactory factory,
            Action<IDbConnection> setup)
        {
            IDbConnection connection = factory.CreateConnection();
            setup(connection);
            return connection;
        }
        public static DbParameter CreateParameter(this DbProviderFactory factory,
            Action<DbParameter> setup)
        {
            DbParameter parameter = factory.CreateParameter();
            setup(parameter);
            return parameter;
        }
        public static DbCommand CreateCommand(this DbProviderFactory factory,
            Action<DbCommand> setup)
        {
            DbCommand command = factory.CreateCommand();
            setup(command);
            return command;
        }

    }
}
