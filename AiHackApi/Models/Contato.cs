using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiHackApi.Models
{
    // Define o nome da tabela como "tb_contatos" no banco de dados
    [Table("tb_contatos")]
    public class Contato
    {
        // Define a propriedade Email como chave primária da tabela
        [Key]
        [Column("email")] // Mapeia a coluna "email" no banco de dados
        public string Email { get; set; }

        // Define a propriedade NomeContato como campo obrigatório
        [Required] // O campo é obrigatório
        [Column("nome_contato")] // Mapeia a coluna "nome_contato" no banco de dados
        [StringLength(100, ErrorMessage = "O nome do contato pode ter no máximo 100 caracteres.")]
        public string NomeContato { get; set; }

        // Define a propriedade Telefone como campo obrigatório
        [Required] // O campo é obrigatório
        [Column("telefone")] // Mapeia a coluna "telefone" no banco de dados
        [StringLength(15, ErrorMessage = "O telefone pode ter no máximo 15 caracteres.")]
        public string Telefone { get; set; }

        // Construtor que inicializa os campos obrigatórios com valores válidos
        public Contato(string nomeContato, string telefone, string email)
        {
            NomeContato = nomeContato ?? throw new ArgumentNullException(nameof(nomeContato)); // Verifica se NomeContato é nulo
            Telefone = telefone ?? throw new ArgumentNullException(nameof(telefone)); // Verifica se telefone é nulo
            Email = email ?? throw new ArgumentNullException(nameof(email)); // Verifica se email é nulo
        }

        // Construtor sem parâmetros, inicializando os campos com valores padrão
        public Contato()
        {
            // Inicializa as propriedades com valores padrões para evitar nulos
            NomeContato = string.Empty;
            Telefone = string.Empty;
            Email = string.Empty;
        }
    }
}
