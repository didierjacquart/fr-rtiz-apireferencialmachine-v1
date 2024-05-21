
### Create Clause

#### Inputs: EditionClause

#### Outputs: Http 201 (Created)

:::mermaid
sequenceDiagram
	Consumer->>+ Service : Request 
	Service -->> Consumer : 400 - BadRequest(request null or empty)
	Service ->>+ Data Repository : Store Clause
	Data Repository ->>- Service : Operation Result  
	Service ->>Service : Log Event, log exception (if any)
	Service -->> Consumer : 500 - Internal Server Error (error on database access)	
	Service -->> Consumer : 201 - Success 
:::