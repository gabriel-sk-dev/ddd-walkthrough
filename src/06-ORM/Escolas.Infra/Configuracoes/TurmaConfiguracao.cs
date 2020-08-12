using Escolas.Dominio;
using Escolas.Dominio.Turmas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Escolas.Infra.Configuracoes
{
    public class TurmaConfiguracao : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.ToTable("Turmas");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("varchar(37)");
            builder.Property(c => c.Descricao).HasColumnType("varchar(100)");
            builder.Property(c => c.IdadeMinima);
            builder.Property(c => c.LimiteAlunos);            
            builder.Property(c => c.DuracaoEmMeses);
            builder.Property(c => c.ValorMensal).HasColumnType("decimal(18,2)");
            builder.Property(c => c.Aberta);
            builder.Property(c => c.TotalInscritos);
        }
    }
}
