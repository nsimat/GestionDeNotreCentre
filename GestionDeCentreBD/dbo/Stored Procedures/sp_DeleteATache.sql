CREATE PROCEDURE [dbo].[sp_DeleteATache]
	@IdTache INT
	
AS
	BEGIN
	IF EXISTS(SELECT * FROM TACHE
				WHERE IdTache = @IdTache)
				DELETE FROM TACHE
				WHERE IdTache =@IdTache
		ELSE 
		THROW 50021, 'La tache que vous voulez supprimer n"existe pas dans la table tache' , 1 ;
	END