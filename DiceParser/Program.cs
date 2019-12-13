using System;

namespace DiceParser
{
    public class Program
    {

        private static Version version = new Version(1, 2, 0);
        private static bool isRunning = true;

        public static void Main(string[] args)
        {
            Interpreter interpreter = new Interpreter();
            
            bool verbose = isVerbose(args);

            Console.Title = "Dice Parser";
            Console.WriteLine($"Welcome to Dice Parser v. {version}.");
            Console.WriteLine("Enter expressions in algebraic form below (e.g.: 2d20 * (1 + 2)):");

            while (isRunning)
            {
                string input = Console.ReadLine();

                if (isCommand(input)) continue;

                try
                {
                    Console.WriteLine("> " + interpreter.Interpret(input));
                }
                catch (Exception e)
                {
                    if (verbose) Console.WriteLine($"Encountered error: {e.Message}");
                    else Console.WriteLine("Invalid expression.");
                }
            }
        }

        private static bool isVerbose(string[] args)
        {
            #if DEBUG
            return true;
            #endif

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].ToLower() == "-v") return true;
            }

            return false;
        }

        private static bool isCommand(string input)
        {
            input = input.ToLower().Trim();

            switch (input)
            {
                case "clear":
                    Console.Clear();
                    return true;
                case "exit":
                    isRunning = false;
                    return true;
                case "":
                    return true;
                default:
                    return false;
            }
        }
    }
}
