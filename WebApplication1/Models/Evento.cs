using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Evento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome é Obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A Banda é Obrigatória!")]
        public string Banda { get; set; }

        [Required(ErrorMessage = "O Local é Obrigatório!")]
        public string Local { get; set; }

        [Required(ErrorMessage = "A Data do Evento é Obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data do Evento")]
        public DateTime Data { get; set; }

        public string GetDataEvento()
        {
            return Data.ToString("dd/MM/yyyy");
        }


        public static void GerarLista(HttpSessionStateBase session)
        {
            if (session["ListaEvento"] == null)
            {
                session["ListaEvento"] = new List<Evento>();
            }
        }


        public void AdicionarEvento(HttpSessionStateBase session)
        {
            var lista = session["ListaEvento"] as List<Evento>;
            this.Id = lista.Any() ? lista.Max(e => e.Id) + 1 : 0;
            lista.Add(this);
        }


        public static Evento ProcurarEvento(HttpSessionStateBase session, int id)
        {
            var lista = session["ListaEvento"] as List<Evento>;
            return lista?.FirstOrDefault(e => e.Id == id);
        }


        public void ExcluirEvento(HttpSessionStateBase session)
        {
            var lista = session["ListaEvento"] as List<Evento>;
            lista?.RemoveAll(e => e.Id == this.Id);
            session["ListaEvento"] = lista;
        }


        public void EditarEvento(HttpSessionStateBase session, int id)
        {
            var lista = session["ListaEvento"] as List<Evento>;
            var original = lista?.FirstOrDefault(e => e.Id == id);
            if (original != null)
            {
                original.Nome = this.Nome;
                original.Banda = this.Banda;
                original.Local = this.Local;
                original.Data = this.Data;
            }
        }
    }
}
