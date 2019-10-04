CREATE PROCEDURE [dbo].[sp_UpdateAPlanification]
		@IdPlanification INT,  
		@DatePlanification DATETIME,
		@IdTypeJour INT,
		@IdFormateur INT,
		@IdInstanceFormation INT,
		@IdModule INT
		AS
		BEGIN
		IF EXISTS(SELECT * FROM PLANIFICATION WHERE IdPlanification = @IdPlanification)
			UPDATE PLANIFICATION
				SET
					DatePlanification =	@DatePlanification,
					IdTypeJour = @IdTypeJour,
					IdFormateur = @IdFormateur,
					IdInstanceFormation = @IdInstanceFormation ,
					IdModule = @IdModule 
	       ELSE
	   THROW 50021, 'La Planification que vous voulez mettre à jour n''existe pas dans la table planification' , 1;
		END