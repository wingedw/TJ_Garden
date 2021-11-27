using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Garden_API.Models
{
    [Table("Plants")]
    public class PlantDetails
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [StringLength(50)]
        public string  Plant_Type { get; set; }

        [StringLength(50)]
        public string Common_Name { get; set; }

        [StringLength(50)]
        public string Scientific_Name { get; set; }
                
        public int Sprout_Min { get; set; }

        public int Sprout_Max { get; set; }

        public int Mature_Time { get; set; }

        public int Spacing { get; set; }

        public string Description { get; set; }






    }
}
