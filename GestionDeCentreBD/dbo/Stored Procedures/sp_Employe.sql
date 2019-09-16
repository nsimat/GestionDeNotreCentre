--Procédure stockée pour insérer une personne dans la table EMPLOYE--
create procedure sp_Employe 
       @IdEmploye int 

as
  begin
    if not exists(SELECT * FROM EMPLOYE WHERE IdEmploye = @IdEmploye) and exists (SELECT * FROM PERSONNE WHERE IdPersonne = @IdEmploye) 
	   insert into EMPLOYE(IdEmploye) values (@IdEmploye);
	else
	   throw 50021, 'L''employé existe déjà dans la table EMPLOYE', 1; 
  end