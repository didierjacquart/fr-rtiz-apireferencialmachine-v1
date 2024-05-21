-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 19/01/2022
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : fix sur le type de la colonne machine rate
------------------------------------------------------------------------
USE [mchn]

ALTER TABLE [sch_mchn].[T_MACHINE_SPECIFICATION]
ALTER COLUMN [MACHINE_RATE] decimal(18,8);