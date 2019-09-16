create table PLANIFICATION (     
	IdPlanification int IDENTITY(1,1) not null,     
	DatePlanification date not null,     
	IdTypeJour int not null,     
	[IdFormateur] INT not null,     
	IdInstanceFormation int not null,     
	IdModule int not null,     
	constraint ID_PLANIFICATION primary key (IdPlanification));
GO
alter table PLANIFICATION 
add constraint REF_PLANI_TYPE__FK foreign key (IdTypeJour) references TYPE_JOUR;
GO
alter table PLANIFICATION 
add constraint REF_PLANI_FORMA_FK foreign key ([IdFormateur]) references FORMATEUR;
GO
alter table PLANIFICATION 
add constraint EQU_PLANI_INSTA_FK foreign key (IdInstanceFormation) references INSTANCEFORMATION;
GO
alter table PLANIFICATION 
add constraint REF_PLANI_MODUL_FK foreign key (IdModule) references MODULE;
GO
create unique index ID_PLANIFICATION_IND on PLANIFICATION (IdPlanification);
GO
create index REF_PLANI_TYPE_IND on PLANIFICATION (IdTypeJour);
GO
create index REF_PLANI_FORMA_IND on PLANIFICATION ([IdFormateur]);
GO
create index EQU_PLANI_INSTA_IND on PLANIFICATION (IdInstanceFormation);
GO
create index REF_PLANI_MODUL_IND on PLANIFICATION (IdModule);