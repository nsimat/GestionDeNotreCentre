CREATE PROCEDURE [dbo].[sp_UpdateATchat]
	@IdTchat INT,
	@MessageTchat VARCHAR(MAX),
	@DateDebut DATETIME,
	@DateCloture DATETIME,
	@IdEnvoyeur INT,
	@IdRecepteur INT
AS
	BEGIN 
	IF EXISTS(SELECT * FROM TCHAT WHERE IdTchat = @IdTchat)
	UPDATE TCHAT
	SET

	MessageTchat = @MessageTchat ,
	DateDebut = @DateDebut ,
	DateCloture = @DateCloture ,
	IdEnvoyeur = @IdEnvoyeur ,
	IdRecepteur = @IdRecepteur 
	ELSE
	THROW 50021, 'La tache que vous voulez mettre à jour n"existe pas dans la table' , 1;

	END