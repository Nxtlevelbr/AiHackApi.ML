using AiHackApi.Data;
using AiHackApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AiHackApi.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly ApplicationDbContext _context;

        // Construtor que injeta o contexto
        public PacienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retorna todos os pacientes
        public async Task<IEnumerable<Paciente>> ObterTodosAsync()
        {
            return await _context.Pacientes.AsNoTracking().ToListAsync();
        }

        // Busca um paciente pelo CPF
        public async Task<Paciente> ObterPorCpfAsync(string cpf)
        {
            var paciente = await _context.Pacientes.AsNoTracking()
                .FirstOrDefaultAsync(p => p.CPF == cpf);

            if (paciente == null)
            {
                throw new KeyNotFoundException($"Paciente com CPF {cpf} não encontrado.");
            }

            return paciente;
        }

        // Adiciona um novo paciente e retorna o paciente adicionado
        public async Task<Paciente> AdicionarAsync(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return paciente; // Retorna o paciente adicionado
        }

        // Atualiza um paciente e retorna o paciente atualizado
        public async Task<Paciente> AtualizarAsync(Paciente paciente)
        {
            var pacienteExistente = await _context.Pacientes.FindAsync(paciente.CPF);

            if (pacienteExistente == null)
            {
                throw new KeyNotFoundException($"Paciente com CPF {paciente.CPF} não encontrado.");
            }

            _context.Entry(pacienteExistente).CurrentValues.SetValues(paciente);
            await _context.SaveChangesAsync();

            return paciente; // Retorna o paciente atualizado
        }

        // Deleta um paciente pelo CPF e retorna true se for bem-sucedido
        public async Task<bool> DeletarAsync(string cpf)
        {
            var paciente = await _context.Pacientes.FindAsync(cpf);

            if (paciente == null)
            {
                return false; // Paciente não encontrado
            }

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();

            return true; // Deleção bem-sucedida
        }
    }
}
