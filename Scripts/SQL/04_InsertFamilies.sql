-------------------------------------------------------------------------
-- Auteur    : S838981
-- Date      : 08/12/2021
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : rechargement des familles
-------------------------------------------------------------------------

USE [mchn]

delete from [sch_mchn].[T_FAMILY];

INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('6','BTP','1','Terrassement et chargement')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('10','Manutention','1','Industrie et entrepot')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('6','BTP','2','Levage - manutention')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('11','Médical-Paramédical','1','Autre')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('9','Images et sons','1','Photo-Vidéo')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('12','Travail des matières','1','Métal')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('6','BTP','3','Divers')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('9','Images et sons','2','Autre')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('8','Contrôles et mesures','1','Multi-Usages')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('1','Agricole','1','Forestier')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('8','Contrôles et mesures','2','Topographie')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('11','Médical-Paramédical','2','Imagerie, mesure et analyse')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('4','Autre','1','Libre-service')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('4','Autre','2','Sécurité et Protection')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('3','Automobile','1','Station-service')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('7','Bureautique et Imprimerie','1','Bureautique')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('1','Agricole','2','Entretien des espaces verts')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('6','BTP','4','Routes - Voirie')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('7','Bureautique et Imprimerie','2','Imprimerie Presse')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('11','Médical-Paramédical','3','Equipements de spécialité médicale')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('4','Autre','3','Energie')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('1','Agricole','3','Multi-Usages')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('4','Autre','4','Assainissement et Hygiène')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('10','Manutention','2','Multi-Usages')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('3','Automobile','2','Outils techniques')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('12','Travail des matières','2','Bois')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('2','Alimentaire','1','Café et restaurant')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('4','Autre','5','Conditionnement et stockage')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('2','Alimentaire','2','Industrie - artisanat')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('1','Agricole','4','Culture')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('11','Médical-Paramédical','4','Kinésithérapie et rééducation')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('6','BTP','5','Fabrication et transport du béton')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('12','Travail des matières','3','Pierre et ciment')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('7','Bureautique et Imprimerie','3','Imprimerie Façonnage')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('11','Médical-Paramédical','5','Optique et audition')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('8','Contrôles et mesures','3','Automobile')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('12','Travail des matières','4','Caoutchouc et plastique')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('11','Médical-Paramédical','6','Esthétique')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('4','Autre','6','Multi-secteurs')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('1','Agricole','5','Irrigation')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('1','Agricole','6','Elevage et laiterie')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('9','Images et sons','3','Sono')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('1','Agricole','7','Vinification et cidrerie')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('12','Travail des matières','5','Textile')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('4','Autre','7','Blanchisserie - Laverie')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('4','Autre','8','Télécom')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('1','Agricole','8','Conditionnement et stockage')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('4','Autre','9','Ski - Neige')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('8','Contrôles et mesures','4','Eléctricité')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('6','BTP','6','Topographie')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('10','Manutention','3','BTP')
INSERT INTO [sch_mchn].[T_FAMILY] ([CODE],[NAME], [SUB_CODE],[SUB_NAME])  VALUES  ('10','Manutention','4','Agricole')