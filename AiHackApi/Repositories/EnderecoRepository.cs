using AiHackApi.Data; // Importa o contexto do banco de dados
using AiHackApi.Models; // Importa o modelo Endereco
using Microsoft.EntityFrameworkCore; // Usado para interagir com o banco de dados via Entity Framework
using System.Collections.Generic; // Para trabalhar com coleções como IEnumerable
using System.Threading.Tasks; // Para operações assíncronas

namespace AiHackApi.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        // Campo _context para acessar o banco de dados via Entity Framework
        private readonly ApplicationDbContext _context;

        // Construtor que injeta o contexto do banco de dados
        public EnderecoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém um endereço específico pelo ID.
        /// </summary>
        /// <param name="id">ID do endereço a ser buscado.</param>
        /// <returns>Retorna o endereço encontrado ou lança uma exceção se não for encontrado.</returns>
        public async Task<Endereco?> ObterPorIdAsync(int id)
        {
            // Busca o endereço pelo ID
            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null)
            {
                // Lança uma exceção se o endereço não for encontrado
                throw new KeyNotFoundException("Endereço não encontrado");
            }
            return endereco; // Retorna o endereço encontrado
        }

        /// <summary>
        /// Obtém todos os endereços cadastrados no banco de dados.
        /// </summary>
        /// <returns>Retorna uma lista de endereços.</returns>
        public async Task<IEnumerable<Endereco>> ObterTodosAsync()
        {
            // Busca todos os endereços no banco de dados
            var enderecos = await _context.Enderecos.ToListAsync();
            // Se a lista for nula, retorna uma lista vazia
            return enderecos ?? new List<Endereco>();
        }

        /// <summary>
        /// Adiciona um novo endereço ao banco de dados.
        /// </summary>
        /// <param name="endereco">O endereço a ser adicionado.</param>
        /// <returns>O endereço recém-criado.</returns>
        public async Task<Endereco> AdicionarAsync(Endereco endereco)
        {
            // Adiciona o novo endereço ao contexto do banco de dados
            await _context.Enderecos.AddAsync(endereco);
            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();
            return endereco; // Retorna o endereço criado
        }

        /// <summary>
        /// Atualiza um endereço existente.
        /// </summary>
        /// <param name="endereco">O endereço com as informações atualizadas.</param>
        /// <returns>O endereço atualizado.</returns>
        public async Task<Endereco> AtualizarAsync(Endereco endereco)
        {
            // Marca o endereço como modificado para que o Entity Framework rastreie as mudanças
            _context.Entry(endereco).State = EntityState.Modified;
            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();
            return endereco; // Retorna o endereço atualizado
        }

        /// <summary>
        /// Deleta um endereço pelo ID.
        /// </summary>
        /// <param name="id">ID do endereço a ser deletado.</param>
        /// <returns>True se a exclusão for bem-sucedida, caso contrário false.</returns>
        public async Task<bool> DeletarAsync(int id)
        {
            // Busca o endereço pelo ID
            var endereco = await ObterPorIdAsync(id);
            if (endereco == null) return false; // Retorna false se o endereço não for encontrado

            // Remove o endereço do contexto e salva as alterações
            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();
            return true; // Retorna true se a exclusão foi bem-sucedida
        }
    }
}

