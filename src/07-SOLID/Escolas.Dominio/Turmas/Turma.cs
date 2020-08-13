using Escolas.Dominio.Alunos;
using System;

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

        public static Turma CriarFechadaSemDescontos(string descricao, int limiteAlunos, int idadeMinima, int duracaoEmMeses, decimal valorMensal)
        {
            return new Turma(Guid.NewGuid().ToString(),
                descricao,
                new ConfiguracaoInscricao(limiteAlunos, idadeMinima, duracaoEmMeses),
                0,
                new ConfiguracaoValor(valorMensal, 0m, 0m, 0m, 0m, 0m),
                false);
        }

        public void AbrirParaInscricoes()
        {
            Aberta = true;
        }

        public void AplicarDescontoMulheres(decimal valorEmPercentual)
        {
            ConfiguracaoValor = new ConfiguracaoValor(ConfiguracaoValor.ValorMensal, valorEmPercentual, ConfiguracaoValor.DescontoCriancas, ConfiguracaoValor.DescontoPagamentoAntecipado, ConfiguracaoValor.DescontoDistancia, ConfiguracaoValor.DescontoMaximo);
        }

        public void AplicarDescontoCriancas(decimal valorEmPercentual)
        {
            ConfiguracaoValor = new ConfiguracaoValor(ConfiguracaoValor.ValorMensal, ConfiguracaoValor.DescontoMulheres, valorEmPercentual, ConfiguracaoValor.DescontoPagamentoAntecipado, ConfiguracaoValor.DescontoDistancia, ConfiguracaoValor.DescontoMaximo);
        }

        public void AplicarDescontoPagamentoAntecipado(decimal valorEmPercentual)
        {
            ConfiguracaoValor = new ConfiguracaoValor(ConfiguracaoValor.ValorMensal, ConfiguracaoValor.DescontoMulheres, ConfiguracaoValor.DescontoCriancas, valorEmPercentual, ConfiguracaoValor.DescontoDistancia, ConfiguracaoValor.DescontoMaximo);
        }

        public void AplicarDescontoDistancia(decimal valorEmPercentual)
        {
            ConfiguracaoValor = new ConfiguracaoValor(ConfiguracaoValor.ValorMensal, ConfiguracaoValor.DescontoMulheres, ConfiguracaoValor.DescontoCriancas, ConfiguracaoValor.DescontoDistancia, valorEmPercentual, ConfiguracaoValor.DescontoMaximo);
        }

        public void ConfigurarDescontoMaximo(decimal valorEmPercentual)
        {
            ConfiguracaoValor = new ConfiguracaoValor(ConfiguracaoValor.ValorMensal, ConfiguracaoValor.DescontoMulheres, ConfiguracaoValor.DescontoCriancas, ConfiguracaoValor.DescontoDistancia, ConfiguracaoValor.DescontoDistancia, valorEmPercentual);
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

        internal decimal CalcularValorMensal(Inscricao inscricao)
        {
            var desconto = 0m;
            if (inscricao.Aluno.Sexo == Aluno.ESexo.Feminino)
                desconto += ConfiguracaoValor.DescontoMulheres;
            if (inscricao.Aluno.IdadeHoje <= 12)
                desconto += ConfiguracaoValor.DescontoMulheres;
            if(inscricao.TipoPagamento == Inscricao.ETipoPagamento.Antecipado)
                desconto += ConfiguracaoValor.DescontoMulheres;
            if(inscricao.Aluno.Endereco.DistanciaAteEscola > 5)
                desconto += ConfiguracaoValor.DescontoDistancia;
            if (desconto > ConfiguracaoValor.DescontoMaximo)
                desconto = ConfiguracaoValor.DescontoMaximo;
            return ConfiguracaoValor.ValorMensal - (ConfiguracaoValor.ValorMensal * (desconto /100m));
        }

        internal void ConfirmarInscricao()
        {
            TotalInscritos++;
        }

    }

    public sealed class ConfiguracaoInscricao
    {
        public ConfiguracaoInscricao(int limiteAlunos, int idadeMinima, int duracaoEmMeses)
        {
            LimiteAlunos = limiteAlunos;
            IdadeMinima = idadeMinima;
            DuracaoEmMeses = duracaoEmMeses;
        }

        public int LimiteAlunos { get; }
        public int IdadeMinima { get; }
        public int DuracaoEmMeses { get; }
    }

    public sealed class ConfiguracaoValor
    {
        public ConfiguracaoValor(decimal valorMensal, decimal descontoMulheres, decimal descontoCriancas, decimal descontoPagamentoAntecipado, decimal descontoDistancia, decimal descontoMaximo)
        {
            ValorMensal = valorMensal;
            DescontoMulheres = descontoMulheres;
            DescontoCriancas = descontoCriancas;
            DescontoPagamentoAntecipado = descontoPagamentoAntecipado;
            DescontoDistancia = descontoDistancia;
            DescontoMaximo = descontoMaximo;
        }

        public decimal ValorMensal { get; }
        public decimal DescontoMulheres { get; }
        public decimal DescontoCriancas { get; }
        public decimal DescontoPagamentoAntecipado { get; }
        public decimal DescontoDistancia { get; }
        public decimal DescontoMaximo { get; }

        internal decimal CalcularValorDesconto(decimal valorPercentual)
        {
            return ValorMensal * (valorPercentual / 100m);
        }
    }
}
