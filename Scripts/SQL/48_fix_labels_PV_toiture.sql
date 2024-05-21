-------------------------------------------------------------------------
-- Auteur    : S862460
-- Date      : 22/06/2023
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : corrections libellés
-------------------------------------------------------------------------

USE [mchn]

UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]= N'Installation sur toiture' WHERE [CODE]='80003';

