using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using training.api.Controllers;
using training.api.Model;

namespace training.api.Service
{
    public class OutputData
    {
        public void Exporta()
        {
            var options = new DbContextOptionsBuilder<TrainingContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TrainingDb;Trusted_Connection=True;MultipleActiveResultSets=True").Options;
            using (var context = new TrainingContext(options))
            {
                try
                {
                    XSSFWorkbook workbook = new XSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("output");

                    var contextPessoas = context.Pessoas.Include(p => p.Enderecos).ToList();
                    var estados = context.Estados.ToList();
                    var cidades = context.Cidades.ToList();

                    IRow header = sheet.CreateRow(0);
                    header.CreateCell(0).SetCellValue("Nome");
                    header.CreateCell(1).SetCellValue("CPF");
                    header.CreateCell(4).SetCellValue("Rua");
                    header.CreateCell(2).SetCellValue("Telefone");
                    header.CreateCell(3).SetCellValue("Sexo");
                    header.CreateCell(5).SetCellValue("Numero");
                    header.CreateCell(6).SetCellValue("Bairro");
                    header.CreateCell(7).SetCellValue("Sigla");
                    header.CreateCell(8).SetCellValue("Cidade");

                    int row = sheet.LastRowNum+1;
                    foreach (var pessoa in contextPessoas)
                    {
                        IRow linha = sheet.CreateRow(row++);
                        linha.CreateCell(0).SetCellValue(pessoa.Nome);
                        linha.CreateCell(1).SetCellValue(pessoa.Cpf);
                        linha.CreateCell(2).SetCellValue(pessoa.Telefone);
                        linha.CreateCell(3).SetCellValue(pessoa.Sexo.ToString());

                        var endereco = pessoa.Enderecos.FirstOrDefault(e => e.IdPessoa == pessoa.Id);
                        if (endereco != null)
                        {
                            linha.CreateCell(4).SetCellValue(endereco.Rua);
                            linha.CreateCell(5).SetCellValue(endereco.Numero.ToString());
                            linha.CreateCell(6).SetCellValue(endereco.Bairro);

                            var cidade = cidades.FirstOrDefault(c => c.Id == endereco.idCidade);
                            var estado = estados.FirstOrDefault(e => e.Id == cidade.Estado.Id);
                            if (estado != null)
                            {
                                linha.CreateCell(7).SetCellValue(estado.UF);
                            }

                            if (cidade != null)
                            {
                                linha.CreateCell(8).SetCellValue(cidade.Nome);
                            }
                        }
                    }
                    using (FileStream file = new FileStream("D:\\Repos\\training.api\\training.api\\Exportacao.xlsx", FileMode.Create, FileAccess.Write))
                    {
                        workbook.Write(file);
                    }
                    Console.WriteLine("Dados exportados com sucesso!");
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}