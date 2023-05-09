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
        public ActionResult<Endereco> Create(long idPessoa, string rua, string numero, string bairro, long idEstado, long idCidade) 
        {
            var endereco = new Endereco
            {
                IdPessoa = idPessoa,
                IdEstado = idEstado,
                IdCidade = idCidade,
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
            };

            context.Enderecos.Add(endereco);
            context.SaveChanges();

            return CreatedAtAction(nameof(Create), new { id = endereco.Id}, endereco);
        }

        [HttpDelete]
        public ActionResult Delete(long idPessoa, long id) 
        {
            var endereco = context.Enderecos.FirstOrDefault(p => p.IdPessoa == idPessoa && p.Id == id);
            if (endereco == null) 
            {
                return NotFound();
            }

            context.Enderecos.Remove(endereco);
            context.SaveChanges();

            return Ok("Endereço excluido com sucesso!");
        }

        [HttpPut("{id}")]
        public ActionResult<Endereco> Update(long id, string rua, string numero, string bairro, long idEstado, long idCidade)
        {
            var endereco = context.Enderecos.FirstOrDefault(p => p.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }

            endereco.IdEstado = idEstado;
            endereco.IdCidade = idCidade;
            endereco.Rua = rua;
            endereco.Numero = numero;
            endereco.Bairro = bairro;

            context.Update(endereco);
            context.SaveChanges();

            return Ok("Endereço alterado com sucesso!");
        }
    }
}
