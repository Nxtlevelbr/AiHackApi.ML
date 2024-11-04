using AiHackApi.Data; // Para acessar o contexto do banco de dados
using AiHackApi.Models; // Importa a entidade Medico
using Microsoft.EntityFrameworkCore; // Para usar as funcionalidades do Entity Framework Core
using System.Collections.Generic; // Para o uso de IEnumerable
using System.Threading.Tasks; // Para operações assíncronas

// Implementa a interface IMedicoRepository para lidar com a lógica de acesso a dados relacionada aos médicos
public class MedicoRepository : IMedicoRepository
{
    // Campo privado para o contexto do banco de dados
    private readonly ApplicationDbContext _context;

    // Construtor que injeta o contexto no repositório
    public MedicoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adiciona um novo médico ao banco de dados.
    /// </summary>
    /// <param name="medico">O objeto Medico a ser criado.</param>
    /// <returns>O objeto Medico recém-criado.</returns>
    public async Task<Medico> CreateMedicoAsync(Medico medico)
    {
        // Adiciona o médico ao contexto
        _context.Medicos.Add(medico);
        // Salva as mudanças no banco de dados
        await _context.SaveChangesAsync();
        return medico;
    }

    /// <summary>
    /// Obtém um médico específico pelo CRM.
    /// </summary>
    /// <param name="crmMedico">O CRM do médico.</param>
    /// <returns>O objeto Medico correspondente ao CRM.</returns>
    public async Task<Medico> GetMedicoByCrmAsync(int crmMedico)
    {
        // Busca o médico pelo CRM no banco de dados
        var medico = await _context.Medicos.FindAsync(crmMedico);
        // Retorna o médico ou lança uma exceção se não for encontrado
        return medico ?? throw new NotFoundException("Médico não encontrado");
    }

    /// <summary>
    /// Obtém todos os médicos cadastrados no banco de dados.
    /// </summary>
    /// <returns>Uma lista de objetos Medico.</returns>
    public async Task<IEnumerable<Medico>> GetAllMedicosAsync()
    {
        // Retorna todos os médicos, incluindo suas especialidades, contatos e endereços
        return await _context.Medicos
                             .Include(m => m.Especialidade) // Inclui o relacionamento com a especialidade
                             .Include(m => m.Contato)       // Inclui o relacionamento com o contato
                             .Include(m => m.Endereco)      // Inclui o relacionamento com o endereço
                             .AsNoTracking()                // Usa AsNoTracking para otimizar a consulta
                             .ToListAsync();                // Converte o resultado em uma lista assíncrona
    }

    /// <summary>
    /// Atualiza os dados de um médico no banco de dados.
    /// </summary>
    /// <param name="medico">O objeto Medico com os dados atualizados.</param>
    /// <returns>O objeto Medico atualizado.</returns>
    public async Task<Medico> UpdateMedicoAsync(Medico medico)
    {
        // Marca o objeto como modificado
        _context.Entry(medico).State = EntityState.Modified;
        // Salva as mudanças no banco de dados
        await _context.SaveChangesAsync();
        return medico;
    }

    /// <summary>
    /// Exclui um médico do banco de dados pelo CRM.
    /// </summary>
    /// <param name="crmMedico">O CRM do médico a ser excluído.</param>
    /// <returns>True se o médico foi excluído com sucesso, caso contrário False.</returns>
    public async Task<bool> DeleteMedicoAsync(int crmMedico)
    {
        // Busca o médico pelo CRM
        var medico = await _context.Medicos.FindAsync(crmMedico);
        // Se não for encontrado, retorna false
        if (medico == null) return false;

        // Remove o médico do contexto
        _context.Medicos.Remove(medico);
        // Salva as mudanças no banco de dados
        await _context.SaveChangesAsync();
        return true;
    }
}
