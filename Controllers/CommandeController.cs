using Microsoft.AspNetCore.Mvc;
using Gestions_Client_Commande.Data;
using Gestions_Client_Commande.Models;

namespace Gestions_Client_Commande.Controllers
{
    public class CommandeControleur : Controller
    {
        private readonly DepotCommandes _depotCommandes;
        private readonly DepotClients _depotClients;

        public CommandeControleur(DepotCommandes depotCommandes, DepotClients depotClients)
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

        // GET : /Commande/Creer?clientId=1
        public IActionResult Creer(int idClient)
        {
            var clientAssocie = _depotClients.ObtenirClientParId(idClient);
            if (clientAssocie == null)
                return NotFound();

            ViewBag.NomClient = clientAssocie.Nom;
            ViewBag.IdClient = idClient;
            return View();
        }

        // POST : /Commande/Creer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Creer(Commande commande)
        {
            if (ModelState.IsValid)
            {
                _depotCommandes.AjouterCommande(commande);
                return RedirectToAction(nameof(Index));
            }

            var clientAssocie = _depotClients.ObtenirClientParId(commande.IdClient);
            ViewBag.NomClient = clientAssocie?.Nom;
            ViewBag.IdClient = commande.IdClient;
            return View(commande);
        }

        // GET : /Commande/Modifier/5
        public IActionResult Modifier(int id)
        {
            var commandeTrouvee = _depotCommandes.ObtenirCommandeParId(id);
            if (commandeTrouvee == null)
                return NotFound();

            var clientAssocie = _depotClients.ObtenirClientParId(commandeTrouvee.IdClient);
            ViewBag.NomClient = clientAssocie?.Nom;
            return View(commandeTrouvee);
        }

        // POST : /Commande/Modifier/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Modifier(int id, Commande commande)
        {
            if (id != commande.IdCommande)
                return BadRequest();

            if (ModelState.IsValid)
            {
                _depotCommandes.MettreAJourCommande(commande);
                return RedirectToAction(nameof(Index));
            }

            var clientAssocie = _depotClients.ObtenirClientParId(commande.IdClient);
            ViewBag.NomClient = clientAssocie?.Nom;
            return View(commande);
        }

        // GET : /Commande/Supprimer/5
        public IActionResult Supprimer(int id)
        {
            var commandeTrouvee = _depotCommandes.ObtenirCommandeParId(id);
            if (commandeTrouvee == null)
                return NotFound();

            var clientAssocie = _depotClients.ObtenirClientParId(commandeTrouvee.IdClient);
            ViewBag.NomClient = clientAssocie?.Nom;
            return View(commandeTrouvee);
        }

        // POST : /Commande/Supprimer/5
        [HttpPost, ActionName("Supprimer")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmerSuppression(int id)
        {
            var commandeTrouvee = _depotCommandes.ObtenirCommandeParId(id);
            if (commandeTrouvee != null)
            {
                _depotCommandes.SupprimerCommande(id);
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}
