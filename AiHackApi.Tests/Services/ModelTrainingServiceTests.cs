using AiHackApi.Services;
using Microsoft.ML;
using Moq;

namespace AiHackApi.Tests.Services;

public class ModelTrainingServiceTests
{
    private readonly Mock<MLContext> _mlContextMock;
    private readonly string _dataPath;
    private readonly string _modelPath;

    public ModelTrainingServiceTests()
    {
        _mlContextMock = new Mock<MLContext>();
        _dataPath = Path.Combine(Directory.GetCurrentDirectory(), "DataSet/especialidade_sintomas.csv");
        _modelPath = Path.Combine(Directory.GetCurrentDirectory(), "ML/MlModels/modelo.zip");
    }

    [Fact]
    public void TreinarModelo_ThrowsFileNotFoundException_WhenDataFileDoesNotExist()
    {
        // Arrange
        var service = new ModelTrainingService(_mlContextMock.Object);
        
        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => service.TreinarModelo());
    }

    [Fact]
    public void TreinarModelo_TrainsAndSavesModelSuccessfully_WhenDataFileExists()
    {
        // Arrange
        File.Create(_dataPath).Dispose(); // Cria um arquivo de dados falso
        var mlContext = new MLContext();
        var service = new ModelTrainingService(mlContext);

        try
        {
            // Act
            service.TreinarModelo();

            // Assert
            Assert.True(File.Exists(_modelPath), "O modelo não foi salvo no caminho especificado.");
        }
        finally
        {
            // Clean up
            File.Delete(_dataPath);
            File.Delete(_modelPath);
        }
    }

    [Fact]
    public void TreinarModelo_PrintsMetricsToConsole()
    {
        // Arrange
        var mlContext = new MLContext();
        var service = new ModelTrainingService(mlContext);

        // Usar uma string para capturar o console
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act
        service.TreinarModelo();

        // Assert
        var output = stringWriter.ToString();
        Assert.Contains("Log-loss:", output);
        Assert.Contains("Macro Accuracy:", output);
        Assert.Contains("Micro Accuracy:", output);
    }
}