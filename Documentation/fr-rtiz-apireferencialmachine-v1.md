
# fr-projetetdevis-apienrichquotemachine-v1

[[_TOC_]]

## Fiche d'identité

```json
{
    serviceName : "fr-rtiz-apireferencialmachine-v1-vs",
    version : "1.0",
    sa : "RTIZ",
    summary : "Referentiel machine pour la souscription Bris de Machine (BDM_INSURANCE)"
    sonar : "https://afa-sonarqube.azure-paas.com/dashboard?id=af.wsiard.fr.rtiz.apireferencialmachine.v1" 
}
```
## Fiche Technique

|Properties                     | Values                                                                                         |
|--                             | --                                                                                             |
|Document owner                 |**JACQUART Didier; VUE Yoann, WOLFF Xavier**                                                    |
|Developers                     |**-**                                                                                           |
|QA                             |**-**                                                                                           |
|Fonction (*)                   |**Cette API expose les fonctionnalités permettant de lire/modifier/rechercher/créer des machines pour la soucription (BDM_INSURANCE)**|
|Technologie                    |**NET 6.0**                                                                                |
|Architecture  _(REST or SOAP)_ |**REST**                                                                                        |
|Protocole  _(HTTP or Others)_  |**HTTPS**                                                                                       |
|Format _(JSON or XML)_         |**JSON**                                                                                        |
|Sécurité                       |<font color="red">Token Based Auth</font> (Token MAAMv2)                                        |
|Logs                           |Logs Azure (AppInsight, OSIA, la mise en place des Id Correlation)                              |
|Retry                          |Mise en place avec le package Polly                                                             |

 (*) *Renseigner dans cette section la macro description du besoin ayant conduit à la création du service (cf DAG)*

## Historique de version

1. **Version 1.0 - initiale**

* Métier : Gérer le referentiel des machines dans le cadre de la souscription du produit BDM_INSURANCE (Bris de machine)
* Technique : Exposer le service de gestion des machines en REST via Web API.
* Technique : Retourner l'image de la machine mise à jour.

## Description

<table>
    <tr>
        <th>Méthodes</th>
        <th>Description</th>
    </tr>
    <tr>
        <td rowspan="3">
            <B>Create Machine</B>
            <BR/>
            <I>POST /api/machines<BR/>
            </I>
        </td>
        <td>
            <B><U>Input </U></B>Object Json of Machine specification (in body request)
        </td>
    </tr>
    <tr>
        <td>
            <B><U>Output </U></B>Ok Result (201 : object created)
        </td>
    </tr>
    <tr>
        <td>
            <B><U>Fonction </U></B>Create a new machine in the referential
        </td>
    </tr>  
    <tr>
        <td rowspan="3">
            <B>Read Machine</B>
            <BR/>
            <I>Get /api/machines/{machineCode}<BR/>
            </I>
        </td>
        <td>
            <B><U>Input </U></B>id of a machine
        </td>
    </tr>
    <tr>
        <td>
            <B><U>Output </U></B>Ok ObjectResult (200) with the machine specification
        </td>
    </tr>
    <tr>
        <td>
            <B><U>Fonction </U></B>Reads an existing machine in the referential
        </td>
    </tr>  
    <tr>
        <td rowspan="3">
            <B>Update Machine</B>
            <BR/>
            <I>Put /api/machines/{machineCode}<BR/>
            </I>
        </td>
        <td>
            <B><U>Input </U></B>id of a machine, Object Machinespecification (in body request)
        </td>
    </tr>
    <tr>
        <td>
            <B><U>Output </U></B>Ok Result (200)
        </td>
    </tr>
    <tr>
        <td>
            <B><U>Fonction </U></B>Updates an existing machine in the referential
        </td>
    </tr>  
    <tr>
        <td rowspan="3">
            <B>Search Machine</B>
            <BR/>
            <I>POST /api/machines/search<BR/>
            </I>
        </td>
        <td>
            <B><U>Input </U></B>Object MachineSearchCriterias (in body request)
        </td>
    </tr>
    <tr>
        <td>
            <B><U>Output </U></B>OkObjectResult (200) with the list of found Machinespecification
        </td>
    </tr>
    <tr>
        <td>
            <B><U>Fonction </U></B>Searchs the machines in the referential corresponding to submited parameters
        </td>
    </tr>
    <tr>
        <td rowspan="3">
            <B>Create Clause</B>
            <BR/>
            <I>POST /api/machines/clauses<BR/>
            </I>
        </td>
        <td>
            <B><U>Input </U></B>Object Json of EditionClause (in body request)
        </td>
    </tr>
    <tr>
        <td>
            <B><U>Output </U></B>Ok Result (201 : object created)
        </td>
    </tr>
    <tr>
        <td>
            <B><U>Fonction </U></B>Create a new clause in the referential
        </td>
    </tr>  
    <tr>
        <td rowspan="3">
            <B>Read all the clauses for a machine</B>
            <BR/>
            <I>Get /api/machines/{machineCode}/clauses<BR/>
            </I>
        </td>
        <td>
            <B><U>Input </U></B>id of a machine
        </td>
    </tr>
    <tr>
        <td>
            <B><U>Output </U></B>Ok ObjectResult (200) with the EditionClause[]
        </td>
    </tr>
    <tr>
        <td>
            <B><U>Fonction </U></B>Reads existing clauses in the referential
        </td>
    </tr>  
    <tr>
        <td rowspan="3">
            <B>Update CLause for a machine</B>
            <BR/>
            <I>Put /api/machines/{machineCode}/clauses/{clauseCode}<BR/>
            </I>
        </td>
        <td>
            <B><U>Input </U></B>id of a machine, id of a clause, Object EditionClause (in body request)
        </td>
    </tr>
    <tr>
        <td>
            <B><U>Output </U></B>Ok Result (200)
        </td>
    </tr>    
    <tr>
        <td rowspan="3">
            <B>Delete CLause for a machine</B>
            <BR/>
            <I>Delete /api/machines/{machineCode}/clauses/{clauseCode}<BR/>
            </I>
        </td>
        <td>
            <B><U>Input </U></B>id of a machine, id of a clause
        </td>
    </tr>
    <tr>
        <td>
            <B><U>Output </U></B>Ok Result (200)
        </td>
    </tr>

</table>


## Liste des urls du service 

|Environnement|URL|
| -- |--|
|DEV|Eip: https://eip-dev.axa-fr.intraxa/gateway/fr-rtiz-apireferencialmachine-v1-vs|
|INT|Eip: https://eip-int.axa-fr.intraxa/gateway/fr-rtiz-apireferencialmachine-v1-vs|
|REC|Eip: https://eip-rec.axa-fr.intraxa/gateway/fr-rtiz-apireferencialmachine-v1-vs|
|PP|Eip: https://eip-pp.axa-fr.intraxa/gateway/fr-rtiz-apireferencialmachine-v1-vs|
|Prod|Eip: https://eip.axa-fr.intraxa/gateway/fr-rtiz-apireferencialmachine-v1-vs|

## Dette Technique

|ID|Niveau|Description|Statut|
|--|--|--|--|


Niveaux :
• Faible : évolution mineure à prévoir, non vue du consommateur et ne l’impactant donc pas (compatible ascendante). Problématique de code (normes, tests, etc). => nécessite un patch avec TNR (relivraison de la version mineure existante).
Exemple : mise à jour framework de log,
• Moyenne : évolution mineure à prévoir connue du consommateur mais ne l’impactant pas (compatible ascendante). Exemple : mise en place CorrelationID
• Forte : nécessite une refonte de l’existant, impactant le consommateur => nouvelle version majeure. Exemple : renommage d’un WS.

Lien Sonar :
https://afa-sonarqube.azure-paas.com/dashboard?id=af.wsiard.fr.rtiz.apireferencialmachine.v1
