-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 12/04/2022
-- Base      : Sql Server - xbdm
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : supression des types vides
-------------------------------------------------------------------------

USE [mchn]
GO

UPDATE [sch_mchn].[T_EDITION_CLAUSE] set TYPE=null where TYPE='';