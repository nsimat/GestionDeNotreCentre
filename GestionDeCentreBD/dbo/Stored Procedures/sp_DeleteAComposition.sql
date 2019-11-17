CREATE PROCEDURE [dbo].[sp_DeleteAComposition]
	@IdFormation INT,
	@IdModule INT
AS
	BEGIN
	IF EXISTS(SELECT * FROM COMPOSITION
			  WHERE IdFormation = @IdFormation AND IdModule = @IdModule)
			DELETE FROM COMPOSITION WHERE IdFormation = @IdFormation AND IdModule = @IdModule;
	ELSE 
			THROW 50021, 'L"instance de composition que vous voulez supprimer n''existe pas dans la table COMPOSITION' , 1; 
	END
