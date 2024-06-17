using FatecSisMed.Web.Models;
using FatecSisMed.Web.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FatecSisMed.Web.Services.Entities
{
    public class MarcaService : IMarcaService    
    {

        private readonly IHttpClientFactory _clientFactory;

        public MarcaService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true };
        }

        private readonly JsonSerializerOptions _options;
        private const string apiEndpoint = "/api/marca/";
        private MarcaViewModel _marcaViewModel;
        private IEnumerable<MarcaViewModel> marcas;

        public async Task<IEnumerable<MarcaViewModel>> GetAllMarcas(string token)
        {
            var client = _clientFactory.CreateClient("MedicoAPI");
            PutTokenInHeaderAuthorization(token, client);

            var response = await client.GetAsync(apiEndpoint);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                marcas = await JsonSerializer
                    .DeserializeAsync<IEnumerable
                    <MarcaViewModel>>(apiResponse, _options);
            }
            else
                return Enumerable.Empty<MarcaViewModel>();

            return marcas;
        }

        public async Task<MarcaViewModel> FindMarcaById(int id, string token)
        {
            var client = _clientFactory.CreateClient("MedicoAPI");
            PutTokenInHeaderAuthorization(token, client);

            using (var response = await client.GetAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode && response.Content is not null)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _marcaViewModel = await JsonSerializer
                        .DeserializeAsync<MarcaViewModel>(apiResponse, _options);
                }
                else
                    return null;
            }
            return _marcaViewModel;
        }

        public async Task<MarcaViewModel>
            CreateMarca(MarcaViewModel marcaViewModel, string token)
        {
            var client = _clientFactory.CreateClient("MedicoAPI");
            PutTokenInHeaderAuthorization(token, client);

            StringContent content =
                new StringContent(JsonSerializer.Serialize(marcaViewModel),
                Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _marcaViewModel = await JsonSerializer
                        .DeserializeAsync<MarcaViewModel>(apiResponse, _options);
                }
                else
                    return null;
            }
            return _marcaViewModel;
        }

        public async Task<MarcaViewModel>
            UpdateMarca(MarcaViewModel marcaViewModel, string token)
        {
            var client = _clientFactory.CreateClient("MedicoAPI");
            PutTokenInHeaderAuthorization(token, client);

            MarcaViewModel marca = new MarcaViewModel();

            using (var response = await
                client.PutAsJsonAsync(apiEndpoint, marcaViewModel))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    marca = await JsonSerializer
                        .DeserializeAsync<MarcaViewModel>(apiResponse, _options);
                }
                else
                    return null;
            }
            return marca;
        }

        public async Task<bool> DeleteMarcaById(int id, string token)
        {
            var client = _clientFactory.CreateClient("MedicoAPI");
            PutTokenInHeaderAuthorization(token, client);

            using (var response = await client.DeleteAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode) return true;
            }
            return false;
        }

        private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}

