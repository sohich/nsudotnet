using System;

namespace Posokhin.Nsudotnet.NumberGuesser
{
    class LogStorage
    {
        public int Capacity { get; private set; }
        public int CurrentOccupancy { get; private set; }

        private LogRecord[] Storage;

        public LogStorage(int capacity)
        {
            this.Capacity = capacity;
            Storage = new LogRecord[Capacity];
            CurrentOccupancy = 0;
        }

        public void AddRecord(LogRecord record)
        {
            if (CurrentOccupancy == Capacity)
            {
                throw new System.Exception("Log storage is full");
            }

            Storage[CurrentOccupancy] = record;
            ++CurrentOccupancy;
        }

        public void PrintLog()
        {
            if (CurrentOccupancy == 0)
            {
                Console.WriteLine("Log is empty.");
                return;
            }

            for (int i = 0; i < CurrentOccupancy; ++i)
            {
                Console.WriteLine("Type: {0}\tValue: {1}\n", Storage[i].MissType, Storage[i].Value);
            }
        }
    }

}
