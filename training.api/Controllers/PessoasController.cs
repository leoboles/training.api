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

        [HttpGet("Filtro")]
        public ActionResult<IEnumerable<Pessoa>> Get(string order = "Nome", Sexo? sexo = null)
        {
            IQueryable<Pessoa> query = context.Pessoas;

            if (sexo != null)
            {
                query = query.Where(p => p.Sexo == sexo);
            }

            if (order == "Nome")
            {
                query = query.OrderBy(p => p.Nome);
            }

            return Ok(query);
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
        public ActionResult<Pessoa> Create(string nome, string cpf, string telefone, Sexo sexo) 
        {
            var pessoa = new Pessoa
            {
                Nome = nome,
                Cpf = cpf,
                Telefone = telefone,
                Sexo = sexo
            };

            bool cpfExists = context.Pessoas.Any(p => p.Cpf == cpf);

            if (Pessoa.ValidarCPF(cpf) == false)
            {
                return BadRequest("CPF inválido");
            } 
            else if (cpfExists)
            {
                return BadRequest("CPF já existe");
            }

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
