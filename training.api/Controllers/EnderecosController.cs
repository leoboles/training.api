using Microsoft.AspNetCore.Mvc;
using training.api.Model;

namespace training.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EndereçosController : ControllerBase
    {
        private readonly TrainingContext context;

        public EndereçosController(TrainingContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Endereco>> GetAll()
        {
            return Ok(context.Enderecos);
        }
        [HttpPost]
        public ActionResult<Endereco> Create(long id, long idPessoa, string rua, string numero, string bairro, string estado) 
        {
            var endereco = new Endereco
            {
                Id = id,
                IdPessoa = idPessoa,
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
                Estado = estado
            };

            context.Enderecos.Add(endereco);
            context.SaveChanges();

            return CreatedAtAction(nameof(Create), new { id = endereco.Id}, endereco);
        }
        [HttpDelete]
        public ActionResult Delete(long idPessoa) 
        {
            var endereco = context.Enderecos.FirstOrDefault(p => p.IdPessoa == idPessoa);
            if (endereco == null) 
            {
                return NotFound();
            }

            context.Enderecos.Remove(endereco);
            context.SaveChanges();

            return Ok("Endereço excluido com sucesso!");
        }
        [HttpPut("{idPessoa}")]
        public ActionResult<Endereco> Update(long id, long idPessoa, string rua, string numero, string bairro, string estado)
        {
            var endereco = context.Enderecos.FirstOrDefault(p => p.IdPessoa == idPessoa);
            if (endereco == null)
            {
                return NotFound();
            }

            endereco.Rua = rua;
            endereco.Numero = numero;
            endereco.Bairro = bairro;
            endereco.Estado = estado;

            context.Update(endereco);
            context.SaveChanges();

            return Ok("Endereço alterado com sucesso!");
        }
    }
}
