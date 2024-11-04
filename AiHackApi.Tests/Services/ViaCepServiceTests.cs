using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AiHackApi.DTOs;
using AiHackApi.Services;
using Moq;
using Moq.Protected;
using Xunit;

namespace AiHackApi.Tests.Services
{
    public class ViaCepServiceTests
    {
        [Fact]
        public async Task BuscarEnderecoPorCepAsync_DeveRetornarEndereco_QuandoCepValido()
        {
            // Arrange: Cria uma resposta de simulação com os dados esperados
            var jsonResponse = @"{
                ""cep"": ""01001-000"",
                ""logradouro"": ""Praça da Sé"",
                ""bairro"": ""Sé"",
                ""localidade"": ""São Paulo"",
                ""uf"": ""SP""
            }";

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonResponse),
                });

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://viacep.com.br/ws/")
            };

            var viaCepService = new ViaCepService(httpClient);

            // Act: Chama o método com um CEP válido
            var endereco = await viaCepService.BuscarEnderecoPorCepAsync("01001000");

            // Assert: Verifica se o retorno é o esperado
            Assert.NotNull(endereco);
            Assert.Equal("Praça da Sé", endereco.Logradouro);
            Assert.Equal("Sé", endereco.Bairro);
            Assert.Equal("São Paulo", endereco.Localidade);
            Assert.Equal("SP", endereco.Uf);
            Assert.Equal("01001-000", endereco.Cep);
        }

        [Fact]
        public async Task BuscarEnderecoPorCepAsync_DeveRetornarNulo_QuandoCepInvalido()
        {
            // Arrange: Simula uma resposta 404 Not Found
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                });

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://viacep.com.br/ws/")
            };

            var viaCepService = new ViaCepService(httpClient);

            // Act: Chama o método com um CEP inválido
            var endereco = await viaCepService.BuscarEnderecoPorCepAsync("00000000");

            // Assert: Verifica que o retorno é nulo
            Assert.Null(endereco);
        }
    }
}
