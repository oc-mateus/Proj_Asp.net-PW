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

        public DateTime Fabrica { get; set; }

        public string GetDataCelular()
        {
            return Fabrica.ToString("dd/MM/yyyy");
        }

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
            lista.Add(new Celular { Marca = "Samsung", Numero = 123456789, Novo = true, Fabrica = new DateTime(2025, 1, 1) });
            lista.Add(new Celular { Marca = "Apple", Numero = 987654321, Novo = false, Fabrica = new DateTime(2022, 5, 10) });
            lista.Add(new Celular { Marca = "Xiaomi", Numero = 112233445, Novo = true, Fabrica = new DateTime(2025, 2, 15) });
            lista.Add(new Celular { Marca = "Motorola", Numero = 556677889, Novo = false, Fabrica = new DateTime(2021, 7, 20) });
            lista.Add(new Celular { Marca = "Nokia", Numero = 667788990, Novo = true, Fabrica = new DateTime(2024, 1, 5) });
            lista.Add(new Celular { Marca = "OnePlus", Numero = 998877665, Novo = true, Fabrica = new DateTime(2024, 9, 25) });
            lista.Add(new Celular { Marca = "Sony", Numero = 334455667, Novo = false, Fabrica = new DateTime(2020, 11, 30) });
            lista.Add(new Celular { Marca = "LG", Numero = 223344556, Novo = false, Fabrica = new DateTime(2019, 6, 18) });
            lista.Add(new Celular { Marca = "Asus", Numero = 778899001, Novo = true, Fabrica = new DateTime(2025, 4, 12) });
            lista.Add(new Celular { Marca = "Huawei", Numero = 556644332, Novo = false, Fabrica = new DateTime(2021, 2, 28) });
            lista.Add(new Celular { Marca = "Realme", Numero = 445566778, Novo = true, Fabrica = new DateTime(2024, 8, 22) });
            lista.Add(new Celular { Marca = "Oppo", Numero = 990011223, Novo = true, Fabrica = new DateTime(2024, 3, 10) });
            lista.Add(new Celular { Marca = "Google", Numero = 887766554, Novo = false, Fabrica = new DateTime(2022, 12, 5) });
            lista.Add(new Celular { Marca = "Lenovo", Numero = 665544332, Novo = false, Fabrica = new DateTime(2020, 9, 14) });
            lista.Add(new Celular { Marca = "Vivo", Numero = 112233556, Novo = true, Fabrica = new DateTime(2025, 5, 1) });


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