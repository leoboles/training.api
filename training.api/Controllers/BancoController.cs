using Microsoft.AspNetCore.Mvc;
using training.api.Model;

namespace training.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BancoController : ControllerBase
    {
        public readonly TrainingContext context;

        public BancoController(TrainingContext context) 
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Banco>> GetAll()
        {
            return Ok(context.Bancos);
        }

        [HttpPost]
        public ActionResult<Banco> Create(string nome, long idCidade)
        {
            var banco = new Banco()
            {
                Name = nome,
                IdCidade = idCidade,
            };
            context.Add(banco);
            context.SaveChanges();
            return CreatedAtAction(nameof(Create),new { id = banco.Id }, banco);  
        }


        [HttpDelete]
        public ActionResult Delete(long id)
        {
            var banco = context.Bancos.FirstOrDefault(p => p.Id == id);
            if (banco == null)
            {
                return NotFound();
            }
            context.Remove(banco);
            context.SaveChanges();
            return Ok("Você Fechou o Banco"+ banco.Name + " com Sucesso !!");
        }

        [HttpPut]
        public ActionResult<Banco> Update(long id, string name)
        {
            var banco = context.Bancos.FirstOrDefault(p => p.Id == id);
            if (banco == null)
            {
                return NotFound();
            }
            banco.Id = id;
            banco.Name = name;
            context.Update(banco);
            context.SaveChanges();
            return Ok(banco);
        }
    }

}
