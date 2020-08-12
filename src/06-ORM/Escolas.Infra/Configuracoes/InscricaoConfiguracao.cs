using Escolas.Dominio;
using Escolas.Dominio.Alunos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Escolas.Infra.Configuracoes
{
    public class InscricaoConfiguracao : IEntityTypeConfiguration<Inscricao>
    {
        public void Configure(EntityTypeBuilder<Inscricao> builder)
        {
            builder.ToTable("Inscricoes");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("varchar(37)");
            builder
                .HasOne(c => c.Turma)
                .WithMany()
                .HasForeignKey("TurmaId");
            builder
                .HasOne<Aluno>()
                .WithMany()
                .HasForeignKey(c => c.AlunoId);
            builder.Property(c => c.InscritoEm);
        }
    }
}
