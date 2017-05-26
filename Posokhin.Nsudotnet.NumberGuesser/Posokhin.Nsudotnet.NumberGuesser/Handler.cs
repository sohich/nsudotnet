using System;
using System.Diagnostics;


namespace Posokhin.Nsudotnet.NumberGuesser
{
    class Handler
    {
        private Game Model;
        private View View;
        private LogStorage Log;
        private Random Random = new Random();
        private int MissCount = 0;
        Stopwatch stopwatch = new Stopwatch();
        private string PlayerName;
        private const int MinimalValue = 0;
        private const int MaximalValue = 100;
        private const int AffrontEquality = 4;
        private const int StorageCapacity = 1000;

        private const string NameRequestMessage = "Hello there! What's your name?";
        private const string NumberRequestMessage = "Enter the number: ";
        private const string BadInputMessage = "Rly? Number. From {1}. To {2}.. U silly, {0}?\n";
        private const string GreetingsMessage = "I guessed the number from {1} to {2}.\nShow me what you got, {0}\n";
        private const string CorrectAnswerMessage = "Well done, {0}! It took {1} minute(s) for {2} tries!\nHere are results:\n";
        private const string Goodbye = "Sorry that it was too hard for you! Gotta go cry to mama?";
        private string[] AffrontationsMessage = 
        {
            "Trying like a lil' bitch!!\n",
            "Don't give up, silly faggot!\n",
            "Aww, so tired.. Go fuck yourself\n",
            "I will hit you against the wall if you ain't gonna do it with your own head..\n",
            "No idea? Try number of times when ur ass has been penetrated\n"
        };


        public Handler()
        {
            Model = new Game(MinimalValue, MaximalValue);
            View = new View(this);
            Log = new LogStorage(StorageCapacity);
        }
        public void StartGame()
        {
            stopwatch.Start();
            SayHello();

            int currentNumber;
            AnswerType result = AnswerType.Unknown;
            do
            {
                try
                {
                    currentNumber = RequestNumber();
                }
                catch (Exception e)
                {
                    ProceedBadInput();
                    continue;
                }
                result = Model.CheckAnswer(currentNumber);
                HandleAnswer(result, currentNumber);
            }
            while (result != AnswerType.Correct);
        }

        public void ProceedForceQuit()
        {
            View.PrintMessage(Goodbye);
            System.Environment.Exit(0);
        }

        private void ProceedBadInput()
        {
            View.PrintMessage(string.Format(BadInputMessage, PlayerName, MinimalValue, MaximalValue));
        }

        private void ProceedMissAnswer(AnswerType answer, int number)
        {
            if (answer != AnswerType.Under && answer != AnswerType.Over)
            {
                return;
            }

            View.PrintMessage(string.Format("{0:G}\n", answer));
            Log.AddRecord(new LogRecord(number, answer));
        }

        private void ProceedCorrectAnswer(int number)
        {
            stopwatch.Stop();
            TimeSpan time = stopwatch.Elapsed;
            Log.AddRecord(new LogRecord(number, AnswerType.Correct));

            Console.WriteLine(CorrectAnswerMessage, PlayerName, time.Minutes, MissCount + 1);
            Log.PrintLog();
        }

        private void SayHello()
        {
            View.PrintMessage(NameRequestMessage);
            PlayerName = View.GetMessage();
            View.PrintMessage(string.Format(GreetingsMessage, PlayerName, MinimalValue, MaximalValue));
        }

        private void HandleAnswer(AnswerType answer, int number)
        {
            switch (answer)
            {
                case AnswerType.Under:
                case AnswerType.Over:
                    ProceedMissAnswer(answer, number);
                    CheckAffront();
                    break;
                case AnswerType.OutOfRange:
                    ProceedBadInput();
                    CheckAffront();
                    break;
                case AnswerType.Correct:
                    ProceedCorrectAnswer(number);
                    break;
            }
        }


        private void CheckAffront()
        {
            if (++MissCount % AffrontEquality == 0)
            {
                int MessageNumber = Random.Next(0, AffrontationsMessage.Length);
                Console.WriteLine(AffrontationsMessage[MessageNumber]);
            }
        }
        private int RequestNumber()
        {
            View.PrintMessage(NumberRequestMessage);
            return Int32.Parse(View.GetMessage());
        }
    }

}
