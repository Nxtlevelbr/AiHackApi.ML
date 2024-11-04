using AiHackApi.ML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using System;
using System.Linq;

namespace AiHackApi.Controllers
{
    // Define este controlador como uma API RESTful e configura a rota base para "api/recomendacao"
    [ApiController]
    [Route("api/[controller]")]
    public class RecomendacaoController : ControllerBase
    {
        // Injeção do PredictionEnginePool para realizar previsões de especialidade com base no sintoma informado
        private readonly PredictionEnginePool<SintomaInput, EspecialidadePrediction> _predictionEnginePool;

        // Define a lista de especialidades médicas correspondentes aos índices de pontuação da predição
        private readonly string[] _especialidades = 
        {
            "Cardiologista", "Neurologista", "Pediatra", "Ortopedista", "Dermatologista",
            "Ginecologista", "Psiquiatra", "Oftalmologista", "Urologista"
        };

        // Construtor que recebe o PredictionEnginePool através de injeção de dependência
        public RecomendacaoController(PredictionEnginePool<SintomaInput, EspecialidadePrediction> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }

        // Endpoint de recomendação de especialidade com base em um sintoma informado
        // Método HTTP POST acessível em "api/recomendacao"
        [HttpPost]
        public IActionResult Recomendacao([FromBody] SintomaInput input)
        {
            // Verifica se o sintoma informado é válido
            if (string.IsNullOrEmpty(input.Sintoma))
            {
                return BadRequest("Sintoma não pode ser vazio.");
            }

            try
            {
                // Realiza a predição usando o PredictionEnginePool com o input do sintoma
                var prediction = _predictionEnginePool.Predict(input);

                // Encontra o índice do maior score no vetor de pontuação, representando a especialidade mais recomendada
                int maxScoreIndex = prediction.Score.ToList().IndexOf(prediction.Score.Max());

                // Obtém a especialidade correspondente ao índice de maior score
                string especialidadeRecomendada = _especialidades[maxScoreIndex];
                
                // Retorna a especialidade recomendada e a pontuação correspondente como resposta
                return Ok(new 
                { 
                    EspecialidadeRecomendada = especialidadeRecomendada,
                    Score = prediction.Score[maxScoreIndex]
                });
            }
            catch (Exception ex)
            {
                // Em caso de erro durante o processamento da predição, retorna uma resposta de erro interno com detalhes
                return StatusCode(500, new { Message = "Erro ao processar a recomendação.", Detalhes = ex.Message });
            }
        }
    }
}
