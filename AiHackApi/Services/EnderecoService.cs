// Importa classes e namespaces necessários para o funcionamento da aplicação.
// AiHackApi.Models: Contém as definições dos modelos de dados, como Endereco.
using AiHackApi.Models;
// AiHackApi.Repositories: Contém a definição das interfaces dos repositórios, como IEnderecoRepository.
using AiHackApi.Repositories;

public class EnderecoService : IEnderecoService
{
    // Repositório usado para interagir com a fonte de dados de endereços.
    private readonly IEnderecoRepository _enderecoRepository;

    /// <summary>
    /// Construtor do serviço de endereços.
    /// </summary>
    /// <param name="enderecoRepository">Instância do repositório de endereços.</param>
    public EnderecoService(IEnderecoRepository enderecoRepository)
    {
        _enderecoRepository = enderecoRepository;
    }

    /// <summary>
    /// Cria um novo endereço e o adiciona ao repositório.
    /// </summary>
    /// <param name="endereco">O objeto <see cref="Endereco"/> a ser criado.</param>
    /// <returns>O endereço criado.</returns>
    public async Task<Endereco> CreateEnderecoAsync(Endereco endereco)
    {
        // Adiciona o endereço ao repositório e retorna o endereço criado.
        return await _enderecoRepository.AdicionarAsync(endereco);
    }

    /// <summary>
    /// Obtém um endereço específico pelo seu identificador.
    /// </summary>
    /// <param name="id">O identificador do endereço.</param>
    /// <returns>O endereço correspondente ao identificador.</returns>
    public async Task<Endereco> GetEnderecoByIdAsync(int id)
    {
        // Recupera o endereço do repositório usando o identificador fornecido.
        return await _enderecoRepository.ObterPorIdAsync(id);
    }

    /// <summary>
    /// Obtém todos os endereços do repositório.
    /// </summary>
    /// <returns>Uma lista de todos os endereços.</returns>
    public async Task<IEnumerable<Endereco>> GetAllEnderecosAsync()
    {
        // Recupera todos os endereços do repositório.
        return await _enderecoRepository.ObterTodosAsync();
    }

    /// <summary>
    /// Atualiza um endereço existente no repositório.
    /// </summary>
    /// <param name="endereco">O objeto <see cref="Endereco"/> com as informações atualizadas.</param>
    /// <returns>O endereço atualizado.</returns>
    public async Task<Endereco> UpdateEnderecoAsync(Endereco endereco)
    {
        // Atualiza o endereço no repositório e retorna o endereço atualizado.
        await _enderecoRepository.AtualizarAsync(endereco);
        return endereco;
    }

    /// <summary>
    /// Remove um endereço do repositório pelo seu identificador.
    /// </summary>
    /// <param name="id">O identificador do endereço a ser removido.</param>
    /// <returns>Um valor booleano indicando se a operação foi bem-sucedida.</returns>
    public async Task<bool> DeleteEnderecoAsync(int id)
    {
        // Remove o endereço do repositório e retorna true se a operação for bem-sucedida.
        return await _enderecoRepository.DeletarAsync(id);
    }
}
