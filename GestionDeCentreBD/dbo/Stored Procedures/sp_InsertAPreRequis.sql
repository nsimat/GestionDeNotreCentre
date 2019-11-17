CREATE PROCEDURE [dbo].[sp_InsertAPreRequis]
	@IdModulePreRequis INT,
	@IdModule INT
AS
	BEGIN
		IF NOT EXISTS(SELECT * FROM PREREQUIS WHERE IdModulePrerequis = @IdModulePreRequis AND IdModule = @IdModule)
			INSERT INTO PREREQUIS(IdModulePrerequis, IdModule)
			VALUES (@IdModulePreRequis, @IdModule )
		ELSE	
			THROW 50021,'Ce prérequis existe déjà dans la table Prerequis pour ce module.' , 1;
	END
