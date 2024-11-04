// Namespace que contém os DTOs da aplicação
namespace AiHackApi.DTOs
{
    // DTO para a entidade Contato. Esse DTO é responsável por transferir dados de contato 
    // sem expor diretamente o modelo de dados do banco de dados.
    public class ContatoDto
    {
        // Propriedade que armazena o nome do contato
        public string NomeContato { get; set; }

        // Propriedade que armazena o número de telefone do contato
        public string Telefone { get; set; }

        // Propriedade que armazena o e-mail do contato (chave primária)
        public string Email { get; set; }

        // Construtor que inicializa o DTO com valores específicos
        public ContatoDto(string nomeContato, string telefone, string email)
        {
            // Inicializa o Nome do contato
            NomeContato = nomeContato ?? throw new ArgumentNullException(nameof(nomeContato));

            // Inicializa o Telefone, verificando se é nulo
            Telefone = telefone ?? throw new ArgumentNullException(nameof(telefone));

            // Inicializa o Email, verificando se é nulo
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        // Construtor padrão que inicializa as propriedades com valores padrão
        public ContatoDto()
        {
            // Inicializa NomeContato, Telefone e Email com strings vazias
            NomeContato = string.Empty;
            Telefone = string.Empty;
            Email = string.Empty;
        }
    }
}
