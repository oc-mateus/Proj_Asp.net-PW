using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AlunoController : Controller
    {
        // GET: Aluno
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listar()
        {
            Aluno.GerarLista(Session);
            return View(Session["ListaAluno"] as List<Aluno>);
        }

        public ActionResult Exibir(int id)
        {
            return View((Session["ListaAluno"] as List<Aluno>).ElementAt(id));
        }

        public ActionResult Delete(int id)
        {
            return View((Session["ListaAluno"] as List<Aluno>).ElementAt(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(int id, Aluno aluno)
        {
            Aluno.Procurar(Session, id)?.Excluir(Session);

            return RedirectToAction("Listar");
        }


        public ActionResult Edit(int id)
        {
            return View((Session["ListaAluno"] as List<Aluno>).ElementAt(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Aluno aluno)
        {
            var listaAluno = Session["ListaAluno"] as List<Aluno>;
            if (listaAluno == null)
            {
                return RedirectToAction("Listar");
            }

            Aluno alunoExistente = listaAluno.ElementAtOrDefault(id);
            if (alunoExistente == null)
            {
                return RedirectToAction("Listar");
            }

            alunoExistente.Nome = aluno.Nome;
            alunoExistente.RA = aluno.RA;
            alunoExistente.Data = aluno.Data;
           

            

            Session["ListaAluno"] = listaAluno;

            return RedirectToAction("Listar");
        }

        public ActionResult Create()
        {
            return View(new Aluno());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Aluno aluno)
        {
            aluno.Adicionar(Session);

            return RedirectToAction("Listar");
        }

    }
}