// Namespace que agrupa todas as classes DTOs (Data Transfer Objects) do projeto
namespace AiHackApi.DTOs
{
    // BairroDto é um objeto simples usado para transferir dados entre camadas, no caso, 
    // representando o modelo de Bairro sem expor diretamente o modelo do banco de dados.
    public class BairroDto
    {
        // Propriedade que representa o ID do Bairro
        public int IdBairro { get; set; }

        // Propriedade que representa o nome do Bairro
        public string NomeBairro { get; set; }

        // Construtor que inicializa o DTO com valores específicos para o IdBairro e NomeBairro
        public BairroDto(int idBairro, string nomeBairro)
        {
            // Verifica se o nomeBairro é nulo e, se for, lança uma exceção
            IdBairro = idBairro;
            NomeBairro = nomeBairro ?? throw new ArgumentNullException(nameof(nomeBairro));
        }

        // Construtor padrão que inicializa o NomeBairro com uma string vazia, para evitar valores nulos
        public BairroDto()
        {
            NomeBairro = string.Empty;
        }
    }
}
