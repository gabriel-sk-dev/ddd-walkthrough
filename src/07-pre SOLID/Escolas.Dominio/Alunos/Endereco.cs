namespace Escolas.Dominio.Alunos
{
    public sealed class Endereco 
    {
        public Endereco(string rua, string numero, string complemento, string bairro, string cidade, string cep, int distanciaAteEscola)
        {
            Rua = rua;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Cep = cep;
            DistanciaAteEscola = distanciaAteEscola;
        }

        public string Rua { get; }
        public string Numero { get; }
        public string Complemento { get; }
        public string Bairro { get; }
        public string Cidade { get; }
        public string Cep { get; }
        public int DistanciaAteEscola { get; }
    }
}
