using ApiCatalogo.DTO;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        public CategoriaController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
        {
            var categorias = await _uof.CategoriaRepository.GetCategorias();
            if (categorias is null || !categorias.Any())
            {
                return NotFound("Categorias não encontradas");
            }


            return Ok(categorias);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Categoria>> GetById(int id)
        {
            var categoria = await _uof.CategoriaRepository.GetCategoriaById(id);
            if (categoria is null)
            {
                return NotFound("Categoria não encontrada");
            }

            return Ok(categoria);
        }

        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasProdutos()
        {
            var categorias = await _uof.CategoriaRepository.GetCategoriasProdutos();
            if (categorias is null || !categorias.Any())
            {
                return NotFound("Categorias não encontradas");
            }

            return Ok(categorias);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> Post(CategoriaCreateDTO categoria)
        {
            if (categoria is null)
            {
                return BadRequest("Categoria inválida");
            }

            var createdCategoria = await _uof.CategoriaRepository.CreateCategoria(categoria);
            _uof.Commit();
            return CreatedAtAction(nameof(GetById), new { id = createdCategoria.CategoriaId }, createdCategoria);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> Put(int id, CategoriaUpdateDTO categoria)
        {
            if (categoria is null) return BadRequest("Categoria inválida");


            var updatedCategoria = await _uof.CategoriaRepository.UpdateCategoria(id, categoria);
            if (updatedCategoria is null)
            {
                return NotFound("Categoria não encontrada");
            }
            _uof.Commit();
            return Ok(updatedCategoria);
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> Delete(int id)
        {
            var deletedCategoria = await _uof.CategoriaRepository.DeleteCategoria(id);
            if (deletedCategoria is null)
            {
                return NotFound("Categoria não encontrada");
            }
            _uof.Commit();
            return Ok(deletedCategoria);
        }
    }
}