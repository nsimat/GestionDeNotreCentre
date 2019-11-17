CREATE PROCEDURE [dbo].[sp_UpdateAModule]
	   @IdModule INT,
	   @Nom VARCHAR(100),
	   @Description VARCHAR(MAX),
	   @TableDeMatieres VARBINARY(MAX),
	   @PrixJournalier MONEY,
	   @NombreJours INT,
	   @NbreJoursAffectes INT 
AS
	BEGIN
	IF EXISTS (SELECT * FROM MODULE WHERE IdModule= @IdModule)
		UPDATE MODULE
			SET			  
			  Nom = @Nom,
			  DescriptionModule = @Description,
			  TableDeMatieres = @TableDeMatieres,
			  PrixJournalier = @PrixJournalier,
			  NombreJours = NombreJours,
			  NbreJoursAffectes = @NbreJoursAffectes
			WHERE IdModule = @IdModule
   ELSE
	  THROW 50021, 'Le module que vous voulez mettre à jour n''existe pas dans la table Module', 1;
	END