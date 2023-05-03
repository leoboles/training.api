using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using Microsoft.EntityFrameworkCore;
using training.api.Model;
using training.api.Controllers;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Razor;

namespace training.api
{
    public class ExcelService
    {
        private PessoasController pessoasController;
        private EnderecoController enderecosController;
        public void ExportarDados()
        {
             var options = new DbContextOptionsBuilder<TrainingContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TrainingDb;Trusted_Connection=True;MultipleActiveResultSets=True")
                .Options;

            try
            {
                using (var context = new TrainingContext(options))
                {
                    var contextPessoas = context.Pessoas
                        .Include(p => p.Enderecos).ToList();
                    var estados = context.Estados.ToList();
                    var cidades = context.Cidades.ToList();

                    XSSFWorkbook workbook = new XSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("Exportação de dados da Pessoa");

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

                    int row = 1;
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

                            var cidade = cidades.FirstOrDefault(c => c.Id == endereco.IdCidade);
                            var estado = estados.FirstOrDefault(e => e.Id == cidade.Estado.Id);
                            if (estado != null)
                            {
                                linha.CreateCell(7).SetCellValue(estado.Sigla);
                            }

                            if (cidade != null)
                            {
                                linha.CreateCell(8).SetCellValue(cidade.Nome);
                            }
                        }
                    }
                    using (FileStream file = new FileStream("C:\\Users\\ferna\\source\\repos\\training.api\\training.api\\ExportaDados.xlsx", FileMode.Create, FileAccess.Write))
                    {
                        workbook.Write(file);
                    }

                    Console.WriteLine("Dados exportados com sucesso.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        public void Importa(TrainingContext context)
        {
            try
            {
                this.pessoasController = new PessoasController(context);
                this.enderecosController = new EnderecoController(context);

                using (FileStream stream = new FileStream("C:\\Users\\ferna\\source\\repos\\training.api\\training.api\\ImportaDados.xlsx", FileMode.Open, FileAccess.Read))
                {
                    XSSFWorkbook workbook = new XSSFWorkbook(stream);
                    ISheet sheet = workbook.GetSheet("Sheet1");

                    for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                    {
                        IRow row = sheet.GetRow(rowIndex);

                        if (row != null)
                        {
                            string nome = row.GetCell(0).StringCellValue;
                            string cpf = row.GetCell(1).StringCellValue;
                            string estado = row.GetCell(2).StringCellValue;
                            string cidade = row.GetCell(3).StringCellValue;
                            string bairro = row.GetCell(4).StringCellValue;
                            int numero = (int)row.GetCell(5).NumericCellValue;
                            int tipoSexo = (int)row.GetCell(6).NumericCellValue;
                            string rua = row.GetCell(7).StringCellValue;
                            int telefone = (int)row.GetCell(8).NumericCellValue;
                            string nomeEstado = row.GetCell(9).StringCellValue;

                            Sexo sexo;
                            if (tipoSexo == 1)
                            {
                                sexo = Sexo.Masculino;
                            }
                            else
                            {
                                sexo = Sexo.Feminino;
                            }

                            this.pessoasController.CreatePessoa( sexo, nome, telefone.ToString(), cpf);
                            long idPessoa = context.Pessoas.Where(p => p.Cpf == cpf).FirstOrDefault().Id;
                            this.enderecosController.CreateEndereco(rua, numero.ToString(), bairro, estado, idPessoa, cidade, nomeEstado);
                        }
                    }
                }
                Console.WriteLine("Importação finalizada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
