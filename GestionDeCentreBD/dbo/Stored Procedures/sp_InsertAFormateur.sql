CREATE PROCEDURE [dbo].[sp_InsertAFormateur]
	@IdFormateur INT

AS
	BEGIN
	IF NOT EXISTS(SELECT * FROM FORMATEUR WHERE IdFormateur = @IdFormateur)
			INSERT INTO FORMATEUR(IdFormateur)
			VALUES (@IdFormateur)
	ELSE
			THROW 50021, 'Ce Formateur existe déjà', 1;
	
	END
