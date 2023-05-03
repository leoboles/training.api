using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using training.api.Controllers;
using training.api.Model;

namespace training.api.Service
{
    public class InputData
    {
        private PessoasController pessoasController;
        private EnderecosController enderecosController;

        public void Importa()
        {
            var options = new DbContextOptionsBuilder<TrainingContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TrainingDb;Trusted_Connection=True;MultipleActiveResultSets=True").Options;

            using (var context = new TrainingContext(options))
            {
                try
                {
                    this.pessoasController = new PessoasController(context);
                    this.enderecosController = new EnderecosController(context);

                    using (FileStream stream = new FileStream("D:\\Repos\\training.api\\training.api\\dados.xlsx", FileMode.Open, FileAccess.Read))
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

                                Sexo sexo;
                                if (tipoSexo == 1)
                                {
                                    sexo = Sexo.Masculino;
                                }
                                else
                                {
                                    sexo = Sexo.Feminino;
                                }

                                this.pessoasController.AddPessoa(nome, sexo, cpf);
                                long idPessoa = context.Pessoas.Where(p => p.Cpf == cpf).FirstOrDefault().Id;
                                this.enderecosController.AddEndereco(idPessoa, rua, bairro, cidade, estado, numero.ToString());
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
}
