using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace training.api.Model
{
    [Table("Enderecos")]
    public class Endereco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Rua { get; set; }

        public string Numero { get; set; }

        public string Bairro { get; set; }

        public string Estado { get; set; }

        public long IdPessoa { get; set; }

        [ForeignKey(nameof(IdPessoa))]
        public virtual Pessoa Pessoa { get; set; }
    }
}