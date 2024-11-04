using AiHackApi.Models; // Importa o modelo Especialidade
using System.Collections.Generic; // Para o uso de IEnumerable
using System.Threading.Tasks; // Para o uso de Task e operações assíncronas

namespace AiHackApi.Repositories
{
    /// <summary>
    /// Interface que define os métodos para o repositório de especialidades.
    /// </summary>
    public interface IEspecialidadeRepository
    {
        /// <summary>
        /// Adiciona uma nova especialidade no banco de dados.
        /// </summary>
        /// <param name="especialidade">A especialidade a ser adicionada.</param>
        /// <returns>A especialidade recém-criada.</returns>
        Task<Especialidade> AddAsync(Especialidade especialidade);

        /// <summary>
        /// Busca uma especialidade específica pelo ID.
        /// </summary>
        /// <param name="id">O ID da especialidade.</param>
        /// <returns>A especialidade correspondente ao ID informado.</returns>
        Task<Especialidade> GetByIdAsync(int id);

        /// <summary>
        /// Retorna todas as especialidades cadastradas.
        /// </summary>
        /// <returns>Uma lista de todas as especialidades.</returns>
        Task<IEnumerable<Especialidade>> GetAllAsync();

        /// <summary>
        /// Atualiza os dados de uma especialidade existente.
        /// </summary>
        /// <param name="especialidade">A especialidade com os dados atualizados.</param>
        /// <returns>A especialidade atualizada.</returns>
        Task<Especialidade> UpdateAsync(Especialidade especialidade);

        /// <summary>
        /// Deleta uma especialidade pelo ID.
        /// </summary>
        /// <param name="id">O ID da especialidade a ser deletada.</param>
        /// <returns>True se a especialidade foi deletada com sucesso, caso contrário False.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
