CREATE PROCEDURE [dbo].[sp_DeleteAFormateur]
	@IdFormateur INT
	
	AS
	BEGIN
	IF EXISTS(SELECT * FROM FORMATEUR
			  WHERE IdFormateur = @IdFormateur)
			DELETE FROM FORMATEUR WHERE IdFormateur = @IdFormateur;
	ELSE 
			THROW 50021, 'L"instance de Formateur que vous voulez supprimer n''existe pas dans la table' , 1; 
	END