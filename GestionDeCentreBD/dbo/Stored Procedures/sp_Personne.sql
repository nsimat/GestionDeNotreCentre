-- Stored Procedures Section
-- _____________

--Procédure stockée pour insérer une personne dans la table PERSONNE--

create procedure sp_Personne 
       @NumeroRegistre varchar(25),
	   @Nom varchar(50),
	   @Prenom varchar(50),
	   @Email varchar(25),
	   @Rue varchar(25),
	   @Ville varchar(25),
	   @CodePostal varchar(25),
	   @Pays varchar(25),
	   @CV varbinary(MAX),
	   @UserLogin varchar(25),
	   @MotDePasse varchar(25),
	   @IdEntreprise int = null
as
  begin
    if not exists(SELECT * FROM PERSONNE WHERE NumeroRegistre = @NumeroRegistre) 
	   insert into PERSONNE(NumeroRegistre, Nom, Prenom, Email, Rue, Ville, CodePostal, Pays, CV, UserLogin, MotDePasse, IdEntreprise) 
	   values (@NumeroRegistre, @Nom, @Prenom, @Email, @Rue, @Ville, @CodePostal, @Pays, @CV, @UserLogin, @MotDePasse, @IdEntreprise);
	else 
	  throw 50021, 'Cette personne existe déjà dans la table PERSONNE', 1;
  end