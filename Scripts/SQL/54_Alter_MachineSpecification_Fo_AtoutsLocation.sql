-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 12/03/2024
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : Ajout colonnes pour machines Atouts Location
-------------------------------------------------------------------------
USE [mchn]

ALTER TABLE [sch_mchn].[T_MACHINE_SPECIFICATION] ADD FINANCIAL_VALUATION_MACHINE_MAX int null;
ALTER TABLE [sch_mchn].[T_MACHINE_SPECIFICATION] ADD FINANCIAL_VALUATION_MACHINE_DELEGATED_MAX int null;