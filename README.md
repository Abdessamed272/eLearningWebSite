Le projet à réaliser sera le suivant: 
l'entreprise MANU_AUTO met en place son site de tutoriels pour aider les utilisateur à réparer leur voiture seul. Les données sont les suivantes :
- les membres de l'entreprise doivent avoir la possibilité de manager un tutoriel ( ajouter, modifier, supprimer, voir la liste) 
- le directeur a le rôle superAdmin et peut décider d'enlever quelqu'un de l'entreprise.
- l'utilisateur lambda accedera aux vidéos et ecrits des tutoriels 
- il y aura un petit carré vert dans la page qui prouve que ce tuto à déja été visité par l'utilisateur connecté
- un utilisateur sans compte n'aura acces qu'aux tutoriels écrits.
- les differents roles (user) sont superAdmin, employé, utilisateur (+ visiteur)
- un tutoriels contient un titre, une date de création, un contenu, une vidéo, une date de derniere modification, une catégorie
- les catégories sont par exemple, climatisation, revision, turbo, ...
- les employés de l'entreprise auront accès à un dashboard qui permettra de gerer les tutoriels.
- les utilisateurs auront la possibilité de voir la liste des tutoriels qu'ils ont déjà vus
- il ne sera pas possible d'avoir deux comptes avec la même adresse Email
Bonus : ils pourront modifier leur mot de passe
Bonus : si l'adresse Email contient @lamanu alors le compte créé aura automatiquement un role employé

il faudra impérativement :
- gérer l'emplacement des images et des vidéos 
- éviter les pages d'erreurs
- un système de connexion

Convention:
- nom (attribut, méthodes) en Anglais (sauf au stade de France !!)
- pascalcase
- autorisations indiquées explicitement dans le code (utilisation 'using' de Identity et authentification)
- Identifiants Rôles = visitor/customer/employee/admin

Gestion des accès:
- Framework Identity

Base de données:
- tables en partage sur Azur (tutoautodb.database.windows.net  // login:lamanu)
- tables générées par Identity
  * [dbo].[AspNetUsers] contient les utilisateurs enregistrés
  * [dbo].[AspNetRoles] contient les définitions des rôles
  * [dbo].[AspNetUserRoles] contient les attributions de rôle à chaque utilisateur enregistré
  * [dbo].[Category] liste les noms des catégories
  * [dbo].[Content] est la liste des tutos (titre, lien, catégorie)
  * [dbo].[History] liste les tuples associant les contenus visités et leur visiteur


Schéma fonctionnel:
1) accueil des 'users'

![image](https://user-images.githubusercontent.com/37933499/171390682-9ff22568-88fa-4dde-90ab-8a4fb4ffe463.png)


Page d'accueil:
Le header contient le nom du site, le bouton connect/disconnect, un logo désignant le rôle actuel du User (ex: gris si visiteur, bleu si utilisateur, vert si employé, rouge si admin et incluant le role en texte)
Le body contient le contenu qui est sélectionné en fonction du rôle (ex: liste de sujets en texte si 'visiteur', liste avec images/vidéos si 'utilisateur') et les différentes vues des tutos en entier. Suivant les rôles, le body accueil également la page admin, le dashboard, l'historique.
Le footer est à définir (copyright, date ...)


2) gestion des services

