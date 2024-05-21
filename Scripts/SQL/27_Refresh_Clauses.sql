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
('MA09','Conditions de garantie des matériels portables au cours des transports : applicables aux ordinateurs et tablettes', 
N'Par extension aux Conditions générales, les micro-ordinateurs portables et les tablettes tactiles sont garantis en tous lieux dans le monde entier, sous réserve des conditions de garantie transport définis ci-après :
- Le transport est effectué pour son propre compte par l’assuré ou les membres de sa société.
- Dans les transports en commun (aérien, maritime, terrestre) ainsi que dans les gares et les lieux  publics, les micro-ordinateurs portables et les tablettes tactiles sont garantis sous réserve qu’ils soient sous la surveillance directe et immédiate de l’assuré ou des personnes qui l’accompagnent.
- Au cours des transports dans un véhicule sont exclus les vols : 
   . qui ne sont pas effectués pour son propre compte par l’assuré ou les membres de sa société,
   . des biens visibles de l’extérieur de véhicule en stationnement,
   . commis dans un véhicule en stationnement qui ne serait pas totalement carrossé en matériaux
     durs et fermé à clef,
   . commis sans effraction de véhicule en stationnement,
   . commis entre 21h et 7h, lorsque le véhicule est en stationnement sur la voie publique.');
   
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA13','Conditions de garantie vol et franchise vol : applicables aux brise-roches', 
N'Conformément aux Conditions générales, le vol de brise-roches, entreposés au sol ou non montés sur les engins, sont garantis à condition qu’ils aient été commis avec effraction ou violence. Toutefois, par dérogation aux Conditions générales la garantie vol des brise-roches montés sur les engins est étendue au vol sans effraction ou violence.
Franchise spécifique vol :
En cas de vol limité aux brise-roches, la franchise sera égale à 10 % de la valeur de remplacement à neufs  au jour du sinistre, du ou des brise-roches volés, avec un minimum de 800 euros.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA14','Dispositions applicables aux sources d’émission ou de réception', 
N'Conformément aux Conditions générales, ne sont pas garantis les dommages limités aux pièces, éléments ou outils, ou composants de machines qui nécessitent de par leur fonctionnement un remplacement périodique.Ces dispositions s’appliquent notamment aux organes mettant en œuvre des phénomènes électromagnétiques, tels que :
- la lumière : lampes, cellules photosensibles
- le laser : tubes d’émission, cellules de réception
- le rayonnement ultra-violet : tubes fluorescents, lampes ultra-violet
- les rayonnements ionisants : tubes Geiger-Müller, détecteurs à semi-conducteur
- les capteurs, détecteurs numériques fixes ou amovibles.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA15','Franchise en cas de dommage consécutif aux effets de la foudre : applicable aux installations électriques et antennes', 
N'Dans le cas où la machine n’est pas, au jour du sinistre, munie d’une installation de protection contre la foudre (parafoudre, parasurtenseur, onduleur), la franchise de base précisée dans l’inventaire des biens assurés est doublée en cas de dommages consécutifs à des effets de la foudre.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA17','Franchise spécifique en cas d’incendie prenant naissance dans la machine : applicable aux machines à électroérosion', 
N'La franchise en cas de dommages consécutifs à un incendie ayant pris naissance dans la machine, sera égale à 10 % du montant des dommages, dans le cas où :
- la machine utilise un hydrocarbure comme liquide diélectrique, 
- et n’est pas équipée d’un dispositif de sécurité en fonctionnement provoquant : 
     .  l’arrêt de la machine en cas de baisse de niveau ou d’élévation anormale de température du liquide diélectrique, 
     .  et le déclenchement d’extinction automatique.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA18','Définition du bien assuré : Installations téléphoniques', 
N'Sont assurés sans inventaire détaillé, tous les équipements constituant l’installation téléphonique (tels que : autocommutateur, central, standard, postes individuels, équipements d’alimentation électrique,  câbles de liaison,  transformateur, redresseur, onduleur, batterie).
La valeur déclarée doit correspondre à la valeur globale de cette installation.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA19','Définition du bien assuré : Installation d’émission-réception radio', 
N'Sont assurés, sans inventaire détaillé, tous les équipements constituant l’installation d’émission / réception à l’exclusion des relais, antennes et pylônes.
La valeur déclarée doit correspondre à la valeur globale de cette installation d’émission /réception');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA20','Utilisation sur la voie publique : disposition applicable aux distributeurs de glaces et aux rôtissoires', 
N'Utilisation sur la voie publique : disposition applicable aux distributeurs de glaces et rôtissoires La garantie est étendue dans les limites du contrat, aux dommages subis par la machine sous réserve qu’elle se trouve sur la voie publique à proximité de l’adresse du risque et qu’elle soit remisée le soir après la fermeture dans un local entièrement clos.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA22','Exclusion des produits ou espèces : applicable aux distributeurs et caisses automatiques', 
N'Il est rappelé que la garantie s’applique uniquement aux éléments constituant la machine. En conséquence, ne sont pas garantis les produits ou les espèces contenus dans le bien assuré.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA23','Dispositions applicables aux sources d’émission ou de réception utilisées dans le domaine médical', 
N'Conformément aux Conditions générales, ne sont pas garantis les dommages limités aux pièces, éléments ou outils, ou composants de machines qui nécessitent de par leur fonctionnement un remplacement périodique. Ces dispositions s’appliquent aux sources d’émission ou de réception d’ondes électromagnétiques, acoustiques ou électroacoustiques comme :
a) les organes mettant en œuvre des phénomènes électromagnétiques, tels que :
- lumière : lampes, cellules photosensibles
- laser : tubes d’émission, cellules de réception
- ultra-violet : tubes fluorescents, lampes ultra-violet
- rayons x : tubes d’émission, détecteurs silicium-lithium
- rayonnements ionisants : tubes Geiger-Muller, détecteurs à semi-conducteur
- les capteurs, détecteurs numériques fixes ou amovibles,
b) les organes mettant en œuvre des phénomènes acoustiques ou électroacoustiques, tels que : sons et ultra-sons : transducteurs, sondes d’échographe, microphones.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA28','Conditions de garantie des véhicules porteurs', 
N'Les véhicules porteurs (carrosserie, cabine, châssis, moteur, transmission, essieux, suspension, roues) sont garantis sous réserve que leurs valeurs soient incluses dans les capitaux assurés. Par dérogation aux Conditions générales, la garantie pour les véhicules porteurs est limitée à tout dommage matériel résultant :
- d’un accident de circulation, d’une collision, d’un choc contre un corps fixe ou mobile, 
- d’un renversement, d’un effondrement, d’un affaissement de terrain, 
- d’une chute à l’eau, d’un contact avec des fumées, liquide ou gaz,
- d’un incendie, d’une explosion, 
- d’un vol, d’un vandalisme,
- d’un événement naturel, 
- d’un attentat ou actes de terrorisme.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA29','Conditions de garantie suite à la solidification du béton', 
N'En cas de sinistre, nous garantissons les dommages résultant de la solidification du béton sous réserve :
- qu’ils  soient la conséquence d’un dommage matériel garanti atteignant la machine assurée, ou 
- qu’ils soient consécutifs à un accident de circulation, avec ou sans dommage matériel à la machine assurée.
Au titre de ces dommages garantis, nous prenons en charge :
- la réparation de la machine sinistrée,
- la perte du béton solidifié pour un montant maximum de 1500 euros,
- les frais engagés pour retirer le béton solidifié dans la limite de 5 % de la valeur de remplacement à neuf au jour du sinistre, de la machine sinistrée.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA30','Exclusion du véhicule porteur : applicable aux bras de levage et bras d’élagage', 
N'Il est précisé que le véhicule porteur (carrosserie, cabine, châssis, moteur, transmission, essieux, suspension, roues) du bien assuré est exclu de la garantie.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA31','Définition de l’installation assurée : installation de balnéothérapie', 
N'Sont assurés sans inventaire détaillé, tous les équipements constituant l’installation de balnéothérapie (équipement électromécanique, baignoire, ...). La valeur déclarée doit correspondre à la valeur globale de cette installation.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA33','Définition du bien assuré : garantie de l’ensemble de l’installation', 
N'Sont assurés sans inventaire détaillé tous les équipements, accessoires et d’une façon générale tous les éléments constituant l’installation. La valeur déclarée doit correspondre à la valeur de remplacement à neuf de l’ensemble de l’installation. Vous devez en cas de sinistre, nous donner tout justificatif de l’exactitude de la déclaration des capitaux (factures, inventaires ...). S’il est constaté que la valeur déclarée est inférieure à la valeur de remplacement à neuf de l’ensemble de l’installation, vous supporteriez la réduction d’indemnité prévue à l’Article L.121.5 du Code des assurances.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA34','Conditions de garantie suite à la solidification de la matière', 
N'En cas de sinistre, nous garantissons les dommages résultant de la solidification des produits ou matière en cours de fabrication ou de traitement sous réserve :
- qu’ils  soient la conséquence d’un dommage matériel garanti atteignant la machine assurée.
Au titre de ces dommages garantis, nous prenons en charge :
- la réparation de la machine sinistrée,
- la perte de produits ou matière en cours de fabrication ou de traitement pour un montant maximum de 1500 euros,
- les frais engagés pour retirer les produits ou matière solidifiés dans la limite de 5 % de la valeur de remplacement à neuf au jour du sinistre, de la machine sinistrée.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA36','Disposition en cas de non utilisation des dispositifs de sécurité', 
N'Lorsque le sinistre résulte ou est aggravé par la mise hors service ou la non utilisation de dispositifs de sécurité équipant l’engin, et que les conditions d’exploitation du matériel nécessitent l’utilisation de dispositifs de sécurité, la franchise est portée à 10 % du montant du dommage avec un minimum égal à 2 fois la franchise de base précisée dans l’inventaire des biens assurés.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA38','Vétusté applicable sur les pièces ou organes sujets à usure', 
N'Sur les pièces ou organes sujets à usure, notamment les tuyaux, flexibles, pneurides, tapis, vessies, membranes, une vétusté est appliquée en fonction du rapport existant entre la durée de fonctionnement réelle depuis leur dernier remplacement et leur durée de vie normale.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA40','Extension au vol sans effraction : applicable aux matériels agricoles exploités à l’adresse du risque', 
N'Par dérogation aux Exclusions générales des Conditions générales, la garantie est étendue au vol sans effraction ou violence. Elle s’exerce à l’adresse du risque dans les bâtiments d’exploitation et cours attenantes.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA47','Dispositions applicables aux sources d’émission ou de réception utilisées dans les endoscopes médicaux et les scanners médicaux', 
N'Conformément aux Conditions générales, ne sont pas garantis les dommages limités aux pièces, éléments ou outils, ou composants de machines qui nécessitent de par leur fonctionnement un remplacement périodique. Ces dispositions s’appliquent notamment aux : lampes, lasers (tube d’émission, cellules de réception), transducteurs.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA48','Dispositions spécifiques aux sondes d’échographie', 
N'Les dommages aux sondes d’échographe désignées au contrat ne donnent lieu à indemnisation que s’ils résultent d’un incendie, d’un vol, d’une chute, d’un choc, d’un dégât des liquides, ou d’un événement n’ayant aucun rapport avec leur usure naturelle. En cas de dommage garanti atteignant une sonde et limité à celle-ci, il sera appliqué une vétusté de 2 % par mois à compter de la date de 1ère mise en service. Cette vétusté est plafonnée à 80 %.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA51','Exclusion des défibrillateurs à usage public', 
N'La garantie ne s’applique pas lorsque le défibrillateur est à usage public.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA52','Extension de garantie relative aux défibrillateurs à usage public', 
N'Par dérogation aux Conditions générales nous garantissons le vol commis avec ou sans effraction, avec ou sans violence, des défibrillateurs à usage public. En cas de vol, la franchise de base indiquée ci-avant dans l’inventaire des biens assurés est multipliée par 2.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA53','Dispositions spécifiques aux enneigeurs', 
N'Franchise applicable aux enneigeurs dans le cas d’un sinistre résultant d’une avalanche artificielle, déclenchée volontairement. Outre la franchise générale du contrat toujours déduite de l’indemnisation, en cas de dommages aux enneigeurs suite à avalanche artificielle déclenchée volontairement, nous n’indemniserons les enneigeurs qu’à compter du sixième enneigeur sinistré. Si moins de six enneigeurs sont sinistrés, aucune indemnisation n’est due à l’assuré au titre des enneigeurs.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA59','Conditions de garantie Vol des échafaudages', 
N'Dans vos locaux : 
- le vol des échafaudages est garanti à condition qu’il ait été commis avec effraction, ou violence.
En dehors de vos locaux :
- le vol des échafaudages sur le lieu du chantier est garanti, avec ou sans effraction ou violence, à condition que les échafaudages soient montés, 
- le vol des échafaudages entreposés au sol sur les lieux de chantier ou dans un véhicule au cours des déplacements est exclu.
Franchise spécifique vol :
En cas de vol, la franchise sera égale à 5 % de la valeur de remplacement à neuf  au jour du sinistre, des échafaudages volés.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA60','Conditions de garantie Vol des banches et coffrages', 
N'En dehors des lieux de chantier (dans vos locaux et en cours de transport) : le vol des banches et coffrages est garanti à condition qu’il ait été commis avec effraction, ou violence. Sur le lieu du chantier : le vol des banches et coffrages est garanti avec ou sans effraction, avec ou sans violence.
Franchise spécifique vol : 
En cas de vol, la franchise sera égale à 5 % de la valeur de remplacement à neufs  au jour du sinistre, des banches et coffrages volés.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA61', 'Exclusions relatives à la garantie Vol des appareil photo, caméras, objectifs photographiques', 
N'En complément des exclusions des Conditions générales, ne sont pas garantis :
- les pertes et les disparitions inexpliquées ;
- les vols commis sans menace, sans agression, ou sans effraction des locaux ou des véhicules dans lesquels se trouvent les biens assurés ;
- les vols survenus dans un lieu public (notamment les transports en commun, les gares et les aéroports) des biens laissés sans surveillance directe et immédiate de l’assuré ou des personnes qui l’accompagnent.
- le vol des biens laissés dans un véhicule, si le vol n’est pas consécutif à une effraction du véhicule, ou au vol du véhicule, ou à un accident de circulation ;
- le vol des biens laissés visibles de l’extérieur dans un véhicule en stationnement ; 
- le vol des biens laissés dans un véhicule en stationnement, commis la nuit entre 22h00 et 7h00 du matin ;
- les vols des biens laissés dans un véhicule non totalement carrossé en matériaux durs.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA62','Exclusions relatives à la garantie dommages des appareils photo, caméras, objectifs photographiques', 
N'En complément des exclusions des Conditions générales ne sont pas garantis :
- les dommages qui sont pris en charge dans le cadre de la garantie "constructeur" ;
- les dommages d’ordre esthétique causés aux parties extérieures des biens ne nuisant pas à son bon fonctionnement (par exemple : les rayures, éraflures, écaillures, piqûres, tâches) ;
- les dommages aux biens non pris en bagage à main dans les transports en commun (terrestre, aérien ou maritime).');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA63','Détermination de l’indemnité – application d’une vétusté forfaitaire des appareils photo et caméras', 
N'En cas de sinistre partiel : sans pouvoir excéder la somme fixée si nécessaire par expertise, le montant de l’indemnité est égal aux frais de réparation, sans application de vétusté ;
En cas de sinistre total : sans pouvoir excéder la somme fixée si nécessaire par expertise, le montant de l’indemnité est égale :
- pendant les trois premières années suivant la date de première mise en service : à la valeur de remplacement à neuf au jour du sinistre,
- après les trois premières années suivant la date de première mise en service : à la valeur de remplacement à neuf au jour du sinistre déduction faite d’une vétusté de 0,70% par mois depuis la date de première mise en service. Cette vétusté est limitée à 60%.
Il sera toujours fait déduction de la franchise et des valeurs de sauvetage s’il y a lieu.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA65','Condition de garantie des capteurs plan', 
N'Les dommages aux capteurs plan ne donnent lieu à indemnisation que s’ils résultent d’un incendie, d’un vol, d’une chute, d’un choc ou d’un événement n’ayant aucun rapport avec leur usure naturelle. En cas de dommage garanti, il sera appliqué :
- pendant les 24 premiers mois suivant la date de 1ère mise en service : une vétusté nulle ;
- après les 24 premiers mois : une vétusté égale à 0,9 % par mois à partir de la date de 1ère mise en service. Cette vétusté est plafonnée à 70 %.
Franchise en cas de sinistre limité à un capteur plan : il sera déduit de l’indemnité une franchise égale à 5 % de sa valeur de remplacement à neuf au jour du sinistre.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA66','Condition de garantie des cabanes de chantier', 
N'Exclusion du contenu :
Il est rappelé que la garantie s’applique uniquement aux éléments constituants la cabane de chantier. En conséquence, ne sont pas garantis l’ensemble du mobilier, produits, objets, valeurs, matériels, équipements, matériaux, marchandises, contenus dans la cabane de chantier. 

Franchise en cas de vol, de vandalisme ou d’incendie : 
La franchise est portée à 5 % de la valeur de remplacement à neuf du bien assuré sinistré, avec un minimum égal à la franchise de base indiquée dans le paragraphe « Inventaire des biens assurés ».');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA67', 'Conditions de garantie vol des distributeurs de glaces et rôtissoires', 
N'Le matériel est garanti en tous lieux sous réserve, qu’en dehors des marchés, le matériel soit remisé dans un endroit clos et fermé à clef.
Franchise spécifique en cas de vol : la franchise de base indiquée ci-avant dans l’inventaire des biens assurés est multipliée par 5.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA70','Exclusion des pertes de liquide ou de gaz des IRM', 
N'Sont exclus toutes pertes d’héium se présentant sous format liquide ou gazeux qui ne font pas suite à un dommage accidentel ayant endommagé d’autres parties de la machine. Sont notamment exclus la perte d’hélium :
- par défaut s’isolation dans le système thermique,
- par déclenchement volontaire de l‘évacuation de l’hélium de la cuve,
- par activation du dispositif de sécurité déclenché par le personnel, volontairement ou involontairement.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA71','Exclusion des pertes de liquide ou de gaz des appareils de cryothérapie', 
N'Sont exclus toutes pertes d’azote se présentant sous format liquide ou gazeux qui ne font pas suite à un dommage matériel ayant endommagé d’autres parties de la machine.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA90','Conditions de garantie vol pour les engins et matériels', 
N'Il est rappelé que les vols commis sans effraction ou violence sont exclus. Toutefois, par dérogation aux Conditions générales, pour les engins et matériels d’une valeur de remplacement à neuf au jour de la souscription supérieure à 25 000 euros, la garantie vol est étendue au vol sans effraction ou violence.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA91','Franchise spécifique vol pour les engins et matériels', 
N'La franchise en cas de vol est portée à 5 % de la valeur de remplacement à neuf au jour du sinistre, de l’engin et/ou matériel volé avec un minimum égal à la franchise de base indiquée dans le paragraphe « Inventaire des biens assurés ».');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA92', 'Conditions de garantie des outils et accessoires des engins de chantier', 
N'Conformément aux Conditions générales, les vols d’outils et accessoires utilisés par les engins (godet, grappin, brise-roche, marteau, cisaille, tilrotator, lame...), entreposés au sol ou non montés sur les engins, sont garantis à condition qu’ils aient été commis avec effraction ou violence. Toutefois, par dérogation aux Conditions générales, les vols limités aux outils et accessoires montés sur les engins sont garantis avec ou sans effraction ou violence. 

Franchise spécifique vol : en cas de vol limité aux outils et accessoires, la franchise sera égale à 10 % de la valeur de remplacement à neuf  au jour du sinistre, du ou des outils et accessoires volés, avec un minimum de 800 euros.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA93','Conditions de garantie des outils et accessoires des engins agricoles', 
N'Par dérogation aux Conditions générales, les vols limités aux outils et accessoires  utilisés par les engins agricoles, sont garantis avec ou sans effraction ou violence. 

Franchise spécifique vol :
En cas de vol limité aux outils et accessoires, la franchise sera égale à 5 % de la valeur de remplacement à neufs  au jour du sinistre, du ou des outils et accessoires volés, avec un minimum de 800 euros.');

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA75','Garantie des accessoires', 
N'Les accessoires des appareils photos, caméras et objectifs photographiques, sont garantis :
- à la condition que leurs valeurs soient comprises dans les capitaux déclarés,
- et à condition que les dommages ou les vols les affectant fassent également suite à un dommage ou un vol des appareils photos, caméras, objectifs.
Par définition sont considérés comme accessoires les biens suivants : les cartes mémoires, chargeurs, batteries, sacs de protection, trépieds, stabilisateurs et télécommandes.');

UPDATE [sch_mchn].[T_EDITION_CLAUSE] SET TYPE = 'VOL' WHERE CODE IN ('MA13','MA40','MA52','MA59','MA60','MA61','MA67','MA90','MA91','MA92','MA93');