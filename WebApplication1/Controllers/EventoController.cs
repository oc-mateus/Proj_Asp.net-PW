using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EventoController : Controller
    {
        public ActionResult Index() => RedirectToAction("Listar");

        public ActionResult Listar()
        {
            Evento.GerarLista(Session);
            return View(Session["ListaEvento"] as List<Evento>);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Evento evento)
        {
            if (ModelState.IsValid)
            {
                evento.AdicionarEvento(Session);
                return RedirectToAction("Index");
            }
            return View(evento);
        }

        public ActionResult Edit(int id)
        {
            var evento = Evento.ProcurarEvento(Session, id);
            if (evento == null)
                return HttpNotFound();
            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Evento evento)
        {
            if (ModelState.IsValid)
            {
                evento.EditarEvento(Session, id);
                return RedirectToAction("Index");
            }
            return View(evento);
        }

        [HttpPost]
        public ActionResult DeleteAjax(int id)
        {
            try
            {
                var lista = Session["ListaEvento"] as List<Evento>;

                if (lista == null)
                    return Json(new { sucesso = false, mensagem = "Lista de eventos não encontrada na sessão." });

                var evento = lista.FirstOrDefault(a => a.Id == id);

                if (evento == null)
                    return Json(new { sucesso = false, mensagem = "Evento não encontrado." });

                lista.Remove(evento);
                Session["ListaEvento"] = lista;

                return Json(new { sucesso = true, mensagem = "Evento excluído com sucesso." });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = "Erro ao excluir evento: " + ex.Message });
            }
        }
    }
}
