-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 08/12/2021
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : fix pricing
-------------------------------------------------------------------------


DELETE from [sch_mchn].[T_PRICING_RATE]  WHERE FK_MACHINE_ID IN (SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE IN ('00659','24042','29002','00046','27010'));

--00659
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('GTL',1,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='00659' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'))
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('INC',10,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='00659' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'))
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('VOL',05,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='00659' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'))
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('BDI',30,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='00659' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'))
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('ADE',55,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='00659' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'))

--24042
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('GTL',0,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='24042' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));	 	
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('INC',0,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='24042' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));	 	
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('VOL',0,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='24042' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));	 	
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('BDI',0,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='24042' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));	 	
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('ADE',0,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='24042' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));

--29002
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('GTL',0,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='29002' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('INC',0,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='29002' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('VOL',0,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='29002' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('BDI',0,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='29002' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('ADE',0,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='29002' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));

--00046
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('GTL',1,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='00046' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('INC',20,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='00046' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('VOL',05,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='00046' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('BDI',60,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='00046' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('ADE',15,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='00046' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));

--27010
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('GTL',1.5,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='27010' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('INC',20,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='27010' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('VOL',20,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='27010' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('BDI',10,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='27010' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('ADE',50,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='27010' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));

--24000
DELETE FROM [sch_mchn].[T_PRICING_RATE] WHERE FK_MACHINE_ID in (SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='24000');
DELETE FROM [sch_mchn].[T_FAMILYT_MACHINE_SPECIFICATION] WHERE [T_MACHINE_SPECIFICATIONMACHINE_ID] in (SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='24000');
DELETE FROM [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE like '24000';

INSERT INTO [sch_mchn].[T_MACHINE_SPECIFICATION] ([CODE],[NAME],[LABEL],[DESCRIPTION],[KEYWORDS],[SCORE],[IS_OUT_OF_MACHINE_INSURANCE],[IS_DELEGATED],[IS_UNREFERENCED],[IS_EXCLUDED],[AGE_LIMIT_ALLOWED],[START_DATETIME_SUBSCRIPTION_PERIOD],[END_DATETIME_SUBSCRIPTION_PERIOD],[MACHINE_RATE],[ALL_PLACE_CORVERED],[PRODUCT],[IS_TRANSPORTABLE]) VALUES  ('24000','Energie > Production','Energie > Production',null,null,'0',0,0,1,0,12,'2022/06/01',null,9.5,'OPTIONAL','INVENTAIRE',0)
INSERT INTO [sch_mchn].[T_FAMILYT_MACHINE_SPECIFICATION]  ([T_FAMILYFAMILY_ID] ,[T_MACHINE_SPECIFICATIONMACHINE_ID])    VALUES   ((SELECT FAMILY_ID FROM [sch_mchn].[T_FAMILY] WHERE CODE='100' AND SUB_CODE ='10'),(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='24000' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'))
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('GTL',1.5,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='24000' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('INC',20,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='24000' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('VOL',05,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='24000' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('BDI',60,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='24000' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('ADE',15,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='24000' AND START_DATETIME_SUBSCRIPTION_PERIOD='2022/06/01'))
