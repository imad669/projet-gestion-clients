INSERT INTO Clients (Nom, Email, Telephone) 
VALUES ('Jean Dupont', 'jean.dupont@example.com', '514-123-4567');
INSERT INTO Commandes (DateCommande, Montant, Description, IdClient) 
VALUES (GETDATE(), 150.75, 'Commande test', 1);
SELECT c.IdCommande, c.DateCommande, c.Montant, cl.Nom, cl.Email, cl.Telephone
FROM Commandes c
JOIN Clients cl ON c.IdClient = cl.IdClient;
