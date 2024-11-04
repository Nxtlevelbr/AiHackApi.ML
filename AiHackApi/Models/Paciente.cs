using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiHackApi.Models
{
    // Define a tabela "tb_pacientes" no banco de dados para esta classe
    [Table("tb_pacientes")]
    public class Paciente
    {
        // Define a propriedade CPF como chave primária
        [Key]
        [Column("cpf")] // Mapeia a coluna "cpf" no banco de dados
        public string CPF { get; set; }

        // Define a propriedade NomePaciente como campo obrigatório
        [Required] // O campo é obrigatório
        [Column("nome_paciente")] // Mapeia a coluna "nome_paciente" no banco de dados
        public string NomePaciente { get; set; }

        // Construtor que assegura que os campos obrigatórios têm valores válidos
        public Paciente(string nomePaciente, string cpf)
        {
            NomePaciente = nomePaciente ?? throw new ArgumentNullException(nameof(nomePaciente));
            CPF = cpf ?? throw new ArgumentNullException(nameof(cpf));
        }

        // Construtor sem parâmetros, inicializando valores padrão
        public Paciente()
        {
            NomePaciente = string.Empty; // Inicializa NomePaciente como string vazia
            CPF = string.Empty; // Inicializa CPF como string vazia
        }
    }
}
