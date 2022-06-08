using AssetManagementSystem.Shared;

namespace AssetManagementSystem.Client.Services
{
    public interface IAssetService
    {
        List<Employee> Employees { get; set; }
        List<Asset> Assets { get; set; }
        Task GetEmployees();
        Task GetAssets();
        Task<Asset> GetAssetById(int assetId);
        Task CreateAsset(Asset asset);
        Task UpdateAsset(Asset asset);
        Task DeleteAsset(int assetId);
    }
}
