-------------------------------------------------------------------------
-- Auteur    : S862460
-- Date      : 26/01/2023
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : Update de la structure des Clauses pour recevoir les Clause PV
-------------------------------------------------------------------------
USE [mchn]

-- Update [CODE] Size 4 > 8
ALTER TABLE [sch_mchn].[T_EDITION_CLAUSE] ALTER COLUMN [CODE] [nvarchar](8) NOT NULL;
-- Update [LABEL] Constraint NOT NULL > NULL
ALTER TABLE [sch_mchn].[T_EDITION_CLAUSE] ALTER COLUMN [LABEL] [nvarchar](150) NULL;

-- CLAUSE_TYPE = { DECLARATION, EDITION }
ALTER TABLE [sch_mchn].[T_EDITION_CLAUSE] ADD [CLAUSE_TYPE] [nvarchar](15) NOT NULL DEFAULT 'EDITION';