create table EMPLOYE (     
	NumeroRegistre varchar(25) unique not null,     
	constraint ID_EMPLO_PERSO_ID primary key (NumeroRegistre));
GO
alter table EMPLOYE 
add constraint ID_EMPLO_PERSO_FK foreign key (NumeroRegistre) references PERSONNE;
GO
create unique index ID_EMPLO_PERSO_IND on EMPLOYE (NumeroRegistre);