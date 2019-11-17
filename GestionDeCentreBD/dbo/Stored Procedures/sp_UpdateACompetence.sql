CREATE PROCEDURE [dbo].[sp_UpdateACompetence]
	@IdFormateur INT,
	@IdModule INT
AS
	BEGIN
	IF EXISTS (SELECT * FROM COMPETENCE WHERE IdFormateur = @IdFormateur AND IdModule= @IdModule)
		UPDATE COMPETENCE
			SET			  
			  IdFormateur = @IdFormateur,
			  IdModule = @IdModule
	  ELSE
	  THROW 50021, 'La Competence que vous voulez mettre à jour n''existe pas dans la table Competence', 1;
	END
