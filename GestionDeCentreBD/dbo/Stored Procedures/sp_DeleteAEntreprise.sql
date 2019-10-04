CREATE PROCEDURE [dbo].[sp_DeleteAEntreprise]
	@IdEntreprise INT
	
AS
	BEGIN
	if exists(SELECT * FROM ENTREPRISE WHERE IdEntreprise = @IdEntreprise)
		DELETE FROM ENTREPRISE WHERE IdEntreprise = @IdEntreprise;
	else
	throw 50021,'L''Entreprise que l''on veut supprimer n''existe pas dans la table Entreprise', 1;
	END
