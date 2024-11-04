using AiHackApi.Data; // Importa o contexto do banco de dados
using AiHackApi.Models; // Importa o modelo Consulta
using Microsoft.EntityFrameworkCore; // Usado para interagir com o banco de dados via Entity Framework
using System.Collections.Generic; // Para o uso de coleções como IEnumerable
using System.Threading.Tasks; // Para operações assíncronas

namespace AiHackApi.Repositories
{
    // Implementa a interface IConsultaRepository
    public class ConsultaRepository : IConsultaRepository
    {
        // O campo _context permite a interação com o banco de dados via Entity Framework
        private readonly ApplicationDbContext _context;

        // Construtor que injeta o contexto do banco de dados
        public ConsultaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria uma nova consulta no banco de dados.
        /// </summary>
        /// <param name="consulta">Objeto Consulta a ser criado.</param>
        /// <returns>A consulta recém-criada.</returns>
        public async Task<Consulta> CriarConsultaAsync(Consulta consulta)
        {
            // Verifica se o objeto Consulta passado é nulo e lança uma exceção se for
            if (consulta == null)
            {
                throw new ArgumentNullException(nameof(consulta)); // Garante que estamos passando um objeto válido
            }

            // Adiciona a nova consulta ao contexto de banco de dados
            await _context.Consultas.AddAsync(consulta);

            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();

            // Retorna a consulta recém-criada
            return consulta;
        }

        /// <summary>
        /// Obtém uma consulta pelo IdConsulta.
        /// </summary>
        /// <param name="idConsulta">O Id da consulta.</param>
        /// <returns>A consulta correspondente ao Id fornecido.</returns>
        public async Task<Consulta> ObterPorIdAsync(int idConsulta)
        {
            // Busca a consulta pelo IdConsulta no banco de dados
            var consulta = await _context.Consultas.FindAsync(idConsulta);

            // Se a consulta não for encontrada, lança uma exceção
            if (consulta == null)
            {
                throw new NotFoundException($"Consulta não encontrada para IdConsulta: {idConsulta}.");
            }

            // Retorna a consulta encontrada
            return consulta;
        }

        /// <summary>
        /// Obtém todas as consultas do banco de dados.
        /// </summary>
        /// <returns>Uma lista de consultas.</returns>
        public async Task<IEnumerable<Consulta>> ObterTodosAsync()
        {
            // Busca todas as consultas no banco de dados sem rastreamento (AsNoTracking)
            var consultas = await _context.Consultas.AsNoTracking().ToListAsync();

            // Se não houver consultas, lança uma exceção
            if (consultas == null || !consultas.Any())
            {
                throw new NotFoundException("Nenhuma consulta encontrada.");
            }

            // Retorna a lista de consultas
            return consultas;
        }

        /// <summary>
        /// Atualiza uma consulta existente.
        /// </summary>
        /// <param name="consulta">A consulta com os dados atualizados.</param>
        /// <returns>A consulta atualizada.</returns>
        public async Task<Consulta> AtualizarConsultaAsync(Consulta consulta)
        {
            // Verifica se o objeto Consulta passado é nulo e lança uma exceção se for
            if (consulta == null)
            {
                throw new ArgumentNullException(nameof(consulta)); // Garante que estamos atualizando um objeto válido
            }

            // Verifica se a consulta existe pelo IdConsulta antes de atualizar
            var existingConsulta = await ObterPorIdAsync(consulta.IdConsulta);
            if (existingConsulta == null)
            {
                throw new NotFoundException($"Consulta não encontrada para IdConsulta: {consulta.IdConsulta}.");
            }

            // Marca a consulta como modificada no contexto do Entity Framework
            _context.Entry(consulta).State = EntityState.Modified;

            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();

            // Retorna a consulta atualizada
            return consulta;
        }

        /// <summary>
        /// Deleta uma consulta existente pelo IdConsulta.
        /// </summary>
        /// <param name="idConsulta">O Id da consulta a ser excluída.</param>
        /// <returns>Booleano indicando sucesso ou falha da exclusão.</returns>
        public async Task<bool> DeletarConsultaAsync(int idConsulta)
        {
            // Busca a consulta pelo IdConsulta antes de tentar excluí-la
            var consulta = await ObterPorIdAsync(idConsulta);
            if (consulta == null)
            {
                return false; // Se a consulta não for encontrada, retorna false
            }

            // Remove a consulta do contexto do banco de dados
            _context.Consultas.Remove(consulta);

            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();

            // Retorna true para indicar que a exclusão foi bem-sucedida
            return true;
        }
    }
}
