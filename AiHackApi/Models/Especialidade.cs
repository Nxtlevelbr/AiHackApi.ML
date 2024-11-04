using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiHackApi.Models
{
    // Define a tabela "tb_especialidades" no banco de dados para esta classe
    [Table("tb_especialidades")]
    public class Especialidade
    {
        // Define a propriedade IdEspecialidade como chave primária
        [Key]
        [Column("id_especialidade")] // Mapeia a coluna "id_especialidade" no banco de dados
        public int IdEspecialidade { get; set; }

        // Define a propriedade NomeEspecialidade como campo obrigatório
        [Required] // O campo é obrigatório
        [Column("nome_especialidade")] // Mapeia a coluna "nome_especialidade" no banco de dados
        public string NomeEspecialidade { get; set; }

        // Define a propriedade Ativo para indicar se a especialidade está ativa (1 para ativo, 0 para inativo)
        [Required] // O campo é obrigatório
        [Column("ativo")] // Mapeia a coluna "ativo" no banco de dados
        public int Ativo { get; set; } // Usa int para representar o booleano (1 = true, 0 = false)

        // Construtor que assegura que os campos obrigatórios têm valores válidos
        public Especialidade(string nomeEspecialidade, bool ativo)
        {
            // Garante que NomeEspecialidade não seja nulo
            NomeEspecialidade = nomeEspecialidade ?? throw new ArgumentNullException(nameof(nomeEspecialidade));
            // Converte o valor bool de ativo para int (1 ou 0)
            Ativo = ativo ? 1 : 0;
        }

        // Construtor sem parâmetros, inicializando valores padrão
        public Especialidade()
        {
            NomeEspecialidade = string.Empty; // Inicializa NomeEspecialidade como string vazia
            Ativo = 1; // Definir como ativo por padrão (1 = true)
        }
    }
}
