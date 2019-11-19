-- View Section
-- _____________ 

create view V_Employe
as
select p.IdPersonne as IdEmploye, p.NumeroRegistre as NumeroRegistre, Nom, Prenom, Email, Rue, Ville, CodePostal, Pays, CV, UserLogin, MotDePasse, IdEntreprise 
from PERSONNE p JOIN EMPLOYE e on p.IdPersonne = e.IdEmploye;