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
                session["ListaCarro"] = new List<Carro>();  // Corrigido para usar List<Carro>
            }
        }

        // Método para procurar um carro na lista da sessão por ID
        public static Carro ProcurarCarro(HttpSessionStateBase session, int id)
        {
            if (session["ListaCarro"] != null)
            {
                return (session["ListaCarro"] as List<Carro>)?.ElementAtOrDefault(id); // Protege contra exceções
            }

            return null;
        }

        // Método para excluir um carro da lista da sessão
        public void ExcluirCarro(HttpSessionStateBase session)
        {
            if (session["ListaCarro"] != null)
            {
                var lista = session["ListaCarro"] as List<Carro>;
                lista?.Remove(this);  // Protege contra null
            }
        }

        // Método para adicionar um novo carro na lista da sessão
        public void AdicionarCarro(HttpSessionStateBase session)
        {
            var lista = session["ListaCarro"] as List<Carro>;

            if (lista != null)
            {
                // Atribui um novo ID (o maior ID + 1 ou 0 caso seja a primeira adição)
                this.Id = lista.Any() ? lista.Max(c => c.Id) + 1 : 0;
                lista.Add(this);
            }
        }

        // Método para editar um carro na lista da sessão
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
                    carro.Cor = this.Cor;
                }
            }
        }
    }
}
