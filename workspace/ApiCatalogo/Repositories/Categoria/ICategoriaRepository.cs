using ApiCatalogo.DTO;
using ApiCatalogo.Models;

namespace ApiCatalogo.Repositories;


public interface ICategoriaRepository
{
    Task<IEnumerable<CategoriaDTO>> GetCategorias();
    Task<CategoriaDTO> GetCategoriaById(int id);
    Task<IEnumerable<CategoriaDTO>> GetCategoriasProdutos();
    Task<CategoriaDTO> CreateCategoria(CategoriaCreateDTO categoria);
    Task<CategoriaDTO> UpdateCategoria(int id, CategoriaUpdateDTO categoria);
    Task<CategoriaDTO> DeleteCategoria(int id);
}
