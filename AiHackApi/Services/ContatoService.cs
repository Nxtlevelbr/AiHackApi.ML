// Importa classes e namespaces necessários para o funcionamento da aplicação.
// AiHackApi.Models: Contém as definições dos modelos de dados, como Contato.
using AiHackApi.Models;
// AiHackApi.Repositories: Contém a definição das interfaces dos repositórios, como IContatoRepository.
using AiHackApi.Repositories;
// System.Collections.Generic: Fornece interfaces e classes para coleções genéricas, como IEnumerable.
using System.Collections.Generic;
// System.Threading.Tasks: Fornece tipos para operações assíncronas, como Task.
using System.Threading.Tasks;

namespace AiHackApi.Services
{
    /// <summary>
    /// Serviço responsável pela manipulação dos dados de contatos.
    /// Implementa a interface <see cref="IContatoService"/> para fornecer operações CRUD.
    /// </summary>
    public class ContatoService : IContatoService
    {
        // Repositório usado para interagir com a fonte de dados de contatos.
        private readonly IContatoRepository _contatoRepository;

        /// <summary>
        /// Construtor do serviço de contatos.
        /// </summary>
        /// <param name="contatoRepository">Instância do repositório de contatos.</param>
        public ContatoService(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        /// <summary>
        /// Cria um novo contato e o adiciona ao repositório.
        /// </summary>
        /// <param name="contato">O objeto <see cref="Contato"/> a ser criado.</param>
        /// <returns>O contato criado.</returns>
        public async Task<Contato> CriarContatoAsync(Contato contato)
        {
            // Adiciona o contato ao repositório e retorna o contato criado.
            return await _contatoRepository.AdicionarAsync(contato);
        }

        /// <summary>
        /// Obtém um contato específico pelo seu email.
        /// </summary>
        /// <param name="email">O email do contato.</param>
        /// <returns>O contato correspondente ao email.</returns>
        public async Task<Contato> ObterContatoPorEmailAsync(string email)
        {
            // Recupera o contato do repositório usando o email fornecido.
            return await _contatoRepository.ObterPorEmailAsync(email);
        }

        /// <summary>
        /// Obtém todos os contatos do repositório.
        /// </summary>
        /// <returns>Uma lista de todos os contatos.</returns>
        public async Task<IEnumerable<Contato>> ObterTodosContatosAsync()
        {
            // Recupera todos os contatos do repositório e converte para uma lista.
            var contatos = await _contatoRepository.ObterTodosAsync();
            return contatos;
        }

        /// <summary>
        /// Atualiza as informações de um contato existente.
        /// </summary>
        /// <param name="contato">O objeto <see cref="Contato"/> com as informações atualizadas.</param>
        /// <returns>O contato atualizado.</returns>
        public async Task<Contato> AtualizarContatoAsync(Contato contato)
        {
            // Atualiza o contato no repositório e retorna o contato atualizado.
            return await _contatoRepository.AtualizarAsync(contato);
        }

        /// <summary>
        /// Remove um contato do repositório pelo seu email.
        /// </summary>
        /// <param name="email">O email do contato a ser removido.</param>
        /// <returns>Um valor booleano indicando se a operação foi bem-sucedida.</returns>
        public async Task<bool> DeletarContatoAsync(string email)
        {
            // Remove o contato do repositório e retorna true se a operação for bem-sucedida.
            return await _contatoRepository.DeletarAsync(email);
        }
    }
}
