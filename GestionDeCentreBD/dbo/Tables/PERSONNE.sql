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
--alter table MODULE 
--add constraint ID_MODULE_CHK check(exists(select * from PREREQUIS where PREREQUIS.IdModule = IdModule)); 


alter table PERSONNE 
add constraint REF_PERSO_ENTRE_FK foreign key (IdEntreprise) references ENTREPRISE;
GO
create unique index ID_PERSONNE_IND on PERSONNE (NumeroRegistre);
GO
create index REF_PERSO_ENTRE_IND on PERSONNE (IdEntreprise);