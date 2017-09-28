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
        public string Title { get; set;}


		[Required]
		public string Text { get; set; }

		[Required]
		public int Importance { get; set; }


		[Display(Name = "Finish Date")]
		[DataType(DataType.Date)]
		public DateTime FinishDate { get; set; }

        public int Status { get; set; }

    }
}
