using Escolas.Dominio;
using Escolas.Dominio.Alunos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Escolas.Infra.Configuracoes
{
    public class AlunoConfiguracao : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Alunos");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("varchar(37)");
            builder
                .HasMany(c => c.Inscricoes)
                .WithOne()
                .HasForeignKey(c=> c.AlunoId);
            builder
                .HasMany(c => c.Dividas)
                .WithOne()
                .HasForeignKey(c => c.AlunoId);
            builder.Property(c => c.Nome).HasColumnType("varchar(100)");
            builder.Property(c => c.Email).HasColumnType("varchar(100)");
            builder.Property(c => c.DataNascimento);
        }
    }
}
