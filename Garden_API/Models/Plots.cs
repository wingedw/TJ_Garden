using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Garden_API.Models
{
    [Table("Plots")]
	public class Plots
    {
		[Key]
		[Required]
		public int Plot_Id { get; set; }

		[ForeignKey("PlantDetails")]
		public int Plant_Id { get; set; }

		public int X_Location { get; set; }

		public int Y_Location { get; set; }

		public string? Secret { get; set; }

	}
	//subset of a model is usually referred to as a Data Transfer Object (DTO), input model, or view model
	public class PlotsDTO
	{
		[Key]
		[Required]
		public int Plot_Id { get; set; }

		[ForeignKey("PlantDetails")]
		public int Plant_Id { get; set; }

		public int X_Location { get; set; }

		public int Y_Location { get; set; }
	}
}


