using System;

namespace DiceParser
{
    class Program
    {

        private static Version version = new Version(1, 0, 0);
        private static bool isRunning = true;

        static void Main(string[] args)
        {
            Interpreter interpreter = new Interpreter();

            Console.Title = "Dice Parser";
            Console.WriteLine($"Welcome to Dice Parser v. {version}.");
            Console.WriteLine("Enter expressions in algebraic form below (e.g.: 2d20 * (1 + 2)):");

            while (isRunning)
            {
                string input = Console.ReadLine();

                if (input.ToLower() == "clear")
                {
                    Console.Clear();
                    continue;
                }
                else if (input.ToLower() == "exit")
                {
                    isRunning = false;
                    return;
                }
                else if (input.Trim() == "")
                {
                    continue;
                }

                try
                {
                    Console.WriteLine("> " + interpreter.Interpret(input));
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid expression.");
                }
            }
        }
    }
}
