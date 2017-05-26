using System;

namespace Posokhin.Nsudotnet.NumberGuesser
{
    class LogStorage
    {
        private int Capacity;
        private int CurrentOccupancy = 0;

        private LogRecord[] Storage;

        public LogStorage(int capacity)
        {
            this.Capacity = capacity;
            Storage = new LogRecord[Capacity];
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
                Console.WriteLine("Type: {0}\tValue: {1}\n", Storage[i].GetMissType(), Storage[i].GetValue());
            }
        }

        public int GetCapacity()
        {
            return Capacity;
        }

        public int getOccupancy()
        {
            return CurrentOccupancy;
        }
    }

}
