using System.Collections.Generic;
using System.Data.SqlClient;
using Gestions_Client_Commande.Models;

namespace Gestions_Client_Commande.Data
{
    public class DepotCommandes
    {
        private readonly string _chaineConnexion;

        public DepotCommandes(string chaineConnexion)
        {
            _chaineConnexion = chaineConnexion;
        }

        // Récupérer toutes les commandes
        public List<Commande> ObtenirToutesLesCommandes()
        {
            var listeCommandes = new List<Commande>();
            using var connexion = new SqlConnection(_chaineConnexion);
            string requete = "SELECT IdCommande, IdClient, DateCommande, Montant, Description FROM Commandes";
            using var commande = new SqlCommand(requete, connexion);
            connexion.Open();
            using var lecteur = commande.ExecuteReader();

            while (lecteur.Read())
            {
                listeCommandes.Add(new Commande
                {
                    IdCommande = lecteur.GetInt32(0),
                    IdClient = lecteur.GetInt32(1),
                    DateCommande = lecteur.GetDateTime(2),
                    Montant = lecteur.GetDecimal(3),
                    Description = lecteur.IsDBNull(4) ? null : lecteur.GetString(4)
                });
            }
            return listeCommandes;
        }

        // Récupérer une commande par son ID
        public Commande ObtenirCommandeParId(int idCommande)
        {
            Commande commandeTrouvee = null;
            using var connexion = new SqlConnection(_chaineConnexion);
            string requete = "SELECT IdCommande, IdClient, DateCommande, Montant, Description FROM Commandes WHERE IdCommande = @id";
            using var commande = new SqlCommand(requete, connexion);
            commande.Parameters.AddWithValue("@id", idCommande);
            connexion.Open();
            using var lecteur = commande.ExecuteReader();

            if (lecteur.Read())
            {
                commandeTrouvee = new Commande
                {
                    IdCommande = lecteur.GetInt32(0),
                    IdClient = lecteur.GetInt32(1),
                    DateCommande = lecteur.GetDateTime(2),
                    Montant = lecteur.GetDecimal(3),
                    Description = lecteur.IsDBNull(4) ? null : lecteur.GetString(4)
                };
            }
            return commandeTrouvee;
        }

        // Ajouter une nouvelle commande
        public void AjouterCommande(Commande commande)
        {
            using var connexion = new SqlConnection(_chaineConnexion);
            string requete = "INSERT INTO Commandes (IdClient, DateCommande, Montant, Description) VALUES (@IdClient, @DateCommande, @Montant, @Description)";
            using var commandeSql = new SqlCommand(requete, connexion);
            commandeSql.Parameters.AddWithValue("@IdClient", commande.IdClient);
            commandeSql.Parameters.AddWithValue("@DateCommande", commande.DateCommande);
            commandeSql.Parameters.AddWithValue("@Montant", commande.Montant);
            commandeSql.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(commande.Description) ? (object)DBNull.Value : commande.Description);
            connexion.Open();
            commandeSql.ExecuteNonQuery();
        }

        // Mettre à jour une commande existante
        public void MettreAJourCommande(Commande commande)
        {
            using var connexion = new SqlConnection(_chaineConnexion);
            string requete = "UPDATE Commandes SET IdClient=@IdClient, DateCommande=@DateCommande, Montant=@Montant, Description=@Description WHERE IdCommande=@IdCommande";
            using var commandeSql = new SqlCommand(requete, connexion);
            commandeSql.Parameters.AddWithValue("@IdClient", commande.IdClient);
            commandeSql.Parameters.AddWithValue("@DateCommande", commande.DateCommande);
            commandeSql.Parameters.AddWithValue("@Montant", commande.Montant);
            commandeSql.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(commande.Description) ? (object)DBNull.Value : commande.Description);
            commandeSql.Parameters.AddWithValue("@IdCommande", commande.IdCommande);
            connexion.Open();
            commandeSql.ExecuteNonQuery();
        }

        // Supprimer une commande par son ID
        public void SupprimerCommande(int idCommande)
        {
            using var connexion = new SqlConnection(_chaineConnexion);
            string requete = "DELETE FROM Commandes WHERE IdCommande=@IdCommande";
            using var commandeSql = new SqlCommand(requete, connexion);
            commandeSql.Parameters.AddWithValue("@IdCommande", idCommande);
            connexion.Open();
            commandeSql.ExecuteNonQuery();
        }

        // Récupérer toutes les commandes d'un client
        public List<Commande> ObtenirCommandesParClientId(int idClient)
        {
            var listeCommandes = new List<Commande>();
            using var connexion = new SqlConnection(_chaineConnexion);
            string requete = "SELECT IdCommande, IdClient, DateCommande, Montant, Description FROM Commandes WHERE IdClient = @idClient";
            using var commandeSql = new SqlCommand(requete, connexion);
            commandeSql.Parameters.AddWithValue("@idClient", idClient);
            connexion.Open();
            using var lecteur = commandeSql.ExecuteReader();

            while (lecteur.Read())
            {
                listeCommandes.Add(new Commande
                {
                    IdCommande = lecteur.GetInt32(0),
                    IdClient = lecteur.GetInt32(1),
                    DateCommande = lecteur.GetDateTime(2),
                    Montant = lecteur.GetDecimal(3),
                    Description = lecteur.IsDBNull(4) ? null : lecteur.GetString(4)
                });
            }
            return listeCommandes;
        }
    }
}
