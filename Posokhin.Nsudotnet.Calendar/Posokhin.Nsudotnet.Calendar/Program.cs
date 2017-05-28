using System;
using System.Globalization;

namespace Posokhin.Nsudotnet.Calendar
{
    class Program
    {
        private DateTime _selectedDay;


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
            ExtensionMethods.SetBackgroundByDefault();

            for(int i = 0; i <7; ++i)
            {
                ((DayOfWeek)i).SetConsoleForegroundByDay();
                Console.Write("{0,4}", dayNames[i]);
            }
            Console.WriteLine();
        }

        private void DrawDates()
        {
            // filling empty beginning space

            DateTime firstDayOfMonth = new DateTime(_selectedDay.Year, _selectedDay.Month, 1);
            ExtensionMethods.SetBackgroundByDefault();
            for(int i = 0; i < (int)firstDayOfMonth.DayOfWeek; ++i)
            {
                Console.Write("{0,4}", string.Empty);
            }

            for (DateTime day = firstDayOfMonth; day.Month == firstDayOfMonth.Month; day = day.AddDays(1))
            {
                day.DayOfWeek.SetConsoleForegroundByDay();
                day.SetConsoleBackgroundByDay(_selectedDay);
                Console.Write("{0,4}", day.Day);

                if(day.DayOfWeek == DayOfWeek.Saturday)
                {
                    Console.WriteLine();
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

      

    }
}
