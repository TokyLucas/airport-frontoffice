using airport_frontoffice.Database;
using airport_frontoffice.Models;
using System.Data;
using System.Data.SqlClient;

namespace airport_frontoffice.Services
{
    public class VolService
    {
        private readonly string _connectionString;

        public VolService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Vol> FindVol(string airportDepart, string airportArrive, string dateDepart)
        {
            List<Vol> vols = new List<Vol>();

            using (var dbHelper = new DatabaseConnection(_connectionString))
            {
                var connection = dbHelper.GetConnection();
                using (var command = new SqlCommand(null, connection))
                {
                    command.CommandText =
                        "SELECT * FROM Vol WHERE airport_depart = @airport_depart and airport_arrive = @airport_arrive and date_depart <= @date_depart";
                    command.Parameters.Add("@airport_depart", SqlDbType.Int).Value = Int64.Parse(airportDepart);
                    command.Parameters.Add("@airport_arrive", SqlDbType.Int).Value = Int64.Parse(airportArrive);
                    command.Parameters.Add("@date_depart", SqlDbType.DateTime).Value = DateTime.Parse(dateDepart);
                    command.Prepare();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Vol vol = new Vol();
                            vol.VolId = (int)reader["vol_id"];
                            vol.AvionId = (int)reader["avion_id"];
                            vol.AirportDepart = (int)reader["airport_depart"];
                            vol.AirportArrive = (int)reader["airport_arrive"];
                            vol.DateDepart = DateTime.Parse(reader["date_depart"].ToString());
                            vol.PrixDeBaseEconomique = (decimal)reader["prix_de_base_economique"];
                            vol.PrixDeBaseAffaire = (decimal)reader["prix_de_base_affaire"];
                            vol.PrixDeBasePremiere = (decimal)reader["prix_de_base_premiere"];
                            vols.Add(vol);
                        }
                    }
                }
            }

            return vols;
        }
    }
}
