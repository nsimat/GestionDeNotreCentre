create table MODULE (     
	IdModule int IDENTITY(1,1) not null,     
	Nom varchar(100) not null unique,     
	DescriptionModule varchar(MAX) not null,     
	TableDeMatieres varbinary(MAX) not null,     
	PrixJournalier money not null,     
	NombreJours int not null,     
	NbreJoursAffectes int not null,     
	constraint ID_MODULE primary key (IdModule));
GO
create unique index ID_MODULE_IND on MODULE (IdModule);