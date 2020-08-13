using Escolas.Dominio;
using Escolas.Dominio.Alunos;
using Escolas.Dominio.Turmas;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace Escolas.Testes
{
    public class RealizarInscricao
    {
        [Fact]
        public void DadoAlunoComIdadeEAdimplenteETurmaAbertaComVagas_QuandoRealizarInscricao_DevoFazerInscricaoEGerarDividasEIncrmentarInscritos()
        {
            //Ambiente
            var endereco = new Endereco("Rua Caete", "77", "apto 1001", "Vila Rosa", "Novo Hamburgo", "93315100", 10);
            var aluno = Aluno.Criar("Gabriel Schmitt", "gabriel@society.com.br", DateTime.Now.AddYears(-18), Aluno.ESexo.Masculino, endereco);
            var turma = Turma.CriarFechadaSemDescontos("Nata��o - basica", 5, 15, 6, 50m);
            turma.AbrirParaInscricoes();

            //A��o
            var inscricao = aluno.RealizarInscricao(turma, Inscricao.ETipoPagamento.Mensal);

            //Assertiva            
            aluno.Inscricoes.Count().ShouldBe(1);
            aluno.Inscricoes.FirstOrDefault(c => c.Id == inscricao.Id).ShouldNotBeNull();
            inscricao.Turma.Id.ShouldBe(turma.Id);
            aluno.Dividas.Count().ShouldBe(6);
            turma.TotalInscritos.ShouldBe(1);
        }

        [Fact]
        public void DadoAlunoComIdadeEAdimplenteETurmaFechadaComVagas_QuandoRealizarInscricao_DevoReceberErro()
        {
            //Ambiente
            var endereco = new Endereco("Rua Caete", "77", "apto 1001", "Vila Rosa", "Novo Hamburgo", "93315100", 10);
            var aluno = Aluno.Criar("Gabriel Schmitt", "gabriel@society.com.br", DateTime.Now.AddYears(-18), Aluno.ESexo.Masculino, endereco);
            var turma = Turma.CriarFechadaSemDescontos("Nata��o - basica", 5, 15, 6, 50m);

            //A��o
            var erro = Assert.Throws<InvalidOperationException>(() =>  aluno.RealizarInscricao(turma, Inscricao.ETipoPagamento.Mensal));

            //Assertiva
            erro.Message.ShouldBe("Turma fechada para inscri��es");
            aluno.Inscricoes.Count().ShouldBe(0);
            aluno.Dividas.Count().ShouldBe(0);
            turma.TotalInscritos.ShouldBe(0);
        }
    }
}
