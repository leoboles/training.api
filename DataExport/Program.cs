using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using training.api.Model;

namespace ExportDataToExcel
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
                    var cidades = context.Cidades.ToList();
                    var contaBancarias = context.ContaBancarias.ToList();
                    var bancos = context.Bancos.ToList();

                    // Cria o arquivo Excel
                    XSSFWorkbook workbook = new XSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("Pessoas");

                    // Cria a primeira linha com os títulos das colunas
                    IRow headerRow = sheet.CreateRow(0);
                    headerRow.CreateCell(0).SetCellValue("Nome");
                    headerRow.CreateCell(1).SetCellValue("CPF");
                    headerRow.CreateCell(2).SetCellValue("Telefone");
                    headerRow.CreateCell(3).SetCellValue("Sexo");
                    headerRow.CreateCell(4).SetCellValue("Rua");
                    headerRow.CreateCell(5).SetCellValue("Numero");
                    headerRow.CreateCell(6).SetCellValue("Bairro");
                    headerRow.CreateCell(7).SetCellValue("Sigla");
                    headerRow.CreateCell(8).SetCellValue("NomeCidade");
                    headerRow.CreateCell(9).SetCellValue("Agencia");
                    headerRow.CreateCell(10).SetCellValue("Conta");
                    headerRow.CreateCell(11).SetCellValue("Saldo");
                    headerRow.CreateCell(12).SetCellValue("NomeBanco");

                    int rowNum = 1;
                    foreach (var pessoa in pessoas)
                    {
                        IRow row = sheet.CreateRow(rowNum++);
                        row.CreateCell(0).SetCellValue(pessoa.Nome);
                        row.CreateCell(1).SetCellValue(pessoa.Cpf);
                        row.CreateCell(2).SetCellValue(pessoa.Telefone);
                        row.CreateCell(3).SetCellValue(pessoa.Sexo.ToString());

                        var endereco = enderecos.FirstOrDefault(e => e.IdPessoa == pessoa.Id);
                        if (endereco != null)
                        {
                            row.CreateCell(4).SetCellValue(endereco.Rua);
                            row.CreateCell(5).SetCellValue(endereco.Numero.ToString());
                            row.CreateCell(6).SetCellValue(endereco.Bairro);

                            var estado = estados.FirstOrDefault(e => e.Id == endereco.IdEstado);
                            if (estado != null)
                            {
                                row.CreateCell(7).SetCellValue(estado.Sigla);
                            }

                            var cidade = cidades.FirstOrDefault(c => c.Id == endereco.IdCidade);
                            if (cidade != null)
                            {
                                row.CreateCell(8).SetCellValue(cidade.Nome);
                            }
                        }

                        var contaBancaria = contaBancarias.FirstOrDefault(c => c.IdPessoa == pessoa.Id);
                        if (contaBancaria != null)
                        {
                            row.CreateCell(9).SetCellValue(contaBancaria.Agencia.ToString());
                            row.CreateCell(10).SetCellValue(contaBancaria.Conta);
                            row.CreateCell(11).SetCellValue(contaBancaria.Saldo);

                            var banco = bancos.FirstOrDefault(b => b.Id == contaBancaria.IdBanco);
                            if (banco != null)
                            {
                                row.CreateCell(12).SetCellValue(banco.Nome);
                            }
                        }
                    }
                    using (FileStream file = new FileStream("C:\\Users\\victo\\OneDrive\\Documentos\\ExportacaoDeDados.xlsx", FileMode.Create, FileAccess.Write))
                    {
                        workbook.Write(file);
                    }

                    Console.WriteLine("Dados exportados com sucesso.");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Programa finalizado.");
            }

        }
    }
}