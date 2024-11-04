using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiHackApi.Models
{
    // Define o nome da tabela no banco de dados como "tb_consultas"
    [Table("tb_consultas")]
    public class Consulta
    {
        // Define o identificador único da consulta
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_consulta")]
        public int IdConsulta { get; set; }

        // Define a data e hora da consulta, campo obrigatório
        [Required]
        [Column("data_hora_consulta")]
        public DateTime DataHoraConsulta { get; set; }

        // Define o status da consulta, campo obrigatório
        [Required(ErrorMessage = "O status da consulta é obrigatório.")]
        [Column("status_consulta")]
        public string StatusConsulta { get; set; } = string.Empty; // Inicializando com valor padrão

        // Define o campo que armazena o ID do médico, chave estrangeira
        [Required]
        [Column("tb_medicos_id_medico")]
        public int TbMedicosIdMedico { get; set; }

        // Define o campo que armazena o CPF do paciente, chave estrangeira
        [Required]
        [Column("cpf_paciente")]
        public string CpfPaciente { get; set; } = string.Empty; // Inicializando com valor padrão

        // Relacionamento de chave estrangeira com a entidade Medico
        [ForeignKey("TbMedicosIdMedico")]
        public Medico Medico { get; set; } = new Medico(); // Inicializando para evitar nulls

        // Relacionamento de chave estrangeira com a entidade Paciente
        [ForeignKey("CpfPaciente")]
        public Paciente Paciente { get; set; } = new Paciente(); // Inicializando para evitar nulls
    }
}
