using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace training.api.Model
{
    public class Cidade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Nome { get; set; }

        public long IdEstado { get; set; }

        [ForeignKey(nameof(IdEstado))]
        [JsonIgnore]
        public virtual Estado Estado { get; set; }
    }
}