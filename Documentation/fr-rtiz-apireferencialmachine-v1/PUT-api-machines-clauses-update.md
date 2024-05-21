
### Update Clause

#### Inputs:  ClauseId + EditionClause 
#### Outputs: Http 200 (Request OK)

:::mermaid
sequenceDiagram
	Consumer->>+ Service : Request 
	Service -->> Consumer : 400 - BadRequest(request null or empty)
	Service ->>+ Data Repository : Search clause in database by MachineId
	Data Repository ->>- Service : Operation Result  
	Service ->>Service : Log Event, log exception (if any)
	Service -->> Consumer : 404 - Clause not found
	Service -->> Consumer : 500 - Internal Server Error (error on database access)	
	Service -->> Consumer : 200 - Success (EditionClause)
:::
