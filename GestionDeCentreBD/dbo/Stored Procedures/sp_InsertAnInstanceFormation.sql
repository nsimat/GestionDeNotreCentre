CREATE PROCEDURE [dbo].[sp_InsertAnInstanceFormation]
	@Statut VARCHAR(25),
	@DateDebut DateTime,
	@DateFin DateTime,
	@IdFormation INT,
	@IdEmploye INT
AS
	BEGIN
		IF NOT EXISTS(SELECT * FROM INSTANCEFORMATION WHERE IdFormation = @IdFormation AND
		IdEmploye = @IdEmploye)
		INSERT INTO INSTANCEFORMATION(Statut, DateDebut, DateFin, IdFormation, IdEmploye)
		VALUES(@Statut, @DateDebut, @DateFin, @IdFormation, @IdEmploye)
		ELSE
		THROW 50021, 'L''instance de la Formation existe déjà dans la Table' , 1;
	END