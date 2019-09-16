USE master
GO

/****** Object:  Database GestionDeCentre******/
IF DB_ID('GestionDeCentreBD') IS NOT NULL
	DROP DATABASE GestionDeCentreBD
GO

-- Database Section
-- ________________ 

create database GestionDeCentreBD;
GO

Use GestionDeCentreBD
GO

-- DBSpace Section
-- _______________

-- Tables Section
-- _____________ 

create table COMPOSITION (     
	IdFormation int not null,     
	IdModule int not null,     
	DateAjout DateTime not null,     
	DateSuppression DateTime,     
	constraint ID_COMPOSITION primary key (IdModule, IdFormation));
GO


create table EMPLOYE (     
	NumeroRegistre varchar(25) unique not null,     
	constraint ID_EMPLO_PERSO_ID primary key (NumeroRegistre));
GO


create table INSCRIPTION (     
	IdInscription int IDENTITY(1,1) not null,     
	DateInscription DateTime not null,     
	DateValidation DateTime,     
	IdInstanceFormation int not null,     
	NumeroRegistreStagiaire varchar(25) not null,     
	NumeroRegistreEmploye varchar(25) not null,     
	constraint ID_INSCRIPTION primary key (IdInscription));
GO


create table ENTREPRISE (     
	IdEntreprise int IDENTITY(1,1) not null,     
	NomEntreprise varchar(25) not null unique,     
	Rue varchar(25) not null,     
	Ville varchar(25) not null,     
	CodePostal varchar(25) not null,     
	Pays varchar(25) not null,
	NumeroTelephone varchar(25) not null,
	constraint ID_ENTREPRISE primary key (IdEntreprise));
GO


create table FORMATEUR (     
	NumeroRegistre varchar(25) unique not null,     
	constraint ID_FORMATEUR primary key (NumeroRegistre));
GO


create table FORMATION (     
    IdFormation int IDENTITY(1,1) not null,     
	Nom varchar(50) not null unique,     
	constraint ID_FORMATION primary key (IdFormation));
GO


create table INSTANCEFORMATION (     
	IdInstanceFormation int IDENTITY(1,1) not null,     
	Statut varchar(25) not null check ( Statut in ('in preparation', 'started', 'canceled', 'compeleted')),     
	DateDebut DateTime not null,     
	DateFin DateTime not null,     
	IdFormation int not null,     
	NumeroRegistre varchar(25) not null,     
	constraint ID_INSTANCEFORMATION primary key (IdInstanceFormation));
GO


create table MODULE (     
	IdModule int IDENTITY(1,1) not null,     
	Nom varchar(50) not null unique,     
	DescriptionModule varchar(50) not null,     
	TableDeMatieres varbinary(MAX) not null,     
	PrixJournalier money not null,     
	NombreJours int not null,     
	NbreJoursAffectes int not null,     
	constraint ID_MODULE primary key (IdModule));
GO


create table PERSONNE ( 
    IdPersonne int IDENTITY(1,1) not null,
	NumeroRegistre varchar(25) unique not null,     
	Nom varchar(50) not null,     
	Prenom varchar(50) not null,     
	Email varchar(25) not null,     
	Rue varchar(25) not null,     
	Ville varchar(25) not null,     
	CodePostal varchar(25) not null,     
	Pays varchar(25) not null, 
	NumeroTelephone varchar(25) not null,
	CV varbinary(MAX) not null,     
	UserLogin varchar(25) not null,     
	MotDePasse varchar(25) not null,     
	IdEntreprise int,     
	constraint ID_PERSONNE primary key (IdPersonne));
GO


create table PLANIFICATION (     
	IdPlanification int IDENTITY(1,1) not null,     
	DatePlanification date not null,     
	IdTypeJour int not null,     
	NumeroRegistre varchar(25) not null,     
	IdInstanceFormation int not null,     
	IdModule int not null,     
	constraint ID_PLANIFICATION primary key (IdPlanification));
GO


create table COMPETENCE (     
	NumeroRegistre varchar(25) not null,     
	IdModule int not null,     
	constraint ID_COMPETENCE primary key (IdModule, NumeroRegistre));
GO


create table PREREQUIS (     
	IdModulePrerequis int IDENTITY(1,1) not null,     
	IdModule int not null,     
	constraint ID_PREREQUIS_ID primary key (IdModule, IdModulePrerequis));
GO


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


create table TCHAT (     
	IdTchat int IDENTITY(1,1) not null,     
	MessageTchat varchar(100) not null,     
	DateDebut DateTime not null,     
	DateCloture DateTime,     
	NumeroRegistreEnvoyeur varchar(25) not null,     
	NumeroRegistreRecepteur varchar(25) not null,     
	constraint ID_TCHAT primary key (IdTchat));
GO


create table TYPE_JOUR (     
	IdTypeJour int IDENTITY(1,1) not null,     
	TypeJour varchar(50) not null check( TypeJour in ('normal', 'selfstudy', 'labo')),     
	constraint ID_TYPE_JOUR primary key (IdTypeJour));
GO


-- Constraints Section
-- ___________________ 

alter table COMPOSITION 
add constraint COMPO_MODULE_FK foreign key (IdModule) references MODULE;
GO


alter table COMPOSITION 
add constraint COMPO_FORMA_FK foreign key (IdFormation) references FORMATION;
GO


alter table EMPLOYE 
add constraint ID_EMPLO_PERSO_FK foreign key (NumeroRegistre) references PERSONNE;
GO


alter table INSCRIPTION 
add constraint REF_INSCR_INSTA_FK foreign key (IdInstanceFormation) references INSTANCEFORMATION;
GO


alter table INSCRIPTION 
add constraint REF_INSCR_PERSO_FK foreign key (NumeroRegistreStagiaire) references PERSONNE;
GO


alter table INSCRIPTION 
add constraint REF_INSCR_EMPLO_FK foreign key (NumeroRegistreEmploye) references EMPLOYE;
GO


alter table FORMATEUR 
add constraint ID_FORMA_PERSO_FK foreign key (NumeroRegistre) references PERSONNE;
GO


--alter table FORMATION 
--add constraint ID_FORMATION_CHK check(exists(select * from COMPOSITION where COMPOSITION.IdFormation = IdFormation)); 
--GO


--alter table INSTANCEFORMATION 
--add constraint ID_INSTANCEFORMATION_CHK check(exists(select * from PLANIFICATION where PLANIFICATION.IdInstanceFormation = IdInstanceFormation));
--GO

 
alter table INSTANCEFORMATION 
add constraint REF_INSTA_FORMA_FK foreign key (IdFormation) references FORMATION;
GO


alter table INSTANCEFORMATION 
add constraint REF_INSTA_EMPLO_FK foreign key (NumeroRegistre) references EMPLOYE;
GO


--alter table MODULE 
--add constraint ID_MODULE_CHK check(exists(select * from PREREQUIS where PREREQUIS.IdModule = IdModule)); 


alter table PERSONNE 
add constraint REF_PERSO_ENTRE_FK foreign key (IdEntreprise) references ENTREPRISE;
GO


alter table PLANIFICATION 
add constraint REF_PLANI_TYPE__FK foreign key (IdTypeJour) references TYPE_JOUR;
GO


alter table PLANIFICATION 
add constraint REF_PLANI_FORMA_FK foreign key (NumeroRegistre) references FORMATEUR;
GO


alter table PLANIFICATION 
add constraint EQU_PLANI_INSTA_FK foreign key (IdInstanceFormation) references INSTANCEFORMATION;
GO


alter table PLANIFICATION 
add constraint REF_PLANI_MODUL_FK foreign key (IdModule) references MODULE;
GO


alter table COMPETENCE 
add constraint REF_COMPE_MODUL foreign key (IdModule) references MODULE;
GO


alter table COMPETENCE 
add constraint REF_COMPE_FORMA_FK foreign key (NumeroRegistre) references FORMATEUR;
GO


alter table PREREQUIS 
add constraint EQU_PRERE_MODUL foreign key (IdModule) references MODULE;
GO


alter table PREREQUIS 
add constraint REF_PRERE_MODUL_FK  foreign key (IdModulePrerequis) references MODULE;
GO


alter table TACHE 
add constraint REF_TACHE_EMPLO_1_FK foreign key (NumeroRegistreCreateur) references EMPLOYE;
GO


alter table TACHE 
add constraint REF_TACHE_EMPLO_FK foreign key (NumeroRegistreRealisateur) references EMPLOYE;
GO


alter table TCHAT 
add constraint REF_TCHAT_EMPLO_1_FK foreign key (NumeroRegistreEnvoyeur) references EMPLOYE;
GO


alter table TCHAT 
add constraint REF_TCHAT_EMPLO_FK foreign key (NumeroRegistreRecepteur) references EMPLOYE;
GO


-- Index Section
-- _____________ 

create unique index ID_COMPOSITION_IND on COMPOSITION (IdModule, IdFormation);
GO


create index EQU_COMPO_FORMA_IND on COMPOSITION (IdFormation);
GO


create unique index ID_EMPLO_PERSO_IND on EMPLOYE (NumeroRegistre);
GO


create unique index ID_INSCRIPTION_IND on INSCRIPTION (IdInscription);
GO


create index REF_INSCR_INSTA_IND  on INSCRIPTION (IdInstanceFormation);
GO


create index REF_INSCR_PERSO_IND on INSCRIPTION (NumeroRegistreStagiaire);
GO


create index REF_INSCR_EMPLO_IND on INSCRIPTION (NumeroRegistreEmploye);
GO


create unique index ID_ENTREPRISE_IND on ENTREPRISE (IdEntreprise);
GO


create unique index ID_FORMA_PERSO_IND on FORMATEUR (NumeroRegistre);
GO


create unique index ID_FORMATION_IND on FORMATION (IdFormation);
GO


create unique index ID_INSTANCEFORMATION_IND on INSTANCEFORMATION (IdInstanceFormation);
GO


create index REF_INSTA_FORMA_IND on INSTANCEFORMATION (IdFormation);
GO


create index REF_INSTA_EMPLO_IND on INSTANCEFORMATION (NumeroRegistre);
GO


create unique index ID_MODULE_IND on MODULE (IdModule);
GO


create unique index ID_PERSONNE_IND on PERSONNE (NumeroRegistre);
GO


create index REF_PERSO_ENTRE_IND on PERSONNE (IdEntreprise);
GO


create unique index ID_PLANIFICATION_IND on PLANIFICATION (IdPlanification);
GO


create index REF_PLANI_TYPE_IND on PLANIFICATION (IdTypeJour);
GO


create index REF_PLANI_FORMA_IND on PLANIFICATION (NumeroRegistre);
GO


create index EQU_PLANI_INSTA_IND on PLANIFICATION (IdInstanceFormation);
GO


create index REF_PLANI_MODUL_IND on PLANIFICATION (IdModule);
GO


create unique index ID_COMPETENCE_IND on COMPETENCE (IdModule, NumeroRegistre);
GO


create index REF_COMPE_FORMA_IND on COMPETENCE (NumeroRegistre);
GO


create unique index ID_PREREQUIS_IND on PREREQUIS (IdModule, IdModulePrerequis);
GO


create index REF_PRERE_MODUL_IND on PREREQUIS (IdModulePrerequis);
GO


create unique index ID_TACHE_IND on TACHE (IdTache);
GO


create index REF_TACHE_EMPLO_1_IND on TACHE (NumeroRegistreCreateur);
GO


create index REF_TACHE_EMPLO_IND on TACHE (NumeroRegistreRealisateur);
GO


create unique index ID_TCHAT_IND on TCHAT (IdTchat);
GO


create index REF_TCHAT_EMPLO_1_IND on TCHAT (NumeroRegistreEnvoyeur);
GO


create index REF_TCHAT_EMPLO_IND on TCHAT (NumeroRegistreRecepteur);
GO


create unique index ID_TYPE_JOUR_IND on TYPE_JOUR (IdTypeJour);
GO


-- View Section
-- _____________ 

create view V_Employe
as
select p.NumeroRegistre, Nom, Prenom, Email, Rue, Ville, CodePostal, Pays, CV, UserLogin, MotDePasse, IdEntreprise 
from PERSONNE p JOIN EMPLOYE e on p.NumeroRegistre = e.NumeroRegistre;
GO

create view V_Formateur
as
select p.NumeroRegistre, Nom, Prenom, Email, Rue, Ville, CodePostal, Pays, CV, UserLogin, MotDePasse, IdEntreprise
from PERSONNE p join FORMATEUR f on p.NumeroRegistre = f.NumeroRegistre;
GO

create view V_Stagiaire 
as
select *
from PERSONNE 
where IdEntreprise is null;
GO

create view V_Professionel
as
select *
from PERSONNE 
where IdEntreprise is not null;
GO

-- Stored Procedures Section
-- _____________

--Procédure stockée pour insérer une personne dans la table PERSONNE--

create procedure sp_Personne 
       @NumeroRegistre varchar(25),
	   @Nom varchar(50),
	   @Prenom varchar(50),
	   @Email varchar(25),
	   @Rue varchar(25),
	   @Ville varchar(25),
	   @CodePostal varchar(25),
	   @Pays varchar(25),
	   @CV varbinary(MAX),
	   @UserLogin varchar(25),
	   @MotDePasse varchar(25),
	   @IdEntreprise int = null
as
  begin
    if not exists(SELECT * FROM PERSONNE WHERE NumeroRegistre = @NumeroRegistre) 
	   insert into PERSONNE(NumeroRegistre, Nom, Prenom, Email, Rue, Ville, CodePostal, Pays, CV, UserLogin, MotDePasse, IdEntreprise) 
	   values (@NumeroRegistre, @Nom, @Prenom, @Email, @Rue, @Ville, @CodePostal, @Pays, @CV, @UserLogin, @MotDePasse, @IdEntreprise);
	else 
	  throw 50021, 'Cette personne existe déjà dans la table PERSONNE', 1;
  end
GO

--Procédure stockée pour insérer une personne dans la table EMPLOYE--
create procedure sp_Employe 
       @NumeroRegistre varchar(25) 

as
  begin
    if not exists(SELECT * FROM EMPLOYE WHERE NumeroRegistre = @NumeroRegistre) and exists (SELECT * FROM PERSONNE WHERE NumeroRegistre = @NumeroRegistre) 
	   insert into EMPLOYE(NumeroRegistre) values (@NumeroRegistre);
	else
	   throw 50021, 'L''employé existe déjà dans la table EMPLOYE', 1; 
  end
GO

--Procédure stockée pour insérer une personne dans la table FORMATEUR--
create procedure sp_Formateur
       @NumeroRegistre varchar(25) 

as
  begin
    if not exists(SELECT * FROM FORMATEUR WHERE NumeroRegistre = @NumeroRegistre) and exists (SELECT * FROM PERSONNE WHERE NumeroRegistre = @NumeroRegistre) 
	   insert into FORMATEUR(NumeroRegistre) values (@NumeroRegistre);
	else
	   throw 50021, 'Le Formateur existe déjà dans la table EMPLOYE', 1; 
  end
GO

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
GO

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
GO

--Procédure stockée pour insérer une formation dans la table FORMATION--
create procedure sp_Formation 
       @Nom varchar(50) 

as
 begin
   if not exists(SELECT * FROM FORMATION WHERE Nom = @Nom)
      insert into FORMATION(Nom)
	  values (@Nom);
   else
     throw 50021, 'La formation existe déjà dans la table FORMATION', 1;
 end
GO

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
GO





-- Triggers Section
-- _____________

--Trigger pour l'ajout de la date de suppression dans la table COMPOSITION--
create trigger Composition_Update
on  COMPOSITION 
instead of  UPDATE
as
begin
    declare @DateAjout datetime,
	        @DateSuppression datetime,
			@IdComposition1 int,
			@IdComposition2 int;

	select @IdComposition1 = IdFormation, @IdComposition2 = IdModule, @DateAjout = DateAjout, @DateSuppression = DateSuppression
	from inserted;	 
	 
	if(@DateAjout < @DateSuppression)
	  Begin
	     Update COMPOSITION
		 set DateSuppression = @DateSuppression
		 where IdFormation = @IdComposition1 and IdModule = @IdComposition2;
	  End;
	 else
	  throw 50027, 'La date de suppression doit être supérieure à la date d''ajout.', 1;
end;
GO

--Trigger pour l'ajout de la date de fin dans la table INSTANCEFORMATION--
create trigger InstanceFormation_Update
on INSTANCEFORMATION
instead of UPDATE
as
begin
  declare @IdInstanceformation int,
          @DateDebut datetime,
		  @DateFin datetime;

  select @IdInstanceformation = IdInstanceFormation, @DateDebut = DateDebut, @DateFin = DateFin
  from inserted;

  if(@DateFin > @DateDebut)
    UPDATE INSTANCEFORMATION
	SET DateFin = @DateFin 
	WHERE IdInstanceFormation = @IdInstanceformation
   
   else
    throw 50027, 'La date de fin doit être supérieure à la date de début', 1;
end;
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
GO

--Trigger pour l'ajout de la date de fin dans la table TCHAT--
create trigger Tchat_Update
on TCHAT
instead of UPDATE
as
begin
 declare @IdTchat int,
         @DateDebut datetime,
		 @DateCloture datetime;

 select @IdTchat = IdTchat, @DateDebut = DateDebut, @DateCloture = DateCloture
 from inserted;

 if(@DateDebut < @DateCloture)
   UPDATE TCHAT
   SET DateDebut = @DateDebut
   WHERE IdTchat = @IdTchat;
 else
   THROW 50027, 'La date de cloture du tchat doit être supérieure à la date de début de tchat.', 1;
end;
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
GO