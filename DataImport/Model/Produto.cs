using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace training.api.Model
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Nome { get; set; }

        public float Preco { get; set; }

        public long IdLoja { get; set; }
        [ForeignKey(nameof(IdLoja))]
        public virtual Loja Loja { get; set; }  
    }
}