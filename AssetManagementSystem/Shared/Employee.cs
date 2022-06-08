using System.ComponentModel.DataAnnotations;

namespace AssetManagementSystem.Shared
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "IDNP is Required")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "This field must be 13 characters")]
        public string IDNP { get; set; }

        [MaxLength(30)]
        [Required(ErrorMessage = "FirstName is Required")]
        public string FirstName { get; set; }

        [MaxLength(30)]
        [Required(ErrorMessage = "LastName  is Required")]
        public string LastName { get; set; }

        [MaxLength(30)]
        [Required(ErrorMessage = "Email is Required")]

        public string Email { get; set; }
        [MaxLength(20)]
        [Required(ErrorMessage = "PersonalPhone  is Required")]
        public string PersonalPhone { get; set; }

    }
}
