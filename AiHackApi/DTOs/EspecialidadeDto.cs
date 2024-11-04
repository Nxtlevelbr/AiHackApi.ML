// Namespace que contém os DTOs da aplicação
namespace AiHackApi.DTOs
{
    // DTO para a entidade Especialidade. Esse DTO é responsável por transferir dados
    // referentes a especialidades médicas entre camadas da aplicação.
    public class EspecialidadeDto
    {
        // Propriedade que representa o ID da especialidade
        public int IdEspecialidade { get; set; }

        // Propriedade que armazena o nome da especialidade
        public string NomeEspecialidade { get; set; }

        // Construtor que inicializa o DTO com valores específicos
        public EspecialidadeDto(int idEspecialidade, string nomeEspecialidade)
        {
            // Inicializa o ID da especialidade
            IdEspecialidade = idEspecialidade;

            // Verifica se o nome da especialidade é nulo e lança uma exceção se for
            NomeEspecialidade = nomeEspecialidade ?? throw new ArgumentNullException(nameof(nomeEspecialidade));
        }

        // Construtor padrão que inicializa as propriedades com valores padrão
        public EspecialidadeDto()
        {
            // Inicializa o nome da especialidade como uma string vazia
            NomeEspecialidade = string.Empty;
        }
    }
}
