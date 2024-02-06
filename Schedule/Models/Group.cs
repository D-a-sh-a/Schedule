using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Schedule.Models
{
    public class Group
    {
        [Key]
        public int id_group { get; set; }
        public string group_name { get; set; }
        public int group_course { get; set; }
        public string group_specialty { get; set; }
        public string group_specialty_id { get; set; }

    }
}
