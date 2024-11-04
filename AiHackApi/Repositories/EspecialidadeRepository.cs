using AiHackApi.Data; // Importa o contexto do banco de dados
using AiHackApi.Models; // Importa o modelo Especialidade
using Microsoft.EntityFrameworkCore; // Usado para trabalhar com o Entity Framework

namespace AiHackApi.Repositories
{
    public class EspecialidadeRepository : IEspecialidadeRepository
    {
        // Campo _context para acessar o banco de dados via Entity Framework
        private readonly ApplicationDbContext _context;

        // Construtor que injeta o contexto do banco de dados
        public EspecialidadeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona uma nova especialidade ao banco de dados.
        /// </summary>
        /// <param name="especialidade">A especialidade a ser adicionada.</param>
        /// <returns>A especialidade recém-criada.</returns>
        public async Task<Especialidade> AddAsync(Especialidade especialidade)
        {
            // Adiciona a especialidade ao contexto do banco de dados
            await _context.Especialidades.AddAsync(especialidade);
            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();
            return especialidade; // Retorna a especialidade criada
        }

        /// <summary>
        /// Busca uma especialidade pelo ID.
        /// </summary>
        /// <param name="id">O ID da especialidade a ser buscada.</param>
        /// <returns>A especialidade correspondente ao ID fornecido.</returns>
        public async Task<Especialidade> GetByIdAsync(int id)
        {
            // Busca a especialidade pelo ID
            var especialidade = await _context.Especialidades.FindAsync(id);
            if (especialidade == null)
            {
                // Lança uma exceção se a especialidade não for encontrada
                throw new Exception("Especialidade não encontrada");
            }
            return especialidade; // Retorna a especialidade encontrada
        }

        /// <summary>
        /// Retorna todas as especialidades cadastradas.
        /// </summary>
        /// <returns>Uma lista de todas as especialidades.</returns>
        public async Task<IEnumerable<Especialidade>> GetAllAsync()
        {
            // Busca todas as especialidades no banco de dados sem rastreamento (AsNoTracking para melhor desempenho)
            var especialidades = await _context.Especialidades.AsNoTracking().ToListAsync();
            if (!especialidades.Any())
            {
                // Lança uma exceção se nenhuma especialidade for encontrada
                throw new Exception("Nenhuma especialidade encontrada");
            }
            return especialidades; // Retorna a lista de especialidades
        }

        /// <summary>
        /// Atualiza uma especialidade existente.
        /// </summary>
        /// <param name="especialidade">A especialidade com as informações atualizadas.</param>
        /// <returns>A especialidade atualizada.</returns>
        public async Task<Especialidade> UpdateAsync(Especialidade especialidade)
        {
            // Busca a especialidade existente pelo ID
            var existingEspecialidade = await GetByIdAsync(especialidade.IdEspecialidade);
            // Atualiza os valores da especialidade existente com os valores do objeto atualizado
            _context.Entry(existingEspecialidade).CurrentValues.SetValues(especialidade);
            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();
            return especialidade; // Retorna a especialidade atualizada
        }

        /// <summary>
        /// Deleta uma especialidade pelo ID.
        /// </summary>
        /// <param name="id">O ID da especialidade a ser deletada.</param>
        /// <returns>True se a exclusão for bem-sucedida, caso contrário lança uma exceção.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            // Busca a especialidade a ser deletada pelo ID
            var especialidade = await GetByIdAsync(id);
            if (especialidade == null)
            {
                // Lança uma exceção se a especialidade não for encontrada
                throw new Exception("Especialidade não encontrada para exclusão");
            }

            // Remove a especialidade do contexto e salva as alterações
            _context.Especialidades.Remove(especialidade);
            await _context.SaveChangesAsync();
            return true; // Retorna true se a exclusão for bem-sucedida
        }
    }
}
