CREATE PROCEDURE [dbo].[sp_DeleteAPlanification]
	@IdPlanification INT
	AS
		BEGIN
		IF EXISTS( SELECT * FROM PLANIFICATION
					WHERE IdPlanification = @IdPlanification)
					DELETE FROM PLANIFICATION 
					WHERE IdPlanification = @IdPlanification
			ELSE
			THROW 50021, 'La planification que l''on veut supprimer n''existe pas dans la table Planification', 1;

		END