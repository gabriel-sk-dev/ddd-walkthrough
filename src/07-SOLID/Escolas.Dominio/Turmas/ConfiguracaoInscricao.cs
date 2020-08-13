namespace Escolas.Dominio.Turmas
{
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
}
