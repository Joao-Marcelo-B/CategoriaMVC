using CategoriasMVC.Models;

namespace CategoriasMVC.Services;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoViewModel>> GetProdutos(string token);
    Task<ProdutoViewModel> GetProdutoPorId(int id, string token);
    Task<ProdutoViewModel> CriaProduto(ProdutoViewModel model, string token);
    Task<bool> Atualiza(int id, ProdutoViewModel mode, string token);
    Task<bool> DeletaProduto(int id, string token);
}