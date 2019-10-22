CREATE PROCEDURE [dbo].[sp_InsertATchat]
	@IdTchat INt,
	@MessageTchat VARCHAR(Max),
	@DateDebut DATETIME,
	@DateCloture DATETIME,
	@IdEnvoyeur INT,
	@IdRecepteur INT
AS
	BEGIN
		IF NOT EXISTS(SELECT * FROM TCHAT WHERE IdTchat = @IdTchat)
			INSERT INTO TCHAT
			VALUES (@MessageTchat, @DateDebut, @DateCloture, @IdEnvoyeur, @IdRecepteur)
		ELSE
			THROW 50021, 'Cette ligne de tchat existe déjà dans la tabe' , 1;
	END