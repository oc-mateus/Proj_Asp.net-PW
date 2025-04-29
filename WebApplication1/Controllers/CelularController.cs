using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CelularController : Controller
    {
        // GET: Celular
        public ActionResult Index()
        {
            return RedirectToAction("Listar");
        }

        public ActionResult Listar()
        {
            Celular.GerarLista(Session);
            return View(Session["ListaCelular"] as List<Celular>);
        }

        public ActionResult Exibir(int id)
        {
            var lista = Session["ListaCelular"] as List<Celular>;
            var celular = lista?.FirstOrDefault(c => c.Id == id);
            return View(celular);
        }

        public ActionResult Delete(int id)
        {
            var lista = Session["ListaCelular"] as List<Celular>;
            var celular = lista?.FirstOrDefault(c => c.Id == id);
            return View(celular);
        }


        [HttpPost]
        public ActionResult DeleteAjax(int id)
        {
            try
            {
                var lista = Session["ListaCelular"] as List<Celular>;

                if (lista == null)
                    return Json(new { sucesso = false, mensagem = "Lista de celulares não encontrada na sessão." });

                var celular = lista.FirstOrDefault(c => c.Id == id);

                if (celular == null)
                    return Json(new { sucesso = false, mensagem = "Celular não encontrado." });

                lista.Remove(celular);
                Session["ListaCelular"] = lista;

                return Json(new { sucesso = true, mensagem = "Celular excluído com sucesso." });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = "Erro ao excluir celular: " + ex.Message });
            }
        }

        public ActionResult Edit(int id)
        {
            var lista = Session["ListaCelular"] as List<Celular>;
            var celular = lista?.FirstOrDefault(c => c.Id == id);
            return View(celular);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Celular celular)
        {
            var listaCelular = Session["ListaCelular"] as List<Celular>;
            if (listaCelular == null)
                return RedirectToAction("Listar");

            var celularExistente = listaCelular.FirstOrDefault(c => c.Id == id);
            if (celularExistente == null)
                return RedirectToAction("Listar");

            // Atualizar propriedades
            celularExistente.Marca = celular.Marca;
            celularExistente.Numero = celular.Numero;
            celularExistente.Novo = celular.Novo;
            celularExistente.Fabrica = celular.Fabrica;

            Session["ListaCelular"] = listaCelular;
            return RedirectToAction("Listar");
        }

        public ActionResult Create()
        {
            return View(new Celular());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Celular celular)
        {
            celular.Adicionar(Session);
            return RedirectToAction("Listar");
        }
    }
}
