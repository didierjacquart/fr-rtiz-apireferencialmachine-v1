-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 14/09/2023
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : corrections libellés
-------------------------------------------------------------------------
USE [mchn]

update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET [KEYWORDS]=N'Analyseur|détecteur|préleveur amiante|plomb' WHERE code = '21053';
update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]=N'Broyeur-concasseur',[LABEL] =N'Broyeur-concasseur' WHERE code = '00071';
update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET [KEYWORDS]=N'Camion à ordures|Poubelle' WHERE code = '05010';
update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET [KEYWORDS]=N'Rotative|Héliographie|Flexographie|Sérigraphie|Tampographie|Hydrolique|A feuille|Typographique|Offset', [NAME]=N'Presse',[LABEL] =N'Presse' WHERE code = '31012';
update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET [KEYWORDS]=N'Presse à cisaillement|Cisaille à levier|hydraulique|Electromécanique|Cisaille guillotine', [NAME]=N'Cisaille',[LABEL] =N'Cisaille' WHERE code = '35202';
update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET [KEYWORDS]=N'Prise de son|tourne-disque', [NAME]=N'Platine disques',[LABEL] =N'Platine disques' WHERE code = '60012';
