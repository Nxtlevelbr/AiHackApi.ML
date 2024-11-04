using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;

namespace AiHackApi.ML
{
    // Classe responsável pelo treinamento e salvamento do modelo de machine learning
    public class ModelTrainer
    {
        // Caminho do arquivo de dados de entrada (csv com especialidades e sintomas)
        private readonly string _dataPath = Path.Combine(Directory.GetCurrentDirectory(), "DataSet/especialidade_sintomas.csv");

        // Caminho onde o modelo treinado será salvo
        private readonly string _modelPath = Path.Combine(Directory.GetCurrentDirectory(), "ML/MlModels/modelo.zip");

        // Método principal para iniciar o processo de treinamento do modelo
        public void TreinarModelo()
        {
            var mlContext = new MLContext(); // Contexto ML.NET para configuração do pipeline de machine learning

            try
            {
                // Carregar os dados do arquivo especificado
                IDataView dataView = CarregarDados(mlContext);

                // Configurar e realizar o treinamento do modelo
                var model = ConfigurarTreinamento(mlContext, dataView);

                // Salvar o modelo treinado em um arquivo para uso futuro
                SalvarModelo(mlContext, model, dataView.Schema);
            }
            catch (Exception ex)
            {
                // Mensagem de erro caso ocorra falha durante o processo de treinamento
                Console.WriteLine("Erro no processo de treinamento do modelo: " + ex.Message);
            }
        }

        // Método para carregar os dados de treinamento a partir de um arquivo CSV
        private IDataView CarregarDados(MLContext mlContext)
        {
            try
            {
                // Carrega os dados do CSV e define que há um cabeçalho e que as colunas são separadas por vírgulas
                return mlContext.Data.LoadFromTextFile<SintomaInput>(
                    path: _dataPath,
                    hasHeader: true,
                    separatorChar: ',');
            }
            catch (Exception ex)
            {
                // Mensagem de erro caso ocorra falha ao carregar os dados
                Console.WriteLine("Erro ao carregar dados: " + ex.Message);
                throw;
            }
        }

        // Método que configura o pipeline de treinamento e treina o modelo com base nos dados fornecidos
        private ITransformer ConfigurarTreinamento(MLContext mlContext, IDataView dataView)
        {
            // Configura o pipeline para transformar o texto do sintoma em um vetor de características,
            // converte a especialidade para uma chave, treina o modelo usando o SDCA e converte a chave de volta para uma string
            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(SintomaInput.Sintoma))
                .Append(mlContext.Transforms.Conversion.MapValueToKey("Label", "Especialidade"))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedEspecialidade", "Label"));

            return pipeline.Fit(dataView); // Treina o modelo com os dados fornecidos e retorna o modelo treinado
        }

        // Método para salvar o modelo treinado em um arquivo
        private void SalvarModelo(MLContext mlContext, ITransformer model, DataViewSchema schema)
        {
            try
            {
                // Salva o modelo treinado no caminho especificado
                mlContext.Model.Save(model, schema, _modelPath);
                Console.WriteLine("Modelo treinado e salvo em: " + _modelPath);
            }
            catch (Exception ex)
            {
                // Mensagem de erro caso ocorra falha ao salvar o modelo
                Console.WriteLine("Erro ao salvar o modelo: " + ex.Message);
            }
        }
    }
}
