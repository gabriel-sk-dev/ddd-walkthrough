using Escolas.Dominio.Turmas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escolas.Dominio.Alunos
{
    public sealed class Aluno
    {
        private List<Inscricao> _inscricoes;
        private List<Divida> _dividas;

        private Aluno() { }
        private Aluno(string id, string nome, string email, DateTime dataNascimento, ESexo sexo, Endereco endereco, IEnumerable<Inscricao> inscricoes, IEnumerable<Divida> dividas)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            Endereco = endereco;
            _inscricoes = inscricoes.ToList();
            _dividas = dividas.ToList();
        }

        public string Id { get; }
        public string Nome { get; }        
        public string Email { get; }
        public DateTime DataNascimento { get; }
        public ESexo Sexo { get; }
        public Endereco Endereco { get; }
        public int IdadeHoje => DataNascimento.CalcularIdade();
        public IEnumerable<Inscricao> Inscricoes => _inscricoes;
        public IEnumerable<Divida> Dividas => _dividas;

        public static Aluno Criar(string nome, string email, DateTime dataNascimento, ESexo sexo, Endereco endereco)
        {
            if (nome.Length < 10)
                throw new ArgumentException("Nome deve ter mais que 10 caracteres", nameof(nome));
            return new Aluno(Guid.NewGuid().ToString(), nome, email, dataNascimento, sexo, endereco, new List<Inscricao>(), new List<Divida>());
        }

        public async Task<Inscricao> RealizarInscricaoAsync(Turma turma, Inscricao.ETipoPagamento tipoPagamento)
        {
            turma.AceitaInscricao(this);
            
            var inscricao = Inscricao.Criar(this, turma, tipoPagamento);
            _inscricoes.Add(inscricao);

            var valorMensal = await turma.CalcularValorMensalAsync(inscricao);
            _dividas.AddRange(inscricao.GerarDividas(valorMensal));

            turma.ConfirmarInscricao();

            return inscricao;
        }

        public enum ESexo
        {
            Masculino,
            Feminino
        }
    }
}
