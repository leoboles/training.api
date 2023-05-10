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
            var cidades = context.Cidades;
            return Ok(cidades);
        }

        [HttpPost]
        public ActionResult<Cidade> AddCidade(string estado, string nome)
        {
            Estado? est = context.Estados.Where(e => e.UF == estado).FirstOrDefault();
            Cidade cid = context.Cidades.Where(c => c.Nome == nome && c.IdEstado == est.Id).FirstOrDefault();
            if (cid == null)
            {
                if (est == null)
                {
                    Estado newEstado = new Estado();

                    newEstado.UF = estado;

                    context.Estados.Add(newEstado);
                    context.SaveChanges();
                    est = newEstado;
                    return Ok(newEstado);
                }
                Cidade newCidade = new Cidade();

                newCidade.IdEstado = est.Id;
                newCidade.Nome = nome;

                context.Cidades.Add(newCidade);
                context.SaveChanges();
                return Ok(newCidade);
            }
            else
            {
                return Problem();
            }
        }

        [HttpPut("{idCidade}")]
        public ActionResult<Cidade> UpdateCidade(long idCidade, string estado, string nome)
        {
            Cidade? cidade = context.Cidades.Where(c => c.Id == idCidade).FirstOrDefault();

            if (cidade != null)
            {
                Estado est = context.Estados.Where(e => e.UF == estado).FirstOrDefault();

                if (est == null)
                {
                    Estado newEstado = new Estado();

                    newEstado.UF = estado;

                    context.Estados.Add(newEstado);
                    est = newEstado;
                }
                cidade.IdEstado = est.Id;
                cidade.Nome = nome;

                context.Cidades.Update(cidade);
                context.SaveChanges();
                return Ok(cidade);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpDelete("{idCidade}")]
        public ActionResult<Cidade> DeleteCidade(long idCidade)
        {
            Cidade? cidade = context.Cidades.Where(e => e.Id == idCidade).FirstOrDefault();

            if (cidade != null)
            {
                context.Cidades.Remove(cidade);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
