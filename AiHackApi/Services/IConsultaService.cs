using AiHackApi.Models; // Importa os modelos de dados, como Consulta
using System.Collections.Generic; // Para coleções genéricas como IEnumerable
using System.Threading.Tasks; // Para o uso de operações assíncronas

public interface IConsultaService
{
    /// <summary>
    /// Obtém todas as consultas do repositório.
    /// </summary>
    /// <returns>A tarefa que representa a operação assíncrona. O resultado é uma coleção de todas as consultas.</returns>
    Task<IEnumerable<Consulta>> GetAllConsultasAsync();

    /// <summary>
    /// Obtém uma consulta específica pelo IdConsulta.
    /// </summary>
    /// <param name="idConsulta">O Id da consulta.</param>
    /// <returns>A tarefa que representa a operação assíncrona. O resultado é a consulta correspondente ao Id fornecido ou null se não for encontrada.</returns>
    Task<Consulta?> GetConsultaByIdAsync(int idConsulta);

    /// <summary>
    /// Cria uma nova consulta e a adiciona ao repositório.
    /// </summary>
    /// <param name="consulta">O objeto <see cref="Consulta"/> a ser criado.</param>
    /// <returns>A tarefa que representa a operação assíncrona.</returns>
    Task CreateConsultaAsync(Consulta consulta);

    /// <summary>
    /// Atualiza uma consulta existente no repositório.
    /// </summary>
    /// <param name="consulta">O objeto <see cref="Consulta"/> com as informações atualizadas.</param>
    /// <returns>A tarefa que representa a operação assíncrona.</returns>
    Task UpdateConsultaAsync(Consulta consulta);

    /// <summary>
    /// Remove uma consulta do repositório pelo IdConsulta.
    /// </summary>
    /// <param name="idConsulta">O Id da consulta a ser removida.</param>
    /// <returns>A tarefa que representa a operação assíncrona.</returns>
    Task DeleteConsultaAsync(int idConsulta);
}
