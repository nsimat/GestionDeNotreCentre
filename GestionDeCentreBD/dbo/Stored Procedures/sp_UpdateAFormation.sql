CREATE PROCEDURE [dbo].[sp_UpdateAFormation]
	@IdFormation INT,
	@Nom VARCHAR(50),
	@DescriptionFormation VARCHAR(MAX)
AS
	BEGIN
	IF EXISTS(SELECT * FROM FORMATION WHERE IdFormation = @IdFormation)
	UPDATE FORMATION
	SET 
	Nom = @Nom,
	DescriptionFormation = @DescriptionFormation
	ELSE
	THROW 50021,'La Formation que vous voulez mettre à jour n''existe pas dans la table Formation' , 1;
	END