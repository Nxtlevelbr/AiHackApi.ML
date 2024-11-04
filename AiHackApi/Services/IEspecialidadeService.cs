using AiHackApi.Models;// Importa classes e namespaces necessários para o funcionamento da aplicação.
                       // AiHackApi.Models: Contém a definição do modelo de dados, como Especialidade.
using AiHackApi.Data;// AiHackApi.Data: Pode conter o contexto de dados ou outras funcionalidades relacionadas a dados.
using System.Collections.Generic;// System.Collections.Generic: Fornece suporte para coleções genéricas, como IEnumerable.
using System.Threading.Tasks;// System.Threading.Tasks: Fornece suporte para operações assíncronas com Task.

namespace AiHackApi.Services
{
    /// <summary>
    /// Define a interface para o serviço de especialidades.
    /// </summary>
    public interface IEspecialidadeService
    {
        /// <summary>
        /// Cria uma nova especialidade e a adiciona ao repositório.
        /// </summary>
        /// <param name="especialidade">O objeto <see cref="Especialidade"/> a ser criado.</param>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é a especialidade criada.</returns>
        Task<Especialidade> CreateEspecialidadeAsync(Especialidade especialidade);

        /// <summary>
        /// Obtém uma especialidade específica pelo seu identificador.
        /// </summary>
        /// <param name="id">O identificador da especialidade.</param>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é a especialidade correspondente ao identificador.</returns>
        Task<Especialidade> GetEspecialidadeByIdAsync(int id);

        /// <summary>
        /// Obtém todas as especialidades do repositório.
        /// </summary>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é uma coleção de todas as especialidades.</returns>
        Task<IEnumerable<Especialidade>> GetAllEspecialidadesAsync();

        /// <summary>
        /// Atualiza uma especialidade existente no repositório.
        /// </summary>
        /// <param name="especialidade">O objeto <see cref="Especialidade"/> com as informações atualizadas.</param>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é a especialidade atualizada.</returns>
        Task<Especialidade> UpdateEspecialidadeAsync(Especialidade especialidade);

        /// <summary>
        /// Remove uma especialidade do repositório pelo seu identificador.
        /// </summary>
        /// <param name="id">O identificador da especialidade a ser removida.</param>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é um valor booleano indicando se a operação foi bem-sucedida.</returns>
        Task<bool> DeleteEspecialidadeAsync(int id);
    }
}


