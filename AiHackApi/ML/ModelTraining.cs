using System;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;
using AiHackApi.ML;

namespace AiHackApi
{
    // Classe responsável pelo treinamento e salvamento do modelo de machine learning
    public class ModelTraining
    {
        private readonly string _dataPath; // Caminho para o arquivo de dados de treinamento
        private readonly string _modelPath; // Caminho onde o modelo treinado será salvo

        // Construtor que define os caminhos do arquivo de dados e do modelo treinado
        public ModelTraining(string dataPath, string modelPath)
        {
            _dataPath = dataPath;
            _modelPath = modelPath;
        }

        // Método principal para treinar o modelo
        public void TreinarModelo()
        {
            var mlContext = new MLContext(); // Contexto ML.NET para configuração do pipeline de machine learning

            // Carrega os dados de treino a partir de um arquivo CSV
            IDataView dataView = mlContext.Data.LoadFromTextFile<SintomaInput>(
                path: _dataPath,
                hasHeader: true,
                separatorChar: ',');

            // Configura o pipeline de treinamento
            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(SintomaInput.Sintoma))
                .Append(mlContext.Transforms.Conversion.MapValueToKey("Label", "Especialidade")) // Converte a coluna "Especialidade" em uma chave numérica como rótulo (Label)
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features")) // Usa SDCA para classificação de múltiplas classes
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedEspecialidade", "Label")); // Converte o rótulo previsto de volta para o valor original (string)

            // Treina o modelo com o pipeline configurado
            var model = pipeline.Fit(dataView);

            // Salva o modelo treinado no caminho especificado
            mlContext.Model.Save(model, dataView.Schema, _modelPath);
            Console.WriteLine("Modelo treinado e salvo em: " + _modelPath); // Confirma que o modelo foi salvo com sucesso
        }
    }
}
