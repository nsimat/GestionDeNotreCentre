--Procédure stockée pour insérer une personne dans la table EMPLOYE--
create procedure sp_Employe 
       @NumeroRegistre varchar(25) 

as
  begin
    if not exists(SELECT * FROM EMPLOYE WHERE NumeroRegistre = @NumeroRegistre) and exists (SELECT * FROM PERSONNE WHERE NumeroRegistre = @NumeroRegistre) 
	   insert into EMPLOYE(NumeroRegistre) values (@NumeroRegistre);
	else
	   throw 50021, 'L''employé existe déjà dans la table EMPLOYE', 1; 
  end