CREATE PROCEDURE [dbo].[sp_InsertAComposition]
	@IdFormation INT,
	@IdModule INT,
	@DateAjout DateTime
AS
	BEGIN
	IF NOT EXISTS(SELECT * FROM COMPOSITION WHERE IdFormation = @IdFormation AND IdModule = @IdModule)
			INSERT INTO COMPOSITION(IdFormation, IdModule, DateAjout)
			VALUES (@IdFormation, @IdModule, @DateAjout);
	ELSE
			THROW 50021, 'Ce Module existe déjà parmi les modules de la formation', 1;
	
	END
