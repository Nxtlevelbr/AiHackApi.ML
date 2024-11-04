using AiHackApi.Models;
using AiHackApi.Repositories;

namespace AiHackApi.Services
{
    /// <summary>
    /// Serviço responsável pela manipulação dos dados de bairros.
    /// Implementa a interface <see cref="IBairroService"/> para fornecer operações CRUD.
    /// </summary>
    public class BairroService : IBairroService
    {
        // Repositório usado para interagir com a fonte de dados de bairros.
        private readonly IBairroRepository _bairroRepository;

        /// <summary>
        /// Construtor do serviço de bairros.
        /// </summary>
        /// <param name="bairroRepository">Instância do repositório de bairros.</param>
        public BairroService(IBairroRepository bairroRepository)
        {
            _bairroRepository = bairroRepository;
        }

        /// <summary>
        /// Cria um novo bairro e o adiciona ao repositório.
        /// </summary>
        /// <param name="bairro">O objeto <see cref="Bairro"/> a ser criado.</param>
        /// <returns>O bairro criado.</returns>
        public async Task<Bairro> CreateBairroAsync(Bairro bairro)
        {
            // Adiciona o bairro ao repositório e retorna o bairro criado.
            return await _bairroRepository.AddAsync(bairro);
        }

        /// <summary>
        /// Obtém um bairro pelo seu identificador.
        /// </summary>
        /// <param name="id">O identificador do bairro.</param>
        /// <returns>O bairro correspondente ao identificador.</returns>
        public async Task<Bairro> GetBairroByIdAsync(int id)
        {
            // Recupera o bairro do repositório usando o identificador fornecido.
            return await _bairroRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Obtém todos os bairros do repositório.
        /// </summary>
        /// <returns>Uma lista de todos os bairros.</returns>
        public async Task<IEnumerable<Bairro>> GetAllBairrosAsync()
        {
            // Recupera todos os bairros do repositório.
            return await _bairroRepository.GetAllAsync();
        }

        /// <summary>
        /// Atualiza as informações de um bairro existente.
        /// </summary>
        /// <param name="bairro">O objeto <see cref="Bairro"/> com as informações atualizadas.</param>
        /// <returns>O bairro atualizado.</returns>
        public async Task<Bairro> UpdateBairroAsync(Bairro bairro)
        {
            // Atualiza o bairro no repositório e retorna o bairro atualizado.
            return await _bairroRepository.UpdateAsync(bairro);
        }

        /// <summary>
        /// Remove um bairro do repositório pelo seu identificador.
        /// </summary>
        /// <param name="id">O identificador do bairro a ser removido.</param>
        /// <returns>Um valor booleano indicando se a operação foi bem-sucedida.</returns>
        public async Task<bool> DeleteBairroAsync(int id)
        {
            // Remove o bairro do repositório e retorna true se a operação for bem-sucedida.
            return await _bairroRepository.DeleteAsync(id);
        }
    }
}
