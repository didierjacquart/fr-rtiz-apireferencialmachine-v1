-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 19/01/2022
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : fix sur le type de la colonne rate des coef de prix
-------------------------------------------------------------------------

ALTER TABLE [sch_mchn].[T_PRICING_RATE]
ALTER COLUMN [RATE] decimal(18,8);