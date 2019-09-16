--Procédure stockée pour insérer une personne dans la table FORMATEUR--
create procedure sp_Formateur
       @IdFormateur int 

as
  begin
    if not exists(SELECT * FROM FORMATEUR WHERE IdFormateur = @IdFormateur) and exists (SELECT * FROM PERSONNE WHERE IdPersonne = @IdFormateur) 
	   insert into FORMATEUR(IdFormateur) values (@IdFormateur);
	else
	   throw 50021, 'Le Formateur existe déjà dans la table FORMATEUR', 1; 
  end