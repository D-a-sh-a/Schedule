using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedule.Models
{
    public class Mark
    {
        [Key]
        public int id_mark { get; set; }
        public int id_student { get; set; }
        public int id_subject { get; set; }
        public int id_group { get; set; }
        public double mark { get; set; }
        public string mark_sort { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM}", ApplyFormatInEditMode = true)]
        public DateTime mark_date { get; set; }
    }
}
