using Microsoft.AspNetCore.Mvc;
using training.api.Model;



namespace training.api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LojasController : ControllerBase
    {
        public readonly TrainingContext context;

        public LojasController(TrainingContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Loja>> GetAll()
        {
            return Ok(context.Lojas);
        }

        [HttpPost]
        public ActionResult<Loja> Create(string nome)
        {
            var loja = new Loja()
            {
                Nome = nome
            };
            context.Add(loja);
            context.SaveChanges();
            return CreatedAtAction(nameof(Create), new { id = loja.Id }, loja);
        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            var loja = context.Lojas.FirstOrDefault(p => p.Id == id);
            if (loja == null)
            {
                return NotFound();
            }
            context.Remove(loja);
            context.SaveChanges();
            return Ok("Você Excluiu a Loja com Sucesso !!");
        }

        [HttpPut]
        public ActionResult<Loja> Update(long id, string nome)
        {
            var loja = context.Lojas.FirstOrDefault(p => p.Id == id);
            if (loja == null)
            {
                return NotFound();
            }
            loja.Id = id;
            loja.Nome = nome;
            context.Update(loja);
            context.SaveChanges();
            return Ok(loja);
        }
    }
}