using AiHackApi.DTOs;

namespace AiHackApi.Services;

/// <summary>
/// Interface para o serviço de busca de endereços por meio da API do ViaCep.
/// Define um método assíncrono para buscar o endereço correspondente a um CEP fornecido.
/// </summary>
public interface IViaCepService
{
    /// <summary>
    /// Busca o endereço completo com base no CEP informado.
    /// Faz uma requisição para a API ViaCep e retorna um DTO com os dados do endereço.
    /// </summary>
    /// <param name="cep">CEP do endereço a ser buscado.</param>
    /// <returns>Um objeto EnderecoDto contendo os detalhes do endereço correspondente.</returns>
    Task<EnderecoDto> BuscarEnderecoPorCepAsync(string cep);
}