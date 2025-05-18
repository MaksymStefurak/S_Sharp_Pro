using System.ComponentModel.DataAnnotations;

namespace WebAppTestMVC.Models
{
    public class Contact
    {
        [Key]
        public long Id { get; set; }

        [Display(Name = "Employee Name")]
        [Required]
        [MaxLength(100)]
        public string? Name {  get; set; }
        [Required]
        [Phone]
        [MaxLength(50)]
        public string? Phone {  get; set; }

        [Phone]
        [MaxLength(50)]
        public string? AdditionalPhone {  get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string? Email {  get; set; }

        [MaxLength(300)]
        public string? Description {  get; set; }

    }
}
