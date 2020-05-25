using System;

namespace CAB301_Assignment.Views
{
    class ErrorView
    {
        public static void Error(Exception e)
        {
            Error(e.Message);
        }

        public static void Error(string msg)
        {
            Console.Clear();
            Console.Beep();
            Console.Write("========== Error ==========\n" +
                          "An error occurred when processing " +
                          "your request.\n\n" + msg + "\n\n" +
                          "Press any key to try again.");
            Console.ReadKey();
        }
    }
}
