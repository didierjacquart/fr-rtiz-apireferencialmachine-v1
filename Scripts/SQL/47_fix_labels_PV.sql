-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 09/05/2023
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : corrections libellés
-------------------------------------------------------------------------

USE [mchn]

UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]= N'Centrale au sol', [LABEL]= N'Centrale au sol', [DESCRIPTION]= N'Dispositif, fixe ou mobile (tracker) posé au sol convertissant une partie du rayonnement solaire en énergie électrique, grâce à des capteurs solaires photovoltaïques.' WHERE [CODE]='80001';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]= N'Installation sur toiture de bâtiment agricole', [LABEL]= N'Installation sur toiture', [DESCRIPTION]= N'Dispositif monté sur des bâtiments agricoles convertissant une partie du rayonnement solaire en énergie électrique, grâce à des capteurs solaires photovoltaïques.' WHERE [CODE]='80002';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]= N'Installation sur toiture de bâtiment non agricole', [LABEL]= N'Installation sur toiture', [DESCRIPTION]= N'Dispositif monté sur des bâtiments non agricoles, convertissant une partie du rayonnement solaire en énergie électrique, grâce à des capteurs solaires photovoltaïques.' WHERE [CODE]='80003';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]= N'Installation sur ombrière de parking', [LABEL]= N'Installation sur ombrière', [DESCRIPTION]= N'Dispositif monté sur ombrière de parking convertissant une partie du rayonnement solaire en énergie électrique, grâce à des capteurs solaires photovoltaïques.' WHERE [CODE]='80004';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]= N'Installation agrivoltaïque', [LABEL]= N'Centrale agrivoltaïque', [DESCRIPTION]= N'Dispositif installé au-dessus des cultures agricoles, convertissant une partie du rayonnement solaire en énergie électrique, grâce à des capteurs solaires photovoltaïques et protégeant des aléas climatiques.' WHERE [CODE]='80005';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]= N'Installation sur serre', [LABEL]= N'Installation sur serre', [DESCRIPTION]= N'Dispositif monté sur serre convertissant une partie du rayonnement solaire en énergie électrique, grâce à des capteurs solaires photovoltaïques.' WHERE [CODE]='80006';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]= N'Installation en panneaux souples et membranes flexibles', [LABEL]= N'Installation souple', [DESCRIPTION]= N'Dispositif monté sur toiture et composé de membranes flexibles ou de panneaux souples munis de capteurs solaires photovoltaïques convertissant une partie du rayonnement solaire en énergie électrique.' WHERE [CODE]='80007';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]= N'Installation aérovoltaïque', [LABEL]= N'Centrale aérovoltaïque', [DESCRIPTION]= N'Dispositif hybride installé en toiture, convertissant une partie du rayonnement solaire en énergie électrique d’une part, grâce à des capteurs solaires photovoltaïques, et en chaleur d’autre part, grâce à des capteurs thermiques positionnés sur la face interne des panneaux.' WHERE [CODE]='80008';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]= N'Installation en façade', [LABEL]= N'Installation en façade', [DESCRIPTION]= N'Dispositif monté en façade de bâtiment, convertissant une partie du rayonnement solaire en énergie électrique, grâce à des capteurs solaires photovoltaïques.' WHERE [CODE]='80009';

DELETE [sch_mchn].[T_EDITION_CLAUSE] WHERE [CLAUSE_TYPE] LIKE 'DECLARATION';

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES (N'DECLA001', N'DECLARATION', N'D’enregistrement, autorisation ou régime SEVESO au titre des ICPE.');
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES (N'DECLA002', N'DECLARATION', N'De collecte, entreposage, tri ou traitement de déchets de toute nature.');
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES (N'DECLA003', N'DECLARATION', N'De traitement de surface des matériaux.');
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES (N'DECLA004', N'DECLARATION', N'De travail du bois (y compris scierie).');
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES (N'DECLA005', N'DECLARATION', N'De distillation et stockage d’alcool pur.');
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES (N'DECLA006', N'DECLARATION', N'De fabrication et/ou entreposage de produits inflammables ou explosifs.');
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES (N'DECLA007', N'DECLARATION', N'Au stockage de fourrage, de céréales ou de paille.');
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES (N'DECLA008', N'DECLARATION', N'A l’élevage intensif.');
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES (N'DECLA009', N'DECLARATION', N'A l’élevage de volaille, porcs/porcelets, lapins/laperaux.');


