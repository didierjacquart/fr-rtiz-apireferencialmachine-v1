-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 09/01/2023
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : update clauses to machines park and fix score
-------------------------------------------------------------------------
USE [mchn]

delete from [sch_mchn].[T_EDITION_CLAUSE] where CODE like 'PA%';

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA01',N'Exclusion des biens suivants',N'Ne sont pas garantis l’ensemble des biens demeurant en permanence à l’intérieur de véhicules.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA02',N'Exclusion des équipements et matériels',N'Ne sont pas garantis l’ensemble des biens suivants :
-	Les échafaudages, les banches, les coffrages, 
-	Les cabanes de chantier, les bases de vie, les abris sanitaires, les conteneurs de stockage,
-	Le matériel de topographie,
-	Les biens utilisés par les ferrailleurs.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA03',N'Exclusion des biens suivants',N'Ne sont pas garantis les distributeurs automatiques et les machines en libre service.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA10',N'Garantie vol : franchise spécifique',N'La franchise en cas de vol est portée à 3 fois la franchise de base définie ci-avant dans le « Tableau des garanties, montants et franchises ».')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA11',N'Garantie vol : franchise spécifique',N'La franchise en cas de vol est portée à 5 fois la franchise de base définie ci-avant dans le « Tableau des garanties, montants et franchises ».')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA12',N'Garantie vol / vandalisme : franchise spécifique',N'La franchise en cas de vol ou de vandalisme est portée à 2 fois la franchise de base définie ci-avant dans le « Tableau des garanties, montants et franchises ».')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA14',N'Garantie des sources d’émission et de réception : conditions de garantie',N'Conformément aux Conditions générales, ne sont pas garantis les dommages limités aux pièces, éléments ou outils, ou composants de machines qui nécessitent de par leur fonctionnement un remplacement périodique.
Ces dispositions s’appliquent notamment aux organes mettant en œuvre des phénomènes électromagnétiques tels que :
- la lumière : lampes, cellules photosensibles,
- le rayonnement ultra-violet : tubes fluorescents, lampes ultra-violet,
- le laser : tubes d’émission, cellules de réception,
- les capteurs, détecteurs numériques fixes ou amovibles.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA17',N'Garantie incendie : franchise applicable en cas d’incendie prenant naissance dans la machine',N'La franchise de base définie ci-avant dans le « Tableau des garanties, montants et franchises », en cas de dommages consécutifs à un incendie ayant pris naissance dans la machine, sera portée à 10 % du montant des dommages, dans le cas où :
- la machine utilise un hydrocarbure comme liquide diélectrique, 
- et n’est pas équipée d’un dispositif de sécurité en fonctionnement provoquant : 
     .  l’arrêt de la machine en cas de baisse de niveau ou d’élévation anormale de température du liquide diélectrique, 
     .  et le déclenchement d’extinction automatique.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA22',N'Exclusion des produits ou espèces',N'Il est rappelé que la garantie s’applique uniquement aux éléments constituant les machines.
En conséquence, ne sont pas garantis les produits ou les espèces contenus dans les biens assurés.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA23',N'Garantie des sources d’émission et de réception : conditions de garantie',N'Conformément aux Conditions générales, ne sont pas garantis les dommages limités aux pièces, éléments ou outils, ou composants de machines qui nécessitent de par leur fonctionnement un remplacement périodique.
Ces dispositions s’appliquent aux sources d’émission ou de réception d’ondes électromagnétiques, acoustiques ou électroacoustiques comme :
a) les organes mettant en œuvre des phénomènes électromagnétiques, tels que :
- lumière : lampes, cellules photosensibles
- laser : tubes d’émission, cellules de réception
- ultra-violet : tubes fluorescents, lampes ultra-violet
- rayons x : tubes d’émission, détecteurs silicium-lithium
- rayonnements ionisants : tubes Geiger-Muller, détecteurs à semi-conducteur
- les capteurs, détecteurs numériques fixes ou amovibles,
b) les organes mettant en œuvre des phénomènes acoustiques ou électroacoustiques, tels que :
- sons et ultra-sons : transducteurs, sondes d’échographe, microphones.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA26',N'Garantie des moteurs de traction des véhicules et engins automoteurs limitée aux évènements externes',N'Par dérogation aux Conditions générales, la garantie des moteurs de traction des engins automoteurs est limitée à tout dommage matériel résultant :
- d’un accident de circulation, d’une collision, d’un choc contre un corps fixe ou mobile, 
- d’un renversement, d’un effondrement, d’un affaissement de terrain, 
- d’une chute à l’eau, d’un contact avec des fumées, liquide ou gaz,
- d’un incendie, d’une explosion, 
- d’un vol ou d’un vandalisme, si la garantie Vol est souscrite sur l’ensemble du contrat,
- d’un événement naturel, 
- d’un attentat ou actes de terrorisme.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA29',N'Solidification du béton',N'En cas de sinistre, nous garantissons les dommages résultant de la solidification du béton sous réserve :
- qu’ils  soient la conséquence d’un dommage matériel garanti atteignant la machine assurée, ou 
- qu’ils soient consécutifs à un accident de circulation, avec ou sans dommage matériel à la machine assurée.
Au titre de ces dommages garantis, nous prenons en charge :
- la réparation de la machine sinistrée,
- la perte du béton solidifié pour un montant maximum de 2000  euros,
- les frais engagés pour retirer le béton solidifié dans la limite de 5 % de la valeur de remplacement à neuf au jour du sinistre, de la machine sinistrée.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA34',N'Solidification de la matière',N'En cas de sinistre, nous garantissons les dommages résultant de la solidification des produits ou matière en cours de fabrication ou de traitement sous réserve :
- qu’ils  soient la conséquence d’un dommage matériel garanti atteignant une machine assurée.
Au titre de ces dommages garantis, nous prenons en charge :
- la réparation de la machine sinistrée,
- la perte de produits ou matière en cours de fabrication ou de traitement pour un montant maximum de 2000 euros,
- les frais engagés pour retirer les produits ou matière solidifiés dans la limite de 5 % de la valeur de remplacement à neuf au jour du sinistre, de la machine sinistrée.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA36',N'Franchise applicable en cas de non utilisation des dispositifs de sécurité',N'Lorsque le sinistre résulte ou est aggravé par la mise hors service ou la non utilisation de dispositifs de sécurité équipant les engins, et que les conditions d’exploitation des engins nécessitent l’utilisation de dispositifs de sécurité, la franchise est portée à 10 % du montant du dommage avec un minimum de 800 euros.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA90',N'Garantie vol : conditions de garantie des engins et matériels',N'Il est rappelé que conformément aux Conditions générales, seuls sont garantis les vols qui ont été  commis avec effraction ou violence. 
Cas particuliers  des engins et matériels de valeurs de remplacement à neuf supérieures à 25 000 € : 
Par dérogation aux Conditions générales, pour les engins et matériels d’une valeur de remplacement à neuf au jour de la souscription supérieure à 25000 euros, la garantie vol est étendue au vol sans effraction ou violence.
Franchise vol : la franchise en cas de vol sera portée à 3 fois la franchise de base définie ci-avant dans le « Tableau des garanties, montants et franchises ».')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA92',N'Garantie vol : conditions de garantie vol limité aux outils et accessoires interchangeables',N'Les vols limités aux outils et accessoires interchangeables et utilisés par les engins (godet, grappin, brise-roche, marteau, cisaille, tilrotator, lame, ...) sont garantis aux conditions suivantes :
Les vols d’outils et accessoires :
- montés sur les engins, au moment du vol : sont garantis, à condition qu’il aient été commis avec ou sans effraction ou violence, et ce, par dérogation aux Conditions générales ; 
- non montés sur les engins et/ou entreposés au sol, au moment du vol : sont garantis à condition qu’ils aient été commis avec effraction ou violence, et ce, conformément aux Conditions générales.
Franchise spécifique vol : en cas de vol limité aux outils et accessoires, la franchise définie ci-avant sera portée à 5 % de la valeur de remplacement à neuf au jour du sinistre, du ou des outils et accessoires volés, avec un minimum de 800 euros.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA94',N'Garantie vol : conditions de garantie – franchise spécifique',N'Extension au vol sans effraction ou violence :
Par dérogation aux Exclusions générales des Conditions générales, la garantie est étendue au vol sans effraction ou violence.

Franchise vol :
Cas particuliers des vols non limités aux vols d’outils et accessoires utilisés par les engins mobiles :
la franchise en cas de vol est portée à 2 fois la franchise de base définie ci-avant dans le « Tableau des garanties, montants et franchises ».

Cas particuliers  des vols limités aux vols d’outils et accessoires utilisés par les engins mobiles :
la franchise de base définie ci-avant dans le « Tableau des garanties, montants et franchises » est portée à 5 % de la valeur de remplacement à neuf  au jour du sinistre, du ou des outils et accessoires volés, avec un minimum de 800 euros.')

UPDATE [sch_mchn].[T_EDITION_CLAUSE] SET TYPE = 'VOL' WHERE CODE IN ('PA10','PA11','PA90','PA92','PA94');

UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [EDITION_CLAUSE_CODES]='PA02|PA26|PA36|PA90|PA92' WHERE CODE='02099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [EDITION_CLAUSE_CODES]='PA10' WHERE CODE='24099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [EDITION_CLAUSE_CODES]='PA26|PA94' WHERE CODE='00099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [EDITION_CLAUSE_CODES]='PA23' WHERE CODE='50099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [EDITION_CLAUSE_CODES]='PA14' WHERE CODE='31099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [EDITION_CLAUSE_CODES]='PA01|PA14|PA10' WHERE CODE='60099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [EDITION_CLAUSE_CODES]='PA01|PA14|PA10' WHERE CODE='21099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [EDITION_CLAUSE_CODES]='PA11' WHERE CODE='06099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [EDITION_CLAUSE_CODES]='PA14' WHERE CODE='18099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [EDITION_CLAUSE_CODES]='PA22|PA12' WHERE CODE='26099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [EDITION_CLAUSE_CODES]='PA03' WHERE CODE='30099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [EDITION_CLAUSE_CODES]='PA10' WHERE CODE='74099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [EDITION_CLAUSE_CODES]='PA14' WHERE CODE='32099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [EDITION_CLAUSE_CODES]='PA14|PA17' WHERE CODE='35099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [EDITION_CLAUSE_CODES]='PA34' WHERE CODE='34099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [EDITION_CLAUSE_CODES]='PA29' WHERE CODE='36099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [EDITION_CLAUSE_CODES]='PA26|PA90' WHERE CODE='23099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [EDITION_CLAUSE_CODES]='PA10' WHERE CODE='76099';

UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='23' WHERE CODE='02099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='22' WHERE CODE='24099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='21' WHERE CODE='00099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='20' WHERE CODE='50099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='19'  WHERE CODE='31099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='18' WHERE CODE='60099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='17' WHERE CODE='21099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='16' WHERE CODE='06099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='15' WHERE CODE='18099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='14' WHERE CODE='26099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='13' WHERE CODE='30099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='12' WHERE CODE='74099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='11' WHERE CODE='32099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='10' WHERE CODE='72099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='9' WHERE CODE='35099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='8' WHERE CODE='33099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='7' WHERE CODE='34099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='6' WHERE CODE='36099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='5' WHERE CODE='23099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='4' WHERE CODE='22099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='3' WHERE CODE='25099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='2' WHERE CODE='76099';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [SCORE]='1' WHERE CODE='79099';
