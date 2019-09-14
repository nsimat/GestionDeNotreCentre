create table TYPE_JOUR (     
	IdTypeJour int IDENTITY(1,1) not null,     
	TypeJour varchar(50) not null check( TypeJour in ('normal', 'selfstudy', 'labo')),     
	constraint ID_TYPE_JOUR primary key (IdTypeJour));
GO
create unique index ID_TYPE_JOUR_IND on TYPE_JOUR (IdTypeJour);