﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class Evento
    {
        // GET: Evento
        public string Nome { get; set; }
        public string Banda { get; set; }
        public string Local { get; set; }
        public DateTime Data { get; set; }
        public string GetDataEvento()
        {
            return Data.ToString("dd/MM/yyyy");
        }

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
            lista.Add(new Evento { Nome = "Rock in SP", Banda = "Iron Maiden", Local = "São Paulo", Data = new DateTime(2025, 3, 15) });
            lista.Add(new Evento { Nome = "Festival do Metal", Banda = "Metallica", Local = "Rio de Janeiro", Data = new DateTime(2025, 4, 20) });
            lista.Add(new Evento { Nome = "Indie Fest", Banda = "Arctic Monkeys", Local = "Belo Horizonte", Data = new DateTime(2025, 5, 10) });
            lista.Add(new Evento { Nome = "Pop Night", Banda = "Coldplay", Local = "Curitiba", Data = new DateTime(2025, 6, 5) });
            lista.Add(new Evento { Nome = "Jazz & Blues", Banda = "BB King Tribute", Local = "Porto Alegre", Data = new DateTime(2025, 7, 18) });
            lista.Add(new Evento { Nome = "Hip Hop Vibes", Banda = "Kendrick Lamar", Local = "São Paulo", Data = new DateTime(2025, 8, 12) });
            lista.Add(new Evento { Nome = "Eletronic Sound", Banda = "David Guetta", Local = "Florianópolis", Data = new DateTime(2025, 9, 22) });
            lista.Add(new Evento { Nome = "Reggae Beach", Banda = "Bob Marley Tribute", Local = "Salvador", Data = new DateTime(2025, 10, 30) });
            lista.Add(new Evento { Nome = "Hard Rock Night", Banda = "Guns N' Roses", Local = "Brasília", Data = new DateTime(2025, 11, 18) });
            lista.Add(new Evento { Nome = "Sertanejo Fest", Banda = "Jorge & Mateus", Local = "Goiânia", Data = new DateTime(2025, 12, 5) });
            lista.Add(new Evento { Nome = "Funk Explosion", Banda = "Anitta", Local = "Rio de Janeiro", Data = new DateTime(2026, 1, 14) });
            lista.Add(new Evento { Nome = "Pagode Sunset", Banda = "Grupo Revelação", Local = "Fortaleza", Data = new DateTime(2026, 2, 25) });
            lista.Add(new Evento { Nome = "Forró Roots", Banda = "Falamansa", Local = "Recife", Data = new DateTime(2026, 3, 17) });
            lista.Add(new Evento { Nome = "Clássicos MPB", Banda = "Caetano Veloso", Local = "São Paulo", Data = new DateTime(2026, 4, 12) });
            lista.Add(new Evento { Nome = "Rock Nacional", Banda = "Legião Urbana", Local = "Belo Horizonte", Data = new DateTime(2026, 5, 20) });
            lista.Add(new Evento { Nome = "Bossa Nova Night", Banda = "João Gilberto Tribute", Local = "Rio de Janeiro", Data = new DateTime(2026, 6, 30) });


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
                evento.Banda = this.Banda;
                evento.Nome = this.Nome;
            }
        }
    }
}