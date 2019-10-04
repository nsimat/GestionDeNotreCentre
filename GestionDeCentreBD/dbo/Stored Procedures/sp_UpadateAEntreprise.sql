CREATE PROCEDURE [dbo].[sp_UpadateAEntreprise]
	@IdEntreprise INT,
	@NomEntreprise VARCHAR (25),
	@Rue  VARCHAR (25),
	@Ville VARCHAR (25),
	@CodePostal VARCHAR (25),
	@Pays VARCHAR (25),
	@NumeroTelephone VARCHAR (25)
	AS
	BEGIN
	IF EXISTS( SELECT * FROM ENTREPRISE WHERE IdEntreprise = @IdEntreprise)
		UPDATE ENTREPRISE
		SET	NomEntreprise = @NomEntreprise,
			Rue = @Rue,
			Ville = @Ville,
			CodePostal = @CodePostal,
			Pays = @Pays,
			NumeroTelephone = @NumeroTelephone
	ELSE
	THROW 50021,'L"Entreprise que vous voulez mettre à jour n"existe pas dans la table Entreprise', 1;

	END