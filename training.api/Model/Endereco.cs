using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        public long IdEstado { get; set; }

        public long IdCidade { get; set; }

        [ForeignKey(nameof(IdCidade))]
        [JsonIgnore]
        public virtual Cidade Cidade { get; set; }

        public long IdPessoa { get; set; }

        [ForeignKey(nameof(IdPessoa))]
        [JsonIgnore]
        public virtual Pessoa Pessoa { get; set; }

    }

}