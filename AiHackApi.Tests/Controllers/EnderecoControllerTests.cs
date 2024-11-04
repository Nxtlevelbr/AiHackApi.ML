using System.Threading.Tasks;
using AiHackApi.Controllers;
using AiHackApi.DTOs;
using AiHackApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AiHackApi.Tests.Controllers
{
    public class EnderecoControllerTests
    {
        [Fact]
        public async Task BuscarEnderecoPorCep_DeveRetornarNotFound_QuandoCepInvalido()
        {
            // Arrange
            var mockViaCepService = new Mock<IViaCepService>();
            mockViaCepService.Setup(service => service.BuscarEnderecoPorCepAsync("00000000"))
                .ReturnsAsync((EnderecoDto)null);

            var controller = new EnderecoController(null, mockViaCepService.Object);

            // Act
            var actionResult = await controller.BuscarEnderecoPorCep("00000000");

            // Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        
        [Fact]
        public async Task BuscarEnderecoPorCep_DeveRetornarEndereco_QuandoCepValido()
        {
            // Arrange
            var enderecoMock = new EnderecoDto
            {
                Cep = "01001-000",
                Logradouro = "Praça da Sé",
                Bairro = "Sé",
                Localidade = "São Paulo",
                Uf = "SP"
            };
            

            var mockViaCepService = new Mock<IViaCepService>();
            mockViaCepService.Setup(service => service.BuscarEnderecoPorCepAsync("01001000"))
                .ReturnsAsync(enderecoMock);

            var controller = new EnderecoController(null, mockViaCepService.Object);

            // Act
            var actionResult = await controller.BuscarEnderecoPorCep("01001000");

            // Assert
            var okResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okResult);

            var endereco = okResult.Value as EnderecoDto;
            Assert.NotNull(endereco);
            Assert.Equal("Praça da Sé", endereco.Logradouro);
            Assert.Equal("Sé", endereco.Bairro);
            Assert.Equal("São Paulo", endereco.Localidade);
            Assert.Equal("SP", endereco.Uf);
            Assert.Equal("01001-000", endereco.Cep);
        }
    }
}