CREATE PROCEDURE [dbo].[sp_InsertAFormation]
	@Nom VARCHAR(50),
	@DescriptionFormation VARCHAR(MAX)
AS
	BEGIN
	IF NOT EXISTS(SELECT * FROM FORMATION WHERE Nom = @Nom)
			INSERT INTO FORMATION(Nom, DescriptionFormation)
			VALUES (@Nom, @DescriptionFormation)
	ELSE
			THROW 50021, 'Cette Formation existe déjà', 1;
	
	END