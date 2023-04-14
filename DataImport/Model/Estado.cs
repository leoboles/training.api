using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace training.api.Model
{
    [Table("Estados")]
    public class Estado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Sigla { get; set; }

        public virtual ICollection<Cidade> Cidades { get; set; }
    }
}
