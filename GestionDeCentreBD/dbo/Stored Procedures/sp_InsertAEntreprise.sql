CREATE PROCEDURE [dbo].[sp_InsertAEntreprise]
	   @NomEntreprise VARCHAR(25),
	   @Rue VARCHAR(25),
	   @Ville VARCHAR(25),
	   @CodePostal VARCHAR(25),
	   @Pays VARCHAR(25),
	   @NumeroTelephone VARCHAR(25)

AS
 BEGIN
 
   IF NOT EXISTS(SELECT * FROM ENTREPRISE WHERE  NomEntreprise = @NomEntreprise) 
      INSERT INTO ENTREPRISE(NomEntreprise, Rue, Ville, CodePostal, Pays, NumeroTelephone) 
	 VALUES (@NomEntreprise, @Rue, @Ville, @CodePostal, @Pays, @NumeroTelephone);
  ELSE
      THROW 50021, 'Le nom de l''entreprise existe déjà dans la table ENTREPRISE', 1;
 END