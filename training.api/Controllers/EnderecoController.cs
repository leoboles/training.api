using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Cryptography;
using training.api.Model;

namespace training.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly TrainingContext context;

        public EnderecoController(TrainingContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Endereco>> GetAll(string? rua = null, string? numero = null, string? bairro = null, string? estado = null)
        {
            IQueryable<Endereco> enderecos = context.Enderecos;
            if (rua != null)
            {
                enderecos = enderecos.Where(e => e.Rua.Contains(rua));
            }
            if (numero != null)
            {
                enderecos = enderecos.Where(e => e.Numero.Contains(numero));

            }
            if (bairro != null)
            {
                enderecos = enderecos.Where(e => e.Bairro.Contains(bairro));
            }
            if (estado != null)
            {
                enderecos = enderecos.Where(e => e.Estado.Contains(estado));
            }
            return Ok(enderecos);
        }
        [HttpGet("{idPessoa}")]
        public ActionResult<IEnumerable<Endereco>> GetEnderecoByIdPessoa(long idPessoa)
        {
            IQueryable<Endereco> endereco = context.Enderecos;
            if (idPessoa > 0)
            {
                endereco = endereco.Where(x => x.IdPessoa == idPessoa);
            }
            return Ok(endereco);
        }

        [HttpPost]
        public ActionResult<Endereco> CreateEndereco(string rua, string numero, string bairro, string estado, long idPessoa)
        {
            var newEndereco = new Endereco
            {
                Rua = rua,
                Numero = numero,
                Bairro = bairro,
                Estado = estado,
                IdPessoa = idPessoa
            };
            context.Enderecos.Add(newEndereco);
            context.SaveChanges();
            return Ok(newEndereco);
        }
        [HttpDelete]
        public void DeleteEndereco(long id)
        {
            var enderecoDelete = context.Pessoas.Find(id);
            if (enderecoDelete != null)
            {
                context.Pessoas.Remove(enderecoDelete);
                context.SaveChanges();
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Endereco> UpdateEndereco(long id, string numero, string rua, string estado, string bairro)
        {
            var enderecoUpdate = context.Enderecos.Find(id);
            if (enderecoUpdate != null)
            {
                enderecoUpdate.Numero = numero;
                enderecoUpdate.Rua = rua;
                enderecoUpdate.Estado = estado;
                enderecoUpdate.Bairro = bairro;
                context.Enderecos.Update(enderecoUpdate);
                context.SaveChanges();
            }
            return Ok(enderecoUpdate);
        }
    }
}
