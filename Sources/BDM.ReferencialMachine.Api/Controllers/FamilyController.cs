using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AF.WSIARD.Exception.Middleware.Model;
using BDM.ReferencialMachine.Core.Constants;
using BDM.ReferencialMachine.Core.Interfaces;
using BDM.ReferencialMachine.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BDM.ReferencialMachine.Api.Controllers
{

    [Produces("application/json")]
    [Authorize(Policy = Scopes.BdmReferencialMachine)]
    [ApiController]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyService _familyService;

        public FamilyController(IFamilyService familyService)
        {
            _familyService = familyService;
        }

        /// <summary>
        /// Opération permettant de lire les informations de toutes les familles d'activité
        /// </summary>
        /// <returns>families</returns>
        [HttpGet("/api/families")]
        [ProducesResponseType(typeof(Family[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Family[]>> ReadFamiliesAsync()
        {
            var families = await _familyService.ReadFamiliesAsync();

            return Ok(families);
        }
        
        /// <summary>
        /// Opération permettant de créer une famille d'activité
        /// </summary>
        /// <param name="family"></param>
        /// <returns></returns>
        [HttpPost("/api/families")]
        [ProducesResponseType(typeof(Family), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateFamilyAsync([FromBody][Required] Family family)
        {
            await _familyService.CreateFamilyAsync(family);

            return new CreatedResult($"/api/families", family);
        }

        /// <summary>
        /// Opération permettant de modifier une famille d'activité
        /// </summary>
        /// <param name="familyCode"></param>
        /// <param name="family"></param>
        /// <returns></returns>
        [HttpPut("/api/families/{familyCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateFamilyAsync([FromRoute, Required] string familyCode, [FromBody][Required] Family family)
        {
            await _familyService.UpdateFamilyAsync(familyCode, family);

            return Ok();
        }

        /// <summary>
        /// Operation permettant de supprimer une famille d'activité
        /// </summary>
        /// <param name="familyCode"></param>
        /// <returns></returns>
        [HttpDelete("/api/families/{familyCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteFamilyAsync([FromRoute, Required] string familyCode)
        {
            await _familyService.DeleteFamilyAsync(familyCode);

            return Ok();
        }
    }
}
