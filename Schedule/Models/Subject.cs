using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedule.Models
{
    public class Subject
    {
        [Key]
        public int id_subject { get; set; }
        public int id_lecturer { get; set; }
        public string subject_name { get; set; }
    }
}
