-------------------------------------------------------------------------
-- Auteur    : S862460
-- Date      : 26/01/2023
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : Ajout des Clauses PV
-------------------------------------------------------------------------
USE [mchn]

DELETE FROM [sch_mchn].[T_EDITION_CLAUSE] WHERE CODE IN ('DECLA001', 'DECLA002', 'DECLA003', 'DECLA004', 'DECLA005', 'DECLA006', 'DECLA007', 'DECLA008', 'DECLA009')

INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES ('DECLA001','DECLARATION','D’enregistrement, autorisation ou régime SEVESO au titre des ICPE.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES ('DECLA002','DECLARATION','De collecte, entreposage, tri ou traitement de déchets de toute nature.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES ('DECLA003','DECLARATION','De traitement de surface des matériaux.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES ('DECLA004','DECLARATION','De travail du bois (y compris scierie).')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES ('DECLA005','DECLARATION','De distillation et stockage d’alcool pur.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES ('DECLA006','DECLARATION','De fabrication et/ou entreposage de produits inflammables ou explosifs.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES ('DECLA007','DECLARATION','Au stockage de fourrage, de céréales ou de paille.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES ('DECLA008','DECLARATION','A l’élevage intensif.')
INSERT INTO [sch_mchn].[T_EDITION_CLAUSE] ([CODE], [CLAUSE_TYPE], [DESCRIPTION]) VALUES ('DECLA009','DECLARATION','A l’élevage de volaille, porcs/porcelets, lapins/laperaux.')
