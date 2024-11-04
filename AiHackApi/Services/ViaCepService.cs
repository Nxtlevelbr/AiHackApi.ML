using AiHackApi.DTOs;

namespace AiHackApi.Services
{
    /// <summary>
    /// Serviço responsável por buscar informações de endereço com base no CEP, usando a API ViaCEP.
    /// </summary>
    public class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Inicializa o serviço ViaCepService e define a URL base da API ViaCEP.
        /// </summary>
        /// <param name="httpClient">Instância de HttpClient para realizar requisições HTTP.</param>
        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://viacep.com.br/ws/");
        }

        /// <summary>
        /// Realiza uma consulta na API ViaCEP para obter o endereço com base no CEP informado.
        /// </summary>
        /// <param name="cep">CEP a ser pesquisado.</param>
        /// <returns>Objeto <see cref="EnderecoDto"/> com os dados de endereço, caso a requisição seja bem-sucedida.</returns>
        /// <exception cref="Exception">Lança uma exceção se a requisição à API ViaCEP falhar.</exception>
        public async Task<EnderecoDto> BuscarEnderecoPorCepAsync(string cep)
        {
            var response = await _httpClient.GetAsync($"{cep}/json/");
            if (response.IsSuccessStatusCode)
            {
                // Lê e deserializa o conteúdo da resposta JSON em um objeto EnderecoDto
                var endereco = await response.Content.ReadFromJsonAsync<EnderecoDto>();
                return endereco;
            }
            else
            {
                // Lança uma exceção caso a resposta da API não seja bem-sucedida
                throw new Exception("Erro ao buscar o endereço no ViaCEP.");
            }
        }
    }
}