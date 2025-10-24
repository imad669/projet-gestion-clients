using System.Collections.Generic;
using System.Data.SqlClient;
using Gestions_Client_Commande.Models;

namespace Gestions_Client_Commande.Data
{
    public class DepotClients
    {
        private readonly string _chaineConnexion;

        public DepotClients(string chaineConnexion)
        {
            _chaineConnexion = chaineConnexion;
        }

        // Récupération de tous les clients
        public List<Client> ObtenirTousLesClients()
        {
            var listeClients = new List<Client>();
            using var connexion = new SqlConnection(_chaineConnexion);
            string requete = "SELECT IdClient, Nom, Email, Telephone FROM Clients";
            using var commande = new SqlCommand(requete, connexion);
            connexion.Open();
            using var lecteur = commande.ExecuteReader();

            while (lecteur.Read())
            {
                listeClients.Add(new Client
                {
                    IdClient = lecteur.GetInt32(0),
                    Nom = lecteur.GetString(1),
                    Courriel = lecteur.GetString(2),
                    Telephone = lecteur.GetString(3)
                });
            }
            return listeClients;
        }

        // Recherche d’un client par son ID
        public Client ObtenirClientParId(int idClient)
        {
            Client clientTrouve = null;
            using var connexion = new SqlConnection(_chaineConnexion);
            string requete = "SELECT IdClient, Nom, Email, Telephone FROM Clients WHERE IdClient = @id";
            using var commande = new SqlCommand(requete, connexion);
            commande.Parameters.AddWithValue("@id", idClient);
            connexion.Open();
            using var lecteur = commande.ExecuteReader();

            if (lecteur.Read())
            {
                clientTrouve = new Client
                {
                    IdClient = lecteur.GetInt32(0),
                    Nom = lecteur.GetString(1),
                    Courriel = lecteur.GetString(2),
                    Telephone = lecteur.GetString(3)
                };
            }
            return clientTrouve;
        }

        // Ajout d’un nouveau client
        public void AjouterClient(Client client)
        {
            using var connexion = new SqlConnection(_chaineConnexion);
            string requete = "INSERT INTO Clients (Nom, Email, Telephone) VALUES (@Nom, @Email, @Telephone)";
            using var commande = new SqlCommand(requete, connexion);
            commande.Parameters.AddWithValue("@Nom", client.Nom);
            commande.Parameters.AddWithValue("@Email", client.Courriel);
            commande.Parameters.AddWithValue("@Telephone", client.Telephone);
            connexion.Open();
            commande.ExecuteNonQuery();
        }

        // Mise à jour d’un client existant
        public void MettreAJourClient(Client client)
        {
            using var connexion = new SqlConnection(_chaineConnexion);
            string requete = "UPDATE Clients SET Nom=@Nom, Email=@Email, Telephone=@Telephone WHERE IdClient=@IdClient";
            using var commande = new SqlCommand(requete, connexion);
            commande.Parameters.AddWithValue("@Nom", client.Nom);
            commande.Parameters.AddWithValue("@Email", client.Courriel);
            commande.Parameters.AddWithValue("@Telephone", client.Telephone);
            commande.Parameters.AddWithValue("@IdClient", client.IdClient);
            connexion.Open();
            commande.ExecuteNonQuery();
        }

        // Suppression d’un client
        public void SupprimerClient(int idClient)
        {
            using var connexion = new SqlConnection(_chaineConnexion);
            string requete = "DELETE FROM Clients WHERE IdClient=@IdClient";
            using var commande = new SqlCommand(requete, connexion);
            commande.Parameters.AddWithValue("@IdClient", idClient);
            connexion.Open();
            commande.ExecuteNonQuery();
        }
    }
}
