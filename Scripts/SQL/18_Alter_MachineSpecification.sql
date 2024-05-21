-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 18/03/2022
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : Ajout d'une colonne à la table des machines
--             pour le lien entre les machines et les clauses
------------------------------------------------------------------------- 
USE [mchn]

ALTER TABLE [sch_mchn].[T_MACHINE_SPECIFICATION] ADD [EDITION_CLAUSE_CODES][nvarchar](50) NULL;



