CREATE PROCEDURE [dbo].[sp_InsertAnInscription]
	@DateInscription DATETIME,
	@IdInstanceFormation INT,
	@IdStagiaire INT,
	@IdEmploye INT

AS
	BEGIN
		IF NOT EXISTS(SELECT * FROM INSCRIPTION WHERE IdInstanceFormation = @IdInstanceFormation AND
		                                              IdStagiaire = @IdStagiaire AND
													  IdEmploye = @IdEmploye) 
													        OR
		   NOT EXISTS(SELECT * FROM INSCRIPTION WHERE IdInstanceFormation = @IdInstanceFormation AND IdStagiaire = @IdStagiaire AND IdEmploye = @IdEmploye AND DateValidation = 1)

			INSERT INTO INSCRIPTION(DateInscription, IdInstanceFormation, IdStagiaire, IdEmploye)
			VALUES(@DateInscription, @IdInstanceFormation, @IdStagiaire, @IdEmploye)
	    ELSE
		THROW 50021, 'Cette ligne d''inscription existe déjà dans la Table INSCRIPTION.' , 1;
	END