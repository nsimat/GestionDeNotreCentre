create view V_Formateur
as
select p.NumeroRegistre, Nom, Prenom, Email, Rue, Ville, CodePostal, Pays, CV, UserLogin, MotDePasse, IdEntreprise
from PERSONNE p join FORMATEUR f on p.IdPersonne = f.IdFormateur;