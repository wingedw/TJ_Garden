using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Garden_API.Models
{
    [Table("Schedule")]
    public class Schedules
    {
        [Key]
        [Required]
        public int Schedule_Id { get; set; }

        [ForeignKey("Plot")]
        public int Plot_Id { get; set; }

        [ForeignKey("Events")]
        public int Event_Id { get; set; }

    }
}

