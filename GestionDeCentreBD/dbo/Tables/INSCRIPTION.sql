create table INSCRIPTION (     
	IdInscription int IDENTITY(1,1) not null,     
	DateInscription DateTime not null,     
	DateValidation DateTime,     
	IdInstanceFormation int not null,     
	[IdStagiaire] INT not null,     
	[IdEmploye] INT not null,     
	constraint ID_INSCRIPTION primary key (IdInscription));
GO
alter table INSCRIPTION 
add constraint REF_INSCR_INSTA_FK foreign key (IdInstanceFormation) references INSTANCEFORMATION;
GO
alter table INSCRIPTION 
add constraint REF_INSCR_PERSO_FK foreign key ([IdStagiaire]) references PERSONNE;
GO
alter table INSCRIPTION 
add constraint REF_INSCR_EMPLO_FK foreign key ([IdEmploye]) references EMPLOYE;
GO
create unique index ID_INSCRIPTION_IND on INSCRIPTION (IdInscription);
GO
create index REF_INSCR_INSTA_IND  on INSCRIPTION (IdInstanceFormation);
GO
create index REF_INSCR_PERSO_IND on INSCRIPTION ([IdStagiaire]);
GO
create index REF_INSCR_EMPLO_IND on INSCRIPTION ([IdEmploye]);
GO
--Trigger pour l'ajout de la date de fin dans la table INSCRIPTION--
create trigger Inscription_Update
on INSCRIPTION 
instead of UPDATE
as
begin
 declare @IdInscription int,
         @DateInscription datetime,
		 @DateValidation datetime;

 select @IdInscription = IdInscription, @DateInscription = DateInscription, @DateValidation = DateValidation
 from inserted;

 if(@DateInscription < @DateValidation)
   UPDATE INSCRIPTION
   SET DateValidation = @DateValidation
   WHERE IdInscription = @IdInscription;
 else
   THROW 50027, 'La date de validation doit être supérieure à la date d''inscription', 1;
end;