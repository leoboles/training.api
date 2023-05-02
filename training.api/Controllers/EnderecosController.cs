using Microsoft.AspNetCore.Mvc;
using training.api.Model;

namespace training.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecosController : ControllerBase
    {
        private readonly TrainingContext context;

        public EnderecosController(TrainingContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Endereco>> GetAll()
        {
            return Ok(context.Enderecos);
        }

        [HttpPost("{idPessoa}")]
        public ActionResult<Endereco> AddEndereco(long idPessoa, string rua, string bairro, string cidade, string estado, string numero)
        {
            Estado? newEstado = context.Estados.Where(e => e.UF == estado).FirstOrDefault();
            if (newEstado == null)
            {
                Estado est = new Estado();
                est.UF = estado;
                context.Estados.Add(est);
                context.SaveChanges();
                newEstado = est;
                return Ok(est);
            }

            Cidade? newCidade = context.Cidades.Where(c => c.Nome == cidade && c.IdEstado == newEstado.Id).FirstOrDefault();
            if (newCidade == null)
            {
                Cidade cid = new Cidade();
                cid.Nome = cidade;
                cid.IdEstado = context.Estados.Where(e => e.UF == estado).FirstOrDefault().Id;
                context.Cidades.Add(cid);
                context.SaveChanges();
                newCidade = cid;
                return Ok(cid);
            }

            Pessoa? pessoa = context.Pessoas.Where(p => p.Id == idPessoa).FirstOrDefault();
            if (pessoa != null)
            {
                Endereco newEndereco = new Endereco();

                newEndereco.Rua = rua;
                newEndereco.Numero = numero ?? "S/Nº";
                newEndereco.Bairro = bairro;
                newEndereco.idCidade = newCidade.Id;
                newEndereco.IdPessoa = idPessoa;
                newEndereco.Pessoa = pessoa;

                context.Enderecos.Add(newEndereco);
                context.SaveChanges();
                return Ok(newEndereco);
            }
            else
            {
                return NotFound(pessoa);
            }
        }

        [HttpPut("{idEndereco}")]
        public ActionResult<Endereco> UpdateEndereco(long idEndereco, string rua, string bairro, string cidade, string numero, string estado)
        {
            Endereco? endereco = context.Enderecos.Where(e => e.Id == idEndereco).FirstOrDefault();

            if (endereco != null)
            {
                Estado? newEstado = context.Estados.Where(e => e.UF == estado).FirstOrDefault();
                if (newEstado == null)
                {
                    Estado est = new Estado();
                    est.UF = estado;
                    context.Estados.Add(est);
                    context.SaveChanges();
                    newEstado = est;
                    return Ok(est);
                }

                Cidade? newCidade = context.Cidades.Where(c => c.Nome == cidade && c.IdEstado == newEstado.Id).FirstOrDefault();
                if (newCidade == null)
                {
                    Cidade cid = new Cidade();
                    cid.Nome = cidade;
                    cid.IdEstado = context.Estados.Where(e => e.UF == estado).FirstOrDefault().Id;
                    context.Cidades.Add(cid);
                    context.SaveChanges();
                    newCidade = cid;
                    return Ok(cid);
                }

                endereco.Rua = rua;
                endereco.Bairro = bairro;
                endereco.Numero = numero;
                endereco.idCidade = newCidade.Id;

                context.Enderecos.Update(endereco);
                context.SaveChanges();
                return Ok(endereco);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{idEndereco}")]
        public ActionResult<Endereco> DeleteEndereco(long idEndereco)
        {

            Endereco? endereco = context.Enderecos.Where(e => e.Id == idEndereco).FirstOrDefault();

            if (endereco != null)
            {
                context.Enderecos.Remove(endereco);
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
