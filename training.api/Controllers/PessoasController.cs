using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using training.api.Model;



namespace training.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly TrainingContext context;





        public PessoasController(TrainingContext context)
        {
            this.context = context;
        }



        [HttpGet]
        public ActionResult<IEnumerable<Pessoa>> GetAll()
        {
            return Ok(context.Pessoas);
        }
        [HttpPost]
        public ActionResult Create(long id, string nome, string telefone, Sexo sexo)
        {
            Pessoa pessoa = new Pessoa();
            {
                pessoa.Id = id;
                pessoa.Nome = nome;
                pessoa.Telefone = telefone;
                pessoa.Sexo = sexo;



            }



            context.Pessoas.Add(pessoa);
            context.SaveChanges();
            return Ok(context.Add(pessoa));
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            var pessoa = context.Pessoas.First(p => p.Id == id);
            if (pessoa == null)
            {
                return BadRequest();
            }
            context.Pessoas.Remove(pessoa);
            context.SaveChanges();
            return Ok("Você Excluiu a pessoa com Sucesso!!");
        }





    }
}

