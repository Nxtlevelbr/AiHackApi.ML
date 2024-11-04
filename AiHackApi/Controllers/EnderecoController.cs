// Importações necessárias para o funcionamento do controlador
using Microsoft.AspNetCore.Mvc; // Biblioteca essencial para controladores de API
using Swashbuckle.AspNetCore.Annotations; // Usado para gerar documentação Swagger
using System.Collections.Generic; // Para trabalhar com listas genéricas (IEnumerable)
using System.Threading.Tasks;
using AiHackApi.DTOs; // Para operações assíncronas (async/await)
using AiHackApi.Models; // Modelos da aplicação, neste caso, a entidade 'Endereco'
using AiHackApi.Services; // Serviços para acessar a lógica de negócio de 'Endereco'

namespace AiHackApi.Controllers
{
    [ApiController] // Indica que a classe é um controlador de API
    [Route("api/[controller]")] // Define a rota para os endpoints (ex: api/Endereco)
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService; // Campo para o serviço de endereços
        private readonly IViaCepService _viaCepService; // Campo para o serviço ViaCEP

        // Construtor que injeta os serviços de endereços e ViaCEP
        public EnderecoController(IEnderecoService enderecoService, IViaCepService viaCepService)
        {
            _enderecoService = enderecoService;
            _viaCepService = viaCepService;
        }

        /// <summary>
        /// Lista todos os endereços cadastrados.
        /// </summary>
        /// <returns>Uma lista de endereços.</returns>
        [SwaggerOperation(Summary = "Lista todos os endereços", Description = "Este endpoint retorna todos os endereços cadastrados no sistema.")]
        [SwaggerResponse(200, "Endereços listados com sucesso", typeof(IEnumerable<Endereco>))] // 200 OK com a lista de endereços
        [HttpGet] // Método responde a requisições HTTP GET
        public async Task<ActionResult<IEnumerable<Endereco>>> GetEnderecos()
        {
            var enderecos = await _enderecoService.GetAllEnderecosAsync();
            return Ok(enderecos); // Retorna 200 OK com a lista de endereços
        }

        /// <summary>
        /// Obtém um endereço específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do endereço.</param>
        /// <returns>O endereço correspondente ao ID informado.</returns>
        [SwaggerOperation(Summary = "Obtém um endereço específico", Description = "Este endpoint retorna os detalhes de um endereço com base no ID fornecido.")]
        [SwaggerResponse(200, "Endereço encontrado com sucesso", typeof(Endereco))] // 200 OK se encontrado
        [SwaggerResponse(404, "Endereço não encontrado")] // 404 se não encontrado
        [HttpGet("{id}")] // Método responde a GET com um ID na URL (ex: api/Endereco/1)
        public async Task<ActionResult<Endereco>> GetEndereco(int id)
        {
            var endereco = await _enderecoService.GetEnderecoByIdAsync(id);
            if (endereco == null)
            {
                return NotFound(new { message = "Endereço não encontrado." });
            }
            return Ok(endereco);
        }

        /// <summary>
        /// Cadastra um novo endereço.
        /// </summary>
        /// <param name="endereco">Objeto com os dados do endereço a ser criado.</param>
        /// <returns>O endereço recém-criado.</returns>
        [SwaggerOperation(Summary = "Cadastra um novo endereço", Description = "Este endpoint cria um novo endereço no sistema.")]
        [SwaggerResponse(201, "Endereço criado com sucesso", typeof(Endereco))] // 201 Created se o endereço for criado
        [HttpPost] // Método responde a HTTP POST para criação
        public async Task<ActionResult<Endereco>> AddEndereco([FromBody] Endereco endereco)
        {
            var enderecoCriado = await _enderecoService.CreateEnderecoAsync(endereco);
            return CreatedAtAction(nameof(GetEndereco), new { id = enderecoCriado.IdEndereco }, enderecoCriado);
        }

        /// <summary>
        /// Atualiza os dados de um endereço existente.
        /// </summary>
        /// <param name="id">O ID do endereço a ser atualizado.</param>
        /// <param name="enderecoAtualizado">Objeto com os dados atualizados do endereço.</param>
        /// <returns>Resposta vazia se a atualização for bem-sucedida.</returns>
        [SwaggerOperation(Summary = "Atualiza um endereço", Description = "Este endpoint atualiza os detalhes de um endereço com base no ID fornecido.")]
        [SwaggerResponse(204, "Endereço atualizado com sucesso")] // 204 No Content se a atualização for bem-sucedida
        [SwaggerResponse(404, "Endereço não encontrado")] // 404 se o endereço não for encontrado
        [HttpPut("{id}")] // Método responde a HTTP PUT com um ID na URL
        public async Task<IActionResult> UpdateEndereco(int id, [FromBody] Endereco enderecoAtualizado)
        {
            if (id != enderecoAtualizado.IdEndereco)
            {
                return BadRequest(new { message = "O ID do endereço não corresponde." });
            }

            var endereco = await _enderecoService.UpdateEnderecoAsync(enderecoAtualizado);
            if (endereco == null)
            {
                return NotFound(new { message = "Endereço não encontrado." });
            }

            return NoContent();
        }

        /// <summary>
        /// Exclui um endereço pelo ID.
        /// </summary>
        /// <param name="id">O ID do endereço a ser excluído.</param>
        /// <returns>Resposta vazia se a exclusão for bem-sucedida.</returns>
        [SwaggerOperation(Summary = "Exclui um endereço", Description = "Este endpoint exclui um endereço com base no ID fornecido.")]
        [SwaggerResponse(204, "Endereço excluído com sucesso")] // 204 No Content se a exclusão for bem-sucedida
        [SwaggerResponse(404, "Endereço não encontrado")] // 404 se o endereço não for encontrado
        [HttpDelete("{id}")] // Método responde a HTTP DELETE com um ID na URL
        public async Task<IActionResult> DeleteEndereco(int id)
        {
            var sucesso = await _enderecoService.DeleteEnderecoAsync(id);
            if (!sucesso)
            {
                return NotFound(new { message = "Endereço não encontrado." });
            }

            return NoContent();
        }

        /// <summary>
        /// Busca um endereço pelo CEP usando a API ViaCEP.
        /// </summary>
        /// <param name="cep">CEP para buscar o endereço.</param>
        /// <returns>O endereço encontrado com base no CEP fornecido.</returns>
        [SwaggerOperation(Summary = "Busca um endereço pelo CEP", Description = "Este endpoint utiliza o serviço ViaCEP para retornar o endereço completo com base no CEP informado.")]
        [SwaggerResponse(200, "Endereço encontrado com sucesso", typeof(EnderecoDto))] // 200 OK com o endereço encontrado
        [SwaggerResponse(404, "Endereço não encontrado para o CEP informado")] // 404 se o CEP não for encontrado
        [HttpGet("buscar-cep/{cep}")] // Define a rota como /api/endereco/buscar-cep/{cep}
        public async Task<ActionResult<EnderecoDto>> BuscarEnderecoPorCep(string cep)
        {
            try
            {
                var endereco = await _viaCepService.BuscarEnderecoPorCepAsync(cep);
                if (endereco == null)
                {
                    return NotFound(new { message = "Endereço não encontrado para o CEP informado." });
                }
                return Ok(endereco);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao buscar o endereço: " + ex.Message });
            }
        }
    }
}