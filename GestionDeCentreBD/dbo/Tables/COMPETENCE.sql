create table COMPETENCE (     
	[IdFormateur] INT not null,     
	IdModule int not null,     
	constraint ID_COMPETENCE primary key (IdModule, [IdFormateur]));
GO
alter table COMPETENCE 
add constraint REF_COMPE_MODUL foreign key (IdModule) references MODULE;
GO
alter table COMPETENCE 
add constraint REF_COMPE_FORMA_FK foreign key ([IdFormateur]) references FORMATEUR;
GO
create unique index ID_COMPETENCE_IND on COMPETENCE (IdModule, [IdFormateur]);
GO
create index REF_COMPE_FORMA_IND on COMPETENCE ([IdFormateur]);