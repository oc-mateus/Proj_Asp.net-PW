using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CarroController : Controller
    {
        // GET: Carro
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listar()
        {
            Carro.GerarListaCarro(Session);
            return View(Session["ListaCarro"] as List<Carro>);
        }

        public ActionResult Exibir(int id)
        {
            return View((Session["ListaCarro"] as List<Carro>).ElementAt(id));
        }

        public ActionResult Delete(int id)
        {
            return View((Session["ListaCarro"] as List<Carro>).ElementAt(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(int id, Carro carro)
        {
            Carro.ProcurarCarro(Session, id)?.ExcluirCarro(Session);
            return RedirectToAction("Listar");
        }

        public ActionResult Edit(int id)
        {
            
            Carro carro = (Session["ListaCarro"] as List<Carro>)?.ElementAt(id);

            if (carro != null)
            {
                return View(carro);
            }

            
            return RedirectToAction("Listar");
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Carro carro)
        {
            
            var listaCarro = Session["ListaCarro"] as List<Carro>;
            if (listaCarro == null)
            {
                return RedirectToAction("Listar");
            }

            Carro carroExistente = listaCarro.ElementAtOrDefault(id);
            if (carroExistente == null)
            {
                return RedirectToAction("Listar");
            }

            
            carroExistente.Placa = carro.Placa;
            carroExistente.Ano = carro.Ano;
            carroExistente.Cor = carro.Cor;
            
            Session["ListaCarro"] = listaCarro;

            
            return RedirectToAction("Listar");
        }

        public ActionResult Create()
        {
            return View(new Carro());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Carro carro)
        {
            carro.AdicionarCarro(Session);

            return RedirectToAction("Listar");
        }
    }
}