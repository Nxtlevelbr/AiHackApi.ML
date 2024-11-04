// Namespace que contém os DTOs (Data Transfer Objects) da aplicação
namespace AiHackApi.DTOs
{
    // DTO para a entidade Consulta. Esse DTO transfere dados sobre consultas 
    // sem expor diretamente o modelo de dados do banco.
    public class ConsultaDto
    {
        // Propriedade que representa o ID único da consulta
        public int IdConsulta { get; set; }

        // Propriedade que representa a data e hora da consulta
        public DateTime DataHoraConsulta { get; set; }

        // Propriedade que representa o CPF do paciente relacionado à consulta
        public string CpfPaciente { get; set; }

        // Propriedade que representa o ID do médico relacionado à consulta
        public int IdMedico { get; set; }

        // Construtor que inicializa o DTO com valores específicos para todas as propriedades
        public ConsultaDto(int idConsulta, DateTime dataHoraConsulta, string cpfPaciente, int idMedico)
        {
            // Inicializa as propriedades com os valores fornecidos
            IdConsulta = idConsulta;
            DataHoraConsulta = dataHoraConsulta;

            // Inicializa o CPF do paciente e o ID do médico
            CpfPaciente = cpfPaciente ?? throw new ArgumentNullException(nameof(cpfPaciente));
            IdMedico = idMedico;
        }

        // Construtor padrão para inicializar o DTO com valores padrão
        public ConsultaDto()
        {
            // Inicializa as propriedades com valores padrão
            IdConsulta = 0;
            DataHoraConsulta = DateTime.MinValue;
            CpfPaciente = string.Empty;
            IdMedico = 0;
        }
    }
}
