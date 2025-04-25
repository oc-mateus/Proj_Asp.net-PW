using System;

using System.Collections.Generic;

using System.Linq;

using System.Web;

using System.Web.Mvc;

using WebApplication1.Models;

using System.IO;

using iTextSharp.text;

using iTextSharp.text.pdf;

using OfficeOpenXml;

using System.Drawing;

namespace WebApplication1.Controllers
{
    public class AlunoController : Controller
    {
        // GET: Aluno
        public ActionResult Index() => RedirectToAction("Listar");

        public ActionResult Listar()
        {
            Aluno.GerarLista(Session);
            return View(Session["ListaAluno"] as List<Aluno>);
        }

        public ActionResult Exibir(int id)
        {
            return View((Session["ListaAluno"] as List<Aluno>).ElementAt(id));
        }



        [HttpPost]
        public ActionResult DeleteAjax(int id)
        {
            var alunos = Session["ListaAluno"] as List<Aluno>;
            var aluno = alunos?.FirstOrDefault(a => a.Id == id);

            if (aluno == null)
                return Json(new { sucesso = false, mensagem = "Aluno não encontrado" });

            alunos.Remove(aluno);
            Session["ListaAluno"] = alunos;
            return Json(new { sucesso = true });
        }


        public ActionResult Edit(int id)
        {
            return View(Aluno.Procurar(Session, id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                aluno.Editar(Session, id);
                return RedirectToAction("Listar");
            }
            return View(aluno);
        }

        public ActionResult Create() => View(new Aluno());

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                aluno.Adicionar(Session);
                return RedirectToAction("Listar");
            }
            return View(aluno);
        }
        

    }







}