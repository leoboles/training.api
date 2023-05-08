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
        public ActionResult <Pessoa> Create(string nome, string cpf, string telefone, Sexo sexo)
        {
            var pessoa = new Pessoa
            {
                Nome = nome.Trim(),
                CPF = cpf.Trim(),
                Telefone = telefone.Trim(),
                Sexo = sexo,
            };
            bool cpfExists = context.Pessoas.Any(p => p.CPF == cpf);

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
            return CreatedAtAction(nameof(Create), new { id = pessoa.Id }, pessoa);
        }

        [HttpDelete("{id}")]
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

        [HttpPut("{id}")]
        public ActionResult<Pessoa> Update(long id, string nome, string cpf, string telefone, Sexo sexo)
        {
            var pessoa = context.Pessoas.FirstOrDefault(p => p.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            }
            pessoa.Id = id;
            pessoa.Nome = nome.Trim().Trim();
            pessoa.Telefone = telefone.Trim();
            pessoa.Sexo = sexo;
            context.Update(pessoa);
            context.SaveChanges();

            return Ok(pessoa);
        }

    }
}

