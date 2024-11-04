using Microsoft.ML.Data;

namespace AiHackApi.ML
{
    // Classe que representa a estrutura de predição para uma especialidade médica recomendada
    public class EspecialidadePrediction
    {
        // Propriedade que armazena a especialidade médica prevista
        // "ColumnName" mapeia esta propriedade ao nome da coluna de saída do modelo ML.NET
        [ColumnName("PredictedEspecialidade")]
        public string PredictedEspecialidade { get; set; }

        // Array de pontuações que indica a confiança do modelo para cada especialidade
        // Cada índice representa uma especialidade diferente com um valor de probabilidade associado
        public float[] Score { get; set; }
    }
}