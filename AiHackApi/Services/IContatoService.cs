// Importa classes e namespaces necessários para o funcionamento da aplicação.
// AiHackApi.Models: Contém a definição do modelo de dados, como Contato.
using AiHackApi.Models;
// System.Collections.Generic: Fornece suporte para coleções genéricas, como IEnumerable.
using System.Collections.Generic;
// System.Threading.Tasks: Fornece suporte para operações assíncronas com Task.
using System.Threading.Tasks;

namespace AiHackApi.Services
{
    /// <summary>
    /// Define a interface para o serviço de contatos.
    /// </summary>
    public interface IContatoService
    {
        /// <summary>
        /// Cria um novo contato e o adiciona ao repositório.
        /// </summary>
        /// <param name="contato">O objeto <see cref="Contato"/> a ser criado.</param>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é o contato criado.</returns>
        Task<Contato> CriarContatoAsync(Contato contato);

        /// <summary>
        /// Obtém um contato específico pelo seu email.
        /// </summary>
        /// <param name="email">O email do contato.</param>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é o contato correspondente ao email.</returns>
        Task<Contato> ObterContatoPorEmailAsync(string email);

        /// <summary>
        /// Obtém todos os contatos do repositório.
        /// </summary>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é uma coleção de todos os contatos.</returns>
        Task<IEnumerable<Contato>> ObterTodosContatosAsync();

        /// <summary>
        /// Atualiza um contato existente no repositório.
        /// </summary>
        /// <param name="contato">O objeto <see cref="Contato"/> com as informações atualizadas.</param>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é o contato atualizado.</returns>
        Task<Contato> AtualizarContatoAsync(Contato contato);

        /// <summary>
        /// Remove um contato do repositório pelo seu email.
        /// </summary>
        /// <param name="email">O email do contato a ser removido.</param>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é um valor booleano indicando se a operação foi bem-sucedida.</returns>
        Task<bool> DeletarContatoAsync(string email);
    }
}
