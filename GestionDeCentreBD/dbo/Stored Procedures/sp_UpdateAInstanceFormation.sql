CREATE PROCEDURE [dbo].[sp_UpdateAInstanceFormation]
	@IdInstanceFormation INT,
	@Statut VARCHAR(50),
	@DateDebut DATETIME,
	@DateFin DATETIME,
	@IdFormation INT,
	@IdEmploye INT


AS
BEGIN
IF EXISTS(SELECT * FROM INSTANCEFORMATION WHERE IdInstanceFormation = @IdInstanceFormation)
		UPDATE INSTANCEFORMATION
		SET 
			Statut = @Statut,
			DateDebut = @DateDebut,
			DateFin = @DateFin,
			IdFormation = @IdFormation,
			IdEmploye = @IdEmploye
ELSE 
	THROW 50021, 'L''instance de formation que vous voulez mettre à jour n''existe pas dans la table', 1;
END