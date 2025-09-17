
using ApiCatalogo.Context;
using ApiCatalogo.DTO;
using ApiCatalogo.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;

class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProdutoRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProdutoDTO>> GetProdutos()
    {
        var produtos = await _context.Produtos.AsNoTracking().ToListAsync();
        return _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
    }

    public async Task<ProdutoDTO> GetProdutoById(int id)
    {
        var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.ProdutoId == id);

        if (produto is null) throw new KeyNotFoundException("Produto não encontrado");

        return _mapper.Map<ProdutoDTO>(produto);
    }

    public async Task<IEnumerable<ProdutoDTO>> GetProdutosCategoria(int categoriaId)
    {
        var produto = await _context.Produtos
            .Where(p => p.CategoriaId == categoriaId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ProdutoDTO>>(produto);
    }

    public async Task<ProdutoDTO> CreateProduto(ProdutoCreateDTO produto)
    {
        var entity = _mapper.Map<Produto>(produto);
        await _context.Produtos.AddAsync(entity);

        return _mapper.Map<ProdutoDTO>(entity);
    }

    public async Task<ProdutoDTO> DeleteProduto(int id)
    {
        var entity = await _context.Produtos.FindAsync(id);
        if (entity == null) throw new KeyNotFoundException("Produto não encontrado");

        _context.Produtos.Remove(entity);
        return _mapper.Map<ProdutoDTO>(entity);
    }

    public async Task<ProdutoDTO> UpdateProduto(int id, ProdutoUpdateDTO produto)
    {
        var entity = await _context.Produtos.FindAsync(id);
        if (entity == null) throw new KeyNotFoundException("Produto não encontrado");

        _mapper.Map(produto, entity);
        entity.ProdutoId = id;
        _context.Produtos.Update(entity);

        return _mapper.Map<ProdutoDTO>(entity);
    }
}