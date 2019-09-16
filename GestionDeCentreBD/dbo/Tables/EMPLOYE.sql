create table EMPLOYE (     
	IdEmploye int unique not null,     
	constraint ID_EMPLO_PERSO_ID primary key (IdEmploye));
GO
alter table EMPLOYE 
add constraint ID_EMPLO_PERSO_FK foreign key (IdEmploye) references PERSONNE;
GO
create unique index ID_EMPLO_PERSO_IND on EMPLOYE (IdEmploye);