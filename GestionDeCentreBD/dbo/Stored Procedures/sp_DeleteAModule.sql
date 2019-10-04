CREATE PROCEDURE [dbo].[sp_DeleteAModule]
	@IdModule INT
	
AS
	BEGIN
	IF EXISTS(SELECT * FROM MODULE
				WHERE IdModule = @IdModule)
				DELETE FROM MODULE
				WHERE IdModule = @IdModule;
		ELSE
		THROW 50021, 'Le module que vous voulez supprimer n''existe pas table MODULE', 1;
	END