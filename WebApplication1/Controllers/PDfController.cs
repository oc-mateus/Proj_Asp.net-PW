using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PdfController : Controller
    {
        // Ação para gerar o PDF com dados específicos de cada tema
        public ActionResult DownloadPdf(string tema)
        {
            // Recuperando as listas específicas com base no parâmetro "tema"
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

            // Gerando o PDF
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                // Adicionando título
                document.Add(new Paragraph($"Relatório de {tema}"));
                document.Add(new Paragraph(" ")); // Espaço

                // Adicionando os dados de acordo com o tema
                if (tema.ToLower() == "aluno")
                {
                    var alunos = listaDados as List<Aluno>;
                    foreach (var aluno in alunos)
                    {
                        document.Add(new Paragraph($"Nome: {aluno.Nome}"));
                        document.Add(new Paragraph($"RA: {aluno.RA}"));
                        document.Add(new Paragraph($"Nascimento: {aluno.Nascimento.ToShortDateString()}"));
                        document.Add(new Paragraph("--------------"));
                    }
                }
                else if (tema.ToLower() == "carro")
                {
                    var carros = listaDados as List<Carro>;
                    foreach (var carro in carros)
                    {
                        document.Add(new Paragraph($"Placa: {carro.Placa}"));
                        document.Add(new Paragraph($"Ano: {carro.Ano.ToString("yyyy")}"));
                        document.Add(new Paragraph($"Cor: {carro.Cor}"));
                        document.Add(new Paragraph("--------------"));
                    }
                }
                else if (tema.ToLower() == "evento")
                {
                    var eventos = listaDados as List<Evento>;
                    foreach (var evento in eventos)
                    {
                        document.Add(new Paragraph($"Nome: {evento.Nome}"));
                        document.Add(new Paragraph($"Banda: {evento.Banda}"));
                        document.Add(new Paragraph($"Local: {evento.Local}"));
                        document.Add(new Paragraph($"Data: {evento.Data.ToShortDateString()}"));
                        document.Add(new Paragraph("--------------"));
                    }
                }
                else if (tema.ToLower() == "celular")
                {
                    var celulares = listaDados as List<Celular>;
                    foreach (var celular in celulares)
                    {
                        document.Add(new Paragraph($"Número: {celular.Numero}"));
                        document.Add(new Paragraph($"Marca: {celular.Marca}"));
                        document.Add(new Paragraph($"Novo: {(celular.Novo ? "Sim" : "Não")}"));
                        document.Add(new Paragraph($"Fabricação: {celular.Fabrica.ToShortDateString()}"));
                        document.Add(new Paragraph("--------------"));
                    }
                }

                document.Close();
                writer.Close();

                byte[] fileBytes = memoryStream.ToArray();
                return File(fileBytes, "application/pdf", $"Relatorio_{tema}.pdf");
            }
        }
    }
}
