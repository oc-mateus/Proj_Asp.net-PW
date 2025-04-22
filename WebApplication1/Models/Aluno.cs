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
            var lista = new List<Aluno>
            { 
                new Aluno { Id = 1, Nome = "João", Email = "joao@email.com", RA = "230211", Nascimento = new DateTime(2007, 2, 17) },
                new Aluno { Id = 2, Nome = "Maria", Email = "maria@email.com", RA = "230212", Nascimento = new DateTime(2006, 5, 12) },
                new Aluno { Id = 3, Nome = "Carlos", Email = "carlos@email.com", RA = "230213", Nascimento = new DateTime(2007, 8, 25) },
                new Aluno { Id = 4, Nome = "Ana", Email = "ana@email.com", RA = "230214", Nascimento = new DateTime(2006, 3, 9) },
                new Aluno { Id = 5, Nome = "Pedro", Email = "pedro@email.com", RA = "230215", Nascimento = new DateTime(2007, 11, 30) },
                new Aluno { Id = 6, Nome = "Fernanda", Email = "fernanda@email.com", RA = "230216", Nascimento = new DateTime(2006, 7, 19) },
                new Aluno { Id = 7, Nome = "Lucas", Email = "lucas@email.com", RA = "230217", Nascimento = new DateTime(2007, 1, 5) },
                new Aluno { Id = 8, Nome = "Juliana", Email = "juliana@email.com", RA = "230218", Nascimento = new DateTime(2006, 9, 14) },
                new Aluno { Id = 9, Nome = "Gustavo", Email = "gustavo@email.com", RA = "230219", Nascimento = new DateTime(2007, 6, 22) },
                new Aluno { Id = 10, Nome = "Camila", Email = "camila@email.com", RA = "230220", Nascimento = new DateTime(2006, 12, 3) },
                new Aluno { Id = 11, Nome = "Rafael", Email = "rafael@email.com", RA = "230221", Nascimento = new DateTime(2007, 4, 8) },
                new Aluno { Id = 12, Nome = "Beatriz", Email = "beatriz@email.com", RA = "230222", Nascimento = new DateTime(2006, 10, 27) },
                new Aluno { Id = 13, Nome = "André", Email = "andre@email.com", RA = "230223", Nascimento = new DateTime(2007, 5, 15) },
                new Aluno { Id = 14, Nome = "Letícia", Email = "leticia@email.com", RA = "230224", Nascimento = new DateTime(2006, 8, 11) },
                new Aluno { Id = 15, Nome = "Eduardo", Email = "eduardo@email.com", RA = "230225", Nascimento = new DateTime(2007, 2, 2) }
            };


        session.Remove("ListaAluno");
            session.Add("ListaAluno", lista);
        }

        public void Adicionar(HttpSessionStateBase session)
        {
            var lista = session["ListaAluno"] as List<Aluno>;
            this.Id = lista.Count;
            lista.Add(this);
        }

        public static Aluno Procurar(HttpSessionStateBase session, int id)
        {
            var lista = session["ListaAluno"] as List<Aluno>;
            return lista.FirstOrDefault(a =>  a.Id == id);
        }

        public void Excluir(HttpSessionStateBase session)
        {
            var lista = session["ListaAluno"] as List<Aluno>;
            lista.RemoveAll(a => a.Id == this.Id);
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