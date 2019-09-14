﻿create table TCHAT (     
	IdTchat int IDENTITY(1,1) not null,     
	MessageTchat varchar(100) not null,     
	DateDebut DateTime not null,     
	DateCloture DateTime,     
	NumeroRegistreEnvoyeur varchar(25) not null,     
	NumeroRegistreRecepteur varchar(25) not null,     
	constraint ID_TCHAT primary key (IdTchat));
GO
alter table TCHAT 
add constraint REF_TCHAT_EMPLO_1_FK foreign key (NumeroRegistreEnvoyeur) references EMPLOYE;
GO
alter table TCHAT 
add constraint REF_TCHAT_EMPLO_FK foreign key (NumeroRegistreRecepteur) references EMPLOYE;
GO
create unique index ID_TCHAT_IND on TCHAT (IdTchat);
GO
create index REF_TCHAT_EMPLO_1_IND on TCHAT (NumeroRegistreEnvoyeur);
GO
create index REF_TCHAT_EMPLO_IND on TCHAT (NumeroRegistreRecepteur);
GO
--Trigger pour l'ajout de la date de fin dans la table TCHAT--
create trigger Tchat_Update
on TCHAT
instead of UPDATE
as
begin
 declare @IdTchat int,
         @DateDebut datetime,
		 @DateCloture datetime;

 select @IdTchat = IdTchat, @DateDebut = DateDebut, @DateCloture = DateCloture
 from inserted;

 if(@DateDebut < @DateCloture)
   UPDATE TCHAT
   SET DateDebut = @DateDebut
   WHERE IdTchat = @IdTchat;
 else
   THROW 50027, 'La date de cloture du tchat doit être supérieure à la date de début de tchat.', 1;
end;