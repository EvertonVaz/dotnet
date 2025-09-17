using ApiCatalogo.Models;
using AutoMapper;

namespace ApiCatalogo.DTO;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<Produto, ProdutoDTO>().ReverseMap();
        CreateMap<Produto, ProdutoCreateDTO>().ReverseMap();
        CreateMap<Produto, ProdutoUpdateDTO>().ReverseMap();
        CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        CreateMap<Categoria, CategoriaCreateDTO>().ReverseMap();
        CreateMap<Categoria, CategoriaUpdateDTO>().ReverseMap();
    }
}