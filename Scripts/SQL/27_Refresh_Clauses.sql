-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 26/10/2022
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : Alimentation de la table T_EDITION_CLAUSE
------------------------------------------------------------------------- 
USE [mchn]

DELETE FROM [sch_mchn].[T_EDITION_CLAUSE];

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA09','Conditions de garantie des mat�riels portables au cours des transports : applicables aux ordinateurs et tablettes', 
N'Par extension aux Conditions g�n�rales, les micro-ordinateurs portables et les tablettes tactiles sont garantis en tous lieux dans le monde entier, sous r�serve des conditions de garantie transport d�finis ci-apr�s :
- Le transport est effectu� pour son propre compte par l�assur� ou les membres de sa soci�t�.
- Dans les transports en commun (a�rien, maritime, terrestre) ainsi que dans les gares et les lieux  publics, les micro-ordinateurs portables et les tablettes tactiles sont garantis sous r�serve qu�ils soient sous la surveillance directe et imm�diate de l�assur� ou des personnes qui l�accompagnent.
- Au cours des transports dans un v�hicule sont exclus les vols : 
   . qui ne sont pas effectu�s pour son propre compte par l�assur� ou les membres de sa soci�t�,
   . des biens visibles de l�ext�rieur de v�hicule en stationnement,
   . commis dans un v�hicule en stationnement qui ne serait pas totalement carross� en mat�riaux
     durs et ferm� � clef,
   . commis sans effraction de v�hicule en stationnement,
   . commis entre 21h et 7h, lorsque le v�hicule est en stationnement sur la voie publique.');
   
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA13','Conditions de garantie vol et franchise vol : applicables aux brise-roches', 
N'Conform�ment aux Conditions g�n�rales, le vol de brise-roches, entrepos�s au sol ou non mont�s sur les engins, sont garantis � condition qu�ils aient �t� commis avec effraction ou violence. Toutefois, par d�rogation aux Conditions g�n�rales la garantie vol des brise-roches mont�s sur les engins est �tendue au vol sans effraction ou violence.
Franchise sp�cifique vol :
En cas de vol limit� aux brise-roches, la franchise sera �gale � 10 % de la valeur de remplacement � neufs  au jour du sinistre, du ou des brise-roches vol�s, avec un minimum de 800 euros.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA14','Dispositions applicables aux sources d��mission ou de r�ception', 
N'Conform�ment aux Conditions g�n�rales, ne sont pas garantis les dommages limit�s aux pi�ces, �l�ments ou outils, ou composants de machines qui n�cessitent de par leur fonctionnement un remplacement p�riodique.Ces dispositions s�appliquent notamment aux organes mettant en �uvre des ph�nom�nes �lectromagn�tiques, tels que :
- la lumi�re : lampes, cellules photosensibles
- le laser : tubes d��mission, cellules de r�ception
- le rayonnement ultra-violet : tubes fluorescents, lampes ultra-violet
- les rayonnements ionisants : tubes Geiger-M�ller, d�tecteurs � semi-conducteur
- les capteurs, d�tecteurs num�riques fixes ou amovibles.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA15','Franchise en cas de dommage cons�cutif aux effets de la foudre : applicable aux installations �lectriques et antennes', 
N'Dans le cas o� la machine n�est pas, au jour du sinistre, munie d�une installation de protection contre la foudre (parafoudre, parasurtenseur, onduleur), la franchise de base pr�cis�e dans l�inventaire des biens assur�s est doubl�e en cas de dommages cons�cutifs � des effets de la foudre.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA17','Franchise sp�cifique en cas d�incendie prenant naissance dans la machine : applicable aux machines � �lectro�rosion', 
N'La franchise en cas de dommages cons�cutifs � un incendie ayant pris naissance dans la machine, sera �gale � 10 % du montant des dommages, dans le cas o� :
- la machine utilise un hydrocarbure comme liquide di�lectrique, 
- et n�est pas �quip�e d�un dispositif de s�curit� en fonctionnement provoquant : 
     .  l�arr�t de la machine en cas de baisse de niveau ou d��l�vation anormale de temp�rature du liquide di�lectrique, 
     .  et le d�clenchement d�extinction automatique.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA18','D�finition du bien assur� : Installations t�l�phoniques', 
N'Sont assur�s sans inventaire d�taill�, tous les �quipements constituant l�installation t�l�phonique (tels que : autocommutateur, central, standard, postes individuels, �quipements d�alimentation �lectrique,  c�bles de liaison,  transformateur, redresseur, onduleur, batterie).
La valeur d�clar�e doit correspondre � la valeur globale de cette installation.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA19','D�finition du bien assur� : Installation d��mission-r�ception radio', 
N'Sont assur�s, sans inventaire d�taill�, tous les �quipements constituant l�installation d��mission / r�ception � l�exclusion des relais, antennes et pyl�nes.
La valeur d�clar�e doit correspondre � la valeur globale de cette installation d��mission /r�ception');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA20','Utilisation sur la voie publique : disposition applicable aux distributeurs de glaces et aux r�tissoires', 
N'Utilisation sur la voie publique : disposition applicable aux distributeurs de glaces et r�tissoires La garantie est �tendue dans les limites du contrat, aux dommages subis par la machine sous r�serve qu�elle se trouve sur la voie publique � proximit� de l�adresse du risque et qu�elle soit remis�e le soir apr�s la fermeture dans un local enti�rement clos.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA22','Exclusion des produits ou esp�ces : applicable aux distributeurs et caisses automatiques', 
N'Il est rappel� que la garantie s�applique uniquement aux �l�ments constituant la machine. En cons�quence, ne sont pas garantis les produits ou les esp�ces contenus dans le bien assur�.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA23','Dispositions applicables aux sources d��mission ou de r�ception utilis�es dans le domaine m�dical', 
N'Conform�ment aux Conditions g�n�rales, ne sont pas garantis les dommages limit�s aux pi�ces, �l�ments ou outils, ou composants de machines qui n�cessitent de par leur fonctionnement un remplacement p�riodique. Ces dispositions s�appliquent aux sources d��mission ou de r�ception d�ondes �lectromagn�tiques, acoustiques ou �lectroacoustiques comme :
a) les organes mettant en �uvre des ph�nom�nes �lectromagn�tiques, tels que :
- lumi�re : lampes, cellules photosensibles
- laser : tubes d��mission, cellules de r�ception
- ultra-violet : tubes fluorescents, lampes ultra-violet
- rayons x : tubes d��mission, d�tecteurs silicium-lithium
- rayonnements ionisants : tubes Geiger-Muller, d�tecteurs � semi-conducteur
- les capteurs, d�tecteurs num�riques fixes ou amovibles,
b) les organes mettant en �uvre des ph�nom�nes acoustiques ou �lectroacoustiques, tels que : sons et ultra-sons : transducteurs, sondes d��chographe, microphones.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA28','Conditions de garantie des v�hicules porteurs', 
N'Les v�hicules porteurs (carrosserie, cabine, ch�ssis, moteur, transmission, essieux, suspension, roues) sont garantis sous r�serve que leurs valeurs soient incluses dans les capitaux assur�s. Par d�rogation aux Conditions g�n�rales, la garantie pour les v�hicules porteurs est limit�e � tout dommage mat�riel r�sultant :
- d�un accident de circulation, d�une collision, d�un choc contre un corps fixe ou mobile, 
- d�un renversement, d�un effondrement, d�un affaissement de terrain, 
- d�une chute � l�eau, d�un contact avec des fum�es, liquide ou gaz,
- d�un incendie, d�une explosion, 
- d�un vol, d�un vandalisme,
- d�un �v�nement naturel, 
- d�un attentat ou actes de terrorisme.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA29','Conditions de garantie suite � la solidification du b�ton', 
N'En cas de sinistre, nous garantissons les dommages r�sultant de la solidification du b�ton sous r�serve :
- qu�ils  soient la cons�quence d�un dommage mat�riel garanti atteignant la machine assur�e, ou 
- qu�ils soient cons�cutifs � un accident de circulation, avec ou sans dommage mat�riel � la machine assur�e.
Au titre de ces dommages garantis, nous prenons en charge :
- la r�paration de la machine sinistr�e,
- la perte du b�ton solidifi� pour un montant maximum de 1500 euros,
- les frais engag�s pour retirer le b�ton solidifi� dans la limite de 5 % de la valeur de remplacement � neuf au jour du sinistre, de la machine sinistr�e.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA30','Exclusion du v�hicule porteur : applicable aux bras de levage et bras d��lagage', 
N'Il est pr�cis� que le v�hicule porteur (carrosserie, cabine, ch�ssis, moteur, transmission, essieux, suspension, roues) du bien assur� est exclu de la garantie.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA31','D�finition de l�installation assur�e : installation de baln�oth�rapie', 
N'Sont assur�s sans inventaire d�taill�, tous les �quipements constituant l�installation de baln�oth�rapie (�quipement �lectrom�canique, baignoire, ...). La valeur d�clar�e doit correspondre � la valeur globale de cette installation.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA33','D�finition du bien assur� : garantie de l�ensemble de l�installation', 
N'Sont assur�s sans inventaire d�taill� tous les �quipements, accessoires et d�une fa�on g�n�rale tous les �l�ments constituant l�installation. La valeur d�clar�e doit correspondre � la valeur de remplacement � neuf de l�ensemble de l�installation. Vous devez en cas de sinistre, nous donner tout justificatif de l�exactitude de la d�claration des capitaux (factures, inventaires ...). S�il est constat� que la valeur d�clar�e est inf�rieure � la valeur de remplacement � neuf de l�ensemble de l�installation, vous supporteriez la r�duction d�indemnit� pr�vue � l�Article L.121.5 du Code des assurances.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA34','Conditions de garantie suite � la solidification de la mati�re', 
N'En cas de sinistre, nous garantissons les dommages r�sultant de la solidification des produits ou mati�re en cours de fabrication ou de traitement sous r�serve :
- qu�ils  soient la cons�quence d�un dommage mat�riel garanti atteignant la machine assur�e.
Au titre de ces dommages garantis, nous prenons en charge :
- la r�paration de la machine sinistr�e,
- la perte de produits ou mati�re en cours de fabrication ou de traitement pour un montant maximum de 1500 euros,
- les frais engag�s pour retirer les produits ou mati�re solidifi�s dans la limite de 5 % de la valeur de remplacement � neuf au jour du sinistre, de la machine sinistr�e.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA36','Disposition en cas de non utilisation des dispositifs de s�curit�', 
N'Lorsque le sinistre r�sulte ou est aggrav� par la mise hors service ou la non utilisation de dispositifs de s�curit� �quipant l�engin, et que les conditions d�exploitation du mat�riel n�cessitent l�utilisation de dispositifs de s�curit�, la franchise est port�e � 10 % du montant du dommage avec un minimum �gal � 2 fois la franchise de base pr�cis�e dans l�inventaire des biens assur�s.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA38','V�tust� applicable sur les pi�ces ou organes sujets � usure', 
N'Sur les pi�ces ou organes sujets � usure, notamment les tuyaux, flexibles, pneurides, tapis, vessies, membranes, une v�tust� est appliqu�e en fonction du rapport existant entre la dur�e de fonctionnement r�elle depuis leur dernier remplacement et leur dur�e de vie normale.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA40','Extension au vol sans effraction : applicable aux mat�riels agricoles exploit�s � l�adresse du risque', 
N'Par d�rogation aux Exclusions g�n�rales des Conditions g�n�rales, la garantie est �tendue au vol sans effraction ou violence. Elle s�exerce � l�adresse du risque dans les b�timents d�exploitation et cours attenantes.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA47','Dispositions applicables aux sources d��mission ou de r�ception utilis�es dans les endoscopes m�dicaux et les scanners m�dicaux', 
N'Conform�ment aux Conditions g�n�rales, ne sont pas garantis les dommages limit�s aux pi�ces, �l�ments ou outils, ou composants de machines qui n�cessitent de par leur fonctionnement un remplacement p�riodique. Ces dispositions s�appliquent notamment aux : lampes, lasers (tube d��mission, cellules de r�ception), transducteurs.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA48','Dispositions sp�cifiques aux sondes d��chographie', 
N'Les dommages aux sondes d��chographe d�sign�es au contrat ne donnent lieu � indemnisation que s�ils r�sultent d�un incendie, d�un vol, d�une chute, d�un choc, d�un d�g�t des liquides, ou d�un �v�nement n�ayant aucun rapport avec leur usure naturelle. En cas de dommage garanti atteignant une sonde et limit� � celle-ci, il sera appliqu� une v�tust� de 2 % par mois � compter de la date de 1�re mise en service. Cette v�tust� est plafonn�e � 80 %.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA51','Exclusion des d�fibrillateurs � usage public', 
N'La garantie ne s�applique pas lorsque le d�fibrillateur est � usage public.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA52','Extension de garantie relative aux d�fibrillateurs � usage public', 
N'Par d�rogation aux Conditions g�n�rales nous garantissons le vol commis avec ou sans effraction, avec ou sans violence, des d�fibrillateurs � usage public. En cas de vol, la franchise de base indiqu�e ci-avant dans l�inventaire des biens assur�s est multipli�e par 2.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA53','Dispositions sp�cifiques aux enneigeurs', 
N'Franchise applicable aux enneigeurs dans le cas d�un sinistre r�sultant d�une avalanche artificielle, d�clench�e volontairement. Outre la franchise g�n�rale du contrat toujours d�duite de l�indemnisation, en cas de dommages aux enneigeurs suite � avalanche artificielle d�clench�e volontairement, nous n�indemniserons les enneigeurs qu�� compter du sixi�me enneigeur sinistr�. Si moins de six enneigeurs sont sinistr�s, aucune indemnisation n�est due � l�assur� au titre des enneigeurs.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA59','Conditions de garantie Vol des �chafaudages', 
N'Dans vos locaux : 
- le vol des �chafaudages est garanti � condition qu�il ait �t� commis avec effraction, ou violence.
En dehors de vos locaux :
- le vol des �chafaudages sur le lieu du chantier est garanti, avec ou sans effraction ou violence, � condition que les �chafaudages soient mont�s, 
- le vol des �chafaudages entrepos�s au sol sur les lieux de chantier ou dans un v�hicule au cours des d�placements est exclu.
Franchise sp�cifique vol :
En cas de vol, la franchise sera �gale � 5 % de la valeur de remplacement � neuf  au jour du sinistre, des �chafaudages vol�s.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA60','Conditions de garantie Vol des banches et coffrages', 
N'En dehors des lieux de chantier (dans vos locaux et en cours de transport) : le vol des banches et coffrages est garanti � condition qu�il ait �t� commis avec effraction, ou violence. Sur le lieu du chantier : le vol des banches et coffrages est garanti avec ou sans effraction, avec ou sans violence.
Franchise sp�cifique vol : 
En cas de vol, la franchise sera �gale � 5 % de la valeur de remplacement � neufs  au jour du sinistre, des banches et coffrages vol�s.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA61', 'Exclusions relatives � la garantie Vol des appareil photo, cam�ras, objectifs photographiques', 
N'En compl�ment des exclusions des Conditions g�n�rales, ne sont pas garantis :
- les pertes et les disparitions inexpliqu�es ;
- les vols commis sans menace, sans agression, ou sans effraction des locaux ou des v�hicules dans lesquels se trouvent les biens assur�s ;
- les vols survenus dans un lieu public (notamment les transports en commun, les gares et les a�roports) des biens laiss�s sans surveillance directe et imm�diate de l�assur� ou des personnes qui l�accompagnent.
- le vol des biens laiss�s dans un v�hicule, si le vol n�est pas cons�cutif � une effraction du v�hicule, ou au vol du v�hicule, ou � un accident de circulation ;
- le vol des biens laiss�s visibles de l�ext�rieur dans un v�hicule en stationnement ; 
- le vol des biens laiss�s dans un v�hicule en stationnement, commis la nuit entre 22h00 et 7h00 du matin ;
- les vols des biens laiss�s dans un v�hicule non totalement carross� en mat�riaux durs.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA62','Exclusions relatives � la garantie dommages des appareils photo, cam�ras, objectifs photographiques', 
N'En compl�ment des exclusions des Conditions g�n�rales ne sont pas garantis :
- les dommages qui sont pris en charge dans le cadre de la garantie "constructeur" ;
- les dommages d�ordre esth�tique caus�s aux parties ext�rieures des biens ne nuisant pas � son bon fonctionnement (par exemple : les rayures, �raflures, �caillures, piq�res, t�ches) ;
- les dommages aux biens non pris en bagage � main dans les transports en commun (terrestre, a�rien ou maritime).');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA63','D�termination de l�indemnit� � application d�une v�tust� forfaitaire des appareils photo et cam�ras', 
N'En cas de sinistre partiel : sans pouvoir exc�der la somme fix�e si n�cessaire par expertise, le montant de l�indemnit� est �gal aux frais de r�paration, sans application de v�tust� ;
En cas de sinistre total : sans pouvoir exc�der la somme fix�e si n�cessaire par expertise, le montant de l�indemnit� est �gale :
- pendant les trois premi�res ann�es suivant la date de premi�re mise en service : � la valeur de remplacement � neuf au jour du sinistre,
- apr�s les trois premi�res ann�es suivant la date de premi�re mise en service : � la valeur de remplacement � neuf au jour du sinistre d�duction faite d�une v�tust� de 0,70% par mois depuis la date de premi�re mise en service. Cette v�tust� est limit�e � 60%.
Il sera toujours fait d�duction de la franchise et des valeurs de sauvetage s�il y a lieu.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA65','Condition de garantie des capteurs plan', 
N'Les dommages aux capteurs plan ne donnent lieu � indemnisation que s�ils r�sultent d�un incendie, d�un vol, d�une chute, d�un choc ou d�un �v�nement n�ayant aucun rapport avec leur usure naturelle. En cas de dommage garanti, il sera appliqu� :
- pendant les 24 premiers mois suivant la date de 1�re mise en service : une v�tust� nulle ;
- apr�s les 24 premiers mois : une v�tust� �gale � 0,9 % par mois � partir de la date de 1�re mise en service. Cette v�tust� est plafonn�e � 70 %.
Franchise en cas de sinistre limit� � un capteur plan : il sera d�duit de l�indemnit� une franchise �gale � 5 % de sa valeur de remplacement � neuf au jour du sinistre.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA66','Condition de garantie des cabanes de chantier', 
N'Exclusion du contenu :
Il est rappel� que la garantie s�applique uniquement aux �l�ments constituants la cabane de chantier. En cons�quence, ne sont pas garantis l�ensemble du mobilier, produits, objets, valeurs, mat�riels, �quipements, mat�riaux, marchandises, contenus dans la cabane de chantier. 

Franchise en cas de vol, de vandalisme ou d�incendie : 
La franchise est port�e � 5 % de la valeur de remplacement � neuf du bien assur� sinistr�, avec un minimum �gal � la franchise de base indiqu�e dans le paragraphe � Inventaire des biens assur�s �.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA67', 'Conditions de garantie vol des distributeurs de glaces et r�tissoires', 
N'Le mat�riel est garanti en tous lieux sous r�serve, qu�en dehors des march�s, le mat�riel soit remis� dans un endroit clos et ferm� � clef.
Franchise sp�cifique en cas de vol : la franchise de base indiqu�e ci-avant dans l�inventaire des biens assur�s est multipli�e par 5.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA70','Exclusion des pertes de liquide ou de gaz des IRM', 
N'Sont exclus toutes pertes d�h�ium se pr�sentant sous format liquide ou gazeux qui ne font pas suite � un dommage accidentel ayant endommag� d�autres parties de la machine. Sont notamment exclus la perte d�h�lium :
- par d�faut s�isolation dans le syst�me thermique,
- par d�clenchement volontaire de l��vacuation de l�h�lium de la cuve,
- par activation du dispositif de s�curit� d�clench� par le personnel, volontairement ou involontairement.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA71','Exclusion des pertes de liquide ou de gaz des appareils de cryoth�rapie', 
N'Sont exclus toutes pertes d�azote se pr�sentant sous format liquide ou gazeux qui ne font pas suite � un dommage mat�riel ayant endommag� d�autres parties de la machine.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA90','Conditions de garantie vol pour les engins et mat�riels', 
N'Il est rappel� que les vols commis sans effraction ou violence sont exclus. Toutefois, par d�rogation aux Conditions g�n�rales, pour les engins et mat�riels d�une valeur de remplacement � neuf au jour de la souscription sup�rieure � 25 000 euros, la garantie vol est �tendue au vol sans effraction ou violence.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA91','Franchise sp�cifique vol pour les engins et mat�riels', 
N'La franchise en cas de vol est port�e � 5 % de la valeur de remplacement � neuf au jour du sinistre, de l�engin et/ou mat�riel vol� avec un minimum �gal � la franchise de base indiqu�e dans le paragraphe � Inventaire des biens assur�s �.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA92', 'Conditions de garantie des outils et accessoires des engins de chantier', 
N'Conform�ment aux Conditions g�n�rales, les vols d�outils et accessoires utilis�s par les engins (godet, grappin, brise-roche, marteau, cisaille, tilrotator, lame...), entrepos�s au sol ou non mont�s sur les engins, sont garantis � condition qu�ils aient �t� commis avec effraction ou violence. Toutefois, par d�rogation aux Conditions g�n�rales, les vols limit�s aux outils et accessoires mont�s sur les engins sont garantis avec ou sans effraction ou violence. 

Franchise sp�cifique vol : en cas de vol limit� aux outils et accessoires, la franchise sera �gale � 10 % de la valeur de remplacement � neuf  au jour du sinistre, du ou des outils et accessoires vol�s, avec un minimum de 800 euros.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA93','Conditions de garantie des outils et accessoires des engins agricoles', 
N'Par d�rogation aux Conditions g�n�rales, les vols limit�s aux outils et accessoires  utilis�s par les engins agricoles, sont garantis avec ou sans effraction ou violence. 

Franchise sp�cifique vol :
En cas de vol limit� aux outils et accessoires, la franchise sera �gale � 5 % de la valeur de remplacement � neufs  au jour du sinistre, du ou des outils et accessoires vol�s, avec un minimum de 800 euros.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA75','Garantie des accessoires', 
N'Les accessoires des appareils photos, cam�ras et objectifs photographiques, sont garantis :
- � la condition que leurs valeurs soient comprises dans les capitaux d�clar�s,
- et � condition que les dommages ou les vols les affectant fassent �galement suite � un dommage ou un vol des appareils photos, cam�ras, objectifs.
Par d�finition sont consid�r�s comme accessoires les biens suivants : les cartes m�moires, chargeurs, batteries, sacs de protection, tr�pieds, stabilisateurs et t�l�commandes.');

UPDATE [sch_mchn].[T_EDITION_CLAUSE] SET TYPE = 'VOL' WHERE CODE IN ('MA13','MA40','MA52','MA59','MA60','MA61','MA67','MA90','MA91','MA92','MA93');