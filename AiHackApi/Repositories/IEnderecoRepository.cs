using AiHackApi.Models; // Importa o modelo Endereco
using System.Collections.Generic; // Para o uso de IEnumerable
using System.Threading.Tasks; // Para o uso de Task e operações assíncronas

namespace AiHackApi.Repositories
{
    /// <summary>
    /// Interface que define os métodos para o repositório de endereços.
    /// </summary>
    public interface IEnderecoRepository
    {
        /// <summary>
        /// Busca um endereço específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do endereço a ser buscado.</param>
        /// <returns>O endereço correspondente ao ID informado, ou null se não for encontrado.</returns>
        Task<Endereco?> ObterPorIdAsync(int id);

        /// <summary>
        /// Retorna todos os endereços cadastrados.
        /// </summary>
        /// <returns>Uma lista de todos os endereços.</returns>
        Task<IEnumerable<Endereco>> ObterTodosAsync();

        /// <summary>
        /// Adiciona um novo endereço no banco de dados.
        /// </summary>
        /// <param name="endereco">O endereço a ser adicionado.</param>
        /// <returns>O endereço recém-criado.</returns>
        Task<Endereco> AdicionarAsync(Endereco endereco);

        /// <summary>
        /// Atualiza os dados de um endereço existente.
        /// </summary>
        /// <param name="endereco">O endereço com os dados atualizados.</param>
        /// <returns>O endereço atualizado.</returns>
        Task<Endereco> AtualizarAsync(Endereco endereco);

        /// <summary>
        /// Deleta um endereço pelo ID.
        /// </summary>
        /// <param name="id">O ID do endereço a ser deletado.</param>
        /// <returns>True se o endereço foi deletado com sucesso, caso contrário False.</returns>
        Task<bool> DeletarAsync(int id);
    }
}

