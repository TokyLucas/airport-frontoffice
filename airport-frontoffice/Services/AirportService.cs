using airport_frontoffice.Database;
using airport_frontoffice.Models;
using System.Data.SqlClient;
using System.Data;

namespace airport_frontoffice.Services
{
    public class AirportService
    {
        private readonly string _connectionString;

        public AirportService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Airport> FindByVille(string ville)
        {
            List<Airport> airports = new List<Airport>();

            using (var dbHelper = new DatabaseConnection(_connectionString))
            {
                var connection = dbHelper.GetConnection();
                using (var command = new SqlCommand(null, connection))
                {
                    command.CommandText =
                        "SELECT * FROM Airport WHERE ville LIKE @ville";
                    command.Parameters.Add("@ville", SqlDbType.VarChar, 25).Value = $"%{ville}%";
                    command.Prepare();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Airport airport = new Airport();
                            airport.AirportId = (int)reader["airport_id"];
                            airport.Nom = reader["nom"].ToString();
                            airport.Ville = reader["ville"].ToString();
                            airport.Pays = reader["pays"].ToString();
                            airport.IATA = reader["IATA"].ToString();
                            airport.ICAO = reader["IcAO"].ToString();
                            airport.Latitude = reader["latitude"].ToString();
                            airport.Longitude = reader["longitude"].ToString();
                            airport.Altitude = reader["altitude"].ToString();
                            airport.Timezone = reader["timezone"].ToString();
                            airports.Add(airport);
                        }
                    }
                }
            }

            return airports;
        }
    }
}
