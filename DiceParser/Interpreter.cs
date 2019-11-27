using System;
using System.Text.RegularExpressions;

namespace DiceParser
{
    public class Interpreter
    {

        private Random rand = new Random();

        public int Interpret(string data)
        {
            Tokenizer tk = new Tokenizer();
            string[] tokens = tk.Tokenize(data.ToLower());

            for (int i = 0; i < tokens.Length; i++)
            {
                if (Regex.IsMatch(tokens[i], "[0-9]+d[0-9]+"))
                {
                    tokens[i] = parseDice(tokens[i]);
                }
                else if (Regex.IsMatch(tokens[i], "d[0-9]+"))
                {
                    tokens[i] = parseDice('1' + tokens[i]);
                }
            }

            SyntaxNode root = tk.ToAST(tokens);

            return root.Evaluate();
        }

        private string parseDice(string input)
        {
            string[] tokens = input.Split('d');
            int amount = int.Parse(tokens[0]);
            int maxValue = int.Parse(tokens[1]) + 1;
            int total = 0;

            for (int i = 0; i < amount; i++)
            {
                total += rand.Next(1, maxValue);
            }

            return total.ToString();
        }
    }
}
