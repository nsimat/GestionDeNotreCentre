CREATE PROCEDURE [dbo].[sp_DeleteAInstanceFormation]
	@IdInstanceFormation INT
AS
	BEGIN
	IF EXISTS(SELECT * FROM INSTANCEFORMATION
			WHERE IdInstanceFormation = @IdInstanceFormation)
			DELETE FROM INSTANCEFORMATION WHERE IdInstanceFormation = @IdInstanceFormation;
	ELSE 
			THROW 50021, 'L"instance de Formation que vous voulez supprimer n''existe pas dans la table' , 1; 
	END
