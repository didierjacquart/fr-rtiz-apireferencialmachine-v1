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

INSERT INTO [sch_mchn].[T_MACHINE_SPECIFICATION]([CODE],[NAME],[LABEL],[SCORE],[IS_OUT_OF_MACHINE_INSURANCE],[IS_DELEGATED],[IS_UNREFERENCED],[IS_EXCLUDED],[START_DATETIME_SUBSCRIPTION_PERIOD],[MACHINE_RATE],[ALL_PLACE_CORVERED],[PRODUCT],[IS_TRANSPORTABLE],[DEDUCTIBLE_MIN_VALUE],[DEDUCTIBLE_MIN_INCLUDE],[DEDUCTIBLE_MAX_VALUE],[DEDUCTIBLE_MAX_INCLUDE],[DEDUCTIBLE_UNIT],[FINANCIAL_VALUATION_DELEGATED_MAX],[EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE], [IS_FIRE_COVERAGE_MANDATORY],[FINANCIAL_VALUATION_MACHINE_MAX],[FINANCIAL_VALUATION_MACHINE_DELEGATED_MAX]) VALUES ('85001',N'BTP - Engins, matériels de chantier',N'BTP - Engins, matériels de chantier','3',0,1,0,0,'2024/03/06',0,'INCLUDED','RENTAL_MACHINE',1,1000,1,4000,1,'EUR',500000,0,0,500000,2000000);
INSERT INTO [sch_mchn].[T_MACHINE_SPECIFICATION]([CODE],[NAME],[LABEL],[SCORE],[IS_OUT_OF_MACHINE_INSURANCE],[IS_DELEGATED],[IS_UNREFERENCED],[IS_EXCLUDED],[START_DATETIME_SUBSCRIPTION_PERIOD],[MACHINE_RATE],[ALL_PLACE_CORVERED],[PRODUCT],[IS_TRANSPORTABLE],[DEDUCTIBLE_MIN_VALUE],[DEDUCTIBLE_MIN_INCLUDE],[DEDUCTIBLE_MAX_VALUE],[DEDUCTIBLE_MAX_INCLUDE],[DEDUCTIBLE_UNIT],[FINANCIAL_VALUATION_DELEGATED_MAX],[EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE], [IS_FIRE_COVERAGE_MANDATORY],[FINANCIAL_VALUATION_MACHINE_MAX],[FINANCIAL_VALUATION_MACHINE_DELEGATED_MAX]) VALUES ('85002',N'Média, Audiovisuel',N'Média, Audiovisuel','2',0,1,0,0,'2024/03/06',0,'INCLUDED','RENTAL_MACHINE',1,1000,1,4000,1,'EUR',200000,0,0,200000,1000000);
INSERT INTO [sch_mchn].[T_MACHINE_SPECIFICATION]([CODE],[NAME],[LABEL],[SCORE],[IS_OUT_OF_MACHINE_INSURANCE],[IS_DELEGATED],[IS_UNREFERENCED],[IS_EXCLUDED],[START_DATETIME_SUBSCRIPTION_PERIOD],[MACHINE_RATE],[ALL_PLACE_CORVERED],[PRODUCT],[IS_TRANSPORTABLE],[DEDUCTIBLE_MIN_VALUE],[DEDUCTIBLE_MIN_INCLUDE],[DEDUCTIBLE_MAX_VALUE],[DEDUCTIBLE_MAX_INCLUDE],[DEDUCTIBLE_UNIT],[FINANCIAL_VALUATION_DELEGATED_MAX],[EXTENDED_FLEET_COVERAGE_ALLOWED_PERCENTAGE], [IS_FIRE_COVERAGE_MANDATORY],[FINANCIAL_VALUATION_MACHINE_MAX],[FINANCIAL_VALUATION_MACHINE_DELEGATED_MAX]) VALUES ('85003',N'Autre famille de matériel',null,'1',0,1,1,0,'2024/03/06',0,'INCLUDED','RENTAL_MACHINE',1,1000,1,4000,1,'EUR',200000,0,0,200000,1000000);

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

INSERT INTO [sch_mchn].[T_RISK_PRECISION]([MACHINE_CODE],[CODE],[LABEL],[DETAIL]) VALUES ('85001','DOMAINS',N'Domaines:',N'Terrassement et chargement, Levage et manutention, Routes et voirie, …');
INSERT INTO [sch_mchn].[T_RISK_PRECISION]([MACHINE_CODE],[CODE],[LABEL],[DETAIL]) VALUES ('85002','DOMAINS',N'Domaines:',N'Audio, Sono, Photo, Vidéo');
INSERT INTO [sch_mchn].[T_RISK_PRECISION]([MACHINE_CODE],[CODE],[LABEL],[DETAIL]) VALUES ('85001','EXAMPLES',N'Exemple de machines:',N'Minipelle, Chariot élévateur de chantier, Chargeur, Grue, Machine à projeter…');
INSERT INTO [sch_mchn].[T_RISK_PRECISION]([MACHINE_CODE],[CODE],[LABEL],[DETAIL]) VALUES ('85002','EXAMPLES',N'Exemple de machines:',N'Appareil photo, Caméra, Panneau d’affichage lumineux, Amplificateur audio, Vidéoprojecteur, Borne de développement photo, Table de mixage, Station de montage, …');
INSERT INTO [sch_mchn].[T_RISK_PRECISION]([MACHINE_CODE],[CODE],[LABEL],[DETAIL]) VALUES ('85001','EXCLUSIONS',N'Attention, machines exclues à la souscription:',N'Véhicule utilitaire, Echafaudage, Cabane de chantier, Topographie, Banche');
INSERT INTO [sch_mchn].[T_RISK_PRECISION]([MACHINE_CODE],[CODE],[LABEL],[DETAIL]) VALUES ('85003','EXCLUSIONS',N'A noter, machines exclues à la souscription:',N'Nouveaux Véhicules Electriques Individuels (Vélos et Trottinettes électriques, draisiennes électriques, Gyropodes, Segway, Hoverboards... ), équipements en sous-sol, téléphones portables');


DELETE FROM [sch_mchn].[T_EDITION_CLAUSE] WHERE CODE IN ('LA01','LA26','LA36','LA90','LA92', 'LA02','LA14','LA10');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('LA01',N'Biens exclus', 
N'Sont exclus pour l’ensemble des garantis les biens suivants :
- les véhicules utilitaires, de société, de fonction, 
- les véhicules destinés au transport de personnes et de marchandises,
- les échafaudages, les banches, les coffrages, 
- Les cabanes de chantier, bases de vie, abris sanitaires, conteneurs de stockage,
- Le matériel de topographie,
- Les biens utilisés par les ferrailleurs.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('LA26',N'Garantie des moteurs de traction des véhicules et engins automoteurs limitée aux évènements externes', 
N'Par dérogation au paragraphe « Dommages garantis » du Chapitre « La Garantie Dommages aux biens » des Conditions générales, la garantie des moteurs de traction des engins automoteurs est limitée à tout dommage matériel résultant :
- d’un accident de circulation, d’une collision, d’un choc contre un corps fixe ou mobile, 
- d’un renversement, d’un effondrement, d’un affaissement de terrain, 
- d’une chute à l’eau, d’un contact avec des fumées, liquide ou gaz,
- d’un incendie, d’une explosion, 
- d’un vol ou d’un vandalisme, 
- d’un événement naturel, 
- d’un attentat ou actes de terrorisme.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('LA36',N'Franchise applicable en cas de non utilisation des dispositifs de sécurité', 
N'Lorsque le sinistre résulte ou est aggravé par la mise hors service ou la non utilisation de dispositifs de sécurité équipant les engins, et que les conditions d’exploitation des engins nécessitent l’utilisation de dispositifs de sécurité, la franchise sera portée à : 3 fois la franchise de base définie ci-avant dans le « Tableau des garanties, montants et franchises ».');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('LA90',N'Cas particuliers  des engins et matériels de valeurs de remplacement à neuf supérieures à 25 000 €', N'Par dérogation à l’exclusion 1 du Chapitre « Exclusions générales » des Conditions générales, pour les engins et matériels d’une valeur de remplacement à neuf au jour de la souscription supérieure à 25000 euros, la garantie vol est étendue au vol sans effraction ou violence.
Franchise vol  pour l’ensemble des engins et matériels : la franchise en cas de vol sera portée à 3 fois la franchise de base définie ci-avant dans le « Tableau des garanties, montants et franchises ».');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('LA92',N'Garantie vol : conditions de garantie vol limité aux outils et accessoires interchangeables',
N'Par dérogation partielle à l’exclusion 1 du Chapitre « Exclusions générales » des Conditions générales les vols limités aux outils et accessoires interchangeables et utilisés par les engins (godet, grappin, brise-roche, marteau, cisaille, tilrotator, lame, ...) sont garantis aux conditions suivantes :
- lorsqu’ils sont montés sur les engins, au moment du vol à condition que le vol ait été commis avec ou sans effraction ou violence ; 
- lorsqu’ils ne sont pas montés sur les engins et/ou entreposés au sol, au moment du vol, à condition que le vol ait été commis avec effraction ou violence.
Franchise spécifique vol : en cas de vol limité aux outils et accessoires, la franchise de base définie ci-avant dans le « Tableau des garanties, montants et franchises » sera portée à 5 % de la valeur de remplacement à neuf  au jour du sinistre, du ou des outils et accessoires volés, avec un minimum de 800 euros.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES ('LA02',N'Biens exclus', N'Sont exclus pour l’ensemble des garantis les biens demeurant en permanence à l’intérieur de véhicules.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES ('LA10',N'Garantie vol : franchise spécifique', N'La franchise en cas de vol est portée à 3 fois la franchise de base définie ci-avant dans le « Tableau des garanties, montants et franchises ».');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES ('LA14',N'Exclusion des sources d’émission et de réception', N'Conformément aux Conditions générales, ne sont pas garantis les dommages limités aux pièces, éléments ou outils, ou composants de machines qui nécessitent de par leur fonctionnement un remplacement périodique.
Ces dispositions s’appliquent notamment aux éléments suivants :
- les lampes, les cellules photosensibles,
- les tubes fluorescents, les lampes ultra-violet,
- les tubes d’émission, les cellules de réception,
- les capteurs, les détecteurs numériques fixes ou amovibles.');

UPDATE [sch_mchn].[T_EDITION_CLAUSE] SET TYPE = 'VOL' WHERE CODE IN ('LA90','LA92','LA10');