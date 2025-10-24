namespace Gestions_Client_Commande.Models
{
    public class Commande
    {
        public int IdCommande { get; set; }    // Pas IDCommande
        public int IdClient { get; set; }      // Pas IDClient
        public DateTime DateCommande { get; set; }
        public decimal Montant { get; set; }
        public string Description { get; set; }
    }
}
