using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EventoController : Controller
    {
        // GET: Evento
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listar()
        {
            Evento.GerarLista(Session);
            return View(Session["ListaEvento"] as List<Evento>);
        }

        public ActionResult Exibir(int id)
        {
            return View((Session["ListaEvento"] as List<Evento>).ElementAt(id));
        }

        public ActionResult Delete(int id)
        {
            return View((Session["ListaEvento"] as List<Evento>).ElementAt(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(int id, Evento evento)
        {
            Evento.Procurar(Session, id)?.Excluir(Session);

            return RedirectToAction("Listar");
        }


        public ActionResult Edit(int id)
        {
            return View((Session["ListaEvento"] as List<Evento>).ElementAt(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Evento evento)
        {
            var listaEvento = Session["ListaEvento"] as List<Evento>;
            if (listaEvento == null)
            {
                return RedirectToAction("Listar");
            }

            Evento eventoExistente = listaEvento.ElementAtOrDefault(id);
            if (eventoExistente == null)
            {
                return RedirectToAction("Listar");
            }

            
            eventoExistente.Data = evento.Data;
            eventoExistente.Local = evento.Local;
            
            

            Session["ListaEvento"] = listaEvento;

            return RedirectToAction("Listar");
        }


        public ActionResult Create()
        {
            return View(new Evento());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Evento evento)
        {
            evento.Adicionar(Session);

            return RedirectToAction("Listar");
        }
    }
}