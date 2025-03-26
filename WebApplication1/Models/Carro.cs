using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Carro
    {
        public string Placa { get; set; }
        public string Ano { get; set; }
        public string Cor { get; set; }

        public static void GerarListaCarro(HttpSessionStateBase session)
        {
            if (session["ListaCarro"] != null)
            {
                if (((List<Carro>)session["ListaCarro"]).Count > 0)
                {
                    return;
                }
            }

            var lista = new List<Carro>();
            lista.Add(new Carro { Placa = "ABC-14F4", Ano = "2009", Cor = "Azul" });
            lista.Add(new Carro { Placa = "DFG-244R", Ano = "2010", Cor = "Prata" });
            lista.Add(new Carro { Placa = "KLP-G66T", Ano = "2015", Cor = "Vermelho" });

            session.Remove("ListaCarro");
            session.Add("ListaCarro", lista);
        }

        public void AdicionarCarro(HttpSessionStateBase session)
        {
            if (session["ListaCarro"] != null)
            {
                (session["ListaCarro"] as List<Carro>).Add(this);
            }
        }

        public static Carro ProcurarCarro(HttpSessionStateBase session, int id)
        {
            if (session["ListaCarro"] != null)
            {
                return (session["ListaCarro"] as List<Carro>).ElementAt(id);
            }

            return null;
        }

        public void ExcluirCarro(HttpSessionStateBase session)
        {
            if (session["ListaCarro"] != null)
            {
                (session["ListaCarro"] as List<Carro>).Remove(this);
            }
        }

        public void EditarCarro(HttpSessionStateBase session, int id)
        {
            if (session["ListaCarro"] != null)
            {
                var carro = Carro.ProcurarCarro(session, id);
                carro.Placa = this.Placa;
                carro.Ano = this.Ano;
                carro.Cor = this.Cor;
            }
        }
    }
}