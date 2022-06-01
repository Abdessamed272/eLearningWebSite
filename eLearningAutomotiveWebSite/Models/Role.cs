using System.ComponentModel.DataAnnotations;

namespace eLearningAutomotiveWebSite.Models
{
    public partial class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool ReadText { get; set; }
        
        [Required]
        public bool ReadVideo { get; set; }
        
        [Required]
        public bool WriteContent { get; set; }
        
        [Required]
        public bool WriteUser { get; set; }

       

    }
}