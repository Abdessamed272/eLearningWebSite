using System.ComponentModel.DataAnnotations;

namespace eLearningAutomotiveWebSite.Models
{
    public partial class Category
    {
        [Key]
        public string Id { get; set; } 

        [Required]
        public String Name { get; set; }

    }
}