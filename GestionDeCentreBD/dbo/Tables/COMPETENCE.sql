create table COMPETENCE (     
	NumeroRegistre varchar(25) not null,     
	IdModule int not null,     
	constraint ID_COMPETENCE primary key (IdModule, NumeroRegistre));
GO
alter table COMPETENCE 
add constraint REF_COMPE_MODUL foreign key (IdModule) references MODULE;
GO
alter table COMPETENCE 
add constraint REF_COMPE_FORMA_FK foreign key (NumeroRegistre) references FORMATEUR;
GO
create unique index ID_COMPETENCE_IND on COMPETENCE (IdModule, NumeroRegistre);
GO
create index REF_COMPE_FORMA_IND on COMPETENCE (NumeroRegistre);