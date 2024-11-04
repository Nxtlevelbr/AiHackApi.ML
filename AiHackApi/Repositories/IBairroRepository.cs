using AiHackApi.Models; // Importa o modelo Bairro
using System.Collections.Generic; // Para o uso de IEnumerable
using System.Threading.Tasks; // Para o uso de Task e operações assíncronas

namespace AiHackApi.Repositories
{
    /// <summary>
    /// Interface que define os métodos para o repositório de bairros.
    /// </summary>
    public interface IBairroRepository
    {
        /// <summary>
        /// Adiciona um novo bairro ao banco de dados.
        /// </summary>
        /// <param name="bairro">O bairro a ser adicionado.</param>
        /// <returns>O bairro recém-adicionado.</returns>
        Task<Bairro> AddAsync(Bairro bairro);

        /// <summary>
        /// Busca um bairro pelo ID.
        /// </summary>
        /// <param name="id">O ID do bairro a ser buscado.</param>
        /// <returns>O bairro correspondente ao ID informado.</returns>
        Task<Bairro> GetByIdAsync(int id);

        /// <summary>
        /// Retorna todos os bairros cadastrados.
        /// </summary>
        /// <returns>Uma lista de todos os bairros.</returns>
        Task<IEnumerable<Bairro>> GetAllAsync();

        /// <summary>
        /// Atualiza os dados de um bairro existente.
        /// </summary>
        /// <param name="bairro">O bairro com os dados atualizados.</param>
        /// <returns>O bairro atualizado.</returns>
        Task<Bairro> UpdateAsync(Bairro bairro);

        /// <summary>
        /// Deleta um bairro pelo ID.
        /// </summary>
        /// <param name="id">O ID do bairro a ser deletado.</param>
        /// <returns>True se o bairro foi deletado com sucesso, caso contrário False.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
