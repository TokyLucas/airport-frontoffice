using System.Data;
using System.Data.SqlClient;

namespace airport_frontoffice.Database
{
    public class DatabaseConnection : IDisposable
    {
        private readonly SqlConnection _connection;

        public DatabaseConnection(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public SqlConnection GetConnection()
        {
            if (_connection.State == ConnectionState.Closed || _connection.State == ConnectionState.Broken)
            {
                _connection.Open();
            }
            return _connection;
        }
        public void Dispose()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }
    }
}
