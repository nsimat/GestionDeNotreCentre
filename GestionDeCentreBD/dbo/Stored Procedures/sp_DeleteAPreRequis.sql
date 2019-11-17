CREATE PROCEDURE [dbo].[sp_DeleteAPreRequis]
	@IdModulePreRequis INT,
	@IdModule INT
AS
	BEGIN
	IF EXISTS(SELECT * FROM PREREQUIS
				WHERE IdModulePrerequis = @IdModulePreRequis AND IdModule = @IdModule)
				DELETE FROM PREREQUIS WHERE IdModulePrerequis = @IdModulePreRequis AND IdModule = @IdModule;
			ELSE
		THROW 50021, 'Le PreRequis que l''on veut supprimer n''existe pas dans la table PreRequis', 1;
	END
