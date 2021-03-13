using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CobraAmin.DB
{
    public class AppDB : IDisposable
    {
        private IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build();

        public MySqlConnection Connection { get; }
        public AppDB(string connectionString)
        {
            Connection = new MySqlConnection(configuration.GetConnectionString(connectionString));
        }
        public AppDB()
        {
            Connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        //public AppDB()
        //{
        //    Connection = new MySqlConnection(_connectionString);
        //}
        public void Dispose() => Connection.Dispose();
    }
}
