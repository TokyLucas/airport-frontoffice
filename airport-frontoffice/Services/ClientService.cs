using airport_frontoffice.Database;
using airport_frontoffice.Helpers;
using airport_frontoffice.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace airport_frontoffice.Services
{
    public class ClientService
    {
        private readonly string _connectionString;

        public ClientService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Client? Login(string email, string motdepasse)
        {
            Client? client = null;

            using (var dbHelper = new DatabaseConnection(_connectionString))
            {
                var connection = dbHelper.GetConnection();
                using (var command = new SqlCommand(null, connection))
                {
                    command.CommandText =
                        "SELECT client_id, nom, prenom, email, telephone, adresse FROM Client " +
                        "WHERE email = @email AND motdepasse = @motdepasse ";
                    SqlParameter emailParam = new SqlParameter("@email", SqlDbType.VarChar, 25);
                    SqlParameter mdpParam = new SqlParameter("@motdepasse", SqlDbType.VarChar, 100);
                    emailParam.Value = email;
                    mdpParam.Value = Crypto.Sha256Hash(motdepasse);
                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(mdpParam);
                    command.Prepare();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            client = new Client();
                            client.ClientId = (int) reader["client_id"];
                            client.Nom = reader["nom"].ToString();
                            client.Prenom = reader["prenom"].ToString();
                            client.Email = reader["email"].ToString();
                            client.Telephone = reader["telephone"].ToString();
                            client.Adresse = reader["adresse"].ToString();
                        }
                    }
                }
            }

            return client;
        }
        public int SignIn(Client client)
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
                            "INSERT INTO Client (nom, prenom, email, motdepasse, telephone, adresse) VALUES " +
                            "(@nom, @prenom, @email, @motdepasse, @telephone, @adresse) ";

                        command.Parameters.Add("@nom", SqlDbType.VarChar, 25).Value = client.Nom;
                        command.Parameters.Add("@prenom", SqlDbType.VarChar, 25).Value = client.Prenom;
                        command.Parameters.Add("@email", SqlDbType.VarChar, 25).Value = client.Email;
                        command.Parameters.Add("@motdepasse", SqlDbType.VarChar, 100).Value = Crypto.Sha256Hash(client.Motdepasse);
                        command.Parameters.Add("@telephone", SqlDbType.VarChar, 25).Value = client.Telephone;
                        command.Parameters.Add("@adresse", SqlDbType.VarChar, 25).Value = client.Adresse;
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
