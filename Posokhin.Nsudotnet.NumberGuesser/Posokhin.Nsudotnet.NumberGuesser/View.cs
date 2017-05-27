using System;

namespace Posokhin.Nsudotnet.NumberGuesser
{
    class View
    {
        private Handler Handler;

        public View(Handler handler)
        {
            this.Handler = handler;
        }
        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string GetMessage()
        {
            string message = Console.ReadLine();

            if (message == "q")
            {
                Handler.ProceedForceQuit();
            }
            return message;
        }
    }
}
