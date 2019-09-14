--Procédure stockée pour insérer une personne dans la table FORMATEUR--
create procedure sp_Formateur
       @NumeroRegistre varchar(25) 

as
  begin
    if not exists(SELECT * FROM FORMATEUR WHERE NumeroRegistre = @NumeroRegistre) and exists (SELECT * FROM PERSONNE WHERE NumeroRegistre = @NumeroRegistre) 
	   insert into FORMATEUR(NumeroRegistre) values (@NumeroRegistre);
	else
	   throw 50021, 'Le Formateur existe déjà dans la table EMPLOYE', 1; 
  end