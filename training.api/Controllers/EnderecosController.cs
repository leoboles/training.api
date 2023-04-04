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
            return Ok(context.Add(endereco));
        }



        [HttpDelete]



        public ActionResult Delete(long idPessoa)
        {
            var endereco = context.Enderecos.FirstOrDefault(p => p.IdPessoa == idPessoa);
            if (endereco == null)
            {
                return BadRequest();
            }
            context.Remove(endereco);
            context.SaveChanges();
            return Ok("Você Excluiu o endereço com Sucesso !!");



        }



    }
}