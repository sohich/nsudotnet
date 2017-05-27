using System;

namespace Posokhin.Nsudotnet.NumberGuesser
{
    class LogRecord
    {
        public int Value { get; private set; }
        public AnswerType MissType { get; private set; }

        public LogRecord(int value, AnswerType type)
        {
            this.Value = value;
            this.MissType = type;
        }

    }
}
