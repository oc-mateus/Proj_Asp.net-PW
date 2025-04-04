﻿using System;
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
            return View();
        }

        public ActionResult Listar()
        {
            Celular.GerarLista(Session);
            return View(Session["ListaCelular"] as List<Celular>);
        }

        public ActionResult Exibir(int id)
        {
            return View((Session["ListaCelular"] as List<Celular>).ElementAt(id));
        }

        public ActionResult Delete(int id)
        {
            return View((Session["ListaCelular"] as List<Celular>).ElementAt(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(int id, Celular celular)
        {
            Celular.Procurar(Session, id)?.Excluir(Session);

            return RedirectToAction("Listar");
        }


        public ActionResult Edit(int id)
        {
            return View((Session["ListaCelular"] as List<Celular>).ElementAt(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Celular celular)
        {
            var listaCelular = Session["ListaCelular"] as List<Celular>;
            if (listaCelular == null)
            {
                return RedirectToAction("Listar");
            }

            Celular celularExistente = listaCelular.ElementAtOrDefault(id);
            if (celularExistente == null)
            {
                return RedirectToAction("Listar");
            }

           
            celularExistente.Marca = celular.Marca;
            celularExistente.Novo = celular.Novo;
            celularExistente.Numero = celular.Numero;
            

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