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

		public int x_Location { get; set; }

		public int y_Location { get; set; }
	}
}


