using AiHackApi.Models;
using AiHackApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiHackApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaService _consultaService;

        public ConsultaController(IConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        /// <summary>
        /// Lista todas as consultas.
        /// </summary>
        /// <returns>Uma lista de consultas.</returns>
        [SwaggerOperation(Summary = "Lista todas as consultas", Description = "Este endpoint retorna todas as consultas cadastradas no sistema.")]
        [SwaggerResponse(200, "Consultas listadas com sucesso", typeof(IEnumerable<Consulta>))]
        [HttpGet]
        public async Task<IEnumerable<Consulta>> GetConsultas()
        {
            return await _consultaService.GetAllConsultasAsync();
        }

        /// <summary>
        /// Obtém uma consulta específica pelo IdConsulta.
        /// </summary>
        /// <param name="id">O IdConsulta da consulta.</param>
        /// <returns>A consulta correspondente ao Id fornecido.</returns>
        [SwaggerOperation(Summary = "Obtém uma consulta específica", Description = "Este endpoint retorna os detalhes de uma consulta com base no Id.")]
        [SwaggerResponse(200, "Consulta encontrada com sucesso", typeof(Consulta))]
        [SwaggerResponse(404, "Consulta não encontrada")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Consulta>> GetConsultaById([FromRoute] int id)
        {
            var consulta = await _consultaService.GetConsultaByIdAsync(id);

            if (consulta == null)
            {
                return NotFound(new { message = "Consulta não encontrada." });
            }

            return Ok(consulta);
        }

        /// <summary>
        /// Cadastra uma nova consulta.
        /// </summary>
        /// <param name="consulta">Objeto com os dados da consulta a ser criada.</param>
        /// <returns>A consulta recém-criada.</returns>
        [SwaggerOperation(Summary = "Cadastra uma nova consulta", Description = "Este endpoint cria uma nova consulta com base nos dados fornecidos.")]
        [SwaggerResponse(201, "Consulta criada com sucesso", typeof(Consulta))]
        [HttpPost]
        public async Task<ActionResult> CreateConsulta([FromBody] Consulta consulta)
        {
            await _consultaService.CreateConsultaAsync(consulta);
            return CreatedAtAction(nameof(GetConsultaById), new { id = consulta.IdConsulta }, consulta);
        }

        /// <summary>
        /// Atualiza os dados de uma consulta existente.
        /// </summary>
        /// <param name="id">O IdConsulta da consulta a ser atualizada.</param>
        /// <param name="consulta">Objeto com os dados atualizados da consulta.</param>
        /// <returns>Resposta vazia se a atualização for bem-sucedida.</returns>
        [SwaggerOperation(Summary = "Atualiza uma consulta existente", Description = "Este endpoint atualiza as informações de uma consulta já existente.")]
        [SwaggerResponse(204, "Consulta atualizada com sucesso")]
        [SwaggerResponse(400, "Os parâmetros fornecidos não correspondem")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsulta([FromRoute] int id, [FromBody] Consulta consulta)
        {
            if (id != consulta.IdConsulta)
            {
                return BadRequest(new { message = "Os parâmetros fornecidos não correspondem." });
            }

            await _consultaService.UpdateConsultaAsync(consulta);
            return NoContent();
        }

        /// <summary>
        /// Exclui uma consulta pelo IdConsulta.
        /// </summary>
        /// <param name="id">O IdConsulta da consulta a ser excluída.</param>
        /// <returns>Resposta vazia se a exclusão for bem-sucedida.</returns>
        [SwaggerOperation(Summary = "Exclui uma consulta", Description = "Este endpoint exclui uma consulta com base no Id.")]
        [SwaggerResponse(204, "Consulta excluída com sucesso")]
        [SwaggerResponse(404, "Consulta não encontrada")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsulta([FromRoute] int id)
        {
            var consulta = await _consultaService.GetConsultaByIdAsync(id);

            if (consulta == null)
            {
                return NotFound(new { message = "Consulta não encontrada." });
            }

            await _consultaService.DeleteConsultaAsync(id);
            return NoContent();
        }
    }
}
