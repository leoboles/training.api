using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace training.api.Model
{
    [Table("Produtos")]
    public class Produto 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Nome { get; set; }

        public float Valor { get; set; }

        public long IdLoja { get; set; }

        [ForeignKey(nameof(IdLoja))]
        [JsonIgnore]
        public virtual Loja Loja { get; set; }

    }
}
