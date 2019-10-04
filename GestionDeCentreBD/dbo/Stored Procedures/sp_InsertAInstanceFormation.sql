CREATE PROCEDURE [dbo].[sp_InsertAInstanceFormation]
	@Statut VARCHAR(25),
	@DateDebut DateTime,
	@DateFin DateTime,
	@IdFormation INT,
	@IdPersonne INT
AS
	BEGIN
		IF NOT EXISTS(SELECT * FROM INSTANCEFORMATION WHERE IdFormation = @IdFormation AND
		IdPersonne = @IdPersonne)
		INSERT INTO INSTANCEFORMATION(Statut, DateDebut, DateFin, IdFormation, IdPersonne)
		VALUES(@Statut, @DateDebut, @DateFin, @IdFormation, @IdPersonne)
		ELSE
		THROW 50021, 'L''instance de la Formation existe déjà dans la Table' , 1;
	END