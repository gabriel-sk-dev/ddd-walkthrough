using Escolas.Dominio;
using Escolas.Dominio.Alunos;
using Escolas.Dominio.Turmas;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Escolas.Testes
{
    public class RealizarInscricao
    {
        [Fact]
        public async Task DadoAlunoComIdadeEAdimplenteETurmaAbertaComVagas_QuandoRealizarInscricao_DevoFazerInscricaoEGerarDividasEIncrmentarInscritos()
        {
            //Ambiente
            var endereco = new Endereco("Rua Caete", "77", "apto 1001", "Vila Rosa", "Novo Hamburgo", "93315100", 10);
            var aluno = Aluno.Criar("Gabriel Schmitt", "gabriel@society.com.br", DateTime.Now.AddYears(-18), Aluno.ESexo.Masculino, endereco);
            var turma = Turma.CriarFechada("Natação - basica", 5, 15, 6, 50m);
            turma.AbrirParaInscricoes();

            //Ação
            var inscricao = await aluno.RealizarInscricaoAsync(turma, Inscricao.ETipoPagamento.Mensal);

            //Assertiva            
            aluno.Inscricoes.Count().ShouldBe(1);
            aluno.Inscricoes.FirstOrDefault(c => c.Id == inscricao.Id).ShouldNotBeNull();
            inscricao.Turma.Id.ShouldBe(turma.Id);
            aluno.Dividas.Count().ShouldBe(6);
            turma.TotalInscritos.ShouldBe(1);
        }

        [Fact]
        public async Task DadoAlunoComIdadeEAdimplenteETurmaFechadaComVagas_QuandoRealizarInscricao_DevoReceberErro()
        {
            //Ambiente
            var endereco = new Endereco("Rua Caete", "77", "apto 1001", "Vila Rosa", "Novo Hamburgo", "93315100", 10);
            var aluno = Aluno.Criar("Gabriel Schmitt", "gabriel@society.com.br", DateTime.Now.AddYears(-18), Aluno.ESexo.Masculino, endereco);
            var turma = Turma.CriarFechada("Natação - basica", 5, 15, 6, 50m);

            //Ação
            var erro = await Assert.ThrowsAsync<InvalidOperationException>(async () => await aluno.RealizarInscricaoAsync(turma, Inscricao.ETipoPagamento.Mensal));

            //Assertiva
            erro.Message.ShouldBe("Turma fechada para inscrições");
            aluno.Inscricoes.Count().ShouldBe(0);
            aluno.Dividas.Count().ShouldBe(0);
            turma.TotalInscritos.ShouldBe(0);
        }
    }
}
