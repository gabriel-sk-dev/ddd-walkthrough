using Escolas.Dominio.Alunos;
using Escolas.Dominio.Turmas;
using Escolas.Dominio.Turmas.Descontos;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Escolas.Testes.RegrasDesconto
{

    public class AplicarDescontoParaCrianca
    {
        [Theory]
        [InlineData(10, 10)]
        [InlineData(13, 0)]
        [InlineData(12, 10)]
        public async Task DadoAlunosComIdadesDiversasETurmaComDescontoParaCriancasAte12Anos_QuandoCalcularDesconto_DevoConsedirarCorretamenteAIdadeDoAluno(int idade, decimal descontoEsperado)
        {
            //Ambiente
            var regra = new RegraDescontoParaCriancasAte12(10m);
            var turma = Turma.CriarFechada("Futsal", 10, 5, 6, 100m);
            var aluno = Aluno.Criar("Gabriel Schmitt", "gabriel@society.com.br", DateTime.Now.AddYears(idade * -1), Aluno.ESexo.Masculino,
                new Endereco("Rua A", "120", "apto 81", "Los Verdes", "Passedena", "93315030", 20));
            var inscricao = Inscricao.Criar(aluno, turma, Inscricao.ETipoPagamento.Mensal);

            //Ação
            var desconto = await regra.GerarAsync(inscricao);

            //Assertiva
            desconto.ShouldBe(descontoEsperado);
        }
    }
}
