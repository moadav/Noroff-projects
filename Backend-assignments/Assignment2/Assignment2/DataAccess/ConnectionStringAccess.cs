using ConsoleApp1.Repositories.CustomerRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.DataAccess
{
    internal static class ConnectionStringAccess
    {

        /// <summary>The connection string of the database</summary>
        /// <returns>returns a string with the connection details to connect to the database</returns>
        public static string ConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.InitialCatalog = "Chinook";
            builder.IntegratedSecurity = true;
            builder.TrustServerCertificate = true;
            return builder.ConnectionString;

        }
    }
}
