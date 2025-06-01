using CategoriasMVC.Models;
using CategoriasMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CategoriasMVC.Controllers;

public class AccountController : Controller
{
    private readonly IAutenticacao _autenticacao;

    public AccountController(IAutenticacao autenticacao)
    {
        _autenticacao = autenticacao;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UsuarioViewModel usuarioViewModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Login Inválido...");
            return View(usuarioViewModel);
        }

        var result = await _autenticacao.AutenticaUsuario(usuarioViewModel);
        if (result is null)
        {
            return BadRequest("Teste");
        }
    }
}
