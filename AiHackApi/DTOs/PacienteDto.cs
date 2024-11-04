// Namespace que contém os DTOs da aplicação
namespace AiHackApi.DTOs
{
    // DTO para a entidade Paciente. Esse DTO é utilizado para transferir informações de pacientes
    // entre diferentes camadas da aplicação.
    public class PacienteDto
    {
        // Propriedade que armazena o nome do paciente
        public string NomePaciente { get; set; }

        // Propriedade que contém o CPF do paciente
        public string CPF { get; set; }

        // Construtor que inicializa o DTO com valores específicos
        public PacienteDto(string nomePaciente, string cpf)
        {
            // Verifica se o nome do paciente é nulo e lança uma exceção se for
            NomePaciente = nomePaciente ?? throw new ArgumentNullException(nameof(nomePaciente));

            // Verifica se o CPF é nulo e lança uma exceção se for
            CPF = cpf ?? throw new ArgumentNullException(nameof(cpf));
        }

        // Construtor padrão que inicializa as propriedades com valores padrão
        public PacienteDto()
        {
            // Inicializa o nome do paciente como uma string vazia
            NomePaciente = string.Empty;

            // Inicializa o CPF como uma string vazia
            CPF = string.Empty;
        }
    }
}
