using Escolas.Dominio;
using Escolas.Dominio.Alunos;
using Escolas.Dominio.Turmas;
using Escolas.Infra.Configuracoes;
using Microsoft.EntityFrameworkCore;

namespace Escolas.Infra
{
    public class EscolasContexto : DbContext
    {
        public EscolasContexto(DbContextOptions<EscolasContexto> options) : base(options) { }

        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TurmaConfiguracao());
            modelBuilder.ApplyConfiguration(new TurmaDescontoBaseConfiguracao());
            modelBuilder.ApplyConfiguration(new TurmaDescontoSimplesConfiguracao());
            modelBuilder.ApplyConfiguration(new TurmaDescontoDistanciaConfiguracao());
            modelBuilder.ApplyConfiguration(new TurmaConfiguracaoValorConfiguracao());
            modelBuilder.ApplyConfiguration(new InscricaoConfiguracao());
            modelBuilder.ApplyConfiguration(new DividaConfiguracao());
            modelBuilder.ApplyConfiguration(new AlunoConfiguracao());
        }
    }
}
