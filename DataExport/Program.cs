using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using training.api.Model;

namespace DateExport
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<TrainingContext>()
           .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TrainingDb;Trusted_Connection=True;MultipleActiveResultSets=True")
           .Options;

            try
            {
                using (var context = new TrainingContext(options))
                {
                    var pessoas = context.Pessoas.ToList();
                    var enderecos = context.Enderecos.ToList();
                    var estados = context.Estados.ToList();
                    var contaBancarias = context.ContaBancarias.ToList();
                    var bancos = context.Bancos.ToList();
                    var cidades = context.Cidades.ToList();

                    var workbook = new XSSFWorkbook();
                    var sheet = workbook.CreateSheet("DadosExportados");

                    var rowNumber = 0;
                    var row = sheet.CreateRow(rowNumber++);
                    row.CreateCell(0).SetCellValue("Nome");
                    row.CreateCell(1).SetCellValue("CPF");
                    row.CreateCell(2).SetCellValue("Telefone");
                    row.CreateCell(3).SetCellValue("Sexo");
                    row.CreateCell(4).SetCellValue("Rua");
                    row.CreateCell(5).SetCellValue("Numero");
                    row.CreateCell(6).SetCellValue("Bairro");
                    row.CreateCell(7).SetCellValue("Siglas");
                    row.CreateCell(8).SetCellValue("Agencia");
                    row.CreateCell(9).SetCellValue("Conta");
                    row.CreateCell(10).SetCellValue("Saldo");
                    row.CreateCell(11).SetCellValue("NomeBanco");
                    row.CreateCell(12).SetCellValue("NomeCidades");

                    foreach (var pessoa in pessoas)
                    {
                        row = sheet.CreateRow(rowNumber++);
                        row.CreateCell(0).SetCellValue(pessoa.Nome);
                        row.CreateCell(1).SetCellValue(pessoa.CPF);
                        row.CreateCell(2).SetCellValue(pessoa.Telefone);
                        row.CreateCell(3).SetCellValue(pessoa.Sexo.ToString());

                        var endereco = enderecos.FirstOrDefault(e => e.IdPessoa == pessoa.Id);
                        if (endereco != null)
                        {
                            row.CreateCell(4).SetCellValue(endereco.Rua);
                            row.CreateCell(5).SetCellValue(endereco.Numero);
                            row.CreateCell(6).SetCellValue(endereco.Bairro);
                        }

                        var estado = estados.FirstOrDefault(e => e.Id == endereco.IdEstado);
                        if (estado != null)
                        {
                            row.CreateCell(7).SetCellValue(estado.Sigla);

                        }

                        var contaBancaria = contaBancarias.FirstOrDefault(c => c.IdPessoa == pessoa.Id);
                        if (contaBancaria != null)
                        {
                            row.CreateCell(8).SetCellValue(contaBancaria.Agencia);
                            row.CreateCell(9).SetCellValue(contaBancaria.Conta);
                            row.CreateCell(10).SetCellValue(contaBancaria.Saldo);

                        }

                        var banco = bancos.FirstOrDefault(b => b.Id == contaBancaria.IdBanco);
                        if (banco != null)
                        {
                            row.CreateCell(11).SetCellValue(banco.Name);

                        }

                        var cidade = cidades.FirstOrDefault(c => c.Id == banco.IdCidade);
                        if (cidade != null)
                        {
                            row.CreateCell(12).SetCellValue(cidade.Nome);

                        }
                    }

                    using (FileStream fileStream = new FileStream("C:/Users/leoo_/Downloads/DadosExportados.xlsx", FileMode.Create, FileAccess.Write))
                    {
                        workbook.Write(fileStream);
                    }

                    Console.WriteLine("Dados exportados com sucesso."); Console.ReadKey();
                }
            }
            catch (Exception ex) { Console.WriteLine("Ocorreu um erro: " + ex.Message); }
            finally { Console.WriteLine("Programa finalizado."); }
        }


    }
}
