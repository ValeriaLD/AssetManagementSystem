using AssetManagementSystem.Server.Data;
using AssetManagementSystem.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly ApplicationDbContext _appDbContext;

        public AssetController(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Asset>>> GetAssets()
        {
            var assets = await _appDbContext.Assets
                .Include(a => a.Employee)
                .ToListAsync();
            return Ok(assets);
        }

        [HttpGet("employees")]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            var employees = await _appDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpGet("GetEmployeeById")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = _appDbContext.Employees
                .FirstOrDefault(employee => employee.Id.Equals(id));
            return Ok(employee);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAssetById(int id)
        {
            var asset = _appDbContext.Assets
                .Include(a => a.Employee)
                .FirstOrDefault(asset => asset.Id.Equals(id));
            if(asset == null) return NotFound("No assets yet!");
            return Ok(asset);
        }

        [HttpPost]
        public async Task<ActionResult<List<Asset>>> CreateAsset(Asset asset)
        {
            asset.Employee = null;

            _appDbContext.Assets.Add(asset);
            await _appDbContext.SaveChangesAsync();

            return Ok(await GetDbAssets());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Asset>>> UpdateAsset(Asset asset, int id)
        {
            var dbAsset = await _appDbContext.Assets
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (dbAsset == null) return NotFound("Asset not found!");

            dbAsset.Name = asset.Name;
            dbAsset.SerialNumber = asset.SerialNumber;
            dbAsset.Description = asset.Description;
            dbAsset.EmployeeId = asset.EmployeeId;
            dbAsset.Status = asset.Status;

            await _appDbContext.SaveChangesAsync();

            return Ok(await GetDbAssets());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Asset>>> DeleteAsset(int id)
        {
            var dbAsset = await _appDbContext.Assets
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (dbAsset == null) return NotFound("Asset not found!");

            _appDbContext.Assets.Remove(dbAsset);
            await _appDbContext.SaveChangesAsync();

            return Ok(await GetDbAssets());
        }

        private async Task<List<Asset>> GetDbAssets()
        {
            return await _appDbContext.Assets.ToListAsync();
        }

        [HttpGet("serialNumber")]
        public IActionResult CheckSerialNumber(string serialNumber)
        {
            var asset = _appDbContext.Assets.Where(e => e.SerialNumber == serialNumber).FirstOrDefault();

            if (asset == null) return NotFound("Asset with this serial number not found");
            return Ok("Exist");
        }
    }
}
