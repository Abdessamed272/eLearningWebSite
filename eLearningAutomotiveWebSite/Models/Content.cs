using System.ComponentModel.DataAnnotations;

namespace eLearningAutomotiveWebSite.Models
{
    public partial class Content
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Text { get; set; }

        public string? Video { get; set; }
        public bool DejaVu { get; set; }

        [Required]
        //[Display(Name = "xxx")] pour donner un exemple de red�finition du nom lu par DisplayNameFor
        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        [Required]
        public int IdCategory { get; set; }

    }
}