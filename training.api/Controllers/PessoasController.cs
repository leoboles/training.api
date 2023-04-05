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
            return CreatedAtAction(nameof(Create), new { id = pessoa.Id }, pessoa);
        }

        [HttpDelete]

        public ActionResult Delete(long id)
        {
            var pessoa = context.Pessoas.First(p => p.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            }
            context.Pessoas.Remove(pessoa);
            context.SaveChanges();
            return Ok("Você Excluiu a pessoa com Sucesso!!");
        }

        [HttpPut]

        public ActionResult<Pessoa> Update(long id, string nome, string telefone, Sexo sexo)
        {
            var pessoa = context.Pessoas.FirstOrDefault(p => p.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            }
            pessoa.Nome = nome.Trim();
            pessoa.Telefone = telefone.Trim();
            pessoa.Sexo = sexo;
            context.Update(pessoa);
            context.SaveChanges();

            return Ok(pessoa);
        }
    }
}

