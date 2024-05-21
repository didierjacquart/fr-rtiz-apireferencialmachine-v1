-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 19/12/2023
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : maj edition clause sur machines PV
-------------------------------------------------------------------------
USE [mchn]

update  [sch_mchn].[T_EDITION_CLAUSE] SET [DESCRIPTION]=N'Du stockage de fourrage, de c�r�ales ou de paille (sur plus de 10% de la surface du b�timent).' WHERE CODE = 'DECLA007';
update  [sch_mchn].[T_EDITION_CLAUSE] SET [DESCRIPTION]=N'De l��levage intensif' WHERE CODE = 'DECLA008';
update  [sch_mchn].[T_EDITION_CLAUSE] SET [DESCRIPTION]=N'De l��levage de volaille, porcs/porcelets, lapins/laperaux.' WHERE CODE = 'DECLA009';