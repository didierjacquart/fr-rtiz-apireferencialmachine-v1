-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 13/04/2023
-- Base      : Sql Server - xbdm
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : mise à jour du code de Garantie en Tous Lieux (GTL) et 
--             et du coefficent correspondant
-------------------------------------------------------------------------
USE [mchn]

update [sch_mchn].[T_MACHINE_SPECIFICATION]  set ALL_PLACE_CORVERED='OPTIONAL' where code like '00046'
update [sch_mchn].[T_MACHINE_SPECIFICATION]  set ALL_PLACE_CORVERED='OPTIONAL' where code like '00071'
update [sch_mchn].[T_MACHINE_SPECIFICATION]  set ALL_PLACE_CORVERED='OPTIONAL' where code like '00076'
update [sch_mchn].[T_MACHINE_SPECIFICATION]  set ALL_PLACE_CORVERED='OPTIONAL' where code like '00079'
update [sch_mchn].[T_MACHINE_SPECIFICATION]  set ALL_PLACE_CORVERED='OPTIONAL' where code like '00103'
update [sch_mchn].[T_MACHINE_SPECIFICATION]  set ALL_PLACE_CORVERED='OPTIONAL' where code like '00109'
update [sch_mchn].[T_MACHINE_SPECIFICATION]  set ALL_PLACE_CORVERED='OPTIONAL' where code like '00114'
update [sch_mchn].[T_MACHINE_SPECIFICATION]  set ALL_PLACE_CORVERED='OPTIONAL' where code like '00182'
update [sch_mchn].[T_MACHINE_SPECIFICATION]  set ALL_PLACE_CORVERED='OPTIONAL' where code like '00184'
update [sch_mchn].[T_MACHINE_SPECIFICATION]  set ALL_PLACE_CORVERED='OPTIONAL' where code like '00186'
update [sch_mchn].[T_MACHINE_SPECIFICATION]  set ALL_PLACE_CORVERED='OPTIONAL' where code like '00187'
update [sch_mchn].[T_MACHINE_SPECIFICATION]  set ALL_PLACE_CORVERED='OPTIONAL' where code like '00188'
update [sch_mchn].[T_MACHINE_SPECIFICATION]  set ALL_PLACE_CORVERED='OPTIONAL' where code like '00211'
update [sch_mchn].[T_MACHINE_SPECIFICATION]  set ALL_PLACE_CORVERED='OPTIONAL' where code like '23013'
update [sch_mchn].[T_MACHINE_SPECIFICATION]  set ALL_PLACE_CORVERED='OPTIONAL' where code like '37004'
update [sch_mchn].[T_MACHINE_SPECIFICATION]  set ALL_PLACE_CORVERED='OPTIONAL' where code like '37007'
update [sch_mchn].[T_MACHINE_SPECIFICATION]  set ALL_PLACE_CORVERED='OPTIONAL' where code like '00212'

update [sch_mchn].[T_PRICING_RATE] set RATE = 1.5 where FK_MACHINE_ID in (select MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] where code like '00046') and CODE like 'GTL'
update [sch_mchn].[T_PRICING_RATE] set RATE = 1.5 where FK_MACHINE_ID in (select MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] where code like '00071') and CODE like 'GTL'
update [sch_mchn].[T_PRICING_RATE] set RATE = 1.5 where FK_MACHINE_ID in (select MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] where code like '00076') and CODE like 'GTL'
update [sch_mchn].[T_PRICING_RATE] set RATE = 1.5 where FK_MACHINE_ID in (select MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] where code like '00079') and CODE like 'GTL'
update [sch_mchn].[T_PRICING_RATE] set RATE = 1.5 where FK_MACHINE_ID in (select MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] where code like '00103') and CODE like 'GTL'
update [sch_mchn].[T_PRICING_RATE] set RATE = 1.5 where FK_MACHINE_ID in (select MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] where code like '00109') and CODE like 'GTL'
update [sch_mchn].[T_PRICING_RATE] set RATE = 1.5 where FK_MACHINE_ID in (select MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] where code like '00114') and CODE like 'GTL'
update [sch_mchn].[T_PRICING_RATE] set RATE = 1.5 where FK_MACHINE_ID in (select MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] where code like '00182') and CODE like 'GTL'
update [sch_mchn].[T_PRICING_RATE] set RATE = 1.5 where FK_MACHINE_ID in (select MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] where code like '00184') and CODE like 'GTL'
update [sch_mchn].[T_PRICING_RATE] set RATE = 1.5 where FK_MACHINE_ID in (select MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] where code like '00186') and CODE like 'GTL'
update [sch_mchn].[T_PRICING_RATE] set RATE = 1.5 where FK_MACHINE_ID in (select MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] where code like '00187') and CODE like 'GTL'
update [sch_mchn].[T_PRICING_RATE] set RATE = 1.5 where FK_MACHINE_ID in (select MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] where code like '00188') and CODE like 'GTL'
update [sch_mchn].[T_PRICING_RATE] set RATE = 1.5 where FK_MACHINE_ID in (select MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] where code like '00211') and CODE like 'GTL'
update [sch_mchn].[T_PRICING_RATE] set RATE = 1.5 where FK_MACHINE_ID in (select MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] where code like '23013') and CODE like 'GTL'
update [sch_mchn].[T_PRICING_RATE] set RATE = 1.5 where FK_MACHINE_ID in (select MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] where code like '37004') and CODE like 'GTL'
update [sch_mchn].[T_PRICING_RATE] set RATE = 1.5 where FK_MACHINE_ID in (select MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] where code like '37007') and CODE like 'GTL'
update [sch_mchn].[T_PRICING_RATE] set RATE = 1.5 where FK_MACHINE_ID in (select MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] where code like '00212') and CODE like 'GTL'




