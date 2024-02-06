using System.Collections.Generic;

namespace Schedule.ViewModels
{
    public class RegisterViewModel
    {
        public string PIB { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public List<string> group_names { get; set;}
        public string group_name { get; set; }
        public int group_course { get; set; }
    }
}
