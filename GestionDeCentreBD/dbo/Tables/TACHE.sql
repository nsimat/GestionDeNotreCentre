create table TACHE (     
	IdTache int IDENTITY(1,1) not null,     
	LibelleTache varchar(20) not null,     
	MessageTache varchar(100) not null,     
	DateCreation DateTime not null,     
	DateCloture DateTime,     
	NumeroRegistreCreateur varchar(25) not null,     
	NumeroRegistreRealisateur varchar(25) not null,     
	constraint ID_TACHE primary key (IdTache));
GO
alter table TACHE 
add constraint REF_TACHE_EMPLO_1_FK foreign key (NumeroRegistreCreateur) references EMPLOYE;
GO
alter table TACHE 
add constraint REF_TACHE_EMPLO_FK foreign key (NumeroRegistreRealisateur) references EMPLOYE;
GO
create unique index ID_TACHE_IND on TACHE (IdTache);
GO
create index REF_TACHE_EMPLO_1_IND on TACHE (NumeroRegistreCreateur);
GO
create index REF_TACHE_EMPLO_IND on TACHE (NumeroRegistreRealisateur);
GO
--Trigger pour l'ajout de la date de fin dans la table TACHE--
create trigger Tache_Update
on TACHE
instead of UPDATE
as
begin
 declare @IdTache int,
         @DateCreation datetime,
		 @DateCloture datetime;

 select @IdTache = IdTache, @DateCreation = DateCreation, @DateCloture = DateCloture
 from inserted;

 if(@DateCreation < @DateCloture)
   UPDATE TACHE
   SET DateCreation = @DateCreation
   WHERE IdTache = @IdTache;
 else
   THROW 50027, 'La date de clôture de la tâche doit être supérieure à la date de création de sa création.', 1;
end;