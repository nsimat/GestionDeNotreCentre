create view V_Stagiaire 
as
select *
from PERSONNE 
where IdEntreprise is null;