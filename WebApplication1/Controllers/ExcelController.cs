using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ExcelController : Controller
    {
        [Obsolete]
        public ActionResult DownloadExcel(string tema)
        {
            object listaDados = null;

            switch (tema.ToLower())
            {
                case "aluno":
                    listaDados = Session["ListaAluno"] as List<Aluno>;
                    break;
                case "carro":
                    listaDados = Session["ListaCarro"] as List<Carro>;
                    break;
                case "evento":
                    listaDados = Session["ListaEvento"] as List<Evento>;
                    break;
                case "celular":
                    listaDados = Session["ListaCelular"] as List<Celular>;
                    break;
                default:
                    return HttpNotFound("Tema não encontrado!");
            }

            if (listaDados == null || (listaDados is IEnumerable<object> lista && !lista.Any()))
            {
                return HttpNotFound("Não há dados cadastrados para o tema selecionado!");
            }

            ExcelPackage.License.SetNonCommercialOrganization("Terrier Corp.");

            using (var pacote = new ExcelPackage())
            {
                var planilha = pacote.Workbook.Worksheets.Add("Relatório");

                // Cabeçalhos
                int coluna = 1;
                int linha = 1;

                if (tema.ToLower() == "aluno")
                {
                    var alunos = listaDados as List<Aluno>;
                    planilha.Cells[linha, 1].Value = "Nome";
                    planilha.Cells[linha, 2].Value = "Email";
                    planilha.Cells[linha, 3].Value = "RA";
                    planilha.Cells[linha, 4].Value = "Nascimento";
                    linha++;

                    foreach (var aluno in alunos)
                    {
                        planilha.Cells[linha, 1].Value = aluno.Nome;
                        planilha.Cells[linha, 2].Value = aluno.Email;
                        planilha.Cells[linha, 3].Value = aluno.RA;
                        planilha.Cells[linha, 4].Value = aluno.Nascimento.ToShortDateString();
                        linha++;
                    }
                }
                else if (tema.ToLower() == "carro")
                {
                    var carros = listaDados as List<Carro>;
                    planilha.Cells[linha, 1].Value = "Placa";
                    planilha.Cells[linha, 2].Value = "Ano";
                    planilha.Cells[linha, 3].Value = "Cor";
                    linha++;

                    foreach (var carro in carros)
                    {
                        planilha.Cells[linha, 1].Value = carro.Placa;
                        planilha.Cells[linha, 2].Value = carro.Ano.ToString("yyyy");
                        planilha.Cells[linha, 3].Value = carro.Cor;
                        linha++;
                    }
                }
                else if (tema.ToLower() == "evento")
                {
                    var eventos = listaDados as List<Evento>;
                    planilha.Cells[linha, 1].Value = "Nome";
                    planilha.Cells[linha, 2].Value = "Banda";
                    planilha.Cells[linha, 3].Value = "Local";
                    planilha.Cells[linha, 4].Value = "Data";
                    linha++;

                    foreach (var evento in eventos)
                    {
                        planilha.Cells[linha, 1].Value = evento.Nome;
                        planilha.Cells[linha, 2].Value = evento.Banda;
                        planilha.Cells[linha, 3].Value = evento.Local;
                        planilha.Cells[linha, 4].Value = evento.Data.ToShortDateString();
                        linha++;
                    }
                }
                else if (tema.ToLower() == "celular")
                {
                    var celulares = listaDados as List<Celular>;
                    planilha.Cells[linha, 1].Value = "Número";
                    planilha.Cells[linha, 2].Value = "Marca";
                    planilha.Cells[linha, 3].Value = "Novo";
                    planilha.Cells[linha, 4].Value = "Fabricação";
                    linha++;

                    foreach (var celular in celulares)
                    {
                        planilha.Cells[linha, 1].Value = celular.Numero;
                        planilha.Cells[linha, 2].Value = celular.Marca;
                        planilha.Cells[linha, 3].Value = celular.Novo ? "Sim" : "Não";
                        planilha.Cells[linha, 4].Value = celular.Fabrica.ToShortDateString();
                        linha++;
                    }
                }

                // Estilo para cabeçalhos
                using (var range = planilha.Cells[1, 1, 1, coluna + 3])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                planilha.Cells.AutoFitColumns();

                string nomeArquivo = $"Relatorio_{tema}.xlsx";
                return File(pacote.GetAsByteArray(),
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            nomeArquivo);
            }
        }
    }
}
