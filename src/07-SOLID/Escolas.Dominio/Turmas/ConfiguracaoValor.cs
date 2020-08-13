using Escolas.Dominio.Alunos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escolas.Dominio.Turmas
{
    public sealed class ConfiguracaoValor
    {
        private List<DescontoBase> _descontos;

        private ConfiguracaoValor() { }
        public ConfiguracaoValor(string turmaId, decimal valorMensal, decimal descontoMaximo, IEnumerable<DescontoBase> descontos)
        {
            TurmaId = turmaId;
            ValorMensal = valorMensal;
            DescontoMaximo = descontoMaximo;
            _descontos = descontos.ToList();
        }

        public string TurmaId { get; }
        public decimal ValorMensal { get; }
        public decimal DescontoMaximo { get; private set; }
        public IEnumerable<DescontoBase> Descontos => _descontos;

        internal void ConfigurarValorMaximo(decimal valorEmPercentual)
        {
            DescontoMaximo = valorEmPercentual;
        }

        internal void RegistrarDesconto(DescontoBase desconto)
        {
            _descontos.Add(desconto);
        }

        internal async Task<decimal> CalcularPercentualDescontoAsync(Inscricao inscricao)
        {
            var descontos = await Task.WhenAll(Descontos.Select(c => c.Regra.GerarAsync(inscricao)));
            var descontoTotal = descontos.Sum(valor => valor);
            return descontoTotal > DescontoMaximo
                ? DescontoMaximo
                : descontoTotal;
        }

        internal decimal CalcularValorDesconto(decimal valorPercentual)
        {
            return ValorMensal * (valorPercentual / 100m);
        }

        internal decimal CalcularValorFinal(decimal percentualDesconto)
        {
            return ValorMensal - CalcularValorDesconto(percentualDesconto);
        }
    }
}
