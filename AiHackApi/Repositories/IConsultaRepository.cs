using AiHackApi.Models; // Importa o modelo Consulta
using System.Collections.Generic; // Para o uso de IEnumerable
using System.Threading.Tasks; // Para o uso de Task e operações assíncronas

namespace AiHackApi.Repositories
{
    /// <summary>
    /// Interface que define os métodos para o repositório de consultas.
    /// </summary>
    public interface IConsultaRepository
    {
        /// <summary>
        /// Cria uma nova consulta no banco de dados.
        /// </summary>
        /// <param name="consulta">A consulta a ser criada.</param>
        /// <returns>A consulta recém-criada.</returns>
        Task<Consulta> CriarConsultaAsync(Consulta consulta);

        /// <summary>
        /// Busca uma consulta pelo IdConsulta.
        /// </summary>
        /// <param name="idConsulta">O Id da consulta.</param>
        /// <returns>A consulta correspondente ao Id fornecido.</returns>
        Task<Consulta> ObterPorIdAsync(int idConsulta);

        /// <summary>
        /// Retorna todas as consultas cadastradas.
        /// </summary>
        /// <returns>Uma lista de todas as consultas.</returns>
        Task<IEnumerable<Consulta>> ObterTodosAsync();

        /// <summary>
        /// Atualiza os dados de uma consulta existente.
        /// </summary>
        /// <param name="consulta">A consulta com os dados atualizados.</param>
        /// <returns>A consulta atualizada.</returns>
        Task<Consulta> AtualizarConsultaAsync(Consulta consulta);

        /// <summary>
        /// Deleta uma consulta pelo IdConsulta.
        /// </summary>
        /// <param name="idConsulta">O Id da consulta a ser deletada.</param>
        /// <returns>True se a consulta foi deletada com sucesso, caso contrário False.</returns>
        Task<bool> DeletarConsultaAsync(int idConsulta);
    }
}
