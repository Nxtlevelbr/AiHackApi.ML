// Importa classes e namespaces necessários para o funcionamento da aplicação.
// AiHackApi.Models: Contém a definição do modelo de dados, como Endereco.
using AiHackApi.Models;

public interface IEnderecoService
{
    /// <summary>
    /// Cria um novo endereço e o adiciona ao repositório.
    /// </summary>
    /// <param name="endereco">O objeto <see cref="Endereco"/> a ser criado.</param>
    /// <returns>A tarefa que representa a operação assíncrona. O resultado é o endereço criado.</returns>
    Task<Endereco> CreateEnderecoAsync(Endereco endereco);

    /// <summary>
    /// Obtém um endereço específico pelo seu identificador.
    /// </summary>
    /// <param name="id">O identificador do endereço.</param>
    /// <returns>A tarefa que representa a operação assíncrona. O resultado é o endereço correspondente ao identificador.</returns>
    Task<Endereco> GetEnderecoByIdAsync(int id);

    /// <summary>
    /// Obtém todos os endereços do repositório.
    /// </summary>
    /// <returns>A tarefa que representa a operação assíncrona. O resultado é uma coleção de todos os endereços.</returns>
    Task<IEnumerable<Endereco>> GetAllEnderecosAsync();

    /// <summary>
    /// Atualiza um endereço existente no repositório.
    /// </summary>
    /// <param name="endereco">O objeto <see cref="Endereco"/> com as informações atualizadas.</param>
    /// <returns>A tarefa que representa a operação assíncrona. O resultado é o endereço atualizado.</returns>
    Task<Endereco> UpdateEnderecoAsync(Endereco endereco);

    /// <summary>
    /// Remove um endereço do repositório pelo seu identificador.
    /// </summary>
    /// <param name="id">O identificador do endereço a ser removido.</param>
    /// <returns>A tarefa que representa a operação assíncrona. O resultado é um valor booleano indicando se a operação foi bem-sucedida.</returns>
    Task<bool> DeleteEnderecoAsync(int id);
}


