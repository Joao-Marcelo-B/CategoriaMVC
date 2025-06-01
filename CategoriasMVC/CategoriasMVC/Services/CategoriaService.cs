using CategoriasMVC.Models;
using System.Text;
using System.Text.Json;

namespace CategoriasMVC.Services;

public class CategoriaService : ICategoriaService
{
    private const string apiEndpoint = "/api/1/categorias/";
    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _clientFactory;

    private CategoriaViewModel? categoriaVM;
    private IEnumerable<CategoriaViewModel>? categoriasVM;

    public CategoriaService(IHttpClientFactory clientFactory)
    {
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _clientFactory = clientFactory;
    }

    public HttpClient CreateClient(string name)
    {
        return _clientFactory.CreateClient(name);
    }

    public async Task<IEnumerable<CategoriaViewModel>?> GetCategorias()
    {
        var client = CreateClient("CategoriasApi");

        using var response = await client.GetAsync(apiEndpoint);
        if (!response.IsSuccessStatusCode)
            return null;

        var endpointResponse = await response.Content.ReadAsStreamAsync();

        categoriasVM = await JsonSerializer.DeserializeAsync<IEnumerable<CategoriaViewModel>>(endpointResponse, _options);

        return categoriasVM;
    }

    public async Task<CategoriaViewModel?> GetCategoriaById(int id)
    {
        var client = CreateClient("CategoriasApi");

        using var response = await client.GetAsync(apiEndpoint + id);
        if (!response.IsSuccessStatusCode)
            return null;

        var endpointResponse = await response.Content.ReadAsStreamAsync();

        categoriaVM = await JsonSerializer.DeserializeAsync<CategoriaViewModel>(endpointResponse, _options);

        return categoriaVM;
    }

    public async Task<CategoriaViewModel?> CreateCategoria(CategoriaViewModel categoria)
    {
        var client = _clientFactory.CreateClient("CategoriasApi");
        var content = new StringContent(JsonSerializer.Serialize(categoria), Encoding.UTF8, "application/json");

        using var response = await client.PostAsync(apiEndpoint, content);
        if (!response.IsSuccessStatusCode)
            return null;

        var endpointResponse = await response.Content.ReadAsStreamAsync();

        categoriaVM = await JsonSerializer.DeserializeAsync<CategoriaViewModel>(endpointResponse, _options);

        return categoriaVM;
    }

    public async Task<bool> UpdateCategoria(int id, CategoriaViewModel categoria)
    {
        var client = _clientFactory.CreateClient("CategoriasApi");

        using var response = await client.PutAsJsonAsync(apiEndpoint + id, categoria);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCategoria(int id)
    {
        var client = _clientFactory.CreateClient("CategoriasApi");

        using var response = await client.DeleteAsync(apiEndpoint + id);

        return response.IsSuccessStatusCode;
    }
}
