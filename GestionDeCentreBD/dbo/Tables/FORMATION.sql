create table FORMATION (     
    IdFormation int IDENTITY(1,1) not null,     
	Nom varchar(50) not null unique, 
	DescriptionFormation varchar(Max) not null,
	constraint ID_FORMATION primary key (IdFormation));
GO
create unique index ID_FORMATION_IND on FORMATION (IdFormation);