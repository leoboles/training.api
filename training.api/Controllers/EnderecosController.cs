using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using training.api.Model;

namespace training.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecosController : ControllerBase
    {
        private readonly TrainingContext context;

        public EnderecosController(TrainingContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Endereco>> GetAll()
        {
            return Ok(context.Enderecos);
        }

        [HttpPost("{idPessoa}")]
        public ActionResult<Endereco> AddEndereco(long idPessoa, string rua, string bairro, string estado, string numero)
        {
            Pessoa? pessoa = context.Pessoas.Where(p => p.Id == idPessoa).FirstOrDefault();
            if (pessoa != null)
            {
                Endereco newEndereco = new Endereco();

                newEndereco.Rua = rua;
                newEndereco.Numero = numero ?? "S/Nº";
                newEndereco.Bairro = bairro;
                newEndereco.Estado = estado;
                newEndereco.IdPessoa = idPessoa;
                newEndereco.Pessoa = pessoa;

                context.Enderecos.Add(newEndereco);
                context.SaveChanges();
                return Ok(newEndereco);
            }
            else
            {
                return NotFound(pessoa);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Endereco> UpdateEndereco(long id, string rua, string bairro, string estado, string numero)
        {
            Endereco? endereco = context.Enderecos.Where(e => e.Id == id).FirstOrDefault();

            if (endereco != null)
            {
                endereco.Rua = rua;
                endereco.Bairro = bairro;
                endereco.Numero = numero;
                endereco.Estado = estado;

                context.Enderecos.Update(endereco);
                context.SaveChanges();
                return Ok(endereco);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Endereco> DeleteEndereco(long id)
        {

            Endereco? endereco = context.Enderecos.Where(e => e.Id == id).FirstOrDefault();

            if (endereco != null)
            {
                context.Enderecos.Remove(endereco);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
