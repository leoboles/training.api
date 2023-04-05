using Microsoft.AspNetCore.Mvc;
using training.api.Model;



namespace training.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecosController : ControllerBase
    {
        public readonly TrainingContext context;

        public EnderecosController(TrainingContext context)
        {
            this.context = context;
        }

        [HttpGet]

        public ActionResult<IEnumerable<Endereco>> GetAll()
        {
            return Ok(context.Enderecos);
        }

        [HttpPost]

        public ActionResult<Endereco> Create(long id, string rua, string numero, string bairro, string estado, long idpessoa)
        {
            var endereco = new Endereco()
            {
                Id = id,
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
                Estado = estado,
                IdPessoa = idpessoa
            };
            context.Add(endereco);
            context.SaveChanges();
            return CreatedAtAction(nameof(Create), new { id = endereco.Id }, endereco);
        }

        [HttpDelete]

        public ActionResult Delete(long id)
        {
            var endereco = context.Enderecos.FirstOrDefault(p => p.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }
            context.Remove(endereco);
            context.SaveChanges();
            return Ok("Você Excluiu o endereço com Sucesso !!");
        }

        [HttpPut]

        public ActionResult<Endereco> Update(long id, string rua, string numero, string bairro, string estado, long idpessoa)
        {
            var endereco = context.Enderecos.FirstOrDefault(p => p.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }
            endereco.Rua = rua.Trim();
            endereco.Numero = numero.Trim();
            endereco.Bairro = bairro.Trim();
            endereco.Estado = estado.Trim();
            endereco.IdPessoa = idpessoa;
            context.Update(endereco);
            context.SaveChanges();
            return Ok(endereco);
        }
    }
}