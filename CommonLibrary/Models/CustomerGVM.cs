using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.Models
{
    public class CustomerGVM
    {
        public int customerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }

        [MaxLength(10, ErrorMessage = "Invalid Mobile Number")]
        public string MobileNumber { get; set; }
        public string City { get; set; }
        public bool IsActive { get; set; }

    }
}
