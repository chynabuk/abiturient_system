using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abiturient_System.Util
{
    public class Connection
    {
        private static Connection connection;
        private NpgsqlConnection conn;

        private Connection()
        {
            conn = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=kuba_admin;Database=abutirient");
        }

        public static Connection getInstance()
        {
            if (connection == null)
            {
                connection = new Connection();
            }
            return connection;
        }

        public NpgsqlConnection getConnection()
        {
            return conn;
        }
    }
}
