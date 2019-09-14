--Procédure stockée pour insérer une entreprise dans la table ENTREPRISE--
create procedure sp_Entreprise 
       @NomEntreprise varchar(25),
	   @Rue varchar(25),
	   @Ville varchar(25),
	   @CodePostal varchar(25),
	   @Pays varchar(25) 

as
 begin
   if not exists(SELECT * FROM ENTREPRISE WHERE  NomEntreprise = @NomEntreprise) 
      insert into ENTREPRISE(NomEntreprise, Rue, Ville, CodePostal, Pays) 
	  values (@NomEntreprise, @Rue, @Ville, @CodePostal, @Pays);
   else
      throw 50021, 'Le nom de l''entreprise existe déjà dans la table ENTREPRISE', 1;
 end