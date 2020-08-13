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
            var aluno = Aluno.Criar("Gabriel Schmitt", "gabriel@society.com.br", DateTime.Now.AddYears(-18));
            var turma = Turma.CriarFechada("Natação - basica", 5, 15, 6, 50m);
            turma.AbrirParaInscricoes();

            //Ação
            var inscricao = aluno.RealizarInscricao(turma);

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
            var aluno = Aluno.Criar("Gabriel Schmitt", "gabriel@society.com.br", DateTime.Now.AddYears(-18));
            var turma = Turma.CriarFechada("Natação - basica", 5, 15, 6, 50m);

            //Ação
            var erro = Assert.Throws<InvalidOperationException>(() =>  aluno.RealizarInscricao(turma));

            //Assertiva
            erro.Message.ShouldBe("Turma fechada para inscrições");
            aluno.Inscricoes.Count().ShouldBe(0);
            aluno.Dividas.Count().ShouldBe(0);
            turma.TotalInscritos.ShouldBe(0);
        }
    }
}
