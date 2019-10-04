CREATE PROCEDURE [dbo].[sp_DeleteAFormation]
	@IdFormation INT
	
AS
	BEGIN
	IF EXISTS(SELECT * FROM FORMATION
				WHERE IdFormation = @IdFormation)
				DELETE FROM FORMATION WHERE IdFormation = @IdFormation;
			ELSE
		THROW 50021, 'La Formation que l''on veut supprimer n''existe pas dans la table Formation', 1;
	END