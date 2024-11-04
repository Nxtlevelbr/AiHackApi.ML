// Importações necessárias para o funcionamento do controlador
using Microsoft.AspNetCore.Mvc; // Biblioteca essencial para controladores de API
using Swashbuckle.AspNetCore.Annotations; // Usado para gerar documentação Swagger
using System.Collections.Generic; // Para trabalhar com listas genéricas (IEnumerable)
using System.Threading.Tasks; // Para operações assíncronas (async/await)
using AiHackApi.Models; // Modelos da aplicação, neste caso, a entidade 'Especialidade'
using AiHackApi.Services; // Serviços para acessar a lógica de negócio de 'Especialidade'

namespace AiHackApi.Controllers
{
    [ApiController] // Indica que a classe é um controlador de API
    [Route("api/[controller]")] // Define a rota para os endpoints (ex: api/Especialidade)
    public class EspecialidadeController : ControllerBase
    {
        private readonly IEspecialidadeService _especialidadeService; // Campo para o serviço de especialidades

        // Construtor que injeta o serviço de especialidades
        public EspecialidadeController(IEspecialidadeService especialidadeService)
        {
            _especialidadeService = especialidadeService;
        }

        /// <summary>
        /// Lista todas as especialidades cadastradas.
        /// </summary>
        /// <returns>Uma lista de especialidades.</returns>
        [SwaggerOperation(Summary = "Lista todas as especialidades", Description = "Este endpoint retorna todas as especialidades cadastradas no sistema.")]
        [SwaggerResponse(200, "Especialidades listadas com sucesso", typeof(IEnumerable<Especialidade>))] // 200 OK com a lista de especialidades
        [HttpGet] // Método responde a requisições HTTP GET
        public async Task<ActionResult<IEnumerable<Especialidade>>> GetEspecialidades()
        {
            // Chama o serviço para obter todas as especialidades e retorna a lista
            var especialidades = await _especialidadeService.GetAllEspecialidadesAsync();
            return Ok(especialidades); // Retorna 200 OK com a lista de especialidades
        }

        /// <summary>
        /// Obtém uma especialidade específica pelo ID.
        /// </summary>
        /// <param name="id">O ID da especialidade.</param>
        /// <returns>A especialidade correspondente ao ID informado.</returns>
        [SwaggerOperation(Summary = "Obtém uma especialidade específica", Description = "Este endpoint retorna os detalhes de uma especialidade com base no ID fornecido.")]
        [SwaggerResponse(200, "Especialidade encontrada com sucesso", typeof(Especialidade))] // 200 OK se encontrada
        [SwaggerResponse(404, "Especialidade não encontrada")] // 404 se não encontrada
        [HttpGet("{id}")] // Método responde a GET com um ID na URL (ex: api/Especialidade/1)
        public async Task<ActionResult<Especialidade>> GetEspecialidade(int id)
        {
            // Usa o serviço para obter a especialidade pelo ID
            var especialidade = await _especialidadeService.GetEspecialidadeByIdAsync(id);

            // Se a especialidade não for encontrada, retorna 404
            if (especialidade == null)
            {
                return NotFound(new { message = "Especialidade não encontrada." });
            }

            // Retorna 200 OK com a especialidade encontrada
            return Ok(especialidade);
        }

        /// <summary>
        /// Cadastra uma nova especialidade.
        /// </summary>
        /// <param name="especialidade">Objeto com os dados da especialidade a ser criada.</param>
        /// <returns>A especialidade recém-criada.</returns>
        [SwaggerOperation(Summary = "Cadastra uma nova especialidade", Description = "Este endpoint cria uma nova especialidade no sistema.")]
        [SwaggerResponse(201, "Especialidade criada com sucesso", typeof(Especialidade))] // 201 Created se a especialidade for criada
        [HttpPost] // Método responde a HTTP POST para criação
        public async Task<ActionResult<Especialidade>> AddEspecialidade([FromBody] Especialidade especialidade)
        {
            // Chama o serviço para criar a nova especialidade
            var especialidadeCriada = await _especialidadeService.CreateEspecialidadeAsync(especialidade);

            // Retorna 201 Created com a URL para acessar a nova especialidade
            return CreatedAtAction(nameof(GetEspecialidade), new { id = especialidadeCriada.IdEspecialidade }, especialidadeCriada);
        }

        /// <summary>
        /// Atualiza os dados de uma especialidade existente.
        /// </summary>
        /// <param name="id">O ID da especialidade a ser atualizada.</param>
        /// <param name="especialidadeAtualizada">Objeto com os dados atualizados da especialidade.</param>
        /// <returns>Resposta vazia se a atualização for bem-sucedida.</returns>
        [SwaggerOperation(Summary = "Atualiza uma especialidade", Description = "Este endpoint atualiza os detalhes de uma especialidade com base no ID fornecido.")]
        [SwaggerResponse(204, "Especialidade atualizada com sucesso")] // 204 No Content se a atualização for bem-sucedida
        [SwaggerResponse(404, "Especialidade não encontrada")] // 404 se a especialidade não for encontrada
        [HttpPut("{id}")] // Método responde a HTTP PUT com um ID na URL
        public async Task<IActionResult> UpdateEspecialidade(int id, [FromBody] Especialidade especialidadeAtualizada)
        {
            // Verifica se o ID na URL corresponde ao ID do objeto especialidade
            if (id != especialidadeAtualizada.IdEspecialidade)
            {
                return BadRequest(new { message = "O ID da especialidade não corresponde." }); // Retorna 400 Bad Request se os IDs não coincidirem
            }

            // Chama o serviço para atualizar a especialidade
            var especialidade = await _especialidadeService.UpdateEspecialidadeAsync(especialidadeAtualizada);
            if (especialidade == null)
            {
                return NotFound(new { message = "Especialidade não encontrada." }); // Retorna 404 se a especialidade não for encontrada
            }

            // Retorna 204 No Content se a atualização for bem-sucedida
            return NoContent();
        }

        /// <summary>
        /// Exclui uma especialidade pelo ID.
        /// </summary>
        /// <param name="id">O ID da especialidade a ser excluída.</param>
        /// <returns>Resposta vazia se a exclusão for bem-sucedida.</returns>
        [SwaggerOperation(Summary = "Exclui uma especialidade", Description = "Este endpoint exclui uma especialidade com base no ID fornecido.")]
        [SwaggerResponse(204, "Especialidade excluída com sucesso")] // 204 No Content se a exclusão for bem-sucedida
        [SwaggerResponse(404, "Especialidade não encontrada")] // 404 se a especialidade não for encontrada
        [HttpDelete("{id}")] // Método responde a HTTP DELETE com um ID na URL
        public async Task<IActionResult> DeleteEspecialidade(int id)
        {
            // Chama o serviço para excluir a especialidade
            var sucesso = await _especialidadeService.DeleteEspecialidadeAsync(id);
            if (!sucesso)
            {
                return NotFound(new { message = "Especialidade não encontrada." }); // Retorna 404 se a especialidade não for encontrada
            }

            // Retorna 204 No Content se a exclusão for bem-sucedida
            return NoContent();
        }
    }
}
