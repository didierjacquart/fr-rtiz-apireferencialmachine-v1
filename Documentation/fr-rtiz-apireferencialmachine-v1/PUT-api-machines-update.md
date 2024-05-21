
### Update Machine

#### Inputs:  MachineId + MachineSpecification 
#### Outputs: Http 200 (Request OK)

:::mermaid
sequenceDiagram
	Consumer->>+ Service : Request 
	Service -->> Consumer : 400 - BadRequest(request null or empty)
	Service ->>+ Data Repository : Search Machine in database by MachineId
	Data Repository ->>- Service : Operation Result  
	Service ->>Service : Log Event, log exception (if any)
	Service -->> Consumer : 404 - Machine not found
	Service -->> Consumer : 500 - Internal Server Error (error on database access)	
	Service -->> Consumer : 200 - Success (MachineSpecification)
:::
