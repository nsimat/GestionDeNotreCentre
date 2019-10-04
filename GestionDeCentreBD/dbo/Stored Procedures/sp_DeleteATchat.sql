CREATE PROCEDURE [dbo].[sp_DeleteATchat]
	@IdTchat INT
	
AS
	BEGIN 
	IF EXISTS(SELECT * FROM TCHAT
			WHERE IdTchat = @IdTchat)
			DELETE FROM TACHE
			WHERE IdTache = @IdTchat
		ELSE
		THROW 50021, 'Le Message du tchat que vous voulez supprimer n"existe pas ', 1;

	END