using Microsoft.AspNetCore.Mvc;
using training.api.Model;

namespace training.api.Controllers
{
    public class EstadoController : ControllerBase
    {
        private readonly TrainingContext context;

        public EstadoController(TrainingContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Estado>> GetAll()
        {
            var estados = context.Estados;
            return Ok(estados);
        }

        [HttpPost]
        public ActionResult<Estado> AddEstado(string uf)
        {
            Estado? newEstado = context.Estados.Where(e => e.UF == uf).FirstOrDefault();
            if (newEstado != null)
            {
                newEstado.UF = uf;

                context.Estados.Add(newEstado);
                context.SaveChanges();
                return Ok(newEstado);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{idEstado}")]
        public ActionResult<Estado> DeleteEstado(long idEstado)
        {
            Estado? estado = context.Estados.Where(e => e.Id == idEstado).FirstOrDefault();

            if (estado != null)
            {
                context.Estados.Remove(estado);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut("{idEstado}")]
        public ActionResult<Estado> UpdateEstado(long idEstado, string uf)
        {
            Estado? estado = context.Estados.Where(e => e.Id == idEstado).FirstOrDefault();

            if (estado != null)
            {
                estado.UF = uf;

                context.Estados.Update(estado);
                context.SaveChanges();
                return Ok(estado);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
