using Microsoft.AspNetCore.Mvc;
using training.api.Model;

namespace training.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaBancariaController : ControllerBase
    {
        public readonly TrainingContext context;

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
        public ActionResult<ContaBancaria> Create(string agencia, string conta, float saldoIncial, long idPessoa)
        {
            var contaBancaria = new ContaBancaria()
            {
                Agencia = agencia,
                Conta = conta,
                Saldo = saldoIncial,
                IdPessoa = idPessoa,
            };         
            if (contaBancaria.Saldo <= 0)
            {
                return NotFound();
            }
            context.Add(contaBancaria);
            context.SaveChanges();
            return CreatedAtAction(nameof(Create), new { id = contaBancaria.Id }, contaBancaria);
        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            var contaBancaria= context.ContaBancarias.FirstOrDefault(p => p.Id == id);
            if (contaBancaria == null)
            {
                return NotFound();
            }
            context.Remove(contaBancaria);
            context.SaveChanges();
            return Ok("Você Excluiu sua Conta Bancaria com Sucesso !!");
        }

        [HttpPut("Deposito")]
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

        [HttpPut("Transação")]
        public ActionResult<ContaBancaria> Update(long id, long idProduto)
        {
            var contaBancaria = context.ContaBancarias.FirstOrDefault(p => p.Id == id);
            var produto = context.Produtos.FirstOrDefault(p => p.Id == idProduto);
            if (contaBancaria == null || produto == null)
            {
                return NotFound();
            }
            id = contaBancaria.Id;
            idProduto = produto.Id;
            contaBancaria.Pagamento(produto);
            context.Update(contaBancaria);
            context.SaveChanges();
            return CreatedAtAction(nameof(Create), new { id = contaBancaria.Id }, contaBancaria);
        }
    }

}

