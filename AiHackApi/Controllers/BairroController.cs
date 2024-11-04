using AiHackApi.Models; // Importa os modelos usados, como "Bairro"
using AiHackApi.Services; // Importa os serviços, como "IBairroService", que contém a lógica de negócios
using Microsoft.AspNetCore.Mvc; // Fornece funcionalidades do ASP.NET Core MVC, como controllers e atributos HTTP
using Swashbuckle.AspNetCore.Annotations; // Permite a documentação da API usando anotações Swagger

[ApiController] // Indica que esta classe é um controller de API
[Route("api/[controller]")] // Define a rota base do controller como "api/Bairro" (o nome do controller é automaticamente usado)
public class BairroController : ControllerBase // A classe herda de ControllerBase, fornecendo funcionalidades básicas para APIs REST
{
    private readonly IBairroService _bairroService; // Dependência do serviço de bairros que será usada neste controller

    // Construtor que injeta o serviço "IBairroService" via injeção de dependência
    public BairroController(IBairroService bairroService)
    {
        _bairroService = bairroService; // Atribui o serviço injetado ao campo privado "_bairroService"
    }

    // Endpoint GET para listar todos os bairros cadastrados
    [SwaggerOperation(Summary = "Lista todos os bairros", Description = "Este endpoint retorna uma lista completa de todos os bairros cadastrados.")]
    [HttpGet] // Define que este método responde a requisições HTTP GET
    public async Task<ActionResult<IEnumerable<Bairro>>> GetBairros()
    {
        var bairros = await _bairroService.GetAllBairrosAsync(); // Chama o serviço para obter todos os bairros
        return Ok(bairros); // Retorna a lista de bairros com status HTTP 200 (OK)
    }

    // Endpoint GET para obter um bairro específico pelo seu ID
    [SwaggerOperation(Summary = "Obtém um bairro específico", Description = "Este endpoint retorna os detalhes de um bairro específico com base no ID fornecido.")]
    [SwaggerResponse(200, "Bairro encontrado com sucesso", typeof(Bairro))] // Documenta que o retorno pode ser 200 com um objeto Bairro
    [SwaggerResponse(404, "Bairro não encontrado")] // Documenta que o retorno pode ser 404 (não encontrado)
    [HttpGet("{id}")] // Define que este método responde a GET e recebe o ID do bairro como parâmetro na rota
    public async Task<ActionResult<Bairro>> GetBairro(int id)
    {
        var bairro = await _bairroService.GetBairroByIdAsync(id); // Chama o serviço para buscar o bairro pelo ID
        if (bairro == null)
        {
            return NotFound(new { message = "Bairro não encontrado." }); // Retorna 404 se o bairro não for encontrado
        }
        return Ok(bairro); // Retorna o bairro encontrado com status HTTP 200 (OK)
    }

    // Endpoint POST para cadastrar um novo bairro
    [SwaggerOperation(Summary = "Cadastra um novo bairro", Description = "Este endpoint cria um novo bairro com base nas informações fornecidas.")]
    [SwaggerResponse(201, "Bairro criado com sucesso")] // Documenta que o retorno será 201 (Criado) com o objeto criado
    [HttpPost] // Define que este método responde a requisições HTTP POST
    public async Task<ActionResult> AddBairro([FromBody] Bairro bairro) // Recebe o objeto Bairro do corpo da requisição
    {
        var bairroCriado = await _bairroService.CreateBairroAsync(bairro); // Chama o serviço para criar o novo bairro
        return CreatedAtAction(nameof(GetBairro), new { id = bairroCriado.IdBairro }, bairroCriado); // Retorna 201 e a URI para acessar o novo recurso
    }

    // Endpoint PUT para atualizar um bairro existente pelo ID
    [SwaggerOperation(Summary = "Atualiza um bairro existente", Description = "Este endpoint atualiza as informações de um bairro com base no ID fornecido.")]
    [SwaggerResponse(204, "Bairro atualizado com sucesso")] // Documenta que o retorno será 204 (No Content) se for atualizado com sucesso
    [SwaggerResponse(404, "Bairro não encontrado")] // Documenta que o retorno pode ser 404 (não encontrado)
    [HttpPut("{id}")] // Define que este método responde a requisições HTTP PUT e recebe o ID do bairro na rota
    public async Task<IActionResult> UpdateBairro(int id, [FromBody] Bairro bairro) // Recebe o objeto Bairro atualizado no corpo da requisição
    {
        if (id != bairro.IdBairro) // Verifica se o ID do bairro corresponde ao ID na URL
        {
            return BadRequest(new { message = "O ID do bairro não corresponde." }); // Retorna 400 (Bad Request) se não corresponder
        }

        var bairroAtualizado = await _bairroService.UpdateBairroAsync(bairro); // Chama o serviço para atualizar o bairro
        if (bairroAtualizado == null)
        {
            return NotFound(new { message = "Bairro não encontrado." }); // Retorna 404 se o bairro não for encontrado
        }

        return NoContent(); // Retorna 204 (No Content) indicando que a atualização foi bem-sucedida
    }

    // Endpoint DELETE para excluir um bairro pelo ID
    [SwaggerOperation(Summary = "Exclui um bairro", Description = "Este endpoint exclui um bairro com base no ID fornecido.")]
    [SwaggerResponse(204, "Bairro excluído com sucesso")] // Documenta que o retorno será 204 (No Content) se for excluído com sucesso
    [SwaggerResponse(404, "Bairro não encontrado")] // Documenta que o retorno pode ser 404 (não encontrado)
    [HttpDelete("{id}")] // Define que este método responde a requisições HTTP DELETE e recebe o ID do bairro na rota
    public async Task<IActionResult> DeleteBairro(int id)
    {
        var success = await _bairroService.DeleteBairroAsync(id); // Chama o serviço para excluir o bairro pelo ID
        if (!success)
        {
            return NotFound(new { message = "Bairro não encontrado." }); // Retorna 404 se o bairro não for encontrado
        }
        return NoContent(); // Retorna 204 (No Content) indicando que a exclusão foi bem-sucedida
    }
}
