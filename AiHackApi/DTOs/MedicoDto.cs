// Namespace que contém os DTOs da aplicação
namespace AiHackApi.DTOs
{
    // DTO para a entidade Médico. Esse DTO é usado para transferir informações sobre médicos
    // entre diferentes camadas da aplicação.
    public class MedicoDto
    {
        // Propriedade que contém o CRM do médico (registro profissional)
        public string CrmMedico { get; set; }

        // Propriedade que armazena o nome do médico
        public string NomeMedico { get; set; }

        // Propriedade que indica a especialidade médica do médico
        public string Especialidade { get; set; }

        // Construtor que inicializa o DTO com valores específicos
        public MedicoDto(string crmMedico, string nomeMedico, string especialidade)
        {
            // Inicializa o CRM do médico
            CrmMedico = crmMedico ?? throw new ArgumentNullException(nameof(crmMedico));

            // Inicializa o nome do médico
            NomeMedico = nomeMedico ?? throw new ArgumentNullException(nameof(nomeMedico));

            // Inicializa a especialidade
            Especialidade = especialidade ?? throw new ArgumentNullException(nameof(especialidade));
        }

        // Construtor padrão que inicializa as propriedades com valores padrão
        public MedicoDto()
        {
            // Inicializa o CRM como uma string vazia
            CrmMedico = string.Empty;

            // Inicializa o nome do médico como uma string vazia
            NomeMedico = string.Empty;

            // Inicializa a especialidade como uma string vazia
            Especialidade = string.Empty;
        }
    }
}
