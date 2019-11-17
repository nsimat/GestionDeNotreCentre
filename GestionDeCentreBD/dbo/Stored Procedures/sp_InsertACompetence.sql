CREATE PROCEDURE [dbo].[sp_InsertACompetence]
	@IdFormateur INT,
	@IdModule INT
AS
	BEGIN
	IF NOT EXISTS(SELECT * FROM COMPETENCE WHERE IdFormateur = @IdFormateur AND IdModule = @IdModule)
			INSERT INTO COMPETENCE(IdFormateur, IdModule)
			VALUES (@IdFormateur, @IdModule)
	ELSE
			THROW 50021, 'Cette Competence existe déjà', 1;
	
	END
