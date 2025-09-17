using ApiCatalogo.Context;
using ApiCatalogo.DTO;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        public ProdutoController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            var produtos = await _uof.ProdutoRepository.GetProdutos();

            if (produtos is null || !produtos.Any())
                return NotFound("Produtos não encontrados");

            return Ok(produtos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> GetById(int id)
        {
            var produto = await _uof.ProdutoRepository.GetProdutoById(id);
            if (produto is null) return NotFound("Produto não encontrado");

            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> Post(ProdutoCreateDTO produto)
        {
            if (produto is null) return BadRequest("Produto inválido");
            if (produto.CategoriaId <= 0) return BadRequest("CategoriaId inválido");

            var createdProduto = await _uof.ProdutoRepository.CreateProduto(produto);
            _uof.Commit();

            return CreatedAtAction(nameof(GetById), new { id = createdProduto.ProdutoId }, createdProduto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> Put(int id, ProdutoUpdateDTO produto)
        {
            if (produto is null) return BadRequest("Produto inválido");

            var updatedProduto = await _uof.ProdutoRepository.UpdateProduto(id, produto);
            _uof.Commit();

            return Ok(updatedProduto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> Delete(int id)
        {
            var produto = await _uof.ProdutoRepository.GetProdutoById(id);
            if (produto is null) return NotFound("Produto não encontrado");

            var deletedProduto = await _uof.ProdutoRepository.DeleteProduto(id);
            _uof.Commit();

            return Ok(deletedProduto);
        }
    }
}