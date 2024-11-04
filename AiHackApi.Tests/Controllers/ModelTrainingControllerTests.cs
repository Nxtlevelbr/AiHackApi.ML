using AiHackApi.Controllers;
using AiHackApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AiHackApi.Tests.Controllers;

public class ModelTrainingControllerTests
{
    private readonly Mock<IModelTrainingService> _modelTrainingServiceMock;
    private readonly ModelTrainingController _controller;

    public ModelTrainingControllerTests()
    {
        // Inicializa o mock para IModelTrainingService
        _modelTrainingServiceMock = new Mock<IModelTrainingService>();
        
        // Passa o mock para o controlador
        _controller = new ModelTrainingController(_modelTrainingServiceMock.Object);
    }

    [Fact]
    public void TrainModel_CallsTreinarModeloMethodOnce()
    {
        // Act
        var result = _controller.TrainModel();

        // Assert: Verifica se o método TreinarModelo foi chamado uma vez
        _modelTrainingServiceMock.Verify(service => service.TreinarModelo(), Times.Once);
        Assert.IsType<OkObjectResult>(result); // Verifica se a resposta é do tipo OkObjectResult
    }

    [Fact]
    public void TrainModel_ReturnsOkWithSuccessMessage()
    {
        // Act
        var result = _controller.TrainModel() as OkObjectResult;

        // Assert: Verifica o conteúdo da resposta
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal("Modelo treinado e salvo com sucesso.", result.Value);
    }
}