using System;
using System.Globalization;

namespace Posokhin.Nsudotnet.Calendar
{
    class Program
    {
        private DateTime _selectedDay;

        private const ConsoleColor _defaultBackgroundColor = ConsoleColor.White;
        private const ConsoleColor _currentDayBackgroundColor = ConsoleColor.Gray;
        private const ConsoleColor _selectedDayBackgroundColor = ConsoleColor.Blue;

        private const ConsoleColor _defaultForegroundColor = ConsoleColor.Black;
        private const ConsoleColor _weekendDayForegroundColor = ConsoleColor.Red;


        static void Main(string[] args)
        {
            new Program().DrawCalendar();

        }

        private void DrawCalendar()
        {
            Console.WriteLine("Please enter the date to dislpay: ");
            string dateUnparsed = Console.ReadLine();

            if(!DateTime.TryParse(dateUnparsed, out _selectedDay))
            {
                Console.WriteLine("Invalid date. Closing :/");
                System.Environment.Exit(0);
            }

            DrawHeader();
            DrawDates();
        }

        private void DrawHeader()
        {
            string[] dayNames = new CultureInfo("en-US").DateTimeFormat.AbbreviatedDayNames;
            Console.BackgroundColor = _defaultBackgroundColor;

            for(int i = 0; i <7; ++i)
            {
                SetConsoleForegroundByDay((DayOfWeek)i);
                Console.Write("{0,4}", dayNames[i]);
            }
            Console.WriteLine();
        }

        private void DrawDates()
        {
            // filling empty beginning space

            DateTime firstDayOfMonth = new DateTime(_selectedDay.Year, _selectedDay.Month, 1);
            Console.BackgroundColor = _defaultBackgroundColor;
            for(int i = 0; i < (int)firstDayOfMonth.DayOfWeek; ++i)
            {
                Console.Write("{0,4}", string.Empty);
            }

            for (DateTime day = firstDayOfMonth; day.Month == firstDayOfMonth.Month; day = day.AddDays(1))
            {
                SetConsoleForegroundByDay(day.DayOfWeek);
                SetConsoleBackgroundByDay(day);
                Console.Write("{0,4}", day.Day);

                if(day.DayOfWeek == DayOfWeek.Saturday)
                {
                    Console.WriteLine();
                }
            }
            Console.BackgroundColor = _defaultForegroundColor;
            Console.ForegroundColor = _defaultBackgroundColor;
            Console.WriteLine();
        }

        private void SetConsoleBackgroundByDay(DateTime day)
        {
            ConsoleColor color;

            if(day.Day == _selectedDay.Day)
            {
                color = _selectedDayBackgroundColor;
            }
            else if(day.Equals(DateTime.Today))
            {
                color = _currentDayBackgroundColor;
            }
            else
            {
                color = _defaultBackgroundColor;
            }
            Console.BackgroundColor = color;
        }

        private void SetConsoleForegroundByDay(DayOfWeek day)
        {
            ConsoleColor color;

            if (CheckWeekend(day))
            {
                color = _weekendDayForegroundColor;
            }
            else
            {
                color = _defaultForegroundColor;
            }
            Console.ForegroundColor = color;
        }

        private bool CheckWeekend(DayOfWeek day)
        {
            return (day == DayOfWeek.Saturday)
                || (day == DayOfWeek.Sunday);
        }
    }
}
