using Microsoft.AspNetCore.Mvc;
using training.api.Model;

namespace training.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CidadeController : ControllerBase
    {
        public readonly TrainingContext context;

        public CidadeController(TrainingContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cidade>> GetAll()
        {
            return Ok(context.Cidades);
        }

        [HttpPost]
        public ActionResult<Cidade> Create(string nome, long idEstado)
        {
            var cidade = new Cidade()
            {
                Nome = nome,
                IdEstado = idEstado,
                
            };
           bool cidadeExistente = context.Cidades.Any(c => c.IdEstado == idEstado && c.Nome.ToLower() == nome.ToLower());
            if (cidadeExistente)
            {
                return BadRequest("Cidade existente");

            }
            context.Add(cidade);
            context.SaveChanges();
            return CreatedAtAction(nameof(Create), new { id = cidade.Id }, cidade);
        }

        [HttpPut]
        public ActionResult<Cidade> Update(long id, string nome, long idEstado)
        {
            var cidade = context.Cidades.FirstOrDefault(p => p.Id == id);
            if (cidade == null)
            {
                return NotFound();
            }
            bool cidadeExistente = context.Cidades.Any(c => c.IdEstado == idEstado && c.Nome.ToLower() == nome.ToLower());
            if (cidadeExistente)
            {
                return BadRequest("Cidade existente");

            }
            cidade.Id = id;
            cidade.Nome = nome.Trim();
            cidade.IdEstado = idEstado;
            context.Update(cidade);
            context.SaveChanges();

            return Ok(cidade);
        }


    }
}
