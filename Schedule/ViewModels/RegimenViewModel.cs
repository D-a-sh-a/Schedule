using System;
using System.Collections.Generic;

namespace Schedule.ViewModels
{
    public class RegimenViewModel
    {
        public List<SheduleDayViewModel> days{get; set;}          
        public List<string> timetable{get; set;}
        public void SortDays()
        {
            var sortedList = new List<SheduleDayViewModel>();
            var counter = 0;
            DayOfWeek currentDay = DayOfWeek.Monday;
            while (days.Count > 0 && counter < 1000)
            {
                for (int i = 0; i < days.Count; i++)
                {
                    if (Enum.TryParse(days[i].day, out DayOfWeek dayOfWeek))
                    {
                        if (dayOfWeek == currentDay)
                        {
                            sortedList.Add(days[i]);
                            days.Remove(days[i]);
                            i--;
                        }
                    }
                }
                currentDay = (DayOfWeek)(((int)currentDay + 1) % 7);
                counter++;

            }
            days = sortedList;

        }
    }
}
