CREATE PROCEDURE [dbo].[sp_DeleteAnEmploye]
	@IdEmploye INT

AS
	BEGIN
	IF EXISTS(SELECT * FROM EMPLOYE
			  WHERE IdEmploye = @IdEmploye)
			DELETE FROM EMPLOYE WHERE IdEmploye = @IdEmploye;
	ELSE 
			THROW 50021, 'L"instance de Employe que vous voulez supprimer n''existe pas dans la table' , 1; 
	END
