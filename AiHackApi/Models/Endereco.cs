using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiHackApi.Models
{
    // Define a tabela "tb_enderecos" no banco de dados para esta classe
    [Table("tb_enderecos")]
    public class Endereco
    {
        // Define a propriedade IdEndereco como chave primária
        [Key]
        [Column("id_endereco")] // Mapeia a coluna "id_endereco" no banco de dados
        public int IdEndereco { get; set; }

        // Define a propriedade Rua como campo obrigatório
        [Required]
        [Column("rua")]
        public string Rua { get; set; }

        // Define a propriedade Numero como campo obrigatório
        [Required]
        [Column("numero")]
        public string Numero { get; set; }

        // Define a propriedade Bairro como campo obrigatório
        [Required]
        [Column("bairro")]
        public string Bairro { get; set; }

        // Define a propriedade Cep como campo obrigatório
        [Required]
        [Column("cep")]
        public string Cep { get; set; }

        // Define a propriedade Cidade como campo obrigatório
        [Required]
        [Column("cidade")]
        public string Cidade { get; set; }

        // Define a propriedade Logradouro como campo obrigatório
        [Required]
        [Column("logradouro")]
        public string Logradouro { get; set; }

        // Define a propriedade UF como campo obrigatório
        [Required]
        [Column("uf")]
        public string UF { get; set; }

        // Construtor que assegura que os campos obrigatórios têm valores válidos
        public Endereco(string rua, string numero, string bairro, string cep, string cidade, string logradouro, string uf)
        {
            Rua = rua ?? throw new ArgumentNullException(nameof(rua));
            Numero = numero ?? throw new ArgumentNullException(nameof(numero));
            Bairro = bairro ?? throw new ArgumentNullException(nameof(bairro));
            Cep = cep ?? throw new ArgumentNullException(nameof(cep));
            Cidade = cidade ?? throw new ArgumentNullException(nameof(cidade));
            Logradouro = logradouro ?? throw new ArgumentNullException(nameof(logradouro));
            UF = uf ?? throw new ArgumentNullException(nameof(uf));
        }

        // Construtor sem parâmetros, inicializando com valores padrão
        public Endereco()
        {
            Rua = string.Empty;
            Numero = string.Empty;
            Bairro = string.Empty;
            Cep = string.Empty;
            Cidade = string.Empty;
            Logradouro = string.Empty;
            UF = string.Empty;
        }
    }
}
