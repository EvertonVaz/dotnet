using ApiCatalogo.Context;
using ApiCatalogo.DTO;
using ApiCatalogo.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;


public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CategoriaRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoriaDTO>> GetCategorias()
    {
        return _mapper.Map<IEnumerable<CategoriaDTO>>(await _context.Categorias.ToListAsync());
    }

    public async Task<CategoriaDTO> GetCategoriaById(int id)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == id);

        if (categoria is null) throw new KeyNotFoundException("Categoria não encontrada");

        return _mapper.Map<CategoriaDTO>(categoria);
    }

    public async Task<IEnumerable<CategoriaDTO>> GetCategoriasProdutos()
    {
        var produtos = await _context.Categorias.Include(c => c.Produtos).ToListAsync();

        return _mapper.Map<IEnumerable<CategoriaDTO>>(produtos);
    }

    public async Task<CategoriaDTO> CreateCategoria(CategoriaCreateDTO categoria)
    {
        if (categoria is null) throw new ArgumentNullException(nameof(categoria), "Categoria inválida");

        var newCategoria = new Categoria
        {
            Nome = categoria.Nome,
            ImagemUrl = categoria.ImagemUrl
        };

        await _context.Categorias.AddAsync(newCategoria);

        return _mapper.Map<CategoriaDTO>(newCategoria);
    }

    public async Task<CategoriaDTO> UpdateCategoria(int id, CategoriaUpdateDTO categoria)
    {
        var existingCategoria = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == id);

        if (existingCategoria is null) throw new KeyNotFoundException("Categoria não encontrada");

        existingCategoria.Nome = categoria.Nome;

        await _context.Categorias
            .Where(c => c.CategoriaId == id)
            .ExecuteUpdateAsync(c => c.SetProperty(c => c.Nome, categoria.Nome!));

        return _mapper.Map<CategoriaDTO>(existingCategoria);
    }

    public async Task<CategoriaDTO> DeleteCategoria(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria is null) throw new KeyNotFoundException("Categoria não encontrada");

        await _context.Categorias
            .Where(c => c.CategoriaId == id)
            .ExecuteDeleteAsync();

        return _mapper.Map<CategoriaDTO>(categoria);
    }
}