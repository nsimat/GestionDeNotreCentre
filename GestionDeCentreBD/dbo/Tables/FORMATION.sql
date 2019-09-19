create table FORMATION (     
    IdFormation int IDENTITY(1,1) not null,     
	Nom varchar(50) not null unique,     
	constraint ID_FORMATION primary key (IdFormation));
GO
create unique index ID_FORMATION_IND on FORMATION (IdFormation);