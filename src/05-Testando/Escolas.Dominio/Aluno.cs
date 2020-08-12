using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Escolas.Dominio
{
    public sealed class Aluno
    {
        private List<Inscricao> _inscricoes;
        private List<Divida> _dividas;

        public Aluno(string id, string nome, string email, DateTime dataNascimento, IEnumerable<Inscricao> inscricoes, IEnumerable<Divida> dividas)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            _inscricoes = inscricoes.ToList();
            _dividas = dividas.ToList();
        }

        public string Id { get; }
        public string Nome { get; }        
        public string Email { get; }
        public DateTime DataNascimento { get; }
        public int IdadeHoje => DataNascimento.CalcularIdade();
        public IEnumerable<Inscricao> Inscricoes => _inscricoes;
        public IEnumerable<Divida> Dividas => _dividas;

        public static Aluno Criar(string nome, string email, DateTime dataNascimento)
        {
            if (nome.Length < 10)
                throw new ArgumentException("Nome deve ter mais que 10 caracteres", nameof(nome));
            return new Aluno(Guid.NewGuid().ToString(), nome, email, dataNascimento, new List<Inscricao>(), new List<Divida>());
        }

        public Inscricao RealizarInscricao(Turma turma)
        {
            turma.AceitaInscricao(this);
            
            var inscricao = Inscricao.Criar(Id, turma);
            _inscricoes.Add(inscricao);

            _dividas.AddRange(inscricao.GerarDividas());

            turma.ConfirmarInscricao();

            return inscricao;
        }
    }
}
