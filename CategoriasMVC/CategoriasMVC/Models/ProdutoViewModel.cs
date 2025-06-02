using System.ComponentModel.DataAnnotations;

namespace CategoriasMVC.Models;

public class ProdutoViewModel
{
    public int ProdutoId { get; set; }
    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    public string? Nome { get; set; }
    [Required(ErrorMessage = "A descrição do produto é obrigatória.")]
    public string? Descricao { get; set; }
    [Required(ErrorMessage = "O preço do produto é obrigatório.")]
    public decimal Preco { get; set; }
    [Display(Name = "Caminho da imagem.")]
    public string? ImagemUrl { get; set; }
    [Display(Name = "Categoria do produto.")]
    public int CategoriaId { get; set; }
}
