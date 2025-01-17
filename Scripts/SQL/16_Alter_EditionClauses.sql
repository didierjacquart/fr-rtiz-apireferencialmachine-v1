-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 18/03/2022
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : Modiciation de la table T_EDITION_CLAUSE
------------------------------------------------------------------------- 
USE [mchn]

DELETE [sch_mchn].[T_EDITION_CLAUSE];

ALTER TABLE [sch_mchn].[T_EDITION_CLAUSE] DROP COLUMN [CONTENT];
ALTER TABLE [sch_mchn].[T_EDITION_CLAUSE] DROP COLUMN [VOL];
ALTER TABLE [sch_mchn].[T_EDITION_CLAUSE] DROP COLUMN [LABEL];
ALTER TABLE [sch_mchn].[T_EDITION_CLAUSE] ADD [LABEL][nvarchar](150) NOT NULL;
ALTER TABLE [sch_mchn].[T_EDITION_CLAUSE] ADD [TYPE][nvarchar](35) NULL;
ALTER TABLE [sch_mchn].[T_EDITION_CLAUSE] ADD [DESCRIPTION][nvarchar](4000) NULL;