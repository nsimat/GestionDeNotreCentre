CREATE PROCEDURE [dbo].[sp_InsertAPlanification]
		@DatePlanification DATETIME,
		@IdTypeJour INT,
		@IdFormateur INT,
		@IdInstanceFormation INT,
		@IdModule INT
AS
	BEGIN
		IF NOT EXISTS(SELECT * FROM PLANIFICATION WHERE IdFormateur = @IdFormateur AND DatePlanification =@DatePlanification)
			INSERT INTO PLANIFICATION(DatePlanification, IdTypeJour, IdFormateur, IdInstanceFormation, IdModule)
			VALUES(@DatePlanification ,	@IdTypeJour, @IdFormateur, @IdInstanceFormation, @IdModule )
		ELSE	
			THROW 50021,'Cette planification existe déjà' , 1;
	END
	