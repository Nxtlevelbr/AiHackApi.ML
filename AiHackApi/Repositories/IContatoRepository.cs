using AiHackApi.Models; // Importa o modelo Contato
using System.Collections.Generic; // Para o uso de IEnumerable
using System.Threading.Tasks; // Para o uso de Task e operações assíncronas

namespace AiHackApi.Repositories
{
    /// <summary>
    /// Interface que define os métodos para o repositório de contatos.
    /// </summary>
    public interface IContatoRepository
    {
        /// <summary>
        /// Adiciona um novo contato no banco de dados.
        /// </summary>
        /// <param name="contato">O contato a ser adicionado.</param>
        /// <returns>O contato recém-criado.</returns>
        Task<Contato> AdicionarAsync(Contato contato);

        /// <summary>
        /// Busca um contato pelo Email.
        /// </summary>
        /// <param name="email">O Email do contato a ser buscado.</param>
        /// <returns>O contato correspondente ao Email informado.</returns>
        Task<Contato> ObterPorEmailAsync(string email);

        /// <summary>
        /// Retorna todos os contatos cadastrados.
        /// </summary>
        /// <returns>Uma lista de todos os contatos.</returns>
        Task<IEnumerable<Contato>> ObterTodosAsync();

        /// <summary>
        /// Atualiza os dados de um contato existente.
        /// </summary>
        /// <param name="contato">O contato com os dados atualizados.</param>
        /// <returns>O contato atualizado.</returns>
        Task<Contato> AtualizarAsync(Contato contato);

        /// <summary>
        /// Deleta um contato pelo Email.
        /// </summary>
        /// <param name="email">O Email do contato a ser deletado.</param>
        /// <returns>True se o contato foi deletado com sucesso, caso contrário False.</returns>
        Task<bool> DeletarAsync(string email);
    }
}
