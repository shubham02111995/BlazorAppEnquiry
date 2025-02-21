using System.ComponentModel.DataAnnotations;

namespace BlazorAppEnquiry.Models
{
    public class Enquiry
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string Surname { get; set; } = string.Empty;
        [Required]
        public string PostalAddress { get; set; } = string.Empty;
        [Required]
        public string EmailAddress { get; set; } = string.Empty;
        [Required]
        public string Details { get; set; } = string.Empty;
        public string Status { get; set; } = "Open";
        [Required]
        public string EnquirySubject { get; set; } = string.Empty;
    }
}
