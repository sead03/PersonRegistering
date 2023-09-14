using MySql.Data.MySqlClient;

namespace PersonRegistering
{
    class myDbConnection
    {
        public MySqlConnection cn;
        public void Connect()
        {
            cn = new MySqlConnection("Datasource = 127.0.0.1;username = root;password=;database=personregistering");

        }

    }
}
