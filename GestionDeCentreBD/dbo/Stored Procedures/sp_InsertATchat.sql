CREATE PROCEDURE [dbo].[sp_InsertATchat]
	@IdTchat INt,
	@MessageTchat VARCHAR(Max),
	@DateDebut DATETIME,
	@DateCloture DATETIME,
	@IdEnvoyeur INT,
	@IdRecepteur INT
AS
	BEGIN
		IF NOT EXISTS(SELECT * FROM TACHE WHERE IdTache = @IdTchat)
			INSERT INTO TACHE
			VALUES (@MessageTchat, @DateDebut, @DateCloture, @IdEnvoyeur, @IdRecepteur)
		ELSE
			THROW 50021, 'cette tchat existe déjà dans la tabe' , 1;
	END