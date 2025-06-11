using System.ComponentModel.DataAnnotations;

namespace CustomerTrainerService.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? FirstName { get; set; } 

        [Required]
        [MaxLength(100)]
        public string? SurName { get; set; } 
        [Required]
        [EmailAddress]
        public string? Email { get; set; } 
        [Required]
        [Phone]
        public string? Phone { get; set; } 

    }
}
