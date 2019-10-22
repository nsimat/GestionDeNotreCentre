CREATE PROCEDURE [dbo].[sp_DeleteAnInscription]
	@IdInscription int
AS
	BEGIN
	IF EXISTS(SELECT * FROM INSCRIPTION
			  WHERE IdInscription = @IdInscription)
			  DELETE FROM INSCRIPTION WHERE IdInscription = @IdInscription;
	ELSE 
			THROW 50021, 'L"instance de Inscription que vous voulez supprimer n''existe pas dans la table' , 1; 
	END
