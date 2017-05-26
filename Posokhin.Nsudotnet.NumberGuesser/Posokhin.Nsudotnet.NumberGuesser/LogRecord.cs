using System;

namespace Posokhin.Nsudotnet.NumberGuesser
{
    class LogRecord
    {
        private int Value;
        private AnswerType MissType;

        public LogRecord(int value, AnswerType type)
        {
            this.Value = value;
            this.MissType = type;
        }

        public int GetValue()
        {
            return Value;
        }

        public AnswerType GetMissType()
        {
            return MissType;
        }
    }
}
