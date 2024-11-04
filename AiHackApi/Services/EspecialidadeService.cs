// Importa classes e namespaces necessários para o funcionamento da aplicação.
// AiHackApi.Models: Contém as definições dos modelos de dados, como Especialidade.
using AiHackApi.Models;
// AiHackApi.Repositories: Contém a definição das interfaces dos repositórios, como IEspecialidadeRepository.
using AiHackApi.Repositories;

namespace AiHackApi.Services
{
    /// <summary>
    /// Serviço responsável pela manipulação dos dados de especialidades.
    /// Implementa a interface <see cref="IEspecialidadeService"/> para fornecer operações CRUD.
    /// </summary>
    public class EspecialidadeService : IEspecialidadeService
    {
        // Repositório usado para interagir com a fonte de dados de especialidades.
        private readonly IEspecialidadeRepository _especialidadeRepository;

        /// <summary>
        /// Construtor do serviço de especialidades.
        /// </summary>
        /// <param name="especialidadeRepository">Instância do repositório de especialidades.</param>
        public EspecialidadeService(IEspecialidadeRepository especialidadeRepository)
        {
            _especialidadeRepository = especialidadeRepository;
        }

        /// <summary>
        /// Cria uma nova especialidade e a adiciona ao repositório.
        /// </summary>
        /// <param name="especialidade">O objeto <see cref="Especialidade"/> a ser criado.</param>
        /// <returns>A especialidade criada.</returns>
        public async Task<Especialidade> CreateEspecialidadeAsync(Especialidade especialidade)
        {
            // Adiciona a especialidade ao repositório e retorna a especialidade criada.
            return await _especialidadeRepository.AddAsync(especialidade);
        }

        /// <summary>
        /// Obtém uma especialidade específica pelo seu identificador.
        /// </summary>
        /// <param name="id">O identificador da especialidade.</param>
        /// <returns>A especialidade correspondente ao identificador.</returns>
        public async Task<Especialidade> GetEspecialidadeByIdAsync(int id)
        {
            // Recupera a especialidade do repositório usando o identificador fornecido.
            return await _especialidadeRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Obtém todas as especialidades do repositório.
        /// </summary>
        /// <returns>Uma lista de todas as especialidades.</returns>
        public async Task<IEnumerable<Especialidade>> GetAllEspecialidadesAsync()
        {
            // Recupera todas as especialidades do repositório.
            return await _especialidadeRepository.GetAllAsync();
        }

        /// <summary>
        /// Atualiza uma especialidade existente no repositório.
        /// </summary>
        /// <param name="especialidade">O objeto <see cref="Especialidade"/> com as informações atualizadas.</param>
        /// <returns>A especialidade atualizada.</returns>
        public async Task<Especialidade> UpdateEspecialidadeAsync(Especialidade especialidade)
        {
            // Atualiza a especialidade no repositório e retorna a especialidade atualizada.
            return await _especialidadeRepository.UpdateAsync(especialidade);
        }

        /// <summary>
        /// Remove uma especialidade do repositório pelo seu identificador.
        /// </summary>
        /// <param name="id">O identificador da especialidade a ser removida.</param>
        /// <returns>Um valor booleano indicando se a operação foi bem-sucedida.</returns>
        public async Task<bool> DeleteEspecialidadeAsync(int id)
        {
            // Remove a especialidade do repositório e retorna true se a operação for bem-sucedida.
            return await _especialidadeRepository.DeleteAsync(id);
        }
    }
}
