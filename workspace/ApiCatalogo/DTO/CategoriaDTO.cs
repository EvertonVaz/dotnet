namespace ApiCatalogo.DTO;

public class CategoriaDTO
{
    public int CategoriaId { get; set; }
    public string? Nome { get; set; }
    public string? ImagemUrl { get; set; }
}

public class CategoriaCreateDTO
{
    public string? Nome { get; set; }
    public string? ImagemUrl { get; set; }
}

public class CategoriaUpdateDTO
{
    public string? Nome { get; set; }
}