-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 09/05/2023
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : corrections libell�s
-------------------------------------------------------------------------

USE [mchn]

UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]= N'Centrale au sol', [LABEL]= N'Centrale au sol', [DESCRIPTION]= N'Dispositif, fixe ou mobile (tracker) pos� au sol convertissant une partie du�rayonnement solaire�en��nergie �lectrique, gr�ce � des�capteurs solaires photovolta�ques.' WHERE [CODE]='80001';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]= N'Installation sur toiture de b�timent agricole', [LABEL]= N'Installation sur toiture', [DESCRIPTION]= N'Dispositif mont� sur des b�timents agricoles convertissant une partie du�rayonnement solaire�en��nergie �lectrique, gr�ce � des�capteurs solaires photovolta�ques.' WHERE [CODE]='80002';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]= N'Installation sur toiture de b�timent non agricole', [LABEL]= N'Installation sur toiture', [DESCRIPTION]= N'Dispositif mont� sur des b�timents non agricoles, convertissant une partie du�rayonnement solaire�en��nergie �lectrique, gr�ce � des�capteurs solaires photovolta�ques.' WHERE [CODE]='80003';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]= N'Installation sur ombri�re de parking', [LABEL]= N'Installation sur ombri�re', [DESCRIPTION]= N'Dispositif mont� sur ombri�re de parking convertissant une partie du�rayonnement solaire�en��nergie �lectrique, gr�ce � des�capteurs solaires photovolta�ques.' WHERE [CODE]='80004';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]= N'Installation agrivolta�que', [LABEL]= N'Centrale agrivolta�que', [DESCRIPTION]= N'Dispositif install� au-dessus des cultures agricoles, convertissant une partie du rayonnement solaire en �nergie �lectrique, gr�ce � des capteurs solaires photovolta�ques et prot�geant des al�as climatiques.' WHERE [CODE]='80005';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]= N'Installation sur serre', [LABEL]= N'Installation sur serre', [DESCRIPTION]= N'Dispositif mont� sur serre convertissant une partie du rayonnement solaire en �nergie �lectrique, gr�ce � des capteurs solaires photovolta�ques.' WHERE [CODE]='80006';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]= N'Installation en panneaux souples et membranes flexibles', [LABEL]= N'Installation souple', [DESCRIPTION]= N'Dispositif mont� sur toiture et compos� de membranes flexibles ou de panneaux souples munis de capteurs solaires photovolta�ques convertissant une partie du rayonnement solaire en �nergie �lectrique.' WHERE [CODE]='80007';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]= N'Installation a�rovolta�que', [LABEL]= N'Centrale a�rovolta�que', [DESCRIPTION]= N'Dispositif hybride install� en toiture, convertissant une partie du rayonnement solaire en �nergie �lectrique d�une part, gr�ce � des capteurs solaires photovolta�ques, et en chaleur d�autre part, gr�ce � des capteurs thermiques positionn�s sur la face interne des panneaux.' WHERE [CODE]='80008';
UPDATE [sch_mchn].[T_MACHINE_SPECIFICATION] SET [NAME]= N'Installation en fa�ade', [LABEL]= N'Installation en fa�ade', [DESCRIPTION]= N'Dispositif mont� en fa�ade de b�timent, convertissant une partie du�rayonnement solaire�en��nergie �lectrique, gr�ce � des�capteurs solaires photovolta�ques.' WHERE [CODE]='80009';

DELETE [sch_mchn].[T_EDITION_CLAUSE] WHERE [CLAUSE_TYPE] LIKE 'DECLARATION';

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES (N'DECLA001', N'DECLARATION', N'D�enregistrement, autorisation ou r�gime SEVESO au titre des ICPE.');
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES (N'DECLA002', N'DECLARATION', N'De collecte, entreposage, tri ou traitement de d�chets de toute nature.');
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES (N'DECLA003', N'DECLARATION', N'De traitement de surface des mat�riaux.');
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES (N'DECLA004', N'DECLARATION', N'De travail du bois (y compris scierie).');
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES (N'DECLA005', N'DECLARATION', N'De distillation et stockage d�alcool pur.');
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES (N'DECLA006', N'DECLARATION', N'De fabrication et/ou entreposage de produits inflammables ou explosifs.');
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES (N'DECLA007', N'DECLARATION', N'Au stockage de fourrage, de c�r�ales ou de paille.');
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES (N'DECLA008', N'DECLARATION', N'A l��levage intensif.');
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES (N'DECLA009', N'DECLARATION', N'A l��levage de volaille, porcs/porcelets, lapins/laperaux.');


