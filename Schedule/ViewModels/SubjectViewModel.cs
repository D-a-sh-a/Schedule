using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Schedule.ViewModels
{
    public class SubjectViewModel
    {
        [Key]
        public int id_subject { get; set; }
        public int id_lecturer { get; set; }
        public string subject_name { get; set; }
        public List<ViewModelsDictionary> Lecturers { get; set; }
    }
}
