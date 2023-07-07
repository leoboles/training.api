using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using training.api.Model;

namespace DataImport.Model
{
    public class PessoaModel
    {
        public string Nome { get; set; }

        public string Telefone { get; set; }

        public Sexo Sexo { get; set; }

        public string CPF { get; set; }
    }
}
