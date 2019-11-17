CREATE PROCEDURE [dbo].[sp_UpdateAFormateur]
	@IdFormateur INT
AS
	bEGIN
	IF EXISTS(SELECT * FROM FORMATEUR WHERE IdFormateur = @IdFormateur)
	UPDATE FORMATEUR
	SET 
	IdFormateur = @IdFormateur
	
	ELSE
		THROW 50021,'Le Formateur que vous voulez mettre à jour n''existe pas dans la table Formateur' , 1;
	END
