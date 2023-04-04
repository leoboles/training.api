using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace training.api.Model
{
    [Table("Pessoas")]
    public class Pessoa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public Sexo Sexo { get; set; }

        public virtual ICollection<Endereco> Enderecos { get; set; }
    }
}
