using Escolas.Dominio;
using Escolas.Dominio.Alunos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
                .WithOne(c=> c.Aluno)
                .HasForeignKey("AlunoId");
            builder
                .HasMany(c => c.Dividas)
                .WithOne()
                .HasForeignKey(c => c.AlunoId);
            builder.Property(c => c.Nome).HasColumnType("varchar(100)");
            builder.Property(c => c.Email).HasColumnType("varchar(100)");
            builder.Property(c => c.DataNascimento);
            builder.Property(c => c.Sexo)
                .HasColumnType("varchar(30)")
                .HasConversion(new EnumToStringConverter<Aluno.ESexo>());
            builder.OwnsOne(c => c.Endereco, endereco => 
            {
                endereco.Property(c => c.Rua).HasColumnType("varchar(60)");
                endereco.Property(c => c.Numero).HasColumnType("varchar(20)");
                endereco.Property(c => c.Complemento).HasColumnType("varchar(40)");
                endereco.Property(c => c.Bairro).HasColumnType("varchar(60)");
                endereco.Property(c => c.Cidade).HasColumnType("varchar(60)");
                endereco.Property(c => c.Cep).HasColumnType("varchar(20)");
                endereco.Property(c => c.DistanciaAteEscola);
            });
        }
    }
}
