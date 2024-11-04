// Importações necessárias para a execução do controlador
using Microsoft.AspNetCore.Mvc; // Ferramentas do ASP.NET Core MVC para criação de APIs
using Swashbuckle.AspNetCore.Annotations; // Usado para documentar endpoints no Swagger
using System.Collections.Generic; // Para uso de coleções genéricas, como List e IEnumerable
using System.Threading.Tasks; // Para operações assíncronas
using AiHackApi.Models; // Modelos de entidades da aplicação
using AiHackApi.Services; // Serviços que contêm a lógica de negócios

namespace AiHackApi.Controllers
{
    [ApiController] // Define que esta classe é um controlador de API
    [Route("api/[controller]")] // Define a rota para acessar este controlador (ex: api/Medico)
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoService _medicoService; // Serviço que manipula a lógica de negócio relacionada a médicos

        // Construtor que injeta o serviço de médicos
        public MedicoController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        /// <summary>
        /// Lista todos os médicos cadastrados no sistema.
        /// </summary>
        /// <returns>Uma lista de médicos.</returns>
        [SwaggerOperation(Summary = "Lista todos os médicos", Description = "Este endpoint retorna todos os médicos cadastrados no sistema.")]
        [SwaggerResponse(200, "Médicos listados com sucesso", typeof(IEnumerable<Medico>))] // Documenta o código de resposta 200
        [HttpGet] // Método responde a requisições HTTP GET
        public async Task<ActionResult<IEnumerable<Medico>>> GetMedicos()
        {
            // Obtém a lista de médicos usando o serviço e retorna como resposta
            var medicos = await _medicoService.GetAllMedicosAsync();
            return Ok(medicos); // Retorna 200 OK com a lista de médicos
        }

        /// <summary>
        /// Obtém os detalhes de um médico específico pelo CRM.
        /// </summary>
        /// <param name="crmMedico">O CRM do médico.</param>
        /// <returns>O médico correspondente ao CRM fornecido.</returns>
        [SwaggerOperation(Summary = "Obtém um médico específico", Description = "Este endpoint retorna os detalhes de um médico com base no CRM fornecido.")]
        [SwaggerResponse(200, "Médico encontrado com sucesso", typeof(Medico))] // Resposta para sucesso
        [SwaggerResponse(404, "Médico não encontrado")] // Resposta para erro 404
        [HttpGet("{crmMedico}")] // Método responde a HTTP GET com CRM na URL (ex: api/Medico/123456)
        public async Task<ActionResult<Medico>> GetMedico(int crmMedico)
        {
            // Obtém o médico pelo CRM usando o serviço
            var medico = await _medicoService.GetMedicoByCrmAsync(crmMedico);

            // Se o médico não for encontrado, retorna 404 Not Found
            if (medico == null)
            {
                return NotFound(new { message = "Médico não encontrado." });
            }

            // Retorna 200 OK com os dados do médico
            return Ok(medico);
        }

        /// <summary>
        /// Cadastra um novo médico no sistema.
        /// </summary>
        /// <param name="medico">Os dados do médico a ser criado.</param>
        /// <returns>O médico recém-criado.</returns>
        [SwaggerOperation(Summary = "Cadastra um novo médico", Description = "Este endpoint cria um novo médico no sistema.")]
        [SwaggerResponse(201, "Médico criado com sucesso", typeof(Medico))] // Indica que o recurso foi criado com sucesso
        [HttpPost] // Método responde a requisições HTTP POST para criar um novo médico
        public async Task<ActionResult<Medico>> AddMedico([FromBody] Medico medico)
        {
            // Cria o novo médico usando o serviço
            var medicoCriado = await _medicoService.CreateMedicoAsync(medico);

            // Retorna 201 Created com a URL do novo médico
            return CreatedAtAction(nameof(GetMedico), new { crmMedico = medicoCriado.CrmMedico }, medicoCriado);
        }

        /// <summary>
        /// Atualiza os dados de um médico existente.
        /// </summary>
        /// <param name="crmMedico">O CRM do médico a ser atualizado.</param>
        /// <param name="medicoAtualizado">Os dados atualizados do médico.</param>
        /// <returns>Resposta vazia se a atualização for bem-sucedida.</returns>
        [SwaggerOperation(Summary = "Atualiza um médico", Description = "Este endpoint atualiza os detalhes de um médico com base no CRM fornecido.")]
        [SwaggerResponse(204, "Médico atualizado com sucesso")] // Resposta para atualização bem-sucedida
        [SwaggerResponse(404, "Médico não encontrado")] // Resposta para caso o médico não seja encontrado
        [HttpPut("{crmMedico}")] // Método responde a HTTP PUT com CRM na URL (ex: api/Medico/123456)
        public async Task<IActionResult> UpdateMedico(int crmMedico, [FromBody] Medico medicoAtualizado)
        {
            // Verifica se o CRM na URL corresponde ao CRM do objeto
            if (crmMedico != medicoAtualizado.CrmMedico)
            {
                return BadRequest(new { message = "O CRM do médico não corresponde." });
            }

            // Atualiza o médico usando o serviço
            var medico = await _medicoService.UpdateMedicoAsync(medicoAtualizado);

            // Se o médico não for encontrado, retorna 404 Not Found
            if (medico == null)
            {
                return NotFound(new { message = "Médico não encontrado." });
            }

            // Retorna 204 No Content para indicar sucesso sem conteúdo adicional
            return NoContent();
        }

        /// <summary>
        /// Exclui um médico pelo CRM.
        /// </summary>
        /// <param name="crmMedico">O CRM do médico a ser excluído.</param>
        /// <returns>Resposta vazia se a exclusão for bem-sucedida.</returns>
        [SwaggerOperation(Summary = "Exclui um médico", Description = "Este endpoint exclui um médico com base no CRM fornecido.")]
        [SwaggerResponse(204, "Médico excluído com sucesso")] // Resposta para exclusão bem-sucedida
        [SwaggerResponse(404, "Médico não encontrado")] // Resposta caso o médico não seja encontrado
        [HttpDelete("{crmMedico}")] // Método responde a HTTP DELETE com CRM na URL (ex: api/Medico/123456)
        public async Task<IActionResult> DeleteMedico(int crmMedico)
        {
            // Exclui o médico pelo CRM usando o serviço
            var sucesso = await _medicoService.DeleteMedicoAsync(crmMedico);

            // Se o médico não for encontrado, retorna 404 Not Found
            if (!sucesso)
            {
                return NotFound(new { message = "Médico não encontrado." });
            }

            // Retorna 204 No Content para indicar que a exclusão foi bem-sucedida
            return NoContent();
        }
    }
}

