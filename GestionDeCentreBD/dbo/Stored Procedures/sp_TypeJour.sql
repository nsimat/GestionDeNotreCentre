--Procédure stockée pour insérer un type de jour dans la table TYPE_JOUR--
create procedure sp_TypeJour 
       @TypeJour varchar(50)

as 
 begin
  if not exists(SELECT * FROM TYPE_JOUR WHERE TypeJour = @TypeJour) 
     insert into TYPE_JOUR(TypeJour) 
	 values (@TypeJour);
  else
     throw 50021, 'Ce type de jour existe déjà dans la table TYPE_JOUR.', 1;
 end