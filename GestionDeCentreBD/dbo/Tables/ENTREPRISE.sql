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
create unique index ID_ENTREPRISE_IND on ENTREPRISE (IdEntreprise);