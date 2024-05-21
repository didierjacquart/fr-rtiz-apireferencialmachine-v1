-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 15/02/2023
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : Update label pour les exclusions
------------------------------------------------------------------------- 
USE [mchn]

UPDATE [sch_mchn].[T_RISK_PRECISION] SET LABEL= N'Machines exclues:' WHERE CODE = 'EXCLUSIONS' and MACHINE_CODE != '79099';

UPDATE [sch_mchn].[T_RISK_PRECISION] SET 
LABEL = N'Attention, machines exclues à la souscription:',
CODE = 'EXCLUSIONS',
DETAIL = N'Manèges forains, Nouveaux Véhicules Electriques Individuels (Vélos et Trottinettes électriques, Draisienne électriques, Gyropodes, Segway,Hoverboards, ... )'
WHERE CODE = 'DOMAINS' and MACHINE_CODE = '79099';