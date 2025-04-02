using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Carro
    {
        public string Placa { get; set; }
        public DateTime Ano { get; set; }
        public string Cor { get; set; }
        public string GetDataCarro()
        {
            return Ano.ToString("yyyy");
        }

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
            lista.Add(new Carro { Placa = "ABC-14F4", Ano = new DateTime(2015, 1, 1), Cor = "Azul" });
            lista.Add(new Carro { Placa = "DEF-23G5", Ano = new DateTime(2018, 1, 1), Cor = "Vermelho" });
            lista.Add(new Carro { Placa = "GHI-56H7", Ano = new DateTime(2020, 1, 1), Cor = "Preto" });
            lista.Add(new Carro { Placa = "JKL-78I9", Ano = new DateTime(2017, 1, 1), Cor = "Branco" });
            lista.Add(new Carro { Placa = "MNO-90J1", Ano = new DateTime(2019, 1, 1), Cor = "Prata" });
            lista.Add(new Carro { Placa = "PQR-12K3", Ano = new DateTime(2016, 1, 1), Cor = "Cinza" });
            lista.Add(new Carro { Placa = "STU-34L5", Ano = new DateTime(2021, 1, 1), Cor = "Verde" });
            lista.Add(new Carro { Placa = "VWX-56M7", Ano = new DateTime(2013, 1, 1), Cor = "Amarelo" });
            lista.Add(new Carro { Placa = "YZA-78N9", Ano = new DateTime(2014, 1, 1), Cor = "Azul" });
            lista.Add(new Carro { Placa = "BCD-90O1", Ano = new DateTime(2022, 1, 1), Cor = "Vermelho" });
            lista.Add(new Carro { Placa = "EFG-12P3", Ano = new DateTime(2015, 1, 1), Cor = "Preto" });
            lista.Add(new Carro { Placa = "HIJ-34Q5", Ano = new DateTime(2018, 1, 1), Cor = "Branco" });
            lista.Add(new Carro { Placa = "KLM-56R7", Ano = new DateTime(2020, 1, 1), Cor = "Prata" });
            lista.Add(new Carro { Placa = "NOP-78S9", Ano = new DateTime(2017, 1, 1), Cor = "Cinza" });
            lista.Add(new Carro { Placa = "QRS-90T1", Ano = new DateTime(2016, 1, 1), Cor = "Verde" });

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