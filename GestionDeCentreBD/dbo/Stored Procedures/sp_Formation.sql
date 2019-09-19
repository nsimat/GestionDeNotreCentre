--Procédure stockée pour insérer une formation dans la table FORMATION--
create procedure sp_Formation 
       @Nom varchar(50), 
	   @DescriptionFormation varchar(Max)

as
 begin
   if not exists(SELECT * FROM FORMATION WHERE Nom = @Nom)
      insert into FORMATION(Nom, DescriptionFormation)
	  values (@Nom, @DescriptionFormation);
   else
     throw 50021, 'La formation existe déjà dans la table FORMATION', 1;
 end