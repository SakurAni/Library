using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace SakurAni_Lib.Models 
{
    public class SakurAniLibContext
    {
        public string ConnectionString { get; set; }

        public SakurAniLibContext(string connectionString) 
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}