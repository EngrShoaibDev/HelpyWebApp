using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HelpyWebApp.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Gender { get; set; }

        [Required]
        [StringLength(255)]
        public string Sexuality { get; set; }

        [Required]
        [StringLength(255)]
        public string Ethnicity { get; set; }

        [Required]
        [StringLength(255)]
        public string Occupation { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Town { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [Required]
        [StringLength(100)]
        public string Phone { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; }

        public bool IsActive { get; set; }

        public int SubscriptionId { get; set; }

        public DateTime ExpireDate { get; set; }

        public DateTime PackageRenewalDate { get; set; }

        public DateTime UserLastLogin { get; set; }

        public int? ProfileImageId { get; set; }
    }
}
