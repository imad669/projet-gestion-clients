# projet-gestion-clients
# Projet Gestion Clients et Commandes

Ce projet est une application web développée avec ASP.NET Core MVC permettant la gestion complète des clients et de leurs commandes associées.

## Fonctionnalités principales

- Liste des clients avec affichage de leurs informations de contact
- Consultation des commandes liées à chaque client
- Ajout, modification et suppression de clients
- Ajout, modification et suppression de commandes
- Génération d’un rapport affichant toutes les commandes d’un client sélectionné, avec détails et totaux
- Interface utilisateur intuitive avec tableaux triables et filtrables

## Technologies utilisées

- ASP.NET Core MVC
- Razor Pages
- C#
- Entity Framework Core (pour la gestion des données)
- Bootstrap (pour le design responsive)

## Structure du projet

- **Controllers/** : contrôleurs MVC gérant la logique métier et les actions utilisateur
- **Models/** : classes modèles représentant les entités Client, Commande, etc.
- **Views/** : vues Razor affichant les pages HTML
- **Data/** : classes pour la gestion des données (dépôts, contextes)
- **wwwroot/** : ressources statiques (CSS, JS, images)

## Installation et utilisation

1. Cloner le dépôt :  
   `git clone https://github.com/ton-utilisateur/gestion-clients-commandes.git`

2. Ouvrir le projet dans Visual Studio ou VS Code

3. Configurer la chaîne de connexion à la base de données dans `appsettings.json`

4. Appliquer les migrations Entity Framework pour créer la base :  
   `dotnet ef database update`

5. Lancer l’application en mode debug ou production

6. Accéder à l’interface web via `https://localhost:5001` (ou autre port configuré)

## Contribution

Les contributions sont les bienvenues. Merci de forker le projet et proposer vos modifications via pull request.

## Licence

Ce projet est sous licence MIT. Voir le fichier LICENSE pour plus de détails.




