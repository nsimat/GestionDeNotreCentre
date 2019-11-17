CREATE PROCEDURE [dbo].[sp_InsertAModule]
	   @Nom VARCHAR(100),
	   @Description VARCHAR(MAX),
	   @TableDeMatieres VARBINARY(MAX),
	   @PrixJournalier MONEY,
	   @NombreJours INT,
	   @NbreJoursAffectes INT 

AS
 BEGIN
  IF NOT EXISTS(SELECT * FROM MODULE WHERE Nom = @Nom) 
     INSERT INTO MODULE(Nom, DescriptionModule, TableDeMatieres, PrixJournalier, NombreJours, NbreJoursAffectes) 
	 VALUES (@Nom, @Description, @TableDeMatieres, @PrixJournalier, @NombreJours, @NbreJoursAffectes);
  ELSE
     THROW 50021, 'Le module existe déjà dans la table MODULE', 1;
 END