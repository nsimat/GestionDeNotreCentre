create table PREREQUIS (     
	IdModulePrerequis int not null,     
	IdModule int not null,     
	constraint ID_PREREQUIS_ID primary key (IdModule, IdModulePrerequis));
GO
alter table PREREQUIS 
add constraint EQU_PRERE_MODUL foreign key (IdModule) references MODULE;
GO
alter table PREREQUIS 
add constraint REF_PRERE_MODUL_FK  foreign key (IdModulePrerequis) references MODULE;
GO
create unique index ID_PREREQUIS_IND on PREREQUIS (IdModule, IdModulePrerequis);
GO
create index REF_PRERE_MODUL_IND on PREREQUIS (IdModulePrerequis);