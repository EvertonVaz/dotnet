using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _context.Produtos.ToList();
            if (produtos is null || !produtos.Any())
            {
                return NotFound("Produtos não encontrados");
            }

            return Ok(produtos);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Produto> GetById(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }

            return Ok(produto);
        }

        [HttpPost]
        public ActionResult<Produto> Post(Produto produto)
        {
            if (produto is null)
            {
                return BadRequest("Produto inválido");
            }

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = produto.ProdutoId }, produto);
        }

    }
}