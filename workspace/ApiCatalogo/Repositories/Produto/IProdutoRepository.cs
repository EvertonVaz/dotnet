using ApiCatalogo.DTO;
using ApiCatalogo.Models;

namespace ApiCatalogo.Repositories;

public interface IProdutoRepository
{
    Task<IEnumerable<ProdutoDTO>> GetProdutos();
    Task<ProdutoDTO> GetProdutoById(int id);
    Task<IEnumerable<ProdutoDTO>> GetProdutosCategoria(int categoriaId);
    Task<ProdutoDTO> CreateProduto(ProdutoCreateDTO produto);
    Task<ProdutoDTO> UpdateProduto(int id, ProdutoUpdateDTO produto);
    Task<ProdutoDTO> DeleteProduto(int id);
}