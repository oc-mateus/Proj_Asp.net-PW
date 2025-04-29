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
            return View((Session["ListaCarro"] as List<Carro>)?.ElementAt(id));
        }


        [HttpPost]
        public ActionResult DeleteAjax(int id)
        {
            try
            {
                var carros = Session["ListaCarro"] as List<Carro>;
                var carro = carros?.FirstOrDefault(c => c.Id == id);

                if (carro == null)
                    return Json(new { sucesso = false, mensagem = "Carro não encontrado." });

                carros.Remove(carro);
                Session["ListaCarro"] = carros;

                return Json(new { sucesso = true, mensagem = "Carro excluído com sucesso." });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = "Erro ao excluir carro: " + ex.Message });
            }
        }


        public ActionResult Edit(int id)
        {
            var carro = (Session["ListaCarro"] as List<Carro>)?.ElementAtOrDefault(id);
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

            var carroExistente = listaCarro.ElementAtOrDefault(id);
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
            if (ModelState.IsValid)
            {
                carro.AdicionarCarro(Session);
                return RedirectToAction("Listar");
            }

  
            return View(carro);
        }
    }
}
