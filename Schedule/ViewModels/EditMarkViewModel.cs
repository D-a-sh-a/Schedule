using Microsoft.AspNetCore.Identity;
using Schedule.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Schedule.ViewModels
{
    public class EditMarkViewModel
    {
        public int groupId { get ; set; }
        public int subjectId { get ; set; }
        public List<string> RowNames { get; set; }

        public List<MarksRow> Rows { get; set; }

        public List<string> ChangedNames { get; set; } = new List<string>();
        public List<string> ChangedDates { get; set; } = new List<string>();
        public List<string> ChangedMarks { get; set; } = new List<string>();

        public List<ViewModelsDictionary> Subjects { get; set; }
        public List<ViewModelsDictionary> Groups { get; set; }

    }
}
