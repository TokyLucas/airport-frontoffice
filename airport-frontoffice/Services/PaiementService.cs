using airport_frontoffice.Database;
using airport_frontoffice.Models;
using System.Data.SqlClient;
using System.Data;
using airport_frontoffice.Helpers;

namespace airport_frontoffice.Services
{
    public class PaiementService
    {
        private readonly string _connectionString;

        public PaiementService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int Payer(Paiement paiement)
        {
            int result = 0;
            try
            {
                using (var dbHelper = new DatabaseConnection(_connectionString))
                {
                    var connection = dbHelper.GetConnection();
                    using (var command = new SqlCommand(null, connection))
                    {
                        command.CommandText =
                            "INSERT INTO Paiement (reservation_id, numero_carte_bancaire) VALUES " +
                            "(@reservation_id, @numero_carte_bancaire)  ";

                        command.Parameters.Add("@reservation_id", SqlDbType.Int).Value = paiement.ReservationId;
                        command.Parameters.Add("@numero_carte_bancaire", SqlDbType.VarChar, 12).Value = paiement.NumeroCarteBancaire;
                        command.Prepare();

                        result = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
