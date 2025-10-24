using Microsoft.AspNetCore.Mvc;
using Gestions_Client_Commande.Data;
using Gestions_Client_Commande.Models;

namespace Gestions_Client_Commande.Controllers
{
    public class CommandeController : Controller
    {
        private readonly DepotCommandes _depotCommandes;
        private readonly DepotClients _depotClients;

        public CommandeController(DepotCommandes depotCommandes, DepotClients depotClients)
        {
            _depotCommandes = depotCommandes;
            _depotClients = depotClients;
        }

        // GET : /Commande/
        public IActionResult Index()
        {
            var toutesLesCommandes = _depotCommandes.ObtenirToutesLesCommandes();
            return View(toutesLesCommandes);
        }

        // GET : /Commande/Creer?idClient=1
        public IActionResult Creer(int idClient)
        {
            var clientAssocie = _depotClients.ObtenirClientParId(idClient);
            if (clientAssocie == null)
            {
                ViewBag.ErrorMessage = "Client non trouvé.";
                return View("Error");
            }

            ViewBag.NomClient = clientAssocie.Nom;
            ViewBag.IdClient = idClient;
            return View("Create"); // Assure-toi que ta vue s'appelle Create.cshtml
        }

        // POST : /Commande/Creer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Creer(Commande commande)
        {
            if (ModelState.IsValid)
            {
                _depotCommandes.AjouterCommande(commande);
                return RedirectToAction("Commandes", "Client", new { id = commande.IdClient });
            }

            var clientAssocie = _depotClients.ObtenirClientParId(commande.IdClient);
            ViewBag.NomClient = clientAssocie?.Nom;
            ViewBag.IdClient = commande.IdClient;
            return View("Create", commande);
        }
    }
}
