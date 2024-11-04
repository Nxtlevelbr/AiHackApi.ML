using AiHackApi.Models; // Importa o modelo Medico
using System.Collections.Generic; // Para o uso de IEnumerable
using System.Threading.Tasks; // Para operações assíncronas

/// <summary>
/// Interface que define os métodos para o repositório de médicos.
/// </summary>
public interface IMedicoRepository
{
    /// <summary>
    /// Adiciona um novo médico no banco de dados.
    /// </summary>
    /// <param name="medico">O objeto médico a ser criado.</param>
    /// <returns>O médico recém-criado.</returns>
    Task<Medico> CreateMedicoAsync(Medico medico);

    /// <summary>
    /// Obtém os detalhes de um médico específico pelo CRM.
    /// </summary>
    /// <param name="crmMedico">O CRM do médico.</param>
    /// <returns>O médico correspondente ao CRM informado.</returns>
    Task<Medico> GetMedicoByCrmAsync(int crmMedico);

    /// <summary>
    /// Retorna todos os médicos cadastrados no banco de dados.
    /// </summary>
    /// <returns>Uma lista de médicos.</returns>
    Task<IEnumerable<Medico>> GetAllMedicosAsync();

    /// <summary>
    /// Atualiza os dados de um médico existente.
    /// </summary>
    /// <param name="medico">O objeto médico com os dados atualizados.</param>
    /// <returns>O médico atualizado.</returns>
    Task<Medico> UpdateMedicoAsync(Medico medico);

    /// <summary>
    /// Remove um médico do banco de dados usando o CRM.
    /// </summary>
    /// <param name="crmMedico">O CRM do médico a ser deletado.</param>
    /// <returns>True se o médico foi deletado com sucesso, caso contrário False.</returns>
    Task<bool> DeleteMedicoAsync(int crmMedico);
}
