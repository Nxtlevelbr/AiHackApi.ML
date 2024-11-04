// Importações necessárias para trabalhar com o Entity Framework e criar o contexto de banco de dados
using AiHackApi.Data; // Importa o DbContext do projeto
using Microsoft.EntityFrameworkCore; // Entity Framework Core
using Microsoft.EntityFrameworkCore.Design; // Necessário para a criação do DbContext em tempo de design (migrations)
using Microsoft.Extensions.Configuration; // Usado para ler configurações, como strings de conexão
using System.IO; // Necessário para acessar o sistema de arquivos

// A classe ApplicationDbContextFactory é necessária para criar instâncias do ApplicationDbContext durante o design-time,
// especialmente para operações de migração (Migrations).
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    // Método obrigatório pela interface IDesignTimeDbContextFactory, utilizado para criar o contexto
    // durante o processo de design (migrations).
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Criação de um DbContextOptionsBuilder, que é usado para configurar as opções do contexto, 
        // como a string de conexão e o provedor de banco de dados.
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // Criação de um objeto de configuração que lê o arquivo appsettings.json para obter as configurações do projeto.
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Define o caminho base para encontrar o arquivo de configuração
            .AddJsonFile("appsettings.json") // Adiciona o arquivo appsettings.json que contém as configurações
            .Build(); // Constrói a configuração

        // Obtenção da string de conexão com o Oracle Database a partir do appsettings.json
        var connectionString = configuration.GetConnectionString("OracleConnection");

        // Configuração do DbContext para usar o banco de dados Oracle com a string de conexão obtida
        optionsBuilder.UseOracle(connectionString);

        // Retorna uma nova instância do ApplicationDbContext configurada com a string de conexão do banco de dados Oracle
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}