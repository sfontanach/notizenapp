using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace notizenapp.Models
{
    public class Note
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set;}

        [Required]
        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Title { get; set;}

		[Required]
		public string Text { get; set; }

		[Required]
        [Range(1, 5)]
		public int Importance { get; set; }

        [Required]
		[Display(Name = "Finish Date")]
		[DataType(DataType.Date)]
		public DateTime FinishDate { get; set; }

        public bool Finished { get; set; }

    }
}
