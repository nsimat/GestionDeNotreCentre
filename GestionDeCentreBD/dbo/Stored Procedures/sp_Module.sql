--Procédure stockée pour insérer un module dans la table MODULE--
create procedure sp_Module 
       @Nom varchar(50),
	   @Description varchar(50),
	   @TableDeMatieres varbinary(MAX),
	   @PrixJournalier money,
	   @NbreJours int,
	   @nbreJoursAffectes int 

as
 begin
  if not exists(SELECT * FROM MODULE WHERE Nom = @Nom) 
     insert into MODULE(Nom, DescriptionModule, TableDeMatieres, PrixJournalier, NombreJours, NbreJoursAffectes) 
	 values (@Nom, @Description, @TableDeMatieres, @PrixJournalier, @NbreJours, @nbreJoursAffectes);
  else
     throw 50021, 'Le module existe déjà dans la table MODULE', 1;
 end