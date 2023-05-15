using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using training.api.Model;

namespace training.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CidadeController : ControllerBase
    {
        private readonly TrainingContext context;

        public CidadeController(TrainingContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cidade>> GetAll()
        {
            IQueryable<Cidade> cidades = context.Cidades;
            return Ok(cidades);
        }

        [HttpPost]
        public ActionResult<Cidade> CreateCidade(string estado, string nome)
        {
            Estado? estados = context.Estados.Where(e => e.Sigla == estado).FirstOrDefault();
            Cidade cidades = context.Cidades.Where(c => c.Nome == nome && c.IdEstado == estados.Id).FirstOrDefault();

            if(cidades == null)
            {
                if(estados == null)
                {
                    Estado newEstado = new Estado();
                    newEstado.Sigla = estado;
                    context.Estados.Add(newEstado);
                    context.SaveChanges();
                    estados = newEstado;
                }
                var cidade = new Cidade
                {
                    Nome = nome,
                    IdEstado = estados.Id
                };
                context.Cidades.Add(cidade);
                context.SaveChanges();
                return Ok(cidade);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public void DeleteCidade(long id)
        {
            var cidadeDelete = context.Cidades.Find(id);
            if (cidadeDelete != null)
            {
                context.Cidades.Remove(cidadeDelete);
                context.SaveChanges();
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Cidade> UpdateCidade(long id, string estado, string nome)
        {
            var cidadeUpdate = context.Cidades.Find(id);
            Estado? estados = context.Estados.Where(e => e.Sigla == estado).FirstOrDefault();
            if (cidadeUpdate != null)
            {
                if(estados == null)
                {
                    Estado newEstado = new Estado();
                    newEstado.Sigla = estado;
                    context.Estados.Add(newEstado);
                    estados = newEstado;
                }
                cidadeUpdate.Nome = nome;
                cidadeUpdate.IdEstado = estados.Id;
                context.Cidades.Update(cidadeUpdate);
                context.SaveChanges();
            }
            return Ok(cidadeUpdate);
        }
    }
}
