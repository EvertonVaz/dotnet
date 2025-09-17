using ApiCatalogo.Context;
using AutoMapper;

namespace ApiCatalogo.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IProdutoRepository? _produtoRepo;
    private ICategoriaRepository? _categoriaRepo;
    public AppDbContext _context;
    public IMapper _mapper;

    public UnitOfWork(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IProdutoRepository ProdutoRepository
    {
        get => _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context, _mapper);
    }

    public ICategoriaRepository CategoriaRepository
    {
        get => _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context, _mapper);
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}