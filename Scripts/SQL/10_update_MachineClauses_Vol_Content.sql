-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 08/12/2021
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : update des champs vols et content
-------------------------------------------------------------------------

USE [mchn]


update [sch_mchn].[T_MACHINE_EDITION_CLAUSE] 
set VOL = EC.VOL,
	CONTENT = EC.CONTENT
FROM [sch_mchn].[T_EDITION_CLAUSE] EC
WHERE 
[sch_mchn].[T_MACHINE_EDITION_CLAUSE].[CODE] = EC.CODE;