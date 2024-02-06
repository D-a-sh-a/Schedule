using System.Collections.Generic;

namespace Schedule.ViewModels
{
    public class SheduleDayViewModel
    {
        public string day { get; set; }
        //public string date { get; set; }
        public List<ViewModelsDictionary> subjectNames { get; set; }       
        public List<ViewModelsDictionary> subjectLecturerNames { get; set; }    
    }
}
