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
    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(UsuarioViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Login Inválido....");
            return View(model);
        }
        var result = await _autenticacao.AutenticaUsuario(model);


        if (result is null)
        {
            ModelState.AddModelError(string.Empty, "Login Inválido....");
            return View(model);
        }

        Response.Cookies.Append("X-Access-Token", result.Token!, new CookieOptions()
        {
            Secure = true,
            HttpOnly = true,
            SameSite = SameSiteMode.Strict
        });

        return Redirect("/");
    }
}
