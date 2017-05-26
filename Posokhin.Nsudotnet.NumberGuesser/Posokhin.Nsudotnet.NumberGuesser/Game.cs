using System;

namespace Posokhin.Nsudotnet.NumberGuesser
{  
    class Game
    {
        private int GuessedNumber;
        private int MinimalValue;
        private int MaximalValue;

        public Game(int min, int max)
        {
            this.MinimalValue = min;
            this.MaximalValue = max;
            Random random = new Random();
            GuessedNumber = random.Next(MinimalValue, MaximalValue + 1);
        }

        public AnswerType CheckAnswer(int value)
        {
            AnswerType result;

            if (value < MinimalValue || value > MaximalValue)
            {
                result = AnswerType.OutOfRange;
            }
            else if (value < GuessedNumber)
            {
                result = AnswerType.Under;
            }
            else if (value > GuessedNumber)
            {
                result = AnswerType.Over;
            }
            else
            {
                result = AnswerType.Correct;
            }

            return result;
        }
    }

}
