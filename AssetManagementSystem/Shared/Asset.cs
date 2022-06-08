using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Shared
{
    public class Asset
    {
        public int Id { get; set; }

        [Required, MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [Required, MinLength(6), MaxLength(8)]
        public string SerialNumber { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public Employee? Employee { get; set; }
        public int EmployeeId { get; set; }

        public AssetStatus Status { get; set; }
    }
}
