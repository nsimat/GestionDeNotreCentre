CREATE PROCEDURE [dbo].[sp_DeleteACompetence]
	@IdFormateur INT,
	@IdModule INT
AS
	BEGIN
	IF EXISTS(SELECT * FROM COMPETENCE
			  WHERE IdFormateur = @IdFormateur AND IdModule = @IdModule)
			DELETE FROM COMPETENCE WHERE IdFormateur = @IdFormateur AND IdModule = @IdModule;
	ELSE 
			THROW 50021, 'L"instance de Competence que vous voulez supprimer n''existe pas dans la table' , 1; 
	END
