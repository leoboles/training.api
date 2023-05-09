using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace training.api.Model
{
    public class Banco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Nome { get; set; }

        public long IdCidade { get; set; }

        [ForeignKey(nameof(IdCidade))]
        [JsonIgnore]
        public virtual Cidade Cidade { get; set; }
    }
}