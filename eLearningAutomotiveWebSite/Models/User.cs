namespace eLearningAutomotiveWebSite.Models
{
using System.ComponentModel.DataAnnotations;

public partial class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Mail { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int IdRole { get; set; }
        
       

        

    }
}