// Importações necessárias para as anotações de validação e mapeamento de banco de dados
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiHackApi.Models
{
    // Define a tabela do banco de dados à qual essa classe está mapeada
    [Table("tb_bairros")]
    public class Bairro
    {
        // Define a propriedade IdBairro como a chave primária da tabela
        [Key]
        [Column("id_bairro")] // Mapeia a coluna "id_bairro" no banco de dados
        public int IdBairro { get; set; }

        // Propriedade NomeBairro é obrigatória e mapeada para a coluna "nome_bairro"
        [Required] // Indica que essa propriedade é obrigatória
        [Column("nome_bairro")] // Mapeia a coluna "nome_bairro" no banco de dados
        public string NomeBairro { get; set; }

        // Construtor que assegura que NomeBairro tem um valor válido
        public Bairro(string nomeBairro)
        {
            // Se o valor passado for nulo, uma exceção é lançada
            NomeBairro = nomeBairro ?? throw new ArgumentNullException(nameof(nomeBairro));
        }

        // Construtor sem parâmetros, útil para inicialização sem dados
        public Bairro()
        {
            // Inicializando NomeBairro com um valor padrão para evitar valores nulos
            NomeBairro = string.Empty;
        }
    }
}



