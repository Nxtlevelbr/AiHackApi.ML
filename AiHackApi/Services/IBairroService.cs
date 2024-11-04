// Importa classes e namespaces necessários para o funcionamento da aplicação.
// AiHackApi.Models: Contém a definição do modelo de dados, como Bairro.
using AiHackApi.Models;
// System.Collections.Generic: Fornece suporte para coleções genéricas, como IEnumerable.
using System.Collections.Generic;
// System.Threading.Tasks: Fornece suporte para operações assíncronas com Task.
using System.Threading.Tasks;

namespace AiHackApi.Services
{
    /// <summary>
    /// Define a interface para o serviço de bairros.
    /// </summary>
    public interface IBairroService
    {
        /// <summary>
        /// Cria um novo bairro e o adiciona ao repositório.
        /// </summary>
        /// <param name="bairro">O objeto <see cref="Bairro"/> a ser criado.</param>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é o bairro criado.</returns>
        Task<Bairro> CreateBairroAsync(Bairro bairro);

        /// <summary>
        /// Obtém um bairro específico pelo seu identificador.
        /// </summary>
        /// <param name="id">O identificador do bairro.</param>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é o bairro correspondente ao identificador.</returns>
        Task<Bairro> GetBairroByIdAsync(int id);

        /// <summary>
        /// Obtém todos os bairros do repositório.
        /// </summary>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é uma coleção de todos os bairros.</returns>
        Task<IEnumerable<Bairro>> GetAllBairrosAsync();

        /// <summary>
        /// Atualiza um bairro existente no repositório.
        /// </summary>
        /// <param name="bairro">O objeto <see cref="Bairro"/> com as informações atualizadas.</param>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é o bairro atualizado.</returns>
        Task<Bairro> UpdateBairroAsync(Bairro bairro);

        /// <summary>
        /// Remove um bairro do repositório pelo seu identificador.
        /// </summary>
        /// <param name="id">O identificador do bairro a ser removido.</param>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é um valor booleano indicando se a operação foi bem-sucedida.</returns>
        Task<bool> DeleteBairroAsync(int id);
    }
}
