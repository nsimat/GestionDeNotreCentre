CREATE PROCEDURE [dbo].[sp_UpdateAComposition]
	@IdFormation INT,
	@IdModule INT,
	@DateAjout DateTime,
	@DateSuppression DateTime
AS
	BEGIN
	IF EXISTS (SELECT * FROM COMPOSITION WHERE IdFormation = @IdFormation AND IdModule= @IdModule)
		UPDATE COMPOSITION
			SET			  
			  IdFormation = @IdFormation,
			  IdModule = @IdModule,
			  DateAjout = @DateAjout,
			  DateSuppression = @DateSuppression

			  WHERE IdFormation = @IdFormation AND IdModule = @IdModule
	  ELSE
	    THROW 50021, 'La Composition que vous voulez mettre à jour n''existe pas dans la table Composition', 1;
	END
