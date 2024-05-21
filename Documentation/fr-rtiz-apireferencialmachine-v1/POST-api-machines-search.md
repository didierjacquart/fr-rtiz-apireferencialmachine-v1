
### Recherche Machine

#### Inputs:  MachineSearchCriterias
#### Outputs: Http 200 (Request OK) + List Machine

:::mermaid
sequenceDiagram
	Consumer->>+ Service : Request 
	Service -->> Consumer : 400 - BadRequest(request null or empty)	
	Service->>+ Data Repository : Search Machines in database by criterias
	Data Repository ->>- Service : Operation Result  
	Service ->>Service : Log Event, log exception (if any)
	Service -->> Consumer : 500 - Internal Server Error (erreur accès base de données)	
	Service -->> Consumer : 404 - Machine not found
	Service -->> Consumer : 200 - Success + List Machine
:::
