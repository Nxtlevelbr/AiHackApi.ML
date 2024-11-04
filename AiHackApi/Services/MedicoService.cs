// Importa classes e namespaces necessários para o funcionamento da aplicação.
using AiHackApi.Models; // Contém as definições dos modelos de dados, como Medico.
using AiHackApi.Data; // Contém as definições do repositório para acesso aos dados, como IMedicoRepository.

public class MedicoService : IMedicoService
{
    // Repositório utilizado para acessar e manipular dados de médicos.
    private readonly IMedicoRepository _medicoRepository;

    // Construtor que recebe uma instância de IMedicoRepository para acesso aos dados.
    // Isso permite a injeção de dependências e facilita o teste da classe.
    public MedicoService(IMedicoRepository medicoRepository)
    {
        _medicoRepository = medicoRepository;
    }

    /// <summary>
    /// Cria um novo médico e o adiciona ao repositório.
    /// </summary>
    /// <param name="medico">O objeto <see cref="Medico"/> que contém as informações do médico a ser criado.</param>
    /// <returns>A tarefa que representa a operação assíncrona. O resultado é o objeto <see cref="Medico"/> criado.</returns>
    public async Task<Medico> CreateMedicoAsync(Medico medico)
    {
        return await _medicoRepository.CreateMedicoAsync(medico);
    }

    /// <summary>
    /// Obtém um médico específico pelo seu CRM.
    /// </summary>
    /// <param name="crmMedico">O CRM do médico.</param>
    /// <returns>A tarefa que representa a operação assíncrona. O resultado é o objeto <see cref="Medico"/> correspondente ao CRM fornecido.</returns>
    public async Task<Medico> GetMedicoByCrmAsync(int crmMedico)
    {
        return await _medicoRepository.GetMedicoByCrmAsync(crmMedico);
    }

    /// <summary>
    /// Obtém todos os médicos do repositório.
    /// </summary>
    /// <returns>A tarefa que representa a operação assíncrona. O resultado é uma coleção de objetos <see cref="Medico"/> representando todos os médicos.</returns>
    public async Task<IEnumerable<Medico>> GetAllMedicosAsync()
    {
        return await _medicoRepository.GetAllMedicosAsync();
    }

    /// <summary>
    /// Atualiza um médico existente no repositório.
    /// </summary>
    /// <param name="medico">O objeto <see cref="Medico"/> com as informações atualizadas do médico.</param>
    /// <returns>A tarefa que representa a operação assíncrona. O resultado é o objeto <see cref="Medico"/> atualizado.</returns>
    public async Task<Medico> UpdateMedicoAsync(Medico medico)
    {
        return await _medicoRepository.UpdateMedicoAsync(medico);
    }

    /// <summary>
    /// Remove um médico do repositório pelo seu CRM.
    /// </summary>
    /// <param name="crmMedico">O CRM do médico a ser removido.</param>
    /// <returns>A tarefa que representa a operação assíncrona. O resultado é um valor booleano indicando se a remoção foi bem-sucedida.</returns>
    public async Task<bool> DeleteMedicoAsync(int crmMedico)
    {
        return await _medicoRepository.DeleteMedicoAsync(crmMedico);
    }
}
