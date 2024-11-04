using Microsoft.EntityFrameworkCore;
using AiHackApi.Models;

namespace AiHackApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet representa uma coleção de entidades no banco de dados.
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Bairro> Bairros { get; set; }
        public DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da chave primária do Paciente
            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasKey(e => e.CPF);
                entity.Property(e => e.CPF).IsRequired();
                entity.Property(e => e.NomePaciente).IsRequired();
            });

            // Configuração da chave primária do Medico
            modelBuilder.Entity<Medico>(entity =>
            {
                entity.HasKey(e => e.CrmMedico);
                entity.Property(e => e.CrmMedico).IsRequired();
                entity.Property(e => e.NmMedico).IsRequired();
            });

            // Configuração da chave primária da entidade Consulta
            modelBuilder.Entity<Consulta>(entity =>
            {
                entity.HasKey(e => e.IdConsulta);
                entity.Property(e => e.DataHoraConsulta).IsRequired();
                entity.Property(e => e.CpfPaciente).IsRequired();
                entity.Property(e => e.TbMedicosIdMedico).IsRequired();

                // Definição de relacionamentos
                entity.HasOne(e => e.Paciente)
                    .WithMany()
                    .HasForeignKey(e => e.CpfPaciente)
                    .OnDelete(DeleteBehavior.Cascade); // Exclui consultas relacionadas ao excluir Paciente

                entity.HasOne(e => e.Medico)
                    .WithMany()
                    .HasForeignKey(e => e.TbMedicosIdMedico)
                    .OnDelete(DeleteBehavior.Cascade); // Exclui consultas relacionadas ao excluir Médico
            });

            // Configuração da chave primária de Contato
            modelBuilder.Entity<Contato>(entity =>
            {
                entity.HasKey(e => e.Email);
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.NomeContato).IsRequired();
                entity.Property(e => e.Telefone).IsRequired();
            });

            // Configuração da tabela Endereco
            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.HasKey(e => e.IdEndereco);
                entity.Property(e => e.Rua).IsRequired();
                entity.Property(e => e.Numero).IsRequired();
                entity.Property(e => e.Bairro).IsRequired();
                entity.Property(e => e.Cep).IsRequired();         
                entity.Property(e => e.Cidade).IsRequired();       
                entity.Property(e => e.Logradouro).IsRequired();    
                entity.Property(e => e.UF).IsRequired();           
            });

            // Configuração da tabela Bairro
            modelBuilder.Entity<Bairro>(entity =>
            {
                entity.HasKey(e => e.IdBairro);
                entity.Property(e => e.NomeBairro).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
