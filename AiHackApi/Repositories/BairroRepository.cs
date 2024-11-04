using AiHackApi.Data; // Importa o contexto do banco de dados
using AiHackApi.Models; // Importa o modelo de Bairro
using Microsoft.EntityFrameworkCore; // Usado para operações no banco de dados
using System.Collections.Generic; // Usado para coleções como IEnumerable
using System.Threading.Tasks; // Usado para operações assíncronas

namespace AiHackApi.Repositories
{
    // Classe BairroRepository implementa a interface IBairroRepository
    public class BairroRepository : IBairroRepository
    {
        // O campo _context permite o acesso ao banco de dados via Entity Framework
        private readonly ApplicationDbContext _context;

        // O construtor injeta a dependência do ApplicationDbContext no repositório
        public BairroRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona um novo bairro ao banco de dados.
        /// </summary>
        /// <param name="bairro">O objeto Bairro a ser adicionado.</param>
        /// <returns>O bairro recém-adicionado.</returns>
        public async Task<Bairro> AddAsync(Bairro bairro)
        {
            if (bairro == null)
            {
                // Lança exceção se o objeto Bairro for nulo
                throw new ArgumentNullException(nameof(bairro));
            }

            // Adiciona o novo bairro ao contexto do banco de dados
            await _context.Bairros.AddAsync(bairro);
            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();
            return bairro; // Retorna o bairro adicionado
        }

        /// <summary>
        /// Obtém um bairro por seu ID.
        /// </summary>
        /// <param name="id">O ID do bairro.</param>
        /// <returns>O bairro correspondente ao ID, ou uma exceção se não for encontrado.</returns>
        public async Task<Bairro> GetByIdAsync(int id)
        {
            // Busca o bairro pelo seu ID no banco de dados
            var bairro = await _context.Bairros.FindAsync(id);
            if (bairro == null)
            {
                // Lança uma exceção personalizada se o bairro não for encontrado
                throw new NotFoundException($"Bairro com ID {id} não encontrado.");
            }
            return bairro; // Retorna o bairro encontrado
        }

        /// <summary>
        /// Obtém todos os bairros do banco de dados.
        /// </summary>
        /// <returns>Uma lista de bairros.</returns>
        public async Task<IEnumerable<Bairro>> GetAllAsync()
        {
            // Busca todos os bairros do banco de dados, sem rastreamento (AsNoTracking)
            var bairros = await _context.Bairros.AsNoTracking().ToListAsync();
            if (bairros == null || !bairros.Any())
            {
                // Lança exceção se nenhum bairro for encontrado
                throw new NotFoundException("Nenhum bairro encontrado.");
            }
            return bairros; // Retorna a lista de bairros
        }

        /// <summary>
        /// Atualiza um bairro existente no banco de dados.
        /// </summary>
        /// <param name="bairro">O objeto Bairro atualizado.</param>
        /// <returns>O bairro atualizado.</returns>
        public async Task<Bairro> UpdateAsync(Bairro bairro)
        {
            if (bairro == null)
            {
                // Lança exceção se o objeto Bairro for nulo
                throw new ArgumentNullException(nameof(bairro));
            }

            // Busca o bairro existente pelo ID
            var existingBairro = await GetByIdAsync(bairro.IdBairro);
            if (existingBairro == null)
            {
                // Lança exceção se o bairro não for encontrado
                throw new NotFoundException($"Bairro com ID {bairro.IdBairro} não encontrado.");
            }

            // Marca o bairro como modificado e salva as alterações
            _context.Entry(bairro).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return bairro; // Retorna o bairro atualizado
        }

        /// <summary>
        /// Deleta um bairro existente pelo ID.
        /// </summary>
        /// <param name="id">O ID do bairro a ser deletado.</param>
        /// <returns>Um valor booleano indicando se a exclusão foi bem-sucedida.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            // Busca o bairro a ser deletado
            var bairro = await GetByIdAsync(id);
            if (bairro == null)
            {
                // Lança exceção se o bairro não for encontrado
                throw new NotFoundException($"Bairro com ID {id} não encontrado.");
            }

            // Remove o bairro do contexto e salva as alterações
            _context.Bairros.Remove(bairro);
            await _context.SaveChangesAsync();
            return true; // Indica que a exclusão foi bem-sucedida
        }
    }
}

