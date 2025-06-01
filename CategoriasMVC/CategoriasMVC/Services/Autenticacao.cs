using CategoriasMVC.Models;
using System.Text;
using System.Text.Json;

namespace CategoriasMVC.Services;

public class Autenticacao : IAutenticacao
{
    private readonly IHttpClientFactory _clientFactory;
    const string endpointAutentica = "/api/autoriza/login/";
    private readonly JsonSerializerOptions _options;
    private TokenViewModel? tokenUsuario;

    public Autenticacao(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<TokenViewModel?> AutenticaUsuario(UsuarioViewModel usuarioViewModel)
    {
        var client = _clientFactory.CreateClient("AutenticaApi");

        var usuario = JsonSerializer.Serialize(usuarioViewModel, _options);
        StringContent content = new StringContent(usuario, Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(endpointAutentica, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStreamAsync();
                tokenUsuario = await JsonSerializer.DeserializeAsync<TokenViewModel>(responseContent, _options);
            }
            else
            {
                tokenUsuario = new TokenViewModel
                {
                    Authenticated = false,
                    Message = "Usuário ou senha inválidos."
                };
            }

            return tokenUsuario;
        }
    }
}
