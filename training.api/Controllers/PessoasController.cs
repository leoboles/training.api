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
        public ActionResult<IEnumerable<Pessoa>> GetAll()
        {
            return context.Pessoas;
        }
    }
}
