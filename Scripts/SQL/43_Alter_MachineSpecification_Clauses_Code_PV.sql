-------------------------------------------------------------------------
-- Auteur    : S862460
-- Date      : 01/02/2023
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : Update de la structure des Clauses pour recevoir les Clause PV
-------------------------------------------------------------------------
USE [mchn]

-- Update [CODE] Size 8 > 15
ALTER TABLE [sch_mchn].[T_EDITION_CLAUSE] ALTER COLUMN [CODE] [nvarchar](15) NOT NULL;
