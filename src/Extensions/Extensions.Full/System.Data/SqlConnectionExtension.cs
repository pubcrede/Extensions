﻿//-----------------------------------------------------------------------
// <copyright file="SqlConnectionExtension.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
// 
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Genesys.Extensions
{
    /// <summary>
    /// Extends System.Type
    /// </summary>
    [CLSCompliant(true)]
    public static class SqlConnectionExtension
    {
        /// <summary>
        /// Constructs a connection string from multiple string elements
        /// </summary>
        public static void ConnectionString(this SqlConnection connection, string serverName, string databaseName, int timeoutInSeconds = 3)
        {
            StringBuilder connectionString = new StringBuilder();
            connectionString.Append("Data Source=").Append(serverName).Append(";Initial Catalog=");
            connectionString.Append(databaseName).Append(";Persist Security Info=True;Trusted_connection=Yes;").Append(";Connect Timeout=").Append(timeoutInSeconds);
            connection.ConnectionString = connectionString.ToString();
        }
        
        /// <summary>
        /// Tests a connection to see if can open
        /// </summary>
        /// <param name="connection"></param>
        /// <returns>True if this connection can be opened</returns>
        public static bool CanOpen(this SqlConnection connection)
        {
            bool returnValue = TypeExtension.DefaultBoolean;

            try
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    returnValue = true;
                    connection.Close();
                }
            }
            catch
            {
                returnValue = false;
            }
            finally
            {
                connection.Dispose();
            }

            return returnValue;
        }
    }
}
