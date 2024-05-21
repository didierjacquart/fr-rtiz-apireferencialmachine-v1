Feature: MachineManagement
	
Scenario: Get Machine nominal call
	Given a valid machineCode '65021'
	When the service receive a call of GET '/api/machines/65021'
	Then the service return an ok result with machineSpecication

Scenario: Get Machine not found
	Given an unknown machineCode '00000'
	When the service receive a call of GET '/api/machines/00000'
	Then the service return an unknown result

Scenario: Post new machine
	Given a business machine fully filled
	When the service receive a call of POST '/api/machines'
	Then the service return the guid of the new machine

Scenario: Get all machines Inventory
	Given a list of stored machines
	When the service receive a call of GET '/api/machines?product=INVENTAIRE'
	Then the service return the list of the machines

Scenario: Get all machines by product
	Given a list of stored machines
	When the service receive a call of GET '/api/machines?product=product'
	Then the service return the list of the machines for the product 'product'

Scenario: Lecture des clauses pour une liste de machines
	Given J ai en base une liste de 3 clauses liees a 2 machines
	And J ai en en entree les criteres de lecture
	When je demande la lecture pour une liste de machines
	Then Je retourne un code HTTP 200 et un tableau de code machine et liste de clauses

Scenario: Lecture des clauses pour une liste de machines avec une machine inconnue
	Given J ai en base une liste de 3 clauses liees a 2 machines
	And J ai en en entree les criteres de lecture avec un code machine inconnu
	When je demande la lecture pour une liste de machines
	Then Je retourne un code HTTP 200 et un tableau avec seulement les clauses de la machine connue

Scenario: Lecture des clauses pour une liste de machines avec des codes dupliqués
	Given J ai en base une liste de 3 clauses liees a 2 machines
	And J ai en en entree les criteres de lecture avec des codes identiques
	When je demande la lecture pour une liste de machines
	Then Je retourne un code HTTP 400 badrequest

Scenario: Lecture des clauses pour une liste de machines avec trop de codes
	Given J ai en base une liste de 3 clauses liees a 2 machines
	And J ai en en entree les criteres de lecture avec trop de codes
	When je demande la lecture pour une liste de machines
	Then Je retourne un code HTTP 413 requestEntityTooLarge

Scenario: Get all machines Park
	Given a list of parks stored
	When the service receive a call of GET '/api/machines?product=PARK'
	Then the service return a the list of the Parks