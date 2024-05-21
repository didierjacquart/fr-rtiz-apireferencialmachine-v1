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

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA01',N'Exclusion des biens suivants',N'Ne sont pas garantis l�ensemble des biens demeurant en permanence � l�int�rieur de v�hicules.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA02',N'Exclusion des �quipements et mat�riels',N'Ne sont pas garantis l�ensemble des biens suivants :
-	Les �chafaudages, les banches, les coffrages, 
-	Les cabanes de chantier, les bases de vie, les abris sanitaires, les conteneurs de stockage,
-	Le mat�riel de topographie,
-	Les biens utilis�s par les ferrailleurs.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA03',N'Exclusion des biens suivants',N'Ne sont pas garantis les distributeurs automatiques et les machines en libre service.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA10',N'Garantie vol : franchise sp�cifique',N'La franchise en cas de vol est port�e � 3 fois la franchise de base d�finie ci-avant dans le � Tableau des garanties, montants et franchises �.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA11',N'Garantie vol : franchise sp�cifique',N'La franchise en cas de vol est port�e � 5 fois la franchise de base d�finie ci-avant dans le � Tableau des garanties, montants et franchises �.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA12',N'Garantie vol / vandalisme : franchise sp�cifique',N'La franchise en cas de vol ou de vandalisme est port�e � 2 fois la franchise de base d�finie ci-avant dans le � Tableau des garanties, montants et franchises �.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA14',N'Garantie des sources d��mission et de r�ception : conditions de garantie',N'Conform�ment aux Conditions g�n�rales, ne sont pas garantis les dommages limit�s aux pi�ces, �l�ments ou outils, ou composants de machines qui n�cessitent de par leur fonctionnement un remplacement p�riodique.
Ces dispositions s�appliquent notamment aux organes mettant en �uvre des ph�nom�nes �lectromagn�tiques tels que :
- la lumi�re : lampes, cellules photosensibles,
- le rayonnement ultra-violet : tubes fluorescents, lampes ultra-violet,
- le laser : tubes d��mission, cellules de r�ception,
- les capteurs, d�tecteurs num�riques fixes ou amovibles.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA17',N'Garantie incendie : franchise applicable en cas d�incendie prenant naissance dans la machine',N'La franchise de base d�finie ci-avant dans le � Tableau des garanties, montants et franchises �, en cas de dommages cons�cutifs � un incendie ayant pris naissance dans la machine, sera port�e � 10 % du montant des dommages, dans le cas o� :
- la machine utilise un hydrocarbure comme liquide di�lectrique, 
- et n�est pas �quip�e d�un dispositif de s�curit� en fonctionnement provoquant : 
     .  l�arr�t de la machine en cas de baisse de niveau ou d��l�vation anormale de temp�rature du liquide di�lectrique, 
     .  et le d�clenchement d�extinction automatique.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA22',N'Exclusion des produits ou esp�ces',N'Il est rappel� que la garantie s�applique uniquement aux �l�ments constituant les machines.
En cons�quence, ne sont pas garantis les produits ou les esp�ces contenus dans les biens assur�s.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA23',N'Garantie des sources d��mission et de r�ception : conditions de garantie',N'Conform�ment aux Conditions g�n�rales, ne sont pas garantis les dommages limit�s aux pi�ces, �l�ments ou outils, ou composants de machines qui n�cessitent de par leur fonctionnement un remplacement p�riodique.
Ces dispositions s�appliquent aux sources d��mission ou de r�ception d�ondes �lectromagn�tiques, acoustiques ou �lectroacoustiques comme :
a) les organes mettant en �uvre des ph�nom�nes �lectromagn�tiques, tels que :
- lumi�re : lampes, cellules photosensibles
- laser : tubes d��mission, cellules de r�ception
- ultra-violet : tubes fluorescents, lampes ultra-violet
- rayons x : tubes d��mission, d�tecteurs silicium-lithium
- rayonnements ionisants : tubes Geiger-Muller, d�tecteurs � semi-conducteur
- les capteurs, d�tecteurs num�riques fixes ou amovibles,
b) les organes mettant en �uvre des ph�nom�nes acoustiques ou �lectroacoustiques, tels que :
- sons et ultra-sons : transducteurs, sondes d��chographe, microphones.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA26',N'Garantie des moteurs de traction des v�hicules et engins automoteurs limit�e aux �v�nements externes',N'Par d�rogation aux Conditions g�n�rales, la garantie des moteurs de traction des engins automoteurs est limit�e � tout dommage mat�riel r�sultant :
- d�un accident de circulation, d�une collision, d�un choc contre un corps fixe ou mobile, 
- d�un renversement, d�un effondrement, d�un affaissement de terrain, 
- d�une chute � l�eau, d�un contact avec des fum�es, liquide ou gaz,
- d�un incendie, d�une explosion, 
- d�un vol ou d�un vandalisme, si la garantie Vol est souscrite sur l�ensemble du contrat,
- d�un �v�nement naturel, 
- d�un attentat ou actes de terrorisme.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA29',N'Solidification du b�ton',N'En cas de sinistre, nous garantissons les dommages r�sultant de la solidification du b�ton sous r�serve :
- qu�ils  soient la cons�quence d�un dommage mat�riel garanti atteignant la machine assur�e, ou 
- qu�ils soient cons�cutifs � un accident de circulation, avec ou sans dommage mat�riel � la machine assur�e.
Au titre de ces dommages garantis, nous prenons en charge :
- la r�paration de la machine sinistr�e,
- la perte du b�ton solidifi� pour un montant maximum de 2000  euros,
- les frais engag�s pour retirer le b�ton solidifi� dans la limite de 5 % de la valeur de remplacement � neuf au jour du sinistre, de la machine sinistr�e.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA34',N'Solidification de la mati�re',N'En cas de sinistre, nous garantissons les dommages r�sultant de la solidification des produits ou mati�re en cours de fabrication ou de traitement sous r�serve :
- qu�ils  soient la cons�quence d�un dommage mat�riel garanti atteignant une machine assur�e.
Au titre de ces dommages garantis, nous prenons en charge :
- la r�paration de la machine sinistr�e,
- la perte de produits ou mati�re en cours de fabrication ou de traitement pour un montant maximum de 2000 euros,
- les frais engag�s pour retirer les produits ou mati�re solidifi�s dans la limite de 5 % de la valeur de remplacement � neuf au jour du sinistre, de la machine sinistr�e.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA36',N'Franchise applicable en cas de non utilisation des dispositifs de s�curit�',N'Lorsque le sinistre r�sulte ou est aggrav� par la mise hors service ou la non utilisation de dispositifs de s�curit� �quipant les engins, et que les conditions d�exploitation des engins n�cessitent l�utilisation de dispositifs de s�curit�, la franchise est port�e � 10 % du montant du dommage avec un minimum de 800 euros.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA90',N'Garantie vol : conditions de garantie des engins et mat�riels',N'Il est rappel� que conform�ment aux Conditions g�n�rales, seuls sont garantis les vols qui ont �t�  commis avec effraction ou violence. 
Cas particuliers  des engins et mat�riels de valeurs de remplacement � neuf sup�rieures � 25 000 � : 
Par d�rogation aux Conditions g�n�rales, pour les engins et mat�riels d�une valeur de remplacement � neuf au jour de la souscription sup�rieure � 25000 euros, la garantie vol est �tendue au vol sans effraction ou violence.
Franchise vol : la franchise en cas de vol sera port�e � 3 fois la franchise de base d�finie ci-avant dans le � Tableau des garanties, montants et franchises �.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA92',N'Garantie vol : conditions de garantie vol limit� aux outils et accessoires interchangeables',N'Les vols limit�s aux outils et accessoires interchangeables et utilis�s par les engins (godet, grappin, brise-roche, marteau, cisaille, tilrotator, lame, ...) sont garantis aux conditions suivantes :
Les vols d�outils et accessoires :
- mont�s sur les engins, au moment du vol : sont garantis, � condition qu�il aient �t� commis avec ou sans effraction ou violence, et ce, par d�rogation aux Conditions g�n�rales ; 
- non mont�s sur les engins et/ou entrepos�s au sol, au moment du vol : sont garantis � condition qu�ils aient �t� commis avec effraction ou violence, et ce, conform�ment aux Conditions g�n�rales.
Franchise sp�cifique vol : en cas de vol limit� aux outils et accessoires, la franchise d�finie ci-avant sera port�e � 5 % de la valeur de remplacement � neuf au jour du sinistre, du ou des outils et accessoires vol�s, avec un minimum de 800 euros.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE],[LABEL],[DESCRIPTION]) VALUES ('PA94',N'Garantie vol : conditions de garantie � franchise sp�cifique',N'Extension au vol sans effraction ou violence :
Par d�rogation aux Exclusions g�n�rales des Conditions g�n�rales, la garantie est �tendue au vol sans effraction ou violence.

Franchise vol :
Cas particuliers des vols non limit�s aux vols d�outils et accessoires utilis�s par les engins mobiles :
la franchise en cas de vol est port�e � 2 fois la franchise de base d�finie ci-avant dans le � Tableau des garanties, montants et franchises �.

Cas particuliers  des vols limit�s aux vols d�outils et accessoires utilis�s par les engins mobiles :
la franchise de base d�finie ci-avant dans le � Tableau des garanties, montants et franchises � est port�e � 5 % de la valeur de remplacement � neuf  au jour du sinistre, du ou des outils et accessoires vol�s, avec un minimum de 800 euros.')

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
