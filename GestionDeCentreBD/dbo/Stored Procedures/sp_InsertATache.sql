CREATE PROCEDURE [dbo].[sp_InsertATache]
	@IdTache INT,
	@LibelleTache VARCHAR(50),
	@MessageTache VARCHAR(MAX),
	@DateCreation DATETIME,
	@DateCloture DATETIME,
	@IdCreateur INT,
	@IdRealisateur INT
AS
	BEGIN
	IF NOT EXISTS(SELECT * FROM TACHE WHERE IdTache =@IdTache)
			INSERT INTO TACHE
			VALUES (@LibelleTache,	@MessageTache, @DateCreation, @DateCloture, @IdCreateur, @IdRealisateur) 
		ELSE
			THROW 50021, 'cette tache existe déjà dans la table Tache' , 1;
	END