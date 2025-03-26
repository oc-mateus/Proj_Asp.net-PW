using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class Evento
    {
        // GET: Evento
        public string Local { get; set; }
        public DateTime Data { get; set; }

        public static void GerarLista(HttpSessionStateBase session)
        {
            if (session["ListaEvento"] != null)
            {
                if (((List<Evento>)session["ListaEvento"]).Count > 0)
                {
                    return;
                }
            }
            var lista = new List<Evento>();
            lista.Add(new Evento { Local = "São Paulo", Data = new DateTime(2025, 3, 15) });
            lista.Add(new Evento { Local = "Rio de Janeiro", Data = new DateTime(2025, 4, 20) });
            lista.Add(new Evento { Local = "Belo Horizonte", Data = new DateTime(2025, 5, 10) });

            session.Remove("ListaEvento");
            session.Add("ListaEvento", lista);
        }

        public void Adicionar(HttpSessionStateBase session)
        {
            if (session["ListaEvento"] != null)
            {
                (session["ListaEvento"] as List<Evento>).Add(this);
            }
        }

        public static Evento Procurar(HttpSessionStateBase session, int id)
        {
            if (session["ListaEvento"] != null)
            {
                return (session["ListaEvento"] as List<Evento>).ElementAt(id);
            }

            return null;
        }

        public void Excluir(HttpSessionStateBase session)
        {

            if (session["ListaEvento"] != null)
            {
                (session["ListaEvento"] as List<Evento>).Remove(this);
            }
        }

        public void Editar(HttpSessionStateBase session, int id)
        {
            if (session["ListaEvento"] != null)
            {
                var evento = Evento.Procurar(session, id);
                evento.Local = this.Local;
                evento.Data = this.Data;
            }
        }
    }
}