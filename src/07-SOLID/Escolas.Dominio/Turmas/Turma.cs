using Escolas.Dominio.Alunos;
using Escolas.Dominio.Turmas.Descontos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escolas.Dominio.Turmas
{
    public sealed class Turma
    {
        private Turma() { }

        private Turma(string id, string descricao, ConfiguracaoInscricao configuracaoInscricao, int totalInscritos, ConfiguracaoValor configuracaoValor, bool aberta)
        {
            Id = id;
            Descricao = descricao;
            ConfiguracaoInscricao = configuracaoInscricao;
            TotalInscritos = totalInscritos;
            ConfiguracaoValor = configuracaoValor;
            Aberta = aberta;
        }

        public string Id { get; }
        public string Descricao { get; }
        public ConfiguracaoInscricao ConfiguracaoInscricao { get; }
        public ConfiguracaoValor ConfiguracaoValor { get; private set; }
        public int TotalInscritos { get; private set; }
        public bool Aberta { get; private set; }

        public static Turma CriarFechada(string descricao, int limiteAlunos, int idadeMinima, int duracaoEmMeses, decimal valorMensal)
        {
            var id = Guid.NewGuid().ToString();
            return new Turma(
                id,
                descricao,
                new ConfiguracaoInscricao(limiteAlunos, idadeMinima, duracaoEmMeses),
                0,
                new ConfiguracaoValor(id, valorMensal, 0m, new List<DescontoBase>()),
                false);
        }

        public void AbrirParaInscricoes()
        {
            Aberta = true;
        }

        public void ConfigurarDescontoMaximo(decimal valorEmPercentual)
        {
            ConfiguracaoValor.ConfigurarValorMaximo(valorEmPercentual);
        }

        public void AplicarDesconto(IRegraDesconto regra)
        {
            var regraId = Guid.NewGuid().ToString();
            var regraDesconto = regra switch 
            {
                RegraDescontoPorDistancia regraDescontoAntecipado => new DescontoPorDistancia(regraId, regraDescontoAntecipado.GetType().Name, regraDescontoAntecipado.LimiteDistancia, regraDescontoAntecipado.PercentualDesconto),
                _ => new DescontoSimples(regraId, regra.GetType().Name, regra.PercentualDesconto) as DescontoBase
            };
            ConfiguracaoValor.RegistrarDesconto(regraDesconto);
        }

        public async Task<decimal> CalcularValorMensalAsync(Inscricao inscricao)
        {
            var desconto = await ConfiguracaoValor.CalcularPercentualDescontoAsync(inscricao);
            return ConfiguracaoValor.CalcularValorFinal(desconto);
        }

        internal void AceitaInscricao(Aluno aluno)
        {
            if (aluno.IdadeHoje < ConfiguracaoInscricao.IdadeMinima)
                throw new InvalidOperationException("Aluno não possui idade mínima para a turma");
            if ((TotalInscritos + 1) > ConfiguracaoInscricao.LimiteAlunos)
                throw new InvalidOperationException("Turma não aceita mais incrições");
            if (!Aberta)
                throw new InvalidOperationException("Turma fechada para inscrições");
        }

        internal void ConfirmarInscricao()
        {
            TotalInscritos++;
        } 
    }
}
