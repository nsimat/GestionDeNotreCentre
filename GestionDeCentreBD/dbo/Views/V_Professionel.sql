create view V_Professionel
as
select *
from PERSONNE 
where IdEntreprise is not null;