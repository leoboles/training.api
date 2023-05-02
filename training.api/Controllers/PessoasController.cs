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
            if (nome != null)
                pessoas = pessoas.Where(p => p.Nome.Contains(nome));

            if (sexo != null)
                pessoas = pessoas.Where(p => p.Sexo == sexo);

            return Ok(pessoas);
        }

        [HttpPost]
        public ActionResult<Pessoa> AddPessoa(string nome, Sexo sexo, string cpf, string? telefone = "S/Nº")
        {
            Pessoa? pessoa = context.Pessoas.Where(p => p.Cpf == cpf).FirstOrDefault();
            if (pessoa == null)
            {
                Pessoa newPessoa = new Pessoa();

                newPessoa.Nome = nome;
                newPessoa.Sexo = sexo;
                newPessoa.Telefone = telefone;
                newPessoa.Cpf = cpf;

                context.Pessoas.Add(newPessoa);
                context.SaveChanges();
                return Ok(newPessoa);
            }
            else
            {
                return Problem(cpf);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Pessoa> DeletePessoa(long id)
        {

            Pessoa? pessoa = context.Pessoas.Where(p => p.Id == id).FirstOrDefault();

            if (pessoa != null)
            {
                context.Pessoas.Remove(pessoa);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut("{id}")]
        public ActionResult<Pessoa> UpdatePessoa(long id, string nome, Sexo sexo, string cpf,string? telefone = null)
        {

            Pessoa? pessoa = context.Pessoas.Where(p => p.Id == id).FirstOrDefault();

            if (pessoa != null)
            {
                pessoa.Nome = nome;
                pessoa.Sexo = sexo;
                pessoa.Telefone = telefone;
                pessoa.Cpf = cpf;

                context.Pessoas.Update(pessoa);
                context.SaveChanges();
                return Ok(pessoa);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
