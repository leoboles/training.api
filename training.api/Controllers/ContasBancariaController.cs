using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using training.api.Model;

namespace training.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaBancariaController : ControllerBase
    {
        private readonly TrainingContext context;

        public ContaBancariaController(TrainingContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ContaBancaria>> GetAll()
        {
            return Ok(context.ContaBancarias);
        }

        [HttpPost]
        public ActionResult<ContaBancaria> Create(float saldo, string agencia, string conta, long idPessoa, long idBanco)
        {
            var contaBancaria = new ContaBancaria
            {
                IdPessoa = idPessoa,
                IdBanco = idBanco,
                Saldo = saldo,
                Agencia = agencia,
                Conta = conta,
            };

            context.ContaBancarias.Add(contaBancaria);
            context.SaveChanges();

            return CreatedAtAction(nameof(Create), new { id = contaBancaria.Id }, contaBancaria);
        }

        [HttpDelete]
        public ActionResult Delete(long idPessoa, long id)
        {
            var contaBancaria = context.ContaBancarias.FirstOrDefault(p => p.IdPessoa == idPessoa && p.Id == id);
            if (contaBancaria == null)
            {
                return NotFound();
            }

            context.ContaBancarias.Remove(contaBancaria);
            context.SaveChanges();

            return Ok("Conta Bancaria excluida com sucesso!");
        }

        [HttpPut("{id}")]
        public ActionResult<ContaBancaria> Update(long id, float valor)
        {
            var contaBancaria = context.ContaBancarias.FirstOrDefault(p => p.Id == id);
            if (contaBancaria == null)
            {
                return NotFound();
            }
            contaBancaria.Id = id;
            contaBancaria.Deposito(valor);
            context.Update(contaBancaria);
            context.SaveChanges();
            return Ok(contaBancaria);
        }

        [HttpPut("Pagamento")]
        public ActionResult<ContaBancaria> Update(long idProduto, long id)
        {
            var contaBancaria = context.ContaBancarias.FirstOrDefault(p => p.Id == id);
            var produto = context.Produtos.FirstOrDefault(p => p.Id == idProduto);
            if (contaBancaria == null || produto == null)
            {
                return NotFound();
            }
            contaBancaria.Id = id;
            produto.Id = idProduto;
            contaBancaria.Pagamento(produto);
            context.Update(contaBancaria);
            context.SaveChanges();
            return Ok(contaBancaria);
        }

    }
}
