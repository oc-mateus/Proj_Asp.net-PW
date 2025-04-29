using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Aluno
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é Obrigatório!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O e-mail é Obrigatório!")]
        [EmailAddress(ErrorMessage = "E-mail inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O RA é Obrigatório!")]
        public string RA { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]

        [DataType(DataType.Date)]

        [Display(Name = "Data de Nascimento")]
        public DateTime Nascimento { get; set; }
        public string GetData()
        {
            return Nascimento.ToString("dd/MM/yyyy");
        }

        public static void GerarLista(HttpSessionStateBase session)

        {

            if (session["ListaAluno"] == null)

            {

                session["ListaAluno"] = new List<Aluno>();

            }

        }
        public void Adicionar(HttpSessionStateBase session)
        {
            var lista = session["ListaAluno"] as List<Aluno>;

            this.Id = lista.Any() ? lista.Max(a => a.Id) + 1 : 0;

            lista.Add(this);
        }

        public static Aluno Procurar(HttpSessionStateBase session, int id)
        {
            var lista = session["ListaAluno"] as List<Aluno>;
            return lista.FirstOrDefault(a =>  a.Id == id);
        }

        public void Excluir(HttpSessionStateBase session)
        {
            var lista = (List<Aluno>)session["ListaAluno"];
            lista.RemoveAll(a => a.Id == this.Id);
            session["ListaAluno"] = lista;
        }


        public void Editar(HttpSessionStateBase session, int id)
        {
            var lista = session["ListaAluno"] as List<Aluno>;
            var original = lista.FirstOrDefault(a => a.Id == id);
            if (original != null) 
            {
                original.Nome = this.Nome;
                original.RA = this.RA;
                original.Nascimento = this.Nascimento;
                original.Email = this.Email;
            }
        }
    }
}