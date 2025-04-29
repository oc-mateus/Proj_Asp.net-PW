using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Celular
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O número é obrigatório!")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "A marca é obrigatória!")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O estado do aparelho é obrigatório!")]
        public bool Novo { get; set; }

        [Required(ErrorMessage = "A data de fabricação é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Fabricação")]
        public DateTime Fabrica { get; set; }

        public string GetDataCelular()
        {
            return Fabrica.ToString("dd/MM/yyyy");
        }

        public static void GerarLista(HttpSessionStateBase session)
        {
            if (session["ListaCelular"] == null)
            {
                session["ListaCelular"] = new List<Celular>();
            }
        }

        public void Adicionar(HttpSessionStateBase session)
        {
            var lista = session["ListaCelular"] as List<Celular>;

            this.Id = lista.Any() ? lista.Max(c => c.Id) + 1 : 0;

            lista.Add(this);
        }

        public static Celular Procurar(HttpSessionStateBase session, int id)
        {
            var lista = session["ListaCelular"] as List<Celular>;
            return lista.FirstOrDefault(c => c.Id == id);
        }

        public void Excluir(HttpSessionStateBase session)
        {
            var lista = session["ListaCelular"] as List<Celular>;
            lista.RemoveAll(c => c.Id == this.Id);
            session["ListaCelular"] = lista;
        }

        public void Editar(HttpSessionStateBase session, int id)
        {
            var lista = session["ListaCelular"] as List<Celular>;
            var original = lista.FirstOrDefault(c => c.Id == id);
            if (original != null)
            {
                original.Numero = this.Numero;
                original.Marca = this.Marca;
                original.Novo = this.Novo;
                original.Fabrica = this.Fabrica;
            }
        }
    }
}
