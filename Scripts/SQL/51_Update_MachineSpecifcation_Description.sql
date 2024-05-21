-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 28/11/2023
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : maj description sur machines PV
-------------------------------------------------------------------------
USE [mchn]

update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET [DESCRIPTION]=N'Dispositif se substituant � la toiture de b�timents agricoles, convertissant une partie du�rayonnement solaire�en��nergie �lectrique, gr�ce � des�capteurs solaires photovolta�ques.' WHERE code = '80010';
update  [sch_mchn].[T_MACHINE_SPECIFICATION] SET [DESCRIPTION]=N'Dispositif se substituant � la toiture de b�timents non agricoles, convertissant une partie du�rayonnement solaire�en��nergie �lectrique, gr�ce � des�capteurs solaires photovolta�ques.' WHERE code = '80011';