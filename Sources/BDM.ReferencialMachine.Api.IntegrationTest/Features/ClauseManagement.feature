Feature: ClauseManagement	

Scenario: Lecture des toutes les clauses	
	Given J ai en base une liste de 3 clauses
	When je demande la liste des clauses
	Then Je retourne HTTP 200 avec la liste des clauses

Scenario: Création d'une nouvelle clause
	Given J ai en base une liste de 3 clauses
	And J ai en en entree le flux d une clause
	When je demande la creation d une clause
	Then Je retourne un code HTTP 201 created

Scenario: Création d'une clause existante
	Given J ai en base une liste de 3 clauses
	And J ai en en entree le flux d une clause existante
	When je demande la creation d une clause
	Then Je retourne un code HTTP 400 badrequest

Scenario: Création d'une clause sans le champ code
	Given J ai en base une liste de 3 clauses
	And J ai en en entree le flux d une clause sans le champ code
	When je demande la creation d une clause
	Then Je retourne un code HTTP 400 badrequest

Scenario: Création d'une clause sans le champ label
	Given J ai en base une liste de 3 clauses
	And J ai en en entree le flux d une clause sans le champ label
	When je demande la creation d une clause
	Then Je retourne un code HTTP 400 badrequest

Scenario: Mise à jour d'une clause existante
	Given J ai en base une liste de 3 clauses
	And J ai en en entree le flux d une clause existante
	When je demande la mise a jour d une clause
	Then Je retourne un code HTTP 200 ok

Scenario: Mise à jour d'une clause inexsitante
	Given J ai en base une liste de 3 clauses
	And J ai en en entree le flux d une clause inexistante
	When je demande la mise a jour d une clause
	Then Je retourne un code HTTP 404 not found

Scenario: Mise à jour d'une clause exsitante avec un code erroné
	Given J ai en base une liste de 3 clauses
	And J ai en en entree le flux d une clause existante avec un code errone
	When je demande la mise a jour d une clause
	Then Je retourne un code HTTP 400 badrequest

Scenario: Suppression d'une clause existante
	Given J ai en base une liste de 3 clauses
	And J ai en en entree le code d une clause existante
	When je demande la suppression d une clause
	Then Je retourne un code HTTP 200 ok

Scenario: Suppression d'une clause inexistante
	Given J ai en base une liste de 3 clauses
	And J ai en en entree le code d une clause inexistante
	When je demande la suppression d une clause
	Then Je retourne un code HTTP 404 not found

Scenario: Suppression d'une clause existante attachée à une machine
	Given J ai en base une liste de 3 clauses liees a une machine
	And J ai en en entree le code d une clause existante attachee a une machine
	When je demande la suppression d une clause
	Then Je retourne un code HTTP 400 badrequest