using AiHackApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AiHackApi.Controllers
{
    // Define este controlador como uma API RESTful e configura a rota base para "api/modeltraining"
    [ApiController]
    [Route("api/[controller]")]
    public class ModelTrainingController : ControllerBase
    {
        // Dependência do serviço de treinamento do modelo injetada no controlador
        private readonly IModelTrainingService _modelTrainingService;

        // Construtor que recebe uma instância de IModelTrainingService através de injeção de dependência
        public ModelTrainingController(IModelTrainingService modelTrainingService)
        {
            _modelTrainingService = modelTrainingService;
        }

        // Endpoint para treinar o modelo de machine learning
        // Método HTTP POST acessível em "api/modeltraining/train"
        [HttpPost("train")]
        public IActionResult TrainModel()
        {
            // Chama o método de treinamento no serviço, que executa o treinamento do modelo
            _modelTrainingService.TreinarModelo();

            // Retorna uma resposta de sucesso com uma mensagem de confirmação
            return Ok("Modelo treinado e salvo com sucesso.");
        }
    }
}