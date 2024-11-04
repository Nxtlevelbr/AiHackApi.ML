using AiHackApi.Controllers;
using AiHackApi.ML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using Moq;


public class RecomendacaoControllerTests
{
    private readonly Mock<PredictionEnginePool<SintomaInput, EspecialidadePrediction>> _predictionEnginePoolMock;
    private readonly RecomendacaoController _controller;

    private readonly string[] _especialidades = 
    {
        "Cardiologista", "Neurologista", "Pediatra", "Ortopedista", "Dermatologista",
        "Ginecologista", "Psiquiatra", "Oftalmologista", "Urologista"
    };

    public RecomendacaoControllerTests()
    {
        _predictionEnginePoolMock = new Mock<PredictionEnginePool<SintomaInput, EspecialidadePrediction>>();
        _controller = new RecomendacaoController(_predictionEnginePoolMock.Object);
    }

    [Fact]
    public void Recomendacao_ReturnsBadRequest_WhenSintomaIsEmpty()
    {
        // Arrange
        var input = new SintomaInput { Sintoma = string.Empty };

        // Act
        var result = _controller.Recomendacao(input);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Sintoma não pode ser vazio.", (result as BadRequestObjectResult)?.Value);
    }

    [Fact]
    public void Recomendacao_ReturnsInternalServerError_OnPredictionException()
    {
        // Arrange
        var input = new SintomaInput { Sintoma = "Febre" };
        _predictionEnginePoolMock.Setup(m => m.Predict(input)).Throws(new Exception("Erro de predição"));

        // Act
        var result = _controller.Recomendacao(input) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
        Assert.Contains("Erro ao processar a recomendação.", result.Value.ToString());
    }

    [Fact]
    public void Recomendacao_ReturnsCorrectSpecialty_ForValidSymptom()
    {
        // Arrange
        var input = new SintomaInput { Sintoma = "Tosse" };
        var expectedPrediction = new EspecialidadePrediction
        {
            Score = new float[] { 0.2f, 0.1f, 0.6f, 0.05f, 0.03f, 0.02f, 0.0f, 0.0f, 0.0f } // Exemplo de pontuações
        };

        _predictionEnginePoolMock.Setup(m => m.Predict(input)).Returns(expectedPrediction);

        // Act
        var result = _controller.Recomendacao(input) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Pediatra", (result.Value as dynamic).EspecialidadeRecomendada); // Índice 2 corresponde a "Pediatra"
        Assert.Equal(0.6f, (result.Value as dynamic).Score);
    }
}
