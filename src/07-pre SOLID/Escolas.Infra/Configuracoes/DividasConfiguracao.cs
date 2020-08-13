using Escolas.Dominio;
using Escolas.Dominio.Alunos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Escolas.Infra.Configuracoes
{
    public class DividaConfiguracao : IEntityTypeConfiguration<Divida>
    {
        public void Configure(EntityTypeBuilder<Divida> builder)
        {
            builder.ToTable("Dividas");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("varchar(37)");
            builder
                .HasOne<Inscricao>()
                .WithMany()
                .HasForeignKey(c=> c.InscricaoId);
            builder
                .HasOne<Aluno>()
                .WithMany()
                .HasForeignKey(c => c.AlunoId);
            builder.Property(c => c.Vencimento);
            builder.Property(c => c.Valor).HasColumnType("decimal(18,2)");
            builder.Property(c => c.Situacao).HasConversion(new EnumToStringConverter<Divida.ESituacao>());
        }
    }
}
