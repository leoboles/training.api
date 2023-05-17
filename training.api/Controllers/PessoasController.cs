using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<IEnumerable<Pessoa>> GetAll(string? nome = null, Sexo? sexo = null)
        {
            IQueryable<Pessoa> pessoas = context.Pessoas;

            if(nome != null) {
                pessoas = pessoas.Where(x => x.Nome.Contains(nome));
            }
            if(sexo != null)
            {
                pessoas = pessoas.Where(x => x.Sexo == sexo);
            }
            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Pessoa>> GetByIdPessoa(long id)
        {
            IQueryable<Pessoa> pessoas = context.Pessoas;
            if (id > 0)
            {
                pessoas = (IQueryable<Pessoa>)context.Pessoas.Where(p => p.Id == id).FirstOrDefault();
            }
            return Ok(pessoas);
        }

        [HttpPost]
        public ActionResult<Pessoa> CreatePessoa(Sexo sexo, string nome, string telefone, string cpf)
        {
            Pessoa? people = context.Pessoas.Where(p => p.Cpf == cpf).FirstOrDefault();
            if(people != null)
            {
                return BadRequest("Pessoa:" + people.Nome + ", já existe na base de dados!");
            }
            var pessoa = new Pessoa
            {
                Nome = nome,
                Telefone = telefone,
                Sexo = sexo,
                Cpf = cpf
            };
            context.Pessoas.Add(pessoa);
            context.SaveChanges();
            return Ok(pessoa);
        }

        [HttpDelete]
        public ActionResult<Pessoa> DeletePessoa(long id)
        {
            var pessoaDelete = context.Pessoas.Find(id);
            if(pessoaDelete != null)
            {
                context.Pessoas.Remove(pessoaDelete);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Pessoa> UpdatePessoa(long id, string nome, string telefone, Sexo sexo)
        {
            var pessoaUpdate = context.Pessoas.Find(id);
            if (pessoaUpdate != null)
            {
                pessoaUpdate.Nome = nome;
                pessoaUpdate.Telefone = telefone;
                pessoaUpdate.Sexo = sexo;
                context.Pessoas.Update(pessoaUpdate);
                context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok(pessoaUpdate);
        }

    }
}
