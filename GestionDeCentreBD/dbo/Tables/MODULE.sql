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
create unique index ID_MODULE_IND on MODULE (IdModule);