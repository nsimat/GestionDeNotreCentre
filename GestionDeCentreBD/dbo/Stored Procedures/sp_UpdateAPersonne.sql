--Procédure stockée pour mettre à jour une personne présente dans la table PERSONNE--

CREATE PROCEDURE [dbo].[sp_UpdateAPersonne]
	@IdPersonne INT,
	@NumeroRegistre varchar(25),
	@Nom VARCHAR(50),
	@Prenom VARCHAR(50),
	@Email VARCHAR(25),
	@Rue VARCHAR(25),
	@Ville VARCHAR(25),
	@CodePostal VARCHAR(25),
	@Pays VARCHAR(25),
	@NumeroTelephone VARCHAR(25),
	@CV VARBINARY(MAX),
	@UserLogin VARCHAR(25),
	@MotDePasse VARCHAR(25),
	@IdEntreprise INT

AS
  BEGIN 
     IF EXISTS(SELECT * FROM PERSONNE WHERE IdPersonne = @IdPersonne) 
	    UPDATE PERSONNE
		SET NumeroRegistre = @NumeroRegistre,
		    Nom = @Nom,
			Prenom = @Prenom,
			Email = @Email,
			Rue = @Rue,
			Ville = @Ville,
			CodePostal = @CodePostal,
			Pays = @Pays,
			NumeroTelephone = @NumeroTelephone,
			CV = @CV,
			UserLogin = @UserLogin,
			MotDePasse = @MotDePasse,
			IdEntreprise = @IdEntreprise;
	ELSE
	  THROW 50021, 'La personne que l''on veut mettre à jour n''existe pas dans la table Personne', 1;
  END
	
