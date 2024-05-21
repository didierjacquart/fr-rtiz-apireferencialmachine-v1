-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 19/12/2023
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : correction edition clause sur machines PV
-------------------------------------------------------------------------
USE [mchn]

update  [sch_mchn].[T_EDITION_CLAUSE] SET [DESCRIPTION]=N'De l’élevage intensif.' WHERE CODE = 'DECLA008';