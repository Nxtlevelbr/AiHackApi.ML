// Importa classes e namespaces necessários para o funcionamento da aplicação.
using AiHackApi.DTOs; // Contém as definições dos Data Transfer Objects (DTOs), como PacienteDto.
using System.Collections.Generic; // Fornece interfaces e classes que definem coleções genéricas, como IEnumerable.
using System.Threading.Tasks; // Fornece tipos que permitem operações assíncronas, como Task.

namespace AiHackApi.Services
{
    public interface IPacienteService
    {
        /// <summary>
        /// Obtém um paciente específico pelo seu CPF.
        /// </summary>
        /// <param name="cpf">O CPF do paciente.</param>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é um objeto <see cref="PacienteDto"/> representando o paciente com o CPF fornecido.</returns>
        Task<PacienteDto> GetPacienteByCpfAsync(string cpf);

        /// <summary>
        /// Obtém todos os pacientes.
        /// </summary>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é uma coleção de objetos <see cref="PacienteDto"/> representando todos os pacientes.</returns>
        Task<IEnumerable<PacienteDto>> GetAllPacientesAsync();

        /// <summary>
        /// Cria um novo paciente e o adiciona ao repositório.
        /// </summary>
        /// <param name="pacienteDto">O objeto <see cref="PacienteDto"/> que contém as informações do paciente a ser criado.</param>
        /// <returns>A tarefa que representa a operação assíncrona.</returns>
        Task CreatePacienteAsync(PacienteDto pacienteDto);

        /// <summary>
        /// Atualiza um paciente existente no repositório.
        /// </summary>
        /// <param name="pacienteDto">O objeto <see cref="PacienteDto"/> com as informações atualizadas do paciente.</param>
        /// <returns>A tarefa que representa a operação assíncrona.</returns>
        Task UpdatePacienteAsync(PacienteDto pacienteDto);

        /// <summary>
        /// Remove um paciente do repositório pelo seu CPF.
        /// </summary>
        /// <param name="cpf">O CPF do paciente a ser removido.</param>
        /// <returns>A tarefa que representa a operação assíncrona.</returns>
        Task DeletePacienteAsync(string cpf);
    }
}
