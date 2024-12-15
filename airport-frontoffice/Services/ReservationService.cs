using airport_frontoffice.Models;
using System.Data.SqlClient;
using System.Data;
using airport_frontoffice.Helpers;
using airport_frontoffice.Database;
using System.ComponentModel.DataAnnotations;

namespace airport_frontoffice.Services
{
    public class ReservationService
    {
        private readonly string _connectionString;
        public ReservationService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<ReservationDetails> FindById(string id)
        {
            List<ReservationDetails> reservations = new List<ReservationDetails>();

            using (var dbHelper = new DatabaseConnection(_connectionString))
            {
                var connection = dbHelper.GetConnection();
                using (var command = new SqlCommand(null, connection))
                {
                    command.CommandText =
                        "SELECT * FROM ReservationDetails WHERE reservation_id = @id";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = Int64.Parse(id);
                    command.Prepare();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ReservationDetails reservation = new ReservationDetails();
                            reservation.VolId = (int)reader["vol_id"];
                            reservation.ClientId = (int)reader["client_id"];
                            reservation.ReservationId = (int)reader["reservation_id"];
                            reservation.NbPlaceEconomique = (int)reader["nb_place_economique"];
                            reservation.NbPlaceAffaire = (int)reader["nb_place_affaire"];
                            reservation.NbPlacePremiere = (int)reader["nb_place_premiere"];
                            reservation.Personne = reader["personne"].ToString();
                            reservation.Payee = (int)reader["Payee"];
                            reservation.TotalEconomique = (decimal)reader["total_economique"];
                            reservation.TotalAffaire = (decimal)reader["total_affaire"];
                            reservation.TotalPremiere = (decimal)reader["total_premiere"];

                            reservations.Add(reservation);
                        }
                    }
                }
            }
            return reservations;
        }
        public List<ListeReservation> FindByClientId(string clientId)
        {
            List<ListeReservation> reservations = new List<ListeReservation>();

            using (var dbHelper = new DatabaseConnection(_connectionString))
            {
                var connection = dbHelper.GetConnection();
                using (var command = new SqlCommand(null, connection))
                {
                    command.CommandText =
                        "SELECT * FROM ListeReservation WHERE client_id = @id";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = Int64.Parse(clientId);
                    command.Prepare();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListeReservation reservation = new ListeReservation();
                            reservation.ReservationId = (int)reader["reservation_id"];
                            reservation.VolId = (int)reader["vol_id"];
                            reservation.ClientId = (int)reader["client_id"];
                            reservation.Payee = (int)reader["Payee"];
                            reservation.DateDepart = DateTime.Parse(reader["date_depart"].ToString());
                            reservation.Depart = reader["depart"].ToString();
                            reservation.Arrive = reader["arrive"].ToString();

                            reservations.Add(reservation);
                        }
                    }
                }
            }
            return reservations;
        }
        public int Reserver(Reservation reservation, List<PlaceReservation> placeReservations)
        {
            int result = 0;
            using (var dbHelper = new DatabaseConnection(_connectionString))
            {
                var connection = dbHelper.GetConnection();
                // Begin a transaction
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Insert into parent table and retrieve the generated OrderID
                    decimal reservationId = -1;
                    using (var cmdReservation = new SqlCommand(null, connection, transaction))
                    {
                        cmdReservation.CommandText =
                            "INSERT INTO Reservation (client_id, vol_id) VALUES " +
                            "(@client_id, @vol_id); " +
                            "SELECT SCOPE_IDENTITY();";

                        cmdReservation.Parameters.Add("@client_id", SqlDbType.Int).Value = reservation.ClientId;
                        cmdReservation.Parameters.Add("@vol_id", SqlDbType.Int).Value = reservation.VolId;
                        cmdReservation.Prepare();

                        reservationId = (decimal) cmdReservation.ExecuteScalar();
                    }

                    if (reservationId < 0) throw new Exception("Reservation non insérer");

                    using (var cmdPlace = new SqlCommand(null, connection, transaction))
                    {
                        foreach(PlaceReservation placeReservation in placeReservations)
                        {
                            cmdPlace.CommandText =
                                "INSERT INTO PlaceReservation (reservation_id, tarifPersonne_id, nb_place_economique, nb_place_affaire, nb_place_premiere) VALUES " +
                                "(@reservation_id, @tarifPersonne_id, @nb_place_economique, @nb_place_affaire, @nb_place_premiere)";

                            cmdPlace.Parameters.Clear();
                            cmdPlace.Parameters.Add("@reservation_id", SqlDbType.Int).Value = reservationId;
                            cmdPlace.Parameters.Add("@tarifPersonne_id", SqlDbType.Int).Value = placeReservation.TarifPersonneId;
                            cmdPlace.Parameters.Add("@nb_place_economique", SqlDbType.Int).Value = placeReservation.NbPlaceEconomique;
                            cmdPlace.Parameters.Add("@nb_place_affaire", SqlDbType.Int).Value = placeReservation.NbPlaceAffaire;
                            cmdPlace.Parameters.Add("@nb_place_premiere", SqlDbType.Int).Value = placeReservation.NbPlacePremiere;
                            cmdPlace.Prepare();

                            cmdPlace.ExecuteNonQuery();
                        }
                    }

                    // Commit the transaction
                    transaction.Commit();
                    Console.WriteLine("Transaction committed successfully.");
                }
                catch (Exception ex)
                {
                    // Rollback the transaction in case of an error
                    transaction.Rollback();
                    throw ex;
                }
            }

            return result;
        }
    }
}
