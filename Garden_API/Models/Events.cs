using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Garden_API.Models
{
    [Table("Events")]
    public class Events
    {
        [Key]
        [Required]
        public int Event_Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }
}
