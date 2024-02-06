using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedule.Models
{
    public class MarksRow
    {
        public string StudentName { get; set; } = string.Empty;
        public List<string> StudentMarks { get; set; } = new List<string>();
    }
}
