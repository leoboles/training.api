using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static training.api.Model.Produto;

namespace training.api.Model
{
    [Table("ContaBancarias")]
    public class ContaBancaria
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public float Saldo { get; set; }
        public long IdPessoa { get; set; }
        public long IdBanco { get; set; }
       
        public void Deposito(float valor)
        {
            if (valor <= 0)
            {
                throw new InvalidOperationException("Não é permitido depósitos com valor negativo");
            }
            else 
             Saldo += valor;

        }
        public void Pagamento( Produto produto)
        {       
            if (produto.Valor > Saldo)
            {
                throw new InvalidOperationException("Não é permitido depósitos com valor negativo"); ;
            }
            else
               Saldo -= produto.Valor;
        }

        [ForeignKey(nameof(IdPessoa))]

        [JsonIgnore]

        public virtual Pessoa Pessoa { get; set; }

        [ForeignKey(nameof(IdBanco))]

        [JsonIgnore]

        public virtual Banco Banco { get; set; }
    }

}
