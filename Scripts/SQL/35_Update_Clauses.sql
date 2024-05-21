-------------------------------------------------------------------------
-- Auteur    : S857465
-- Date      : 12/12/2022
-- Base      : Sql Server - mchn
-- Version	 : 1.0.0
-- Projet	 : OSE BDM
-- Objet     : update clauses
-------------------------------------------------------------------------
USE [mchn]

delete from [sch_mchn].[T_EDITION_CLAUSE] where CODE = 'PA95';

UPDATE [sch_mchn].[T_EDITION_CLAUSE] SET TYPE = 'VOL' WHERE CODE IN ('PA10','PA11','PA90','PA92','PA94');

UPDATE [sch_mchn].[T_EDITION_CLAUSE] SET TYPE = null WHERE CODE = 'PA12';

UPDATE [sch_mchn].[T_EDITION_CLAUSE] SET DESCRIPTION = N'Par d�rogation aux Conditions g�n�rales, la garantie des moteurs de traction des engins automoteurs est limit�e � tout dommage mat�riel r�sultant :
- d�un accident de circulation, d�une collision, d�un choc contre un corps fixe ou mobile, 
- d�un renversement, d�un effondrement, d�un affaissement de terrain, 
- d�une chute � l�eau, d�un contact avec des fum�es, liquide ou gaz,
- d�un incendie, d�une explosion, 
- d�un vol ou d�un vandalisme, si la garantie Vol est souscrite sur l�ensemble du contrat,
- d�un �v�nement naturel, 
- d�un attentat ou actes de terrorisme.' WHERE CODE = 'PA26';

UPDATE [sch_mchn].[T_EDITION_CLAUSE] SET DESCRIPTION = N'Par d�rogation aux Conditions g�n�rales :
En cas de sinistre partiel : sans pouvoir exc�der la somme fix�e si n�cessaire par expertise, le montant de l''indemnit� est �gal aux frais de r�paration, sans application de v�tust� ; 
En cas de sinistre total : sans pouvoir exc�der la somme fix�e si n�cessaire par expertise, le montant de l''indemnit� est �gal :
- pendant les trois premi�res ann�es suivant la date de premi�re mise en service : � la valeur de remplacement � neuf au jour du sinistre, 
- apr�s les trois premi�res ann�es suivant la date de premi�re mise en service : � la valeur de remplacement � neuf au jour du sinistre d�duction faite d''une v�tust� de 0,70% par mois depuis la date de premi�re mise en service. Cette v�tust� est limit�e � 60%. 
Il sera toujours fait d�duction de la franchise et des valeurs de sauvetage s''il y a lieu.' WHERE CODE = 'MA63';