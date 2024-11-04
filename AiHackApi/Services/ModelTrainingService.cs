using System;
using System.IO;
using Microsoft.ML;
using AiHackApi.ML;

namespace AiHackApi.Services
{
    /// <summary>
    /// Serviço responsável pelo treinamento e avaliação do modelo de recomendação de especialidades médicas.
    /// </summary>
    public class ModelTrainingService : IModelTrainingService
    {
        private readonly MLContext _mlContext;
        private readonly string _dataPath;
        private readonly string _modelPath;

        /// <summary>
        /// Inicializa o serviço de treinamento com o contexto ML e define os caminhos para os arquivos de dados e modelo.
        /// </summary>
        /// <param name="mlContext">Contexto ML para operações de Machine Learning.</param>
        public ModelTrainingService(MLContext mlContext)
        {
            _mlContext = mlContext ?? throw new ArgumentNullException(nameof(mlContext));
            _dataPath = Path.Combine(Directory.GetCurrentDirectory(), "DataSet/especialidade_sintomas.csv");
            _modelPath = Path.Combine(Directory.GetCurrentDirectory(), "ML/MlModels/modelo.zip");
        }

        /// <summary>
        /// Executa o treinamento do modelo com os dados fornecidos, realiza a avaliação e salva o modelo treinado.
        /// </summary>
        public void TreinarModelo()
        {
            try
            {
                if (!File.Exists(_dataPath))
                    throw new FileNotFoundException($"Arquivo de dados não encontrado em {_dataPath}");

                // Carregar os dados de entrada com cabeçalho
                IDataView dataView = _mlContext.Data.LoadFromTextFile<SintomaInput>(
                    path: _dataPath,
                    hasHeader: true,
                    separatorChar: ',');

                // Dividir os dados em conjuntos de treino e teste
                var dataSplit = _mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);
                
                // Configurar o pipeline de treinamento com normalização
                var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(SintomaInput.Sintoma))
                    .Append(_mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(SintomaInput.Especialidade)))
                    .Append(_mlContext.Transforms.NormalizeMinMax("Features"))
                    .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                    .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedEspecialidade", "Label"));

                // Treinar o modelo com o conjunto de treino
                var model = pipeline.Fit(dataSplit.TrainSet);

                // Avaliar o modelo com o conjunto de teste
                var predictions = model.Transform(dataSplit.TestSet);
                var metrics = _mlContext.MulticlassClassification.Evaluate(predictions);
                Console.WriteLine($"Log-loss: {metrics.LogLoss}");
                Console.WriteLine($"Macro Accuracy: {metrics.MacroAccuracy}");
                Console.WriteLine($"Micro Accuracy: {metrics.MicroAccuracy}");

                // Salvar o modelo treinado no caminho especificado
                var modelDirectory = Path.GetDirectoryName(_modelPath);
                if (!Directory.Exists(modelDirectory))
                    Directory.CreateDirectory(modelDirectory);

                _mlContext.Model.Save(model, dataSplit.TrainSet.Schema, _modelPath);
                Console.WriteLine("Modelo treinado e salvo em: " + _modelPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao treinar e salvar o modelo: " + ex.Message);
            }
        }
    }
}
