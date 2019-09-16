create table FORMATEUR (     
	IdFormateur int unique not null,     
	constraint ID_FORMATEUR primary key (IdFormateur));
GO
alter table FORMATEUR 
add constraint ID_FORMA_PERSO_FK foreign key (IdFormateur) references PERSONNE;
GO
create unique index ID_FORMA_PERSO_IND on FORMATEUR (IdFormateur);