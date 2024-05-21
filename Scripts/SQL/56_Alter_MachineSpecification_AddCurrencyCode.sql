-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 12/03/2024
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : Ajout colonnes CURRENCY_CODE
-------------------------------------------------------------------------
USE [mchn]

ALTER TABLE [sch_mchn].[T_MACHINE_SPECIFICATION] ADD FINANCIAL_VALUATION_MACHINE_CURRENCY nvarchar(10) null
GO
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [FINANCIAL_VALUATION_MACHINE_CURRENCY]='EUR' WHERE CODE IN ('85001', '85002', '85003');
GO
