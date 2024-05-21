-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 06/10/2023
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : corrections libell�s er ajouts de nouvelles machines
-------------------------------------------------------------------------
USE [mchn]

update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]=N'Installation surimpos�e sur toiture agricole',[LABEL] =N'Installation surimpos�e' WHERE code = '80002';
update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]=N'Installation surimpos�e sur toiture',[LABEL] =N'Installation surimpos�e' WHERE code = '80003';
update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET EDITION_CLAUSE_CODES = 'VA01' WHERE code = '80004';

update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET SCORE = '1' WHERE code = '80008';
update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET SCORE = '2' WHERE code = '80007';
update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET SCORE = '3' WHERE code = '80001';
update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET SCORE = '4' WHERE code = '80009';
update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET SCORE = '5' WHERE code = '80002';
update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET SCORE = '6' WHERE code = '80010';
update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET SCORE = '7' WHERE code = '80003';
update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET SCORE = '8' WHERE code = '80011';
update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET SCORE = '9' WHERE code = '80004';
update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET SCORE = '10' WHERE code = '80005';
update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET SCORE = '11' WHERE code = '80006';

DELETE [sch_mchn].[T_MACHINE_SPECIFICATION] where CODE in ('80010', '80011');
INSERT INTO [sch_mchn].[T_MACHINE_SPECIFICATION]([CODE],[NAME],[LABEL],[SCORE],[IS_OUT_OF_MACHINE_INSURANCE],[IS_DELEGATED],[IS_UNREFERENCED],[IS_EXCLUDED],[START_DATETIME_SUBSCRIPTION_PERIOD],[END_DATETIME_SUBSCRIPTION_PERIOD],[MACHINE_RATE],[ALL_PLACE_CORVERED],[PRODUCT],[DEDUCTIBLE_MIN_VALUE],[DEDUCTIBLE_MIN_INCLUDE],[DEDUCTIBLE_MAX_VALUE],[DEDUCTIBLE_MAX_INCLUDE],[DEDUCTIBLE_UNIT],[AGE_LIMIT_ALLOWED],[EDITION_CLAUSE_CODES]) VALUES ('80010',N'Installation int�gr�e sur toiture agricole',N'Installation int�gr�e','6',0,2,0,0,'01/01/2023',null,5.2,'POINTLESS','PHOTOVOLTAIC',500,1,5000,1,'EUR',12,'DECLA007|DECLA008|DECLA009');
INSERT INTO [sch_mchn].[T_MACHINE_SPECIFICATION]([CODE],[NAME],[LABEL],[SCORE],[IS_OUT_OF_MACHINE_INSURANCE],[IS_DELEGATED],[IS_UNREFERENCED],[IS_EXCLUDED],[START_DATETIME_SUBSCRIPTION_PERIOD],[END_DATETIME_SUBSCRIPTION_PERIOD],[MACHINE_RATE],[ALL_PLACE_CORVERED],[PRODUCT],[DEDUCTIBLE_MIN_VALUE],[DEDUCTIBLE_MIN_INCLUDE],[DEDUCTIBLE_MAX_VALUE],[DEDUCTIBLE_MAX_INCLUDE],[DEDUCTIBLE_UNIT],[AGE_LIMIT_ALLOWED],[EDITION_CLAUSE_CODES]) VALUES ('80011',N'Installation int�gr�e sur toiture',N'Installation int�gr�e','8',0,3,0,0,'01/01/2023',null,5.2,'POINTLESS','PHOTOVOLTAIC',500,1,5000,1,'EUR',12,'DECLA001|DECLA002|DECLA003|DECLA004|DECLA005|DECLA006');


DELETE [sch_mchn].[T_PRICING_RATE]  where FK_MACHINE_ID in (SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE in ('80010', '80011'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('GTL',1,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='80010' AND START_DATETIME_SUBSCRIPTION_PERIOD='01/01/2023'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('GTL',1,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='80011' AND START_DATETIME_SUBSCRIPTION_PERIOD='01/01/2023'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('VOL',25,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='80010' AND START_DATETIME_SUBSCRIPTION_PERIOD='01/01/2023'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('VOL',25,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='80011' AND START_DATETIME_SUBSCRIPTION_PERIOD='01/01/2023'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('BDI',10,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='80010' AND START_DATETIME_SUBSCRIPTION_PERIOD='01/01/2023'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('BDI',10,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='80011' AND START_DATETIME_SUBSCRIPTION_PERIOD='01/01/2023'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('ADE',15,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='80010' AND START_DATETIME_SUBSCRIPTION_PERIOD='01/01/2023'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('ADE',15,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='80011' AND START_DATETIME_SUBSCRIPTION_PERIOD='01/01/2023'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('INC',50,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='80010' AND START_DATETIME_SUBSCRIPTION_PERIOD='01/01/2023'));
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('INC',50,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='80011' AND START_DATETIME_SUBSCRIPTION_PERIOD='01/01/2023'));

DELETE FROM [sch_mchn].[T_EDITION_CLAUSE] WHERE CODE = 'VA01';
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [LABEL], [DESCRIPTION]) VALUES ('VA01',N'Garantie applicable aux installations photovolta�ques sur ombri�re', N'Conform�ment aux Conditions g�n�rales font partie des �quipements garantis l�ensemble des mat�riels participant � la production d��lectricit� d�origine photovolta�que.

La structure porteuse et la charpente, support des panneaux photovolta�ques, sont �galement garanties � condition que leurs valeurs soient comprises dans les capitaux d�clar�s.');