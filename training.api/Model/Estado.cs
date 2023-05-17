using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace training.api.Model
{
    [Table("Estado")]
    public class Estado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Nome { get; set; }
        public string Sigla { get; set; }

        public virtual ICollection<Cidade> Cidades { get; set; }

    }
}