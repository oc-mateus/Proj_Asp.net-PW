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

        public ActionResult Delete(int id)
        {
            return View((Session["ListaAluno"] as List<Aluno>).ElementAt(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteAjax(int id)
        {
            var aluno = Aluno.Procurar(Session, id);
            if (aluno != null)
            {
                aluno.Excluir(Session);
                return Json(new { sucesso = true });
            }
            return new HttpStatusCodeResult(404, "Aluno não encontrado");
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
        public ActionResult DownloadExcel()

        {

            var lista = Session["ListaAluno"] as List<Aluno>;

            if (lista == null || !lista.Any())

                return RedirectToAction("Listar");

            ExcelPackage.License.SetNonCommercialOrganization("Terrier Corp.");
            using (var pacote = new ExcelPackage())

            {

                var planilha = pacote.Workbook.Worksheets.Add("Alunos");

                planilha.Cells[1, 1].Value = "Nome";

                planilha.Cells[1, 2].Value = "Email";

                planilha.Cells[1, 3].Value = "RA";

                planilha.Cells[1, 4].Value = "Data de Nascimento";


                planilha.Row(1).Style.Font.Bold = true;

                planilha.Row(1).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                planilha.Row(1).Style.Fill.BackgroundColor.SetColor(Color.LightGray);


                for (int i = 0; i < lista.Count; i++)

                {

                    var aluno = lista[i];

                    planilha.Cells[i + 2, 1].Value = aluno.Nome;

                    planilha.Cells[i + 2, 2].Value = aluno.Email;

                    planilha.Cells[i + 2, 3].Value = aluno.RA;

                    planilha.Cells[i + 2, 4].Value = aluno.Nascimento.ToShortDateString();

                }


                planilha.Cells.AutoFitColumns();

                return File(pacote.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Alunos.xlsx");

            }

        }

    }







}