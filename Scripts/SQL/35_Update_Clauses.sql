-------------------------------------------------------------------------
-- Auteur    : S857465
-- Date      : 12/12/2022
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : update clauses
-------------------------------------------------------------------------
USE [mchn]

delete from [sch_mchn].[T_EDITION_CLAUSE] where CODE = 'PA95';

UPDATE [sch_mchn].[T_EDITION_CLAUSE] SET TYPE = 'VOL' WHERE CODE IN ('PA10','PA11','PA90','PA92','PA94');

UPDATE [sch_mchn].[T_EDITION_CLAUSE] SET TYPE = null WHERE CODE = 'PA12';

UPDATE [sch_mchn].[T_EDITION_CLAUSE] SET DESCRIPTION = N'Par dérogation aux Conditions générales, la garantie des moteurs de traction des engins automoteurs est limitée à tout dommage matériel résultant :
- d’un accident de circulation, d’une collision, d’un choc contre un corps fixe ou mobile, 
- d’un renversement, d’un effondrement, d’un affaissement de terrain, 
- d’une chute à l’eau, d’un contact avec des fumées, liquide ou gaz,
- d’un incendie, d’une explosion, 
- d’un vol ou d’un vandalisme, si la garantie Vol est souscrite sur l’ensemble du contrat,
- d’un événement naturel, 
- d’un attentat ou actes de terrorisme.' WHERE CODE = 'PA26';

UPDATE [sch_mchn].[T_EDITION_CLAUSE] SET DESCRIPTION = N'Par dérogation aux Conditions générales :
En cas de sinistre partiel : sans pouvoir excéder la somme fixée si nécessaire par expertise, le montant de l''indemnité est égal aux frais de réparation, sans application de vétusté ; 
En cas de sinistre total : sans pouvoir excéder la somme fixée si nécessaire par expertise, le montant de l''indemnité est égal :
- pendant les trois premières années suivant la date de première mise en service : à la valeur de remplacement à neuf au jour du sinistre, 
- après les trois premières années suivant la date de première mise en service : à la valeur de remplacement à neuf au jour du sinistre déduction faite d''une vétusté de 0,70% par mois depuis la date de première mise en service. Cette vétusté est limitée à 60%. 
Il sera toujours fait déduction de la franchise et des valeurs de sauvetage s''il y a lieu.' WHERE CODE = 'MA63';