CREATE PROCEDURE [dbo].[sp_UpdateATache]
	@IdTache INT,
	@LibelleTache VARCHAR(50),
	@MessageTache VARCHAR(MAX),
	@DateCreation DATETIME,
	@DateCloture DATETIME,
	@IdCreateur INT,
	@IdRealisateur INT
AS
	BEGIN

	IF EXISTS(SELECT * FROM TACHE WHERE IdTache = @IdTache)
	UPDATE TACHE
	SET
	LibelleTache = @LibelleTache,
	MessageTache = @MessageTache,
	DateCreation = @DateCreation ,
	DateCloture = @DateCloture ,
	IdCreateur = @IdCreateur,
	IdRealisateur = @IdRealisateur
	ELSE
	THROW 
	50021, 'La TACHE que vous voulez mettre à jour n"existe pas dans la table' , 1;
	END