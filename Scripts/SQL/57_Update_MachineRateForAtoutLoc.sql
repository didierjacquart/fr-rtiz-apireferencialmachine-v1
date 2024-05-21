-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 15/03/2024
-- Base      : Sql Server - xbdm
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : Mise à jour du champ machineRate Atouts Location
-------------------------------------------------------------------------
USE [mchn]
GO

UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET MACHINE_RATE = 1 WHERE PRODUCT = 'RENTAL_MACHINE';