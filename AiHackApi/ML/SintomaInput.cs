using Microsoft.ML.Data;

namespace AiHackApi.ML
{
    // Classe que define o formato de entrada dos dados para o treinamento do modelo
    public class SintomaInput
    {
        // Define que a primeira coluna do arquivo de dados CSV contém a informação do sintoma
        [LoadColumn(0)]
        public string Sintoma { get; set; }

        // Define que a segunda coluna do arquivo de dados CSV contém a especialidade associada ao sintoma
        [LoadColumn(1)]
        public string Especialidade { get; set; }
    }
}