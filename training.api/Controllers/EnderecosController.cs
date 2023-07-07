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
        public ActionResult<Endereco> Create( string rua, string numero, string bairro, long idEstado, long idCidade,  long idpessoa)
        {
            var endereco = new Endereco()
            {
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
                IdEstado = idEstado,
                IdCidade = idCidade,
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
        public ActionResult<Endereco> Update(long id, string rua, string numero, string bairro, long idEstado, long idCidade, long idpessoa)
        {
            var endereco = context.Enderecos.FirstOrDefault(p => p.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }
            endereco.Id = id;
            endereco.Rua = rua.Trim();
            endereco.Numero = numero.Trim();
            endereco.Bairro = bairro.Trim();
            endereco.IdEstado = idEstado;
            endereco.IdCidade = idCidade;
            endereco.IdPessoa = idpessoa;
            context.Update(endereco);
            context.SaveChanges();
            return Ok(endereco);
        }
    }
}
