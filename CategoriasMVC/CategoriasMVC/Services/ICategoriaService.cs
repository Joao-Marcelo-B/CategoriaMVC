using CategoriasMVC.Models;

namespace CategoriasMVC.Services;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaViewModel>?> GetCategorias();
    Task<CategoriaViewModel?> GetCategoriaById(int id);  
    Task<CategoriaViewModel?> CreateCategoria(CategoriaViewModel categoria);
    Task<bool> UpdateCategoria(int id, CategoriaViewModel categoria);
    Task<bool> DeleteCategoria(int id);
}
