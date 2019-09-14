-- DBSpace Section
-- _______________

-- Tables Section
-- _____________ 

create table COMPOSITION (     
	IdFormation int not null,     
	IdModule int not null,     
	DateAjout DateTime not null,     
	DateSuppression DateTime,     
	constraint ID_COMPOSITION primary key (IdModule, IdFormation));
GO
-- Constraints Section
-- ___________________ 

alter table COMPOSITION 
add constraint COMPO_MODULE_FK foreign key (IdModule) references MODULE;
GO
alter table COMPOSITION 
add constraint COMPO_FORMA_FK foreign key (IdFormation) references FORMATION;
GO
-- Index Section
-- _____________ 

create unique index ID_COMPOSITION_IND on COMPOSITION (IdModule, IdFormation);
GO
create index EQU_COMPO_FORMA_IND on COMPOSITION (IdFormation);
GO
-- Triggers Section
-- _____________

--Trigger pour l'ajout de la date de suppression dans la table COMPOSITION--
create trigger Composition_Update
on  COMPOSITION 
instead of  UPDATE
as
begin
    declare @DateAjout datetime,
	        @DateSuppression datetime,
			@IdComposition1 int,
			@IdComposition2 int;

	select @IdComposition1 = IdFormation, @IdComposition2 = IdModule, @DateAjout = DateAjout, @DateSuppression = DateSuppression
	from inserted;	 
	 
	if(@DateAjout < @DateSuppression)
	  Begin
	     Update COMPOSITION
		 set DateSuppression = @DateSuppression
		 where IdFormation = @IdComposition1 and IdModule = @IdComposition2;
	  End;
	 else
	  throw 50027, 'La date de suppression doit être supérieure à la date d''ajout.', 1;
end;