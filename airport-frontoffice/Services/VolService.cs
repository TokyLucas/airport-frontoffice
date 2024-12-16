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

        public List<VolDetails> FindById(string id)
        {
            List<VolDetails> vols = new List<VolDetails>();

            using (var dbHelper = new DatabaseConnection(_connectionString))
            {
                var connection = dbHelper.GetConnection();
                using (var command = new SqlCommand(null, connection))
                {
                    command.CommandText =
                        "SELECT * FROM VolDetails WHERE vol_id = @id";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = Int64.Parse(id);
                    command.Prepare();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            VolDetails vol = new VolDetails();
                            vol.VolId = (int)reader["vol_id"];
                            vol.AvionId = (int)reader["avion_id"];
                            vol.AirportDepart = (int)reader["airport_depart"];
                            vol.AirportArrive = (int)reader["airport_arrive"];
                            vol.DateDepart = DateTime.Parse(reader["date_depart"].ToString());
                            vol.PrixDeBaseEconomique = (decimal)reader["prix_de_base_economique"];
                            vol.PrixDeBaseAffaire = (decimal)reader["prix_de_base_affaire"];
                            vol.PrixDeBasePremiere = (decimal)reader["prix_de_base_premiere"];

                            Avion avion = new Avion();
                            avion.Marque = reader["marque"].ToString();
                            avion.Modele = reader["modele"].ToString();
                            avion.Immatriculation = reader["immatriculation"].ToString();
                            vol.Avion = avion;

                            Airport airport = new Airport();
                            airport.Nom = reader["depart_nom"].ToString();
                            airport.Pays = reader["depart_pays"].ToString();
                            airport.Ville = reader["depart_ville"].ToString();
                            airport.IATA = reader["depart_IATA"].ToString();
                            airport.Latitude = reader["depart_latitude"].ToString();
                            airport.Longitude = reader["depart_longitude"].ToString();
                            vol.AirportDepartDetails = airport;
                            Airport airportArriveDetails = new Airport();
                            airportArriveDetails.Nom = reader["dest_nom"].ToString();
                            airportArriveDetails.Pays = reader["dest_pays"].ToString();
                            airportArriveDetails.Ville = reader["dest_ville"].ToString();
                            airportArriveDetails.IATA = reader["dest_IATA"].ToString();
                            airportArriveDetails.Latitude = reader["dest_latitude"].ToString();
                            airportArriveDetails.Longitude = reader["dest_longitude"].ToString();
                            vol.AirportArriveDetails = airportArriveDetails;

                            vols.Add(vol);
                        }
                    }
                }
            }

            return vols;
        }

        public List<VolDetails> FindVol(string airportDepart, string airportArrive, string dateDepart)
        {
            List<VolDetails> vols = new List<VolDetails>();

            using (var dbHelper = new DatabaseConnection(_connectionString))
            {
                var connection = dbHelper.GetConnection();
                using (var command = new SqlCommand(null, connection))
                {
                    command.CommandText =
                        "SELECT * FROM VolDetails WHERE airport_depart = @airport_depart and airport_arrive = @airport_arrive and  CAST(date_depart as DATE) = @date_depart";
                    command.Parameters.Add("@airport_depart", SqlDbType.Int).Value = Int64.Parse(airportDepart);
                    command.Parameters.Add("@airport_arrive", SqlDbType.Int).Value = Int64.Parse(airportArrive);
                    command.Parameters.Add("@date_depart", SqlDbType.DateTime).Value = DateTime.Parse(dateDepart);
                    command.Prepare();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            VolDetails vol = new VolDetails();
                            vol.VolId = (int)reader["vol_id"];
                            vol.AvionId = (int)reader["avion_id"];
                            vol.AirportDepart = (int)reader["airport_depart"];
                            vol.AirportArrive = (int)reader["airport_arrive"];
                            vol.DateDepart = DateTime.Parse(reader["date_depart"].ToString());
                            vol.PrixDeBaseEconomique = (decimal)reader["prix_de_base_economique"];
                            vol.PrixDeBaseAffaire = (decimal)reader["prix_de_base_affaire"];
                            vol.PrixDeBasePremiere = (decimal)reader["prix_de_base_premiere"];

                            Avion avion = new Avion();
                            avion.Marque = reader["marque"].ToString();
                            avion.Modele = reader["modele"].ToString();
                            avion.Immatriculation = reader["immatriculation"].ToString();
                            vol.Avion = avion;

                            Airport airport = new Airport();
                            airport.Nom = reader["depart_nom"].ToString();
                            airport.Pays = reader["depart_pays"].ToString();
                            airport.Ville = reader["depart_ville"].ToString();
                            airport.IATA = reader["depart_IATA"].ToString();
                            airport.Latitude = reader["depart_latitude"].ToString();
                            airport.Longitude = reader["depart_longitude"].ToString();
                            vol.AirportDepartDetails = airport;
                            Airport airportArriveDetails = new Airport();
                            airportArriveDetails.Nom = reader["dest_nom"].ToString();
                            airportArriveDetails.Pays = reader["dest_pays"].ToString();
                            airportArriveDetails.Ville = reader["dest_ville"].ToString();
                            airportArriveDetails.IATA = reader["dest_IATA"].ToString();
                            airportArriveDetails.Latitude = reader["dest_latitude"].ToString();
                            airportArriveDetails.Longitude = reader["dest_longitude"].ToString();
                            vol.AirportArriveDetails = airportArriveDetails;

                            vols.Add(vol);
                        }
                    }
                }
            }

            return vols;
        }
    }
}
