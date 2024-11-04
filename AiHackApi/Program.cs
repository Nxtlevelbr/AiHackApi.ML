using AiHackApi.Data;
using AiHackApi.ML;
using AiHackApi.Models;
using AiHackApi.Repositories;
using AiHackApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ML;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using Microsoft.ML;

var builder = WebApplication.CreateBuilder(args);

// Configurar a string de conexão para o banco de dados Oracle
string? connectionString = builder.Configuration.GetConnectionString("OracleConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão 'OracleConnection' não foi encontrada no appsettings.json.");
}

// Configura o DbContext para usar Oracle com a string de conexão configurada
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseOracle(connectionString, oracleOptions =>
    {
        oracleOptions.CommandTimeout(60);
    });
});

// Registrar Controladores
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AiHackApi",
        Version = "v1",
        Description = "API para gerenciamento de Consultas Médicas da AiHack"
    });
    c.EnableAnnotations();
});

// Adicionar integração com o serviço ViaCEP usando HttpClient
builder.Services.AddHttpClient<IViaCepService, ViaCepService>(client =>
{
    client.BaseAddress = new Uri("https://viacep.com.br/ws/");
});

// Registrar repositórios e serviços
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IContatoService, ContatoService>();
builder.Services.AddScoped<IMedicoRepository, MedicoRepository>();
builder.Services.AddScoped<IMedicoService, MedicoService>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<IEnderecoService, EnderecoService>();
builder.Services.AddScoped<IBairroRepository, BairroRepository>();
builder.Services.AddScoped<IBairroService, BairroService>();
builder.Services.AddScoped<IEspecialidadeRepository, EspecialidadeRepository>();
builder.Services.AddScoped<IEspecialidadeService, EspecialidadeService>();
builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();
builder.Services.AddScoped<IConsultaService, ConsultaService>();

// Registrar MLContext como Singleton
builder.Services.AddSingleton<MLContext>();

// Registrar o serviço de treinamento de modelo com a interface IModelTrainingService
builder.Services.AddScoped<IModelTrainingService, ModelTrainingService>();

// Ajustar o caminho do modelo salvo
string modelPath = Path.Combine(Directory.GetCurrentDirectory(), "ML/MlModels/modelo.zip");

// Registrar PredictionEnginePool para ML.NET
builder.Services.AddPredictionEnginePool<SintomaInput, EspecialidadePrediction>()
    .FromFile(modelPath);

var app = builder.Build();

// Chamar o treinamento do modelo ao iniciar a aplicação (opcional)
using (var scope = app.Services.CreateScope())
{
    var modelTrainer = scope.ServiceProvider.GetRequiredService<IModelTrainingService>();
    modelTrainer.TreinarModelo();
}

// Middleware global para tratamento de exceções
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            var response = JsonSerializer.Serialize(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                Details = contextFeature?.Error.Message ?? "Erro desconhecido"
            });
            await context.Response.WriteAsync(response);
        }
    });
});

// Configurar Swagger para UI e documentação
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AiHackApi v1");
    c.RoutePrefix = string.Empty;
});

// Middleware de autorização
app.UseAuthorization();

// Mapear controladores
app.MapControllers();

// Executar a aplicação
app.Run();
