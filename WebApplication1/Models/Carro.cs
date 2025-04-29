using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Carro
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A Placa é Obrigatório!")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "O Modelo é Obrigatório!")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "O Ano de Fabricação é obrigatório.")]

        [DataType(DataType.Date)]

        [Display(Name = "Ano de Fabricação")]
        public DateTime Ano { get; set; }


        
        [Required(ErrorMessage = "A Cor é Obrigatória!")]
        public string Cor { get; set; }

        // Método para retornar o ano do carro
        public string GetDataCarro()
        {
            return Ano.ToString("yyyy");
        }

        // Método para gerar a lista de carros na sessão, caso ainda não exista
        public static void GerarListaCarro(HttpSessionStateBase session)
        {
            if (session["ListaCarro"] == null)
            {
                session["ListaCarro"] = new List<Carro>();  
            }
        }

 
        public static Carro ProcurarCarro(HttpSessionStateBase session, int id)
        {
            if (session["ListaCarro"] != null)
            {
                return (session["ListaCarro"] as List<Carro>)?.ElementAtOrDefault(id); 
            }

            return null;
        }


        public void ExcluirCarro(HttpSessionStateBase session)
        {
            if (session["ListaCarro"] != null)
            {
                var lista = session["ListaCarro"] as List<Carro>;
                lista?.Remove(this); 
            }
        }


        public void AdicionarCarro(HttpSessionStateBase session)
        {
            var lista = session["ListaCarro"] as List<Carro>;

            if (lista != null)
            {
                this.Id = lista.Any() ? lista.Max(c => c.Id) + 1 : 0; 
                lista.Add(this); 
            }
        }


        public void EditarCarro(HttpSessionStateBase session, int id)
        {
            var lista = session["ListaCarro"] as List<Carro>;
            if (lista != null)
            {
                var carro = lista.ElementAtOrDefault(id);
                if (carro != null)
                {
                    carro.Placa = this.Placa;
                    carro.Ano = this.Ano;
                    carro.Modelo = this.Modelo;
                    carro.Cor = this.Cor;
                }
            }
        }
    }
}
