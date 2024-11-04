using AiHackApi.Data; // Importa o contexto de dados da aplicação
using AiHackApi.Models; // Importa os modelos de dados, como Consulta
using Microsoft.EntityFrameworkCore; // Fornece classes e métodos para o Entity Framework Core
using System.Collections.Generic; // Para o uso de coleções genéricas, como IEnumerable
using System.Threading.Tasks; // Para o uso de operações assíncronas

public class ConsultaService : IConsultaService
{
    // Contexto de dados usado para acessar a base de dados.
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Construtor do serviço de consultas.
    /// </summary>
    /// <param name="context">Instância do contexto de dados da aplicação.</param>
    public ConsultaService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtém todas as consultas, incluindo os dados relacionados de pacientes e médicos.
    /// </summary>
    /// <returns>Uma lista de todas as consultas.</returns>
    public async Task<IEnumerable<Consulta>> GetAllConsultasAsync()
    {
        // Recupera todas as consultas, incluindo informações de pacientes e médicos.
        return await _context.Consultas
            .Include(c => c.Paciente) // Inclui as informações do paciente
            .Include(c => c.Medico) // Inclui as informações do médico
            .ToListAsync();
    }

    /// <summary>
    /// Obtém uma consulta específica pelo IdConsulta.
    /// </summary>
    /// <param name="idConsulta">O Id da consulta.</param>
    /// <returns>A consulta correspondente ao Id fornecido ou null se não for encontrada.</returns>
    public async Task<Consulta?> GetConsultaByIdAsync(int idConsulta)
    {
        // Recupera uma consulta pelo IdConsulta, incluindo informações de pacientes e médicos.
        return await _context.Consultas
            .Include(c => c.Paciente) // Inclui as informações do paciente
            .Include(c => c.Medico) // Inclui as informações do médico
            .FirstOrDefaultAsync(c => c.IdConsulta == idConsulta);
    }

    /// <summary>
    /// Cria uma nova consulta e a adiciona ao banco de dados.
    /// </summary>
    /// <param name="consulta">O objeto <see cref="Consulta"/> a ser criado.</param>
    public async Task CreateConsultaAsync(Consulta consulta)
    {
        // Adiciona a nova consulta ao contexto e salva as mudanças no banco de dados.
        _context.Consultas.Add(consulta);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Atualiza uma consulta existente no banco de dados.
    /// </summary>
    /// <param name="consulta">O objeto <see cref="Consulta"/> com as informações atualizadas.</param>
    public async Task UpdateConsultaAsync(Consulta consulta)
    {
        // Marca o objeto consulta como modificado e salva as mudanças no banco de dados.
        _context.Entry(consulta).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Remove uma consulta do banco de dados pelo IdConsulta.
    /// </summary>
    /// <param name="idConsulta">O Id da consulta a ser deletada.</param>
    public async Task DeleteConsultaAsync(int idConsulta)
    {
        // Encontra a consulta pelo IdConsulta e a remove do banco de dados, se existir.
        var consulta = await GetConsultaByIdAsync(idConsulta);
        if (consulta != null)
        {
            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();
        }
    }
}
