using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static training.api.Model.Produto;

namespace training.api.Model
{
    public class ContaBancaria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public float Saldo { get; set; }

        public string Agencia { get; set; }

        public string Conta { get; set; }

        public long IdBanco { get; set; }

        [ForeignKey(nameof(IdBanco))]
        [JsonIgnore]
        public virtual Banco Banco { get; set; }
        public long IdPessoa { get; set; }

        public void Deposito(float valor)
        {
            if (valor <= 0)
            {
                BadRequest();
            }
            else
            {
                Saldo += valor;
            }
        }

        public void Pagamento(Produto produto)
        {
            if (Saldo >= produto.Preco)
            {
                Saldo -= produto.Preco;
            }
            else
            {
                throw new Exception("Saldo insuficiente para realizar a transação");
            }
        }

        private void BadRequest()
        {
            throw new NotImplementedException();
        }


        [ForeignKey(nameof(IdPessoa))]
        [JsonIgnore]
        public virtual Pessoa Pessoa { get; set; }
    }
}