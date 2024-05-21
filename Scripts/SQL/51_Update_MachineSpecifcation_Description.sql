-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 28/11/2023
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : maj description sur machines PV
-------------------------------------------------------------------------
USE [mchn]

update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET [DESCRIPTION]=N'Dispositif se substituant à la toiture de bâtiments agricoles, convertissant une partie du rayonnement solaire en énergie électrique, grâce à des capteurs solaires photovoltaïques.' WHERE code = '80010';
update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET [DESCRIPTION]=N'Dispositif se substituant à la toiture de bâtiments non agricoles, convertissant une partie du rayonnement solaire en énergie électrique, grâce à des capteurs solaires photovoltaïques.' WHERE code = '80011';