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
    [Route("api/clauses")]
    [ApiController]
    public class ClauseController : ControllerBase
    {
        private readonly IClauseService _clauseService;


        public ClauseController(IClauseService clauseService)
        {
            _clauseService = clauseService;
        }

        /// <summary>
        /// Opération permettant de lire l'ensemble des clauses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(EditionClause[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EditionClause[]>> ReadClausesAsync()
        {
            var clauses = await _clauseService.ReadClausesAsync();

            return Ok(clauses);
        }

        /// <summary>
        /// Opération permettant de créer une clause
        /// </summary>
        /// <param name="clause"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(EditionClause), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateClauseAsync([FromBody][Required] EditionClause clause)
        {
            await _clauseService.CreateClauseAsync(clause);

            return new CreatedResult($"/api/clauses", clause);
        }

        /// <summary>
        /// Opération permettant de mettre à jour le détail d'une clause
        /// </summary>
        /// <param name="clauseCode" example="MA19"></param>
        /// <param name="editionClause"></param>
        /// <returns></returns>
        [HttpPut("{clauseCode}")]
        [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateClauseAsync([FromRoute, Required] string clauseCode, [Required, FromBody] EditionClause editionClause)
        {
            await _clauseService.UpdateClauseAsync(clauseCode, editionClause);

            return Ok();
        }

        /// <summary>
        /// Opération permettant de supprimer une clause
        /// </summary>
        /// <param name="clauseCode" example="MA19"></param>
        /// <returns></returns>
        [HttpDelete("{clauseCode}")]
        [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Anomaly), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteClauseAsync([FromRoute, Required] string clauseCode)
        {
            await _clauseService.DeleteClauseAsync(clauseCode);

            return Ok();
        }


    }
}
