using Microsoft.AspNetCore.Mvc;
using Gestions_Client_Commande.Data;
using Gestions_Client_Commande.Models;
using System.Collections.Generic;
using System.Linq;

namespace Gestions_Client_Commande.Controllers
{
    public class ClientController : Controller
    {
        private readonly DepotClients _depotClients;
        private readonly DepotCommandes _depotCommandes;

        public ClientController(DepotClients depotClients, DepotCommandes depotCommandes)
        {
            _depotClients = depotClients;
            _depotCommandes = depotCommandes;
        }

        // GET : /Client/
        public IActionResult Index()
        {
            var listeClients = _depotClients.ObtenirTousLesClients() ?? new List<Client>();
            return View(listeClients);
        }

        // GET : /Client/Commandes/5
        public IActionResult Commandes(int id)
        {
            var clientTrouve = _depotClients.ObtenirClientParId(id);
            if (clientTrouve == null)
                return NotFound();

            var listeCommandes = _depotCommandes.ObtenirCommandesParClientId(id);
            ViewBag.NomClient = clientTrouve.Nom;
            ViewBag.IdClient = id;

            return View(listeCommandes);
        }

        // GET : /Client/Creer (affiche la vue Create.cshtml)
        public IActionResult Creer()
        {
            return View("Create");
        }

        // POST : /Client/Creer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Creer(Client client)
        {
            if (ModelState.IsValid)
            {
                _depotClients.AjouterClient(client);
                return RedirectToAction(nameof(Index));
            }
            return View("Create", client);
        }

        // GET : /Client/Modifier/5 (affiche la vue Edit.cshtml)
        public IActionResult Modifier(int id)
        {
            var clientTrouve = _depotClients.ObtenirClientParId(id);
            if (clientTrouve == null)
                return NotFound();

            return View("Edit", clientTrouve);
        }

        // POST : /Client/Modifier/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Modifier(int id, Client client)
        {
            if (id != client.IdClient)
                return BadRequest();

            if (ModelState.IsValid)
            {
                _depotClients.MettreAJourClient(client);
                return RedirectToAction(nameof(Index));
            }
            return View("Edit", client);
        }

        // GET : /Client/Supprimer/5 (affiche la vue Delete.cshtml)
        public IActionResult Supprimer(int id)
        {
            var clientTrouve = _depotClients.ObtenirClientParId(id);
            if (clientTrouve == null)
                return NotFound();

            return View("Delete", clientTrouve);
        }

        // POST : /Client/Supprimer/5
        [HttpPost, ActionName("Supprimer")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmerSuppression(int id)
        {
            var client = _depotClients.ObtenirClientParId(id);
            if (client == null)
                return NotFound();

            _depotClients.SupprimerClient(id);
            return RedirectToAction(nameof(Index));
        }

        // GET : /Client/Rapport/5
        public IActionResult Rapport(int id)
        {
            var clientTrouve = _depotClients.ObtenirClientParId(id);
            if (clientTrouve == null)
                return NotFound();

            var listeCommandes = _depotCommandes.ObtenirCommandesParClientId(id);
            ViewBag.NomClient = clientTrouve.Nom;
            ViewBag.TotalMontant = listeCommandes.Sum(c => c.Montant);

            return View(listeCommandes);
        }
    }
}
