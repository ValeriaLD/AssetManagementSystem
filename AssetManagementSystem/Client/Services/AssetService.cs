using AssetManagementSystem.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace AssetManagementSystem.Client.Services
{
    public class AssetService : IAssetService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public AssetService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public List<Employee> Employees { get; set; }
        public List<Asset> Assets { get; set; }

        public async Task CreateAsset(Asset asset)
        {
            var result = await _httpClient.PostAsJsonAsync("api/asset", asset);
            SetAssets(result);
        }

        public async Task DeleteAsset(int assetId)
        {
            var result = await _httpClient.DeleteAsync($"api/asset/{assetId}");
            SetAssets(result);
        }

        public async Task<Asset> GetAssetById(int assetId)
        {
            var result = await _httpClient.GetFromJsonAsync<Asset>($"api/asset/{assetId}");
            if (result != null)
                return result;
            throw new Exception("Asset not found!");
        }

        public async Task GetAssets()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Asset>>("api/asset");
            if (result != null) Assets = result;
        }

        public async Task GetEmployees()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Employee>>("api/asset/employees");
            if (result != null) Employees = result;
        }

        public async Task UpdateAsset(Asset asset)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/asset/{asset.Id}", asset);
            SetAssets(result);
        }

        private async Task SetAssets(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Asset>>();
            Assets = response;
            _navigationManager.NavigateTo("assets");
        }
    }
}
