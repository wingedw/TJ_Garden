using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Garden_API.Models
{
    [Table("Users")]
	public class Users
    {
		[Key]
		[Required]
		public int User_Id { get; set; }

		public string First_Name { get; set; }

		public string Last_Name { get; set; }

		public string Email { get; set; }
	}
}

