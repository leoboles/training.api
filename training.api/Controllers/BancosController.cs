using Microsoft.AspNetCore.Mvc;
using training.api.Model;

namespace training.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BancosController : ControllerBase
    {
        private readonly TrainingContext context;

        public BancosController(TrainingContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Banco>> GetAll()
        {
            return Ok(context.Bancos);
        }

        [HttpPost]
        public ActionResult<Banco> Create(long idCidade, string nome) 
        {
            var banco = new Banco
            {
                IdCidade = idCidade,
                Nome = nome
            };

            context.Bancos.Add(banco);
            context.SaveChanges();

            return CreatedAtAction(nameof(Create), new { id = banco.Id}, banco);
        }

        [HttpDelete]
        public ActionResult Delete(long id) 
        {
            var banco = context.Bancos.FirstOrDefault(p => p.Id == id);
            if (banco == null) 
            {
                return NotFound();
            }

            context.Bancos.Remove(banco);
            context.SaveChanges();

            return Ok("Banco excluido com sucesso!");
        }

        [HttpPut("{id}")]
        public ActionResult<Banco> Update(long id, string nome)
        {
            var banco = context.Bancos.FirstOrDefault(p => p.Id == id);
            if (banco == null)
            {
                return NotFound();
            }

            banco.Nome = nome;

            context.Update(banco);
            context.SaveChanges();

            return Ok("Banco alterado com sucesso!");
        }
    }
}
