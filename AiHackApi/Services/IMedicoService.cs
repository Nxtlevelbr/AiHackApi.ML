using AiHackApi.Models; // Contém a definição do modelo de dados, como Medico.
using AiHackApi.Data; // Pode conter o contexto de dados ou outras funcionalidades relacionadas a dados.

public interface IMedicoService
{
    /// <summary>
    /// Cria um novo médico e o adiciona ao repositório.
    /// </summary>
    /// <param name="medico">O objeto <see cref="Medico"/> a ser criado.</param>
    /// <returns>A tarefa que representa a operação assíncrona. O resultado é o médico criado.</returns>
    Task<Medico> CreateMedicoAsync(Medico medico);

    /// <summary>
    /// Obtém um médico específico pelo seu CRM.
    /// </summary>
    /// <param name="crmMedico">O CRM do médico.</param>
    /// <returns>A tarefa que representa a operação assíncrona. O resultado é o médico correspondente ao CRM.</returns>
    Task<Medico> GetMedicoByCrmAsync(int crmMedico);

    /// <summary>
    /// Obtém todos os médicos do repositório.
    /// </summary>
    /// <returns>A tarefa que representa a operação assíncrona. O resultado é uma coleção de todos os médicos.</returns>
    Task<IEnumerable<Medico>> GetAllMedicosAsync();

    /// <summary>
    /// Atualiza um médico existente no repositório.
    /// </summary>
    /// <param name="medico">O objeto <see cref="Medico"/> com as informações atualizadas.</param>
    /// <returns>A tarefa que representa a operação assíncrona. O resultado é o médico atualizado.</returns>
    Task<Medico> UpdateMedicoAsync(Medico medico);

    /// <summary>
    /// Remove um médico do repositório pelo seu CRM.
    /// </summary>
    /// <param name="crmMedico">O CRM do médico a ser removido.</param>
    /// <returns>A tarefa que representa a operação assíncrona. O resultado é um valor booleano indicando se a operação foi bem-sucedida.</returns>
    Task<bool> DeleteMedicoAsync(int crmMedico);
}
