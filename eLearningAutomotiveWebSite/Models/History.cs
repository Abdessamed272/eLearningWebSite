using System.ComponentModel.DataAnnotations;

namespace eLearningAutomotiveWebSite.Models
{
    public partial class History
    {
        [Key]
        public int Id { get; set; }
        //testtests
        public DateTime VisitDate { get; set; }


        [Required]
        [MaxLength(450)]
        public string IdUser { get; set; }

        [Required]
        public int IdContent { get; set; }


    }
}