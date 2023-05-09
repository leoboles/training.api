using Microsoft.AspNetCore.Mvc;
using training.api.Model;

namespace training.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CidadesController : ControllerBase
    {
        private readonly TrainingContext context;

        public CidadesController(TrainingContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cidade>> GetAll()
        {
            return Ok(context.Cidades);
        }

        [HttpPost]
        public ActionResult<Cidade> Create(long idEstado, string nome) 
        {

            bool cidadeExists = context.Cidades.Any(p => p.Nome.ToLower() == nome.ToLower() && p.IdEstado == idEstado);
            if (cidadeExists == true) 
            {
                return BadRequest("Cidade já existe");
            }
            var cidade = new Cidade
            {
                IdEstado = idEstado,
                Nome = nome
            };

            context.Cidades.Add(cidade);
            context.SaveChanges();

            return CreatedAtAction(nameof(Create), new { id = cidade.Id}, cidade);
        }

        [HttpDelete]
        public ActionResult Delete(long id) 
        {
            var cidade = context.Cidades.FirstOrDefault(p => p.Id == id);
            if (cidade == null) 
            {
                return NotFound();
            }

            context.Cidades.Remove(cidade);
            context.SaveChanges();

            return Ok("Cidade excluida com sucesso!");
        }

        [HttpPut("{id}")]
        public ActionResult<Cidade> Update(long id, string nome, long idEstado)
        {
            var cidade = context.Cidades.FirstOrDefault(p => p.Id == id);
            if (cidade == null)
            {
                return NotFound();
            }
            bool cidadeExists = context.Cidades.Any(p => p.Nome.ToLower() == nome.ToLower() && p.IdEstado == idEstado);
            if (cidadeExists == true)
            {
                return BadRequest("Cidade já existe");
            }

            cidade.IdEstado = idEstado;
            cidade.Nome = nome;

            context.Update(cidade);
            context.SaveChanges();

            return Ok("Cidade alterada com sucesso!");
        }
    }
}
