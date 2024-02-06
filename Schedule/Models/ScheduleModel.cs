using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedule.Models
{
    public class ScheduleModel
    {
        public int Id { get; set; }
        public string day { get; set; }
        public int id_timetable { get; set; }
        public int id_group { get; set; }
        public int id_subject { get; set; }
    }
}
