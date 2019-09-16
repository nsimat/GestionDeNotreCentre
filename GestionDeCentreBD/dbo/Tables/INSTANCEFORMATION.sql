create table INSTANCEFORMATION (     
	IdInstanceFormation int IDENTITY(1,1) not null,     
	Statut varchar(25) not null check ( Statut in ('in preparation', 'started', 'canceled', 'compeleted')),     
	DateDebut DateTime not null,     
	DateFin DateTime not null,     
	IdFormation int not null,     
	[IdPersonne] INT not null,     
	constraint ID_INSTANCEFORMATION primary key (IdInstanceFormation));
GO
--alter table FORMATION 
--add constraint ID_FORMATION_CHK check(exists(select * from COMPOSITION where COMPOSITION.IdFormation = IdFormation)); 
--GO


--alter table INSTANCEFORMATION 
--add constraint ID_INSTANCEFORMATION_CHK check(exists(select * from PLANIFICATION where PLANIFICATION.IdInstanceFormation = IdInstanceFormation));
--GO

 
alter table INSTANCEFORMATION 
add constraint REF_INSTA_FORMA_FK foreign key (IdFormation) references FORMATION;
GO
alter table INSTANCEFORMATION 
add constraint REF_INSTA_EMPLO_FK foreign key ([IdPersonne]) references EMPLOYE;
GO
create unique index ID_INSTANCEFORMATION_IND on INSTANCEFORMATION (IdInstanceFormation);
GO
create index REF_INSTA_FORMA_IND on INSTANCEFORMATION (IdFormation);
GO
create index REF_INSTA_EMPLO_IND on INSTANCEFORMATION ([IdPersonne]);
GO
--Trigger pour l'ajout de la date de fin dans la table INSTANCEFORMATION--
create trigger InstanceFormation_Update
on INSTANCEFORMATION
instead of UPDATE
as
begin
  declare @IdInstanceformation int,
          @DateDebut datetime,
		  @DateFin datetime;

  select @IdInstanceformation = IdInstanceFormation, @DateDebut = DateDebut, @DateFin = DateFin
  from inserted;

  if(@DateFin > @DateDebut)
    UPDATE INSTANCEFORMATION
	SET DateFin = @DateFin 
	WHERE IdInstanceFormation = @IdInstanceformation
   
   else
    throw 50027, 'La date de fin doit être supérieure à la date de début', 1;
end;