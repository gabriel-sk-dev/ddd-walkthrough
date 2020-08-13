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
            builder.OwnsOne(c => c.ConfiguracaoValor, configuracao =>
            {
                configuracao.Property(c=> c.ValorMensal).HasColumnType("decimal(18,2)");
                configuracao.Property(c => c.DescontoCriancas).HasColumnType("decimal(18,2)");
                configuracao.Property(c => c.DescontoDistancia).HasColumnType("decimal(18,2)");
                configuracao.Property(c => c.DescontoMulheres).HasColumnType("decimal(18,2)");
                configuracao.Property(c => c.DescontoPagamentoAntecipado).HasColumnType("decimal(18,2)");
                configuracao.Property(c => c.DescontoMaximo).HasColumnType("decimal(18,2)");
            });
            builder.Property(c => c.Aberta);
            builder.Property(c => c.TotalInscritos);
        }
    }
}
