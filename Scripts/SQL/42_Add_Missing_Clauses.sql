-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 27/01/2023
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : Ajout MA54
------------------------------------------------------------------------- 
USE [mchn]

DELETE FROM [sch_mchn].[T_EDITION_CLAUSE] WHERE CODE='MA54';

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] (CODE, LABEL, DESCRIPTION)
VALUES
('MA54',N'Dispositions sp�cifiques aux bennes', 
N'Par d�rogation aux Conditions g�n�rales, la garantie vol est �tendue au vol sans effraction ou violence.

En cas de vol la franchise sera �gale � : 700 euros.');
   
UPDATE [sch_mchn].[T_EDITION_CLAUSE] SET TYPE = 'VOL' WHERE CODE = 'MA54';