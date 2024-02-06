using System;

namespace Schedule.Enums
{ 
	public class DaysEnum
	{
		public static DaysEnum Sunday => new DaysEnum(DayOfWeek.Sunday, "Неділя");
		public static DaysEnum Monday => new DaysEnum(DayOfWeek.Monday, "Понеділок");
		public static DaysEnum Tuesday => new DaysEnum(DayOfWeek.Tuesday, "Вівторок");
		public static DaysEnum Wednesday => new DaysEnum(DayOfWeek.Wednesday, "Середа");
		public static DaysEnum Thursday => new DaysEnum(DayOfWeek.Thursday, "Четверг");
		public static DaysEnum Friday => new DaysEnum(DayOfWeek.Friday, "П`ятниця");
		public static DaysEnum Saturday => new DaysEnum(DayOfWeek.Saturday, "Субота");
		public DayOfWeek DayOfWeek { get; }
		public string DayName { get; }

		private DaysEnum(DayOfWeek dayOfWeek, string dayName)
		{
			DayOfWeek = dayOfWeek;
			DayName = dayName;
		}

		public static string GetDayName(string dayOfWeek)
		{
			if (Enum.TryParse<DayOfWeek>(dayOfWeek, true, out DayOfWeek parsedDay))
			{
				switch (parsedDay)
				{
					case DayOfWeek.Sunday:
						return "Неділя";
					case DayOfWeek.Monday:
						return "Понеділок";
					case DayOfWeek.Tuesday:
						return "Вівторок";
					case DayOfWeek.Wednesday:
						return "Середа";
					case DayOfWeek.Thursday:
						return "Четверг";
					case DayOfWeek.Friday:
						return "П`ятниця";
					case DayOfWeek.Saturday:
						return "Субота";
					default:
						throw new ArgumentOutOfRangeException(nameof(parsedDay), parsedDay, null);
				}
			}
			else
			{
				throw new ArgumentException("Invalid day name", nameof(dayOfWeek));
			}
		}
	}
}
