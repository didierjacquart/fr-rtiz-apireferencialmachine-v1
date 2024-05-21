-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 06/04/2022
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : Ajout d'une colonne à la table des machines
--             pour notion de machine mobile
------------------------------------------------------------------------- 
USE [mchn]

ALTER TABLE [sch_mchn].[T_MACHINE_SPECIFICATION] ADD [IS_TRANSPORTABLE] [bit] NULL;