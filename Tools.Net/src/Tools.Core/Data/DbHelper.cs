using System;
using System.Data;
using System.Data.Common;

namespace Tools.Core.Data
{
    internal static class DbHelper
    {
        internal static void CommitTransaction(DbTransaction dbTrans)
        {
            if (dbTrans == null)
            {
                throw new ArgumentNullException("dbTrans");
            }

            // Get transaction connection for closing before commit
            DbConnection dbConn = dbTrans.Connection;
        
            try
            {
                // Commit transaction
                dbTrans.Commit();
            }
            finally
            {
                // Close connection
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
        }

        internal static void RollbackTransaction(DbTransaction dbTrans)
        {
            if (dbTrans == null)
            {
                throw new ArgumentNullException("dbTrans");
            }

            // Get transaction connection for closing before rollback
            DbConnection dbConn = dbTrans.Connection;
            
            try
            {
                // Rollback transaction
                dbTrans.Rollback();
            }
            finally
            {
                // Close connection
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
        }

        /// <summary>
        /// Provides a null value when <paramref name="value"/> is b>DBNull</b>.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <returns>When <paramref name="value"/> is of type <b>DBNull</b> then a null value; otherwise <paramref name="value"/>.</returns>
        internal static object FromDbNullCheck(object value)
        {
            object objOut;
            
            if (Convert.IsDBNull(value))
            {
                objOut = null;
            }
            else
            {
                objOut = value;
            }

            return objOut;
        }
        
        /// <summary>
        /// Provides a <b>DBNull</b> value when <paramref name="value"/> is <b>null</b>.
        /// </summary>
        /// <param name="value">The value to be tested.</param>
        /// <returns>When <paramref name="value"/> is <b>null</b> then <b>DBNull</b>; otherwise <paramref name="value"/>.</returns>
        internal static object ToDbNullCheck(object value)
        {
            object objOut;
            
            if (value == null)
            {
                objOut = DBNull.Value;
            }
            else
            {
                objOut = value;
            }
            
            return objOut;
        }

        internal static void EnsureConnectionOpen(IDbConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

    }
}
