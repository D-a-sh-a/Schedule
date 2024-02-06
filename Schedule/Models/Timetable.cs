using System;
using System.ComponentModel.DataAnnotations;

namespace Schedule.Models
{
    public class Timetable
    {
        [Key]
        public int id_timetable { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime start_time { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime end_time { get; set; }

    }
}
