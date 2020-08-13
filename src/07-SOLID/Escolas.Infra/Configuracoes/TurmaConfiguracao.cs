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
            builder.OwnsOne(c => c.ConfiguracaoInscricao, configuracao =>
             {
                 configuracao.Property(c => c.IdadeMinima);
                 configuracao.Property(c => c.LimiteAlunos);
                 configuracao.Property(c => c.DuracaoEmMeses);
             });
            builder
                .HasOne(c => c.ConfiguracaoValor)
                .WithOne()
                .HasForeignKey<ConfiguracaoValor>(c=> c.TurmaId);
            builder.Property(c => c.Aberta);
            builder.Property(c => c.TotalInscritos);
        }
    }

    public class TurmaConfiguracaoValorConfiguracao : IEntityTypeConfiguration<ConfiguracaoValor>
    {
        public void Configure(EntityTypeBuilder<ConfiguracaoValor> builder)
        {
            builder.ToTable("TurmasConfiguracaoValor");
            builder.HasKey(c => c.TurmaId);
            builder.Property(c => c.ValorMensal).HasColumnType("decimal(18,2)");
            builder.Property(c => c.DescontoMaximo).HasColumnType("decimal(18,2)");
            builder
                .HasMany(c => c.Descontos)
                .WithOne()
                .HasForeignKey("TurmaId");
        }
    }

    public class TurmaDescontoBaseConfiguracao : IEntityTypeConfiguration<DescontoBase>
    {
        public void Configure(EntityTypeBuilder<DescontoBase> builder)
        {
            builder.ToTable("TurmasDescontos");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("varchar(37)");
            builder.Property(c => c.RegraId).HasColumnType("varchar(100)");
            builder.Property(c => c.PercentualDesconto).HasColumnType("decimal(18,2)");
            builder.HasDiscriminator<string>("Especializacao")
                .HasValue<DescontoSimples>(nameof(DescontoSimples))
                .HasValue<DescontoPorDistancia>(nameof(DescontoPorDistancia));
        }
    }

    public class TurmaDescontoSimplesConfiguracao : IEntityTypeConfiguration<DescontoSimples>
    {
        public void Configure(EntityTypeBuilder<DescontoSimples> builder)
        {
            builder.HasBaseType<DescontoBase>();
        }
    }

    public class TurmaDescontoDistanciaConfiguracao : IEntityTypeConfiguration<DescontoPorDistancia>
    {
        public void Configure(EntityTypeBuilder<DescontoPorDistancia> builder)
        {
            builder.HasBaseType<DescontoBase>();
            builder.Property(c => c.LimiteDistancia);
        }
    }
}
