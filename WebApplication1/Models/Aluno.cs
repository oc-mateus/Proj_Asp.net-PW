using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Aluno
    {
        public string Nome { get; set; }
        public string RA { get; set; }
        public DateTime Nascimento { get; set; }
        public string GetData()
        {
            return Nascimento.ToString("dd/MM/yyyy");
        }

        public static void GerarLista(HttpSessionStateBase session)
        {
            if (session["ListaAluno"] != null)
            {
                if (((List<Aluno>)session["ListaAluno"]).Count > 0)
                {
                    return;
                }
            }
            var lista = new List<Aluno>();
            lista.Add(new Aluno { Nome = "João", RA = "230211", Nascimento = new DateTime(2007, 2, 17) });
            lista.Add(new Aluno { Nome = "Maria", RA = "230212", Nascimento = new DateTime(2006, 5, 12) });
            lista.Add(new Aluno { Nome = "Carlos", RA = "230213", Nascimento = new DateTime(2007, 8, 25) });
            lista.Add(new Aluno { Nome = "Ana", RA = "230214", Nascimento = new DateTime(2006, 3, 9) });
            lista.Add(new Aluno { Nome = "Pedro", RA = "230215", Nascimento = new DateTime(2007, 11, 30) });
            lista.Add(new Aluno { Nome = "Fernanda", RA = "230216", Nascimento = new DateTime(2006, 7, 19) });
            lista.Add(new Aluno { Nome = "Lucas", RA = "230217", Nascimento = new DateTime(2007, 1, 5) });
            lista.Add(new Aluno { Nome = "Juliana", RA = "230218", Nascimento = new DateTime(2006, 9, 14) });
            lista.Add(new Aluno { Nome = "Gustavo", RA = "230219", Nascimento = new DateTime(2007, 6, 22) });
            lista.Add(new Aluno { Nome = "Camila", RA = "230220", Nascimento = new DateTime(2006, 12, 3) });
            lista.Add(new Aluno { Nome = "Rafael", RA = "230221", Nascimento = new DateTime(2007, 4, 8) });
            lista.Add(new Aluno { Nome = "Beatriz", RA = "230222", Nascimento = new DateTime(2006, 10, 27) });
            lista.Add(new Aluno { Nome = "André", RA = "230223", Nascimento = new DateTime(2007, 5, 15) });
            lista.Add(new Aluno { Nome = "Letícia", RA = "230224", Nascimento = new DateTime(2006, 8, 11) });
            lista.Add(new Aluno { Nome = "Eduardo", RA = "230225", Nascimento = new DateTime(2007, 2, 2) });

            session.Remove("ListaAluno");
            session.Add("ListaAluno", lista);
        }

        public void Adicionar(HttpSessionStateBase session)
        {
            if (session["ListaAluno"] != null)
            {
                (session["ListaAluno"] as List<Aluno>).Add(this);
            }
        }

        public static Aluno Procurar(HttpSessionStateBase session, int id)
        {
            if (session["ListaAluno"] != null)
            {
                return (session["ListaAluno"] as List<Aluno>).ElementAt(id);
            }

            return null;
        }

        public void Excluir(HttpSessionStateBase session)
        {

            if (session["ListaAluno"] != null)
            {
                (session["ListaAluno"] as List<Aluno>).Remove(this);
            }
        }

        public void Editar(HttpSessionStateBase session, int id)
        {
            if (session["ListaAluno"] != null)
            {
                var aluno = Aluno.Procurar(session, id);
                aluno.Nome = this.Nome;
                aluno.RA = this.RA;
                aluno.Nascimento = this.Nascimento;
            }
        }
    }
}