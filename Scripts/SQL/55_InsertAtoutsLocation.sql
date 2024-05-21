-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 12/03/2024
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : Insert Atoutsloaction
-------------------------------------------------------------------------
USE [mchn] 

DELETE [sch_mchn].[T_PRICING_RATE] WHERE FK_MACHINE_ID IN (SELECT MACHINE_ID FROM [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE IN ('85001', '85002', '85003'));
DELETE [sch_mchn].[T_RISK_PRECISION] WHERE MACHINE_CODE IN ('85001', '85002', '85003');
DELETE [sch_mchn].[T_MACHINE_SPECIFICATION]  WHERE CODE IN ('85001', '85002', '85003');

INSERT INTO [sch_mchn].[T_MACHINE_SPECIFICATION]([CODE],[NAME],[LABEL],[SCORE],[IS_OUT_OF_MACHINE_INSURANCE],[IS_DELEGATED],[IS_UNREFERENCED],[IS_EXCLUDED],[START_DATETIME_SUBSCRIPTION_PERIOD],[MACHINE_RATE],[ALL_PLACE_CORVERED],[PRODUCT],[IS_TRANSPORTABLE],[DEDUCTIBLE_MIN_VALUE],[DEDUCTIBLE_MIN_INCLUDE],[DEDUCTIBLE_MAX_VALUE],[DEDUCTIBLE_MAX_INCLUDE],[DEDUCTIBLE_UNIT],[FINANCIAL_VALUATION_DELEGATED_MAX],[EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE], [IS_FIRE_COVERAGE_MANDATORY],[FINANCIAL_VALUATION_MACHINE_MAX],[FINANCIAL_VALUATION_MACHINE_DELEGATED_MAX]) VALUES ('85001',N'BTP - Engins, mat�riels de chantier',N'BTP - Engins, mat�riels de chantier','3',0,1,0,0,'2024/03/06',0,'INCLUDED','RENTAL_MACHINE',1,1000,1,4000,1,'EUR',500000,0,0,500000,2000000);
INSERT INTO [sch_mchn].[T_MACHINE_SPECIFICATION]([CODE],[NAME],[LABEL],[SCORE],[IS_OUT_OF_MACHINE_INSURANCE],[IS_DELEGATED],[IS_UNREFERENCED],[IS_EXCLUDED],[START_DATETIME_SUBSCRIPTION_PERIOD],[MACHINE_RATE],[ALL_PLACE_CORVERED],[PRODUCT],[IS_TRANSPORTABLE],[DEDUCTIBLE_MIN_VALUE],[DEDUCTIBLE_MIN_INCLUDE],[DEDUCTIBLE_MAX_VALUE],[DEDUCTIBLE_MAX_INCLUDE],[DEDUCTIBLE_UNIT],[FINANCIAL_VALUATION_DELEGATED_MAX],[EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE], [IS_FIRE_COVERAGE_MANDATORY],[FINANCIAL_VALUATION_MACHINE_MAX],[FINANCIAL_VALUATION_MACHINE_DELEGATED_MAX]) VALUES ('85002',N'M�dia, Audiovisuel',N'M�dia, Audiovisuel','2',0,1,0,0,'2024/03/06',0,'INCLUDED','RENTAL_MACHINE',1,1000,1,4000,1,'EUR',200000,0,0,200000,1000000);
INSERT INTO [sch_mchn].[T_MACHINE_SPECIFICATION]([CODE],[NAME],[LABEL],[SCORE],[IS_OUT_OF_MACHINE_INSURANCE],[IS_DELEGATED],[IS_UNREFERENCED],[IS_EXCLUDED],[START_DATETIME_SUBSCRIPTION_PERIOD],[MACHINE_RATE],[ALL_PLACE_CORVERED],[PRODUCT],[IS_TRANSPORTABLE],[DEDUCTIBLE_MIN_VALUE],[DEDUCTIBLE_MIN_INCLUDE],[DEDUCTIBLE_MAX_VALUE],[DEDUCTIBLE_MAX_INCLUDE],[DEDUCTIBLE_UNIT],[FINANCIAL_VALUATION_DELEGATED_MAX],[EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE], [IS_FIRE_COVERAGE_MANDATORY],[FINANCIAL_VALUATION_MACHINE_MAX],[FINANCIAL_VALUATION_MACHINE_DELEGATED_MAX]) VALUES ('85003',N'Autre famille de mat�riel',null,'1',0,1,1,0,'2024/03/06',0,'INCLUDED','RENTAL_MACHINE',1,1000,1,4000,1,'EUR',200000,0,0,200000,1000000);

INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('GTL',1,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='85001' AND START_DATETIME_SUBSCRIPTION_PERIOD='2024/03/06'))
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('GTL',1,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='85002' AND START_DATETIME_SUBSCRIPTION_PERIOD='2024/03/06'))
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('GTL',1,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='85003' AND START_DATETIME_SUBSCRIPTION_PERIOD='2024/03/06'))
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('INC',10,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='85001' AND START_DATETIME_SUBSCRIPTION_PERIOD='2024/03/06'))
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('INC',20,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='85002' AND START_DATETIME_SUBSCRIPTION_PERIOD='2024/03/06'))
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('INC',10,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='85003' AND START_DATETIME_SUBSCRIPTION_PERIOD='2024/03/06'))
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('VOL',05,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='85001' AND START_DATETIME_SUBSCRIPTION_PERIOD='2024/03/06'))
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('VOL',20,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='85002' AND START_DATETIME_SUBSCRIPTION_PERIOD='2024/03/06'))
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('VOL',05,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='85003' AND START_DATETIME_SUBSCRIPTION_PERIOD='2024/03/06'))
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('BDI',30,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='85001' AND START_DATETIME_SUBSCRIPTION_PERIOD='2024/03/06'))
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('BDI',10,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='85002' AND START_DATETIME_SUBSCRIPTION_PERIOD='2024/03/06'))
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('BDI',30,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='85003' AND START_DATETIME_SUBSCRIPTION_PERIOD='2024/03/06'))
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('ADE',55,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='85001' AND START_DATETIME_SUBSCRIPTION_PERIOD='2024/03/06'))
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('ADE',50,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='85002' AND START_DATETIME_SUBSCRIPTION_PERIOD='2024/03/06'))
INSERT INTO [sch_mchn].[T_PRICING_RATE] ([CODE],[RATE],[FK_MACHINE_ID]) VALUES ('ADE',55,(SELECT MACHINE_ID from [sch_mchn].[T_MACHINE_SPECIFICATION] WHERE CODE ='85003' AND START_DATETIME_SUBSCRIPTION_PERIOD='2024/03/06'))

UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [EDITION_CLAUSE_CODES]='LA01|LA26|LA36|LA90|LA92' WHERE CODE='85001';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [EDITION_CLAUSE_CODES]='LA02|LA14|LA10' WHERE CODE='85002';

INSERT INTO [sch_mchn].[T_RISK_PRECISION]([MACHINE_CODE],[CODE],[LABEL],[DETAIL]) VALUES ('85001','DOMAINS',N'Domaines:',N'Terrassement et chargement, Levage et manutention, Routes et voirie, �');
INSERT INTO [sch_mchn].[T_RISK_PRECISION]([MACHINE_CODE],[CODE],[LABEL],[DETAIL]) VALUES ('85002','DOMAINS',N'Domaines:',N'Audio, Sono, Photo, Vid�o');
INSERT INTO [sch_mchn].[T_RISK_PRECISION]([MACHINE_CODE],[CODE],[LABEL],[DETAIL]) VALUES ('85001','EXAMPLES',N'Exemple de machines:',N'Minipelle, Chariot �l�vateur de chantier, Chargeur, Grue, Machine � projeter�');
INSERT INTO [sch_mchn].[T_RISK_PRECISION]([MACHINE_CODE],[CODE],[LABEL],[DETAIL]) VALUES ('85002','EXAMPLES',N'Exemple de machines:',N'Appareil photo, Cam�ra, Panneau d�affichage lumineux, Amplificateur audio, Vid�oprojecteur, Borne de d�veloppement photo, Table de mixage, Station de montage, �');
INSERT INTO [sch_mchn].[T_RISK_PRECISION]([MACHINE_CODE],[CODE],[LABEL],[DETAIL]) VALUES ('85001','EXCLUSIONS',N'Attention, machines exclues � la souscription:',N'V�hicule utilitaire, Echafaudage, Cabane de chantier, Topographie, Banche');
INSERT INTO [sch_mchn].[T_RISK_PRECISION]([MACHINE_CODE],[CODE],[LABEL],[DETAIL]) VALUES ('85003','EXCLUSIONS',N'A noter, machines exclues � la souscription:',N'Nouveaux V�hicules Electriques Individuels (V�los et Trottinettes �lectriques, draisiennes �lectriques, Gyropodes, Segway, Hoverboards... ), �quipements en sous-sol, t�l�phones portables');


DELETE FROM [sch_mchn].[T_EDITION_CLAUSE] WHERE CODE IN ('LA01','LA26','LA36','LA90','LA92', 'LA02','LA14','LA10');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('LA01',N'Biens exclus', 
N'Sont exclus pour l�ensemble des garantis les biens suivants :
- les v�hicules utilitaires, de soci�t�, de fonction, 
- les v�hicules destin�s au transport de personnes et de marchandises,
- les �chafaudages, les banches, les coffrages, 
- Les cabanes de chantier, bases de vie, abris sanitaires, conteneurs de stockage,
- Le mat�riel de topographie,
- Les biens utilis�s par les ferrailleurs.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('LA26',N'Garantie des moteurs de traction des v�hicules et engins automoteurs limit�e aux �v�nements externes', 
N'Par d�rogation au paragraphe � Dommages garantis � du Chapitre � La Garantie Dommages aux biens � des Conditions g�n�rales, la garantie des moteurs de traction des engins automoteurs est limit�e � tout dommage mat�riel r�sultant :
- d�un accident de circulation, d�une collision, d�un choc contre un corps fixe ou mobile, 
- d�un renversement, d�un effondrement, d�un affaissement de terrain, 
- d�une chute � l�eau, d�un contact avec des fum�es, liquide ou gaz,
- d�un incendie, d�une explosion, 
- d�un vol ou d�un vandalisme, 
- d�un �v�nement naturel, 
- d�un attentat ou actes de terrorisme.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('LA36',N'Franchise applicable en cas de non utilisation des dispositifs de s�curit�', 
N'Lorsque le sinistre r�sulte ou est aggrav� par la mise hors service ou la non utilisation de dispositifs de s�curit� �quipant les engins, et que les conditions d�exploitation des engins n�cessitent l�utilisation de dispositifs de s�curit�, la franchise sera port�e � : 3 fois la franchise de base d�finie ci-avant dans le � Tableau des garanties, montants et franchises �.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('LA90',N'Cas particuliers  des engins et mat�riels de valeurs de remplacement � neuf sup�rieures � 25 000 �', N'Par d�rogation � l�exclusion 1 du Chapitre � Exclusions g�n�rales � des Conditions g�n�rales, pour les engins et mat�riels d�une valeur de remplacement � neuf au jour de la souscription sup�rieure � 25000 euros, la garantie vol est �tendue au vol sans effraction ou violence.
Franchise vol  pour l�ensemble des engins et mat�riels : la franchise en cas de vol sera port�e � 3 fois la franchise de base d�finie ci-avant dans le � Tableau des garanties, montants et franchises �.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('LA92',N'Garantie vol : conditions de garantie vol limit� aux outils et accessoires interchangeables',
N'Par d�rogation partielle � l�exclusion 1 du Chapitre � Exclusions g�n�rales � des Conditions g�n�rales les vols limit�s aux outils et accessoires interchangeables et utilis�s par les engins (godet, grappin, brise-roche, marteau, cisaille, tilrotator, lame, ...) sont garantis aux conditions suivantes :
- lorsqu�ils sont mont�s sur les engins, au moment du vol � condition que le vol ait �t� commis avec ou sans effraction ou violence ; 
- lorsqu�ils ne sont pas mont�s sur les engins et/ou entrepos�s au sol, au moment du vol, � condition que le vol ait �t� commis avec effraction ou violence.
Franchise sp�cifique vol : en cas de vol limit� aux outils et accessoires, la franchise de base d�finie ci-avant dans le � Tableau des garanties, montants et franchises � sera port�e � 5 % de la valeur de remplacement � neuf  au jour du sinistre, du ou des outils et accessoires vol�s, avec un minimum de 800 euros.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES ('LA02',N'Biens exclus', N'Sont exclus pour l�ensemble des garantis les biens demeurant en permanence � l�int�rieur de v�hicules.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES ('LA10',N'Garantie vol : franchise sp�cifique', N'La franchise en cas de vol est port�e � 3 fois la franchise de base d�finie ci-avant dans le � Tableau des garanties, montants et franchises �.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES ('LA14',N'Exclusion des sources d��mission et de r�ception', N'Conform�ment aux Conditions g�n�rales, ne sont pas garantis les dommages limit�s aux pi�ces, �l�ments ou outils, ou composants de machines qui n�cessitent de par leur fonctionnement un remplacement p�riodique.
Ces dispositions s�appliquent notamment aux �l�ments suivants :
- les lampes, les cellules photosensibles,
- les tubes fluorescents, les lampes ultra-violet,
- les tubes d��mission, les cellules de r�ception,
- les capteurs, les d�tecteurs num�riques fixes ou amovibles.');

UPDATE [sch_mchn].[T_EDITION_CLAUSE] SET TYPE = 'VOL' WHERE CODE IN ('LA90','LA92','LA10');