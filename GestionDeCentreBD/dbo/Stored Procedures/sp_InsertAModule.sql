CREATE PROCEDURE [dbo].[sp_InsertAModule]
	   @Nom VARCHAR(50),
	   @Description VARCHAR(50),
	   @TableDeMatieres VARBINARY(MAX),
	   @PrixJournalier MONEY,
	   @NbreJours INT,
	   @nbreJoursAffectes INT 

AS
 BEGIN
  IF NOT EXISTS(SELECT * FROM MODULE WHERE Nom = @Nom) 
     INSERT INTO MODULE(Nom, DescriptionModule, TableDeMatieres, PrixJournalier, NombreJours, NbreJoursAffectes) 
	 VALUES (@Nom, @Description, @TableDeMatieres, @PrixJournalier, @NbreJours, @nbreJoursAffectes);
  ELSE
     THROW 50021, 'Le module existe déjà dans la table MODULE', 1;
 END