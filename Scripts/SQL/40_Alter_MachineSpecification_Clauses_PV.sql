-------------------------------------------------------------------------
-- Auteur    : S862460
-- Date      : 26/01/2023
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : Update de la structure de T_MACHINE_SPECIFICATION.EDITION_CLAUSE_CODES pour recevoir les Clauses PV
-------------------------------------------------------------------------
USE [mchn]

-- Update [CODE] Size 50 > 100
ALTER TABLE [sch_mchn].[T_MACHINE_SPECIFICATION] ALTER COLUMN EDITION_CLAUSE_CODES [nvarchar](100) NULL;