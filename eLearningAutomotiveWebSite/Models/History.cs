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
        public int IdUser { get; set; }

        [Required]
        public int IdContent { get; set; }


    }
}