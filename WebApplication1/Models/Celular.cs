using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class Celular
    {
        public int Numero { get; set; }
        public  string Marca { get; set; }
        public bool Novo { get; set; }

        public static void GerarLista(HttpSessionStateBase session)
        {
            if (session["ListaCelular"] != null)
            {
                if (((List<Celular>)session["ListaCelular"]).Count > 0)
                {
                    return;
                }
            }
            var lista = new List<Celular>();
            lista.Add(new Celular { Marca = "Samsung", Numero = 123456789, Novo = true });
            lista.Add(new Celular { Marca = "Apple", Numero = 987654321, Novo = false });
            lista.Add(new Celular { Marca = "Motorola", Numero = 246801357, Novo = true });


            session.Remove("ListaCelular");
            session.Add("ListaCelular", lista);
        }

        public void Adicionar(HttpSessionStateBase session)
        {
            if (session["ListaCelular"] != null)
            {
                (session["ListaCelular"] as List<Celular>).Add(this);
            }
        }

        public static Celular Procurar(HttpSessionStateBase session, int id)
        {
            if (session["ListaCelular"] != null)
            {
                return (session["ListaCelular"] as List<Celular>).ElementAt(id);
            }

            return null;
        }

        public void Excluir(HttpSessionStateBase session)
        {

            if (session["ListaCelular"] != null)
            {
                (session["ListaCelular"] as List<Celular>).Remove(this);
            }
        }

        public void Editar(HttpSessionStateBase session, int id)
        {
            if (session["ListaCelular"] != null)
            {
                var celular = Celular.Procurar(session, id);
                celular.Marca = this.Marca;
                celular.Numero = this.Numero;
                celular.Novo = this.Novo;
            }
        }
    }
}