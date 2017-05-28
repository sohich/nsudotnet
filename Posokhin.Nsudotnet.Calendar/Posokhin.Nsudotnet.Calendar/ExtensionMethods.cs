using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posokhin.Nsudotnet.Calendar
{
    public static class ExtensionMethods
    {
        private static ConsoleColor _defaultBackgroundColor = ConsoleColor.White;
        private static ConsoleColor _currentDayBackgroundColor = ConsoleColor.Gray;
        private static ConsoleColor _selectedDayBackgroundColor = ConsoleColor.Blue;

        private static ConsoleColor _defaultForegroundColor = ConsoleColor.Black;
        private static ConsoleColor _weekendDayForegroundColor = ConsoleColor.Red;
        public static bool CheckWeekend(this DayOfWeek day)
        {
            return (day == DayOfWeek.Saturday)
                || (day == DayOfWeek.Sunday);
        }

        public static void SetConsoleBackgroundByDay(this DateTime day, DateTime selectedDay)
        {
                        ConsoleColor color;

            if(day.Day == selectedDay.Day)
            {
                color = _selectedDayBackgroundColor;
            }
            else
            {
                color = _defaultBackgroundColor;
            }
            if(day.Equals(DateTime.Today))
            {
                color = _currentDayBackgroundColor;
            }
            
            Console.BackgroundColor = color;
        }

        public static void SetConsoleForegroundByDay(this DayOfWeek day)
        {
            ConsoleColor color;

            if (day.CheckWeekend())
            {
                color = _weekendDayForegroundColor;
            }
            else
            {
                color = _defaultForegroundColor;
            }
            Console.ForegroundColor = color;
        }

        public static void SetBackgroundByDefault()
        {
            Console.BackgroundColor = _defaultBackgroundColor;
        }

        public static void SetForegroundByDefault()
        {
            Console.ForegroundColor = _defaultForegroundColor;
        }
    }
}
