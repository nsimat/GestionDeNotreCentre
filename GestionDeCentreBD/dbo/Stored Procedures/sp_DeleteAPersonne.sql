--Procédure stockée pour supprimer une personne de la table PERSONNE--

CREATE PROCEDURE [dbo].[sp_DeleteAPersonne]
	@IdPersonne INT
AS
  BEGIN
	if exists(SELECT * FROM PERSONNE WHERE IdPersonne = @IdPersonne) 
	   DELETE FROM PERSONNE WHERE IdPersonne = @IdPersonne;
	else
	 throw 50021, 'La personne que l''on veut supprimer n''existe pas dans la table Personne', 1;
  END
