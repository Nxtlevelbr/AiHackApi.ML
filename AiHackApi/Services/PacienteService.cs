// Importa classes e namespaces necessários para o funcionamento da aplicação.
using AiHackApi.DTOs;
using AiHackApi.Models;
using AiHackApi.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiHackApi.Services
{
    public class PacienteService : IPacienteService
    {
        // Repositório utilizado para acessar e manipular dados de pacientes.
        private readonly IPacienteRepository _pacienteRepository;

        // Construtor que recebe uma instância de IPacienteRepository para acesso aos dados.
        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        /// <summary>
        /// Obtém um paciente específico pelo CPF e o retorna como um DTO.
        /// </summary>
        /// <param name="cpf">O CPF do paciente.</param>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é um <see cref="PacienteDto"/> correspondente ao CPF fornecido.</returns>
        /// <exception cref="KeyNotFoundException">Lançado se o paciente não for encontrado.</exception>
        public async Task<PacienteDto> GetPacienteByCpfAsync(string cpf)
        {
            var paciente = await _pacienteRepository.ObterPorCpfAsync(cpf);
            if (paciente == null)
            {
                throw new KeyNotFoundException("Paciente não encontrado.");
            }
            // Converte o modelo de dados Paciente para um PacienteDto
            return new PacienteDto
            {
                CPF = paciente.CPF,
                NomePaciente = paciente.NomePaciente
            };
        }

        /// <summary>
        /// Obtém todos os pacientes e os retorna como uma coleção de DTOs.
        /// </summary>
        /// <returns>A tarefa que representa a operação assíncrona. O resultado é uma coleção de <see cref="PacienteDto"/> representando todos os pacientes.</returns>
        public async Task<IEnumerable<PacienteDto>> GetAllPacientesAsync()
        {
            var pacientes = await _pacienteRepository.ObterTodosAsync();
            // Converte a coleção de modelos de dados Paciente para uma coleção de PacienteDto
            return pacientes.Select(p => new PacienteDto
            {
                CPF = p.CPF,
                NomePaciente = p.NomePaciente
            });
        }

        /// <summary>
        /// Cria um novo paciente com base nas informações fornecidas no DTO.
        /// </summary>
        /// <param name="pacienteDto">O objeto <see cref="PacienteDto"/> que contém as informações do paciente a ser criado.</param>
        /// <returns>A tarefa que representa a operação assíncrona.</returns>
        /// <exception cref="InvalidOperationException">Lançado se já houver um paciente com o mesmo CPF.</exception>
        public async Task CreatePacienteAsync(PacienteDto pacienteDto)
        {
            // Verifica se já existe um paciente com o mesmo CPF
            var pacienteExistente = await _pacienteRepository.ObterPorCpfAsync(pacienteDto.CPF);
            if (pacienteExistente != null)
            {
                throw new InvalidOperationException("O CPF já está registrado.");
            }

            // Prossegue com a criação do novo paciente
            var paciente = new Paciente
            {
                CPF = pacienteDto.CPF,
                NomePaciente = pacienteDto.NomePaciente
            };

            await _pacienteRepository.AdicionarAsync(paciente);
        }

        /// <summary>
        /// Atualiza as informações de um paciente existente com base no DTO fornecido.
        /// </summary>
        /// <param name="pacienteDto">O objeto <see cref="PacienteDto"/> com as informações atualizadas do paciente.</param>
        /// <returns>A tarefa que representa a operação assíncrona.</returns>
        /// <exception cref="KeyNotFoundException">Lançado se o paciente não for encontrado.</exception>
        public async Task UpdatePacienteAsync(PacienteDto pacienteDto)
        {
            var paciente = await _pacienteRepository.ObterPorCpfAsync(pacienteDto.CPF);
            if (paciente == null)
            {
                throw new KeyNotFoundException("Paciente não encontrado.");
            }
            // Atualiza as informações do paciente com base no DTO
            paciente.NomePaciente = pacienteDto.NomePaciente;

            await _pacienteRepository.AtualizarAsync(paciente);
        }

        /// <summary>
        /// Remove um paciente do repositório pelo seu CPF.
        /// </summary>
        /// <param name="cpf">O CPF do paciente a ser removido.</param>
        /// <returns>A tarefa que representa a operação assíncrona.</returns>
        /// <exception cref="KeyNotFoundException">Lançado se o paciente não for encontrado.</exception>
        public async Task DeletePacienteAsync(string cpf)
        {
            var paciente = await _pacienteRepository.ObterPorCpfAsync(cpf);
            if (paciente == null)
            {
                throw new KeyNotFoundException("Paciente não encontrado.");
            }
            await _pacienteRepository.DeletarAsync(cpf);
        }
    }
}
