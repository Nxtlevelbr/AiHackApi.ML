using AiHackApi.Data; // Importa o contexto do banco de dados
using AiHackApi.Models; // Importa o modelo Contato
using AiHackApi.Repositories; // Importa a interface do repositório
using Microsoft.EntityFrameworkCore; // Usado para interagir com o banco de dados
using System.Collections.Generic; // Para o uso de coleções como IEnumerable
using System.Threading.Tasks; // Para operações assíncronas

public class ContatoRepository : IContatoRepository
{
    // Campo _context para interação com o banco de dados via Entity Framework
    private readonly ApplicationDbContext _context;

    // Construtor que injeta o contexto do banco de dados
    public ContatoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtém um contato específico pelo email.
    /// </summary>
    /// <param name="email">Email do contato a ser buscado.</param>
    /// <returns>Retorna o contato encontrado ou lança uma exceção se não for encontrado.</returns>
    public async Task<Contato> ObterPorEmailAsync(string email)
    {
        // Busca o contato pelo email
        var contato = await _context.Contatos.FirstOrDefaultAsync(c => c.Email == email);
        if (contato == null)
        {
            // Lança uma exceção se o contato não for encontrado
            throw new KeyNotFoundException("Contato não encontrado");
        }
        return contato; // Retorna o contato encontrado
    }

    /// <summary>
    /// Obtém todos os contatos cadastrados no banco de dados.
    /// </summary>
    /// <returns>Retorna uma lista de contatos.</returns>
    public async Task<IEnumerable<Contato>> ObterTodosAsync()
    {
        // Retorna uma lista de todos os contatos no banco de dados, sem rastreamento
        return await _context.Contatos.AsNoTracking().ToListAsync();
    }

    /// <summary>
    /// Adiciona um novo contato ao banco de dados.
    /// </summary>
    /// <param name="contato">O contato a ser adicionado.</param>
    /// <returns>O contato recém-criado.</returns>
    public async Task<Contato> AdicionarAsync(Contato contato)
    {
        // Adiciona o novo contato ao contexto
        await _context.Contatos.AddAsync(contato);
        // Salva as alterações no banco de dados
        await _context.SaveChangesAsync();
        return contato; // Retorna o contato criado
    }

    /// <summary>
    /// Atualiza os dados de um contato existente.
    /// </summary>
    /// <param name="contato">O contato com as informações atualizadas.</param>
    /// <returns>O contato atualizado.</returns>
    public async Task<Contato> AtualizarAsync(Contato contato)
    {
        // Marca o contato como modificado
        _context.Entry(contato).State = EntityState.Modified;
        // Salva as alterações no banco de dados
        await _context.SaveChangesAsync();
        return contato; // Retorna o contato atualizado
    }

    /// <summary>
    /// Deleta um contato pelo email.
    /// </summary>
    /// <param name="email">Email do contato a ser deletado.</param>
    /// <returns>True se a exclusão for bem-sucedida, caso contrário false.</returns>
    public async Task<bool> DeletarAsync(string email)
    {
        // Busca o contato pelo email
        var contato = await ObterPorEmailAsync(email);
        if (contato == null) return false; // Retorna false se o contato não for encontrado

        // Remove o contato do contexto e salva as alterações
        _context.Contatos.Remove(contato);
        await _context.SaveChangesAsync();
        return true; // Retorna true para indicar que a exclusão foi bem-sucedida
    }
}
