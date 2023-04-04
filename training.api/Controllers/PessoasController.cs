using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
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
        [HttpGet("Nome")]
        public ActionResult<IEnumerable<Pessoa>> Get()
        {
            return Ok(context.Pessoas.OrderBy(p => p.Nome));
        }
        [HttpGet("Sexo")]
        public ActionResult<IEnumerable<Pessoa>> GetResult(Sexo sexo)
        {
            return Ok(context.Pessoas.Where(p => p.Sexo == sexo));
        }
        [HttpGet("{id}")]
        public ActionResult<Pessoa> Get(long id)
        {
            var pessoa = context.Pessoas.FirstOrDefault(p => p.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            } 
            return Ok(pessoa);
        }
        [HttpPost]
        public ActionResult<Pessoa> Create(long id, string nome, string telefone, Sexo sexo) 
        {
            var pessoa = new Pessoa
            {
                Id = id,
                Nome = nome,
                Telefone = telefone,
                Sexo = sexo
            };

            context.Pessoas.Add(pessoa);
            context.SaveChanges();

            return CreatedAtAction(nameof(Create), new { id = pessoa.Id}, pessoa);
        }
        [HttpDelete]
        public ActionResult Delete(long id) 
        {
            var pessoa = context.Pessoas.FirstOrDefault(p => p.Id == id);
            if (pessoa == null) 
            {
                return NotFound();
            }

            context.Pessoas.Remove(pessoa);
            context.SaveChanges();

            return Ok("Pessoa excluida com sucesso!");
        }
        [HttpPut("{id}")]
        public ActionResult<Pessoa> Update(long id, string nome, string telefone, Sexo sexo)
        {
            var pessoa = context.Pessoas.FirstOrDefault(p => p.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            pessoa.Nome = nome;
            pessoa.Telefone = telefone;
            pessoa.Sexo = sexo;

            context.Update(pessoa);
            context.SaveChanges();

            return Ok(pessoa);
        }
    }
}
