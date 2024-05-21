using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AF.WSIARD.Exception.Middleware.Model;
using BDM.ReferencialMachine.Api.Configuration;
using BDM.ReferencialMachine.Api.Constants;
using BDM.ReferencialMachine.Api.Helpers;
using BDM.ReferencialMachine.Core.Constants;
using BDM.ReferencialMachine.Core.Interfaces;
using BDM.ReferencialMachine.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BDM.ReferencialMachine.Api.Controllers
{
    [Produces("application/json")]
    [Authorize(Policy = Scopes.BdmReferencialMachine)]
    [ApiController]
    [Route("api/machines")]
    public class MachineController : ControllerBase
    {
        private readonly IMachineService _machineService;
        private readonly int _maxCodeInRequestAllowed;

        public MachineController(IMachineService machineService, IOptions<MachinesCriteriasOptions> machineCriteriasOptions)
        {
            _machineService = machineService;
            machineCriteriasOptions ??= Options.Create(new MachinesCriteriasOptions());
            var criteriasOptions = machineCriteriasOptions!.Value;
            _maxCodeInRequestAllowed = criteriasOptions.MaxRequestedCodes == 0 ?
                DefaultConstants.DEFAULT_MAX_REQUESTED_CODES : criteriasOptions.MaxRequestedCodes;
        }

        /// <summary>
        /// Opération permettant récupérer l'ensemble des machines
        /// avec seulement leur description succinte
        /// </summary>
        /// <param name="product" example="INVENTAIRE"></param>
        /// <returns></returns>
        /// <remarks>Cette route permet de récupérer l'exhaustivité des machines dans le référentiel, avec des données minimales permettant
        /// à l'utilisateur de parcourir la liste et faire des recherches. Les machines qui sont désactivées au niveau du référentiel
        /// ne sortiront donc pas dans cette liste
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(MachineSpecification[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MachineSpecification[]>> ReadAllMachinesAsync([FromQuery] string product) 
        {
            return Ok(await _machineService.ReadAllMachinesAsync(product));
        }        
        

        /// <summary>
        /// Opération permettant de lire le détail d'une machine
        /// </summary>
        /// <param name="machineCode" example="02001"></param>
        /// <returns></returns>
        /// <remarks>Cette route permet de récupérer le détail des informations contenues dans le référentiel pour une machine donnée.
        /// Les clauses éditiques sont à récupérer au travers d'un autre appel, ainsi que le nom de l'activité et de la sous-activité.
        /// </remarks>
        [HttpGet("{machineCode}")]
        [ProducesResponseType(typeof(MachineSpecification), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReadMachineAsync([FromRoute, Required] string machineCode)
        {
            var machineSpecification = await _machineService.ReadMachineAsync(machineCode);

            return Ok(machineSpecification);
        }

        /// <summary>
        /// Opération permettant de créer une machine
        /// </summary>
        /// <param name="machine"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(MachineSpecification), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMachineAsync([FromBody] MachineSpecification machine)
        {
            await _machineService.CreateMachineAsync(machine);

            return new CreatedResult("/api/machines", machine);
        }

        /// <summary>
        /// Opération permettant de lire le détail d'une liste de machine
        /// </summary>
        /// <returns></returns>
        [HttpPost("list")]
        [ProducesResponseType(typeof(MachineSpecification[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status413PayloadTooLarge)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MachineSpecification[]>> ReadMachineListAsync([FromBody, Required] MachineCriterias machineCriterias) 
        {
            if (machineCriterias?.MachineCodeList == null)
            {
                throw new ArgumentNullException(nameof(machineCriterias));
            }
            var validator = new ParamsValidator(machineCriterias, _maxCodeInRequestAllowed);
            await validator.ValidateMachineCriteriasAsync(machineCriterias);
            
            var machinesList = await _machineService.ReadMachineListAsync(machineCriterias.MachineCodeList.Distinct());

            return Ok(machinesList);
        }

        /// <summary>
        /// Opération permettant de mettre à jour le détail d'une machine
        /// </summary>
        /// <param name="machineCode" example="65021"></param>
        /// <param name="machineSpecification"></param>
        /// <returns></returns>
        /// <remarks>Cette route permet de mettre à jour les informations détaillées d'une machine dans le référentiel.
        /// La mise à jour se fait en 'annule et remplace'. C'est à dire que les données présentes en base vont être écrasées par celles fournies en entrée.
        /// Pour mettre à jour les informations sur les clauses éditiques une route dédiée est à utiliser ainsi que pour changer le nom d'une famille ou d'une sous-famille
        /// </remarks>
        [HttpPut("{machineCode}")]
        [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMachineAsync([FromRoute, Required] string machineCode, [Required, FromBody] MachineSpecification machineSpecification)
        {
            await _machineService.UpdateMachineAsync(machineCode, machineSpecification);

            return Ok();
        }

        /// <summary>
        /// Opération permettant de lire l'ensemble des clauses d'une liste de machine
        /// </summary>
        /// <param name="machineCriterias"></param>
        /// <remarks>
        /// exemple : { "machineCodeList": [ "02001", "02002" ] }
        /// </remarks>
        /// <returns></returns>
        [HttpPost("clauses/list")]
        [ProducesResponseType(typeof(EditionClausesByMachine[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status413RequestEntityTooLarge)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EditionClausesByMachine[]>> ReadMachineClausesListAsync([FromBody, Required] MachineCriterias machineCriterias)
        {
            if (machineCriterias?.MachineCodeList == null)
            {
                throw new ArgumentNullException(nameof(machineCriterias));
            }

            var validator = new ParamsValidator(machineCriterias, _maxCodeInRequestAllowed);
            await validator.ValidateMachineCriteriasAsync(machineCriterias);

            var clausesByMachine = await _machineService.ReadMachineClausesListAsync(machineCriterias.MachineCodeList);

            return Ok(clausesByMachine);
        }
    }
}
