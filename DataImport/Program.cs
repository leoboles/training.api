using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Runtime.Intrinsics.X86;
using training.api.Model;

namespace ExcelToSqlServer
{
    class Program
    {
        static void Main(string[] args)
        {

            var options = new DbContextOptionsBuilder<TrainingContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TrainingDb;Trusted_Connection=True;MultipleActiveResultSets=True")
            .Options;

            FileStream file = new FileStream("C:\\Users\\victo\\OneDrive\\Documentos\\ImportacaoDeDados.xlsx", FileMode.Open, FileAccess.Read);
            XSSFWorkbook workbook = new XSSFWorkbook(file);
            ISheet sheet = workbook.GetSheetAt(0);

            using (var context = new TrainingContext(options))
            {
                string siglaEstado = null;
                Estado estado = null;
                Pessoa pessoa = null;

                for (int i = 1; i <= 28; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row != null)
                    {
                        string cpf = row.GetCell(3).StringCellValue;

                        pessoa = context.Pessoas.FirstOrDefault(p => p.Cpf == cpf);

                        if (pessoa == null)
                        {
                            string nome = row.GetCell(0).StringCellValue;
                            string telefone = row.GetCell(1).StringCellValue;
                            string sexoStr = row.GetCell(2)?.ToString();
                            Sexo sexo;
                            if (Enum.TryParse(sexoStr, out sexo))
                            {
                                // o valor da célula é um valor válido de Sexo
                            }
                            else
                            {
                                // o valor da célula não é um valor válido de Sexo
                            }

                            pessoa = new Pessoa { Nome = nome, Cpf = cpf, Telefone = telefone, Sexo = sexo };
                            context.Pessoas.Add(pessoa);
                        }
                        else
                        {
                            Console.WriteLine("CPF " + pessoa.Cpf + " já cadastrado.");
                        }

                        string sigla = row.GetCell(4).StringCellValue;

                        if (sigla != siglaEstado)
                        {
                            estado = context.Estados.FirstOrDefault(e => e.Sigla == sigla);

                            if (estado == null)
                            {
                                estado = new Estado { Sigla = sigla };
                                context.Estados.Add(estado);
                            }

                            siglaEstado = sigla;
                        }
                        else
                        {
                            Console.WriteLine("UF " + sigla + " já cadastrado.");
                        }

                        string cidadeNome = row.GetCell(8).StringCellValue;

                        var cidade = context.Cidades.FirstOrDefault(c => c.Nome == cidadeNome && c.Estado.Sigla == siglaEstado);

                        if (cidade == null)
                        {
                            cidade = new Cidade { Nome = cidadeNome, Estado = estado };
                            context.Cidades.Add(cidade);
                        }
                        else
                        {
                            Console.WriteLine("Cidade " + cidadeNome + " já cadastrada.");
                        }

                        string bancoNome = row.GetCell(12).StringCellValue;

                        var banco = context.Bancos.FirstOrDefault(b => b.Nome == bancoNome && b.Cidade.Nome == cidadeNome && b.Cidade.Estado.Sigla == siglaEstado);

                        if (banco == null)
                        {
                            banco = new Banco { Nome = bancoNome, Cidade = cidade };
                            context.Bancos.Add(banco);
                        }
                        else
                        {
                            Console.WriteLine("Banco " + bancoNome + " já cadastrado.");
                        }

                        string agencia = row.GetCell(9)?.ToString();
                        string conta = row.GetCell(10).StringCellValue;
                        float saldo = (float)row.GetCell(11).NumericCellValue;

                        var contaBancaria = context.ContaBancarias.FirstOrDefault(b => b.Agencia == agencia && b.Conta == conta);

                        if (contaBancaria == null)
                        {
                            contaBancaria = new ContaBancaria { Agencia = agencia, Conta = conta, Saldo = saldo, Pessoa = pessoa, Banco = banco };
                            context.ContaBancarias.Add(contaBancaria);
                        }
                        else
                        {
                            Console.WriteLine("Conta " + conta + " já cadastrada.");
                        }

                        string rua = row.GetCell(5).StringCellValue;
                        string numero = row.GetCell(6)?.ToString();
                        string bairro = row.GetCell(7).StringCellValue;

                        var endereco = context.Enderecos.FirstOrDefault(b => b.Rua == rua && b.Bairro == bairro);

                        if (endereco == null)
                        {
                            endereco = new Endereco { Rua = rua, Numero = numero, Bairro = bairro, Pessoa = pessoa, Cidade = cidade, Estado = estado};
                            context.Enderecos.Add(endereco);
                        }
                        else
                        {
                            Console.WriteLine("Endereco " + rua + " já cadastrado.");
                        }
                    }
                }

                context.SaveChanges();
            }

            file.Close();
        }
    }
}
