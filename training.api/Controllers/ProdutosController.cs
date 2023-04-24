using Microsoft.AspNetCore.Mvc;
using training.api.Model;

namespace training.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        public readonly TrainingContext context;

        public ProdutosController(TrainingContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetAll()
        {
            return Ok(context.Produtos);
        }

        [HttpPost]
        public ActionResult<Produto> Create(string nome, float valor, long idLoja)
        {
            var produto = new Produto()
            {
                Nome = nome,
                Valor = valor,
                IdLoja = idLoja
            };
            context.Add(produto);
            context.SaveChanges();
            return CreatedAtAction(nameof(Create), new { id = produto.Id }, produto);
        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            var produto = context.Produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null)
            {
                return NotFound();
            }
            context.Remove(produto);
            context.SaveChanges();
            return Ok("Você Excluiu o Produto com Sucesso !!");
        }

        [HttpPut]
        public ActionResult<Produto> Update(long id, string nome , float valor, long idLoja)
        {
            var produto = context.Produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null)
            {
                return NotFound();
            }
            produto.Id = id;
            produto.Nome = nome;
            produto.Valor = valor;
            produto.IdLoja = idLoja;

            context.Update(produto);
            context.SaveChanges();
            return Ok(produto);
        }

    }
}
