-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 25/03/2022
-- Base      : Sql Server - xbdm
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : retrait top vol pour clause MA66
-------------------------------------------------------------------------

USE [mchn]
GO

UPDATE [sch_mchn].[T_EDITION_CLAUSE] set TYPE=null where CODE='MA66';