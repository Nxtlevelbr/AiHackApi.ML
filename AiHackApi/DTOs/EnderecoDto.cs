using System;

namespace AiHackApi.DTOs
{
    /// <summary>
    /// DTO para a entidade Endereço. Esse DTO é responsável por transferir os dados de endereço
    /// sem expor diretamente o modelo de dados do banco de dados.
    /// </summary>
    public class EnderecoDto
    {
        // Propriedade que representa o ID do endereço
        public int IdEndereco { get; set; }

        // Propriedade que armazena o logradouro (rua ou avenida) do endereço
        public string Logradouro { get; set; }

        // Propriedade que armazena o bairro do endereço
        public string Bairro { get; set; }

        // Propriedade que armazena a cidade do endereço
        public string Localidade { get; set; } // Mapeia a cidade retornada pela API ViaCEP

        // Propriedade que armazena o estado do endereço
        public string Uf { get; set; } // Mapeia o estado retornado pela API ViaCEP

        // Propriedade que armazena o CEP (Código de Endereçamento Postal)
        public string Cep { get; set; }

        /// <summary>
        /// Construtor que inicializa o DTO com valores específicos.
        /// </summary>
        public EnderecoDto(int idEndereco, string logradouro, string bairro, string localidade, string uf, string cep)
        {
            IdEndereco = idEndereco;
            Logradouro = logradouro ?? throw new ArgumentNullException(nameof(logradouro));
            Bairro = bairro ?? throw new ArgumentNullException(nameof(bairro));
            Localidade = localidade ?? throw new ArgumentNullException(nameof(localidade));
            Uf = uf ?? throw new ArgumentNullException(nameof(uf));
            Cep = cep ?? throw new ArgumentNullException(nameof(cep));
        }

        /// <summary>
        /// Construtor padrão que inicializa as propriedades com valores padrão.
        /// </summary>
        public EnderecoDto()
        {
            Logradouro = string.Empty;
            Bairro = string.Empty;
            Localidade = string.Empty;
            Uf = string.Empty;
            Cep = string.Empty;
        }
    }
}
