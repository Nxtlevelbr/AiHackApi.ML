using AiHackApi.Models; // Importa o modelo Paciente
using System.Collections.Generic; // Para o uso de IEnumerable
using System.Threading.Tasks; // Para operações assíncronas

/// <summary>
/// Interface que define os métodos para o repositório de pacientes.
/// </summary>
public interface IPacienteRepository
{
    /// <summary>
    /// Adiciona um novo paciente no banco de dados.
    /// </summary>
    /// <param name="paciente">O objeto paciente a ser adicionado.</param>
    /// <returns>O paciente recém-criado.</returns>
    Task<Paciente> AdicionarAsync(Paciente paciente);

    /// <summary>
    /// Obtém os detalhes de um paciente específico pelo CPF.
    /// </summary>
    /// <param name="cpf">O CPF do paciente.</param>
    /// <returns>O paciente correspondente ao CPF informado ou null se não encontrado.</returns>
    Task<Paciente> ObterPorCpfAsync(string cpf);

    /// <summary>
    /// Retorna todos os pacientes cadastrados no banco de dados.
    /// </summary>
    /// <returns>Uma lista de pacientes.</returns>
    Task<IEnumerable<Paciente>> ObterTodosAsync();

    /// <summary>
    /// Atualiza os dados de um paciente existente.
    /// </summary>
    /// <param name="paciente">O objeto paciente com os dados atualizados.</param>
    /// <returns>O paciente atualizado.</returns>
    Task<Paciente> AtualizarAsync(Paciente paciente);

    /// <summary>
    /// Remove um paciente do banco de dados usando o CPF.
    /// </summary>
    /// <param name="cpf">O CPF do paciente a ser deletado.</param>
    /// <returns>True se o paciente foi deletado com sucesso, caso contrário False.</returns>
    Task<bool> DeletarAsync(string cpf);
}
