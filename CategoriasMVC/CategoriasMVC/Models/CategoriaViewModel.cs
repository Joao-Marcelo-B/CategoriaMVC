using System.ComponentModel.DataAnnotations;

namespace CategoriasMVC.Models;

public class CategoriaViewModel
{
    public int CategoriaId { get; set; }
    [Required(ErrorMessage = "O campo nome é obrigatório")]
    public string? Nome { get; set; }
    [Required]
    [Display(Name = "Imagem")]
    public string? ImagemUrl { get; set; }
}
