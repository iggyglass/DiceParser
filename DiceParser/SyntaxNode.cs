using System.Text.RegularExpressions;
using System;

namespace DiceParser
{
    public class SyntaxNode
    {

        public Func<int, int, int> Value;
        public SyntaxNode Left;
        public SyntaxNode Right;
        public bool isConstant { get; private set; }

        public SyntaxNode(Func<int, int, int> value, bool isConstant)
        {
            Value = value;
            this.isConstant = isConstant;
        }

        public int Evaluate()
        {
            if (isConstant)
            {
                return Value(0, 0);
            }
            else
            {
                if (Left == null || Right == null) throw new Exception("Encountered a null operand.");

                return Value(Left.Evaluate(), Right.Evaluate());
            }
        }

        public static SyntaxNode FromToken(string token)
        {
            token = token.Trim();

            if (Regex.IsMatch(token, @"[0-9]+"))
            {
                int val = int.Parse(token);
                return new SyntaxNode(delegate (int a, int b) { return val; }, true);
            }
            else
            {
                switch (token)
                {
                    case "+":
                        return new SyntaxNode(delegate (int a, int b) { return a + b; }, false);
                    case "-":
                        return new SyntaxNode(delegate (int a, int b) { return a - b; }, false);
                    case "*":
                        return new SyntaxNode(delegate (int a, int b) { return a * b; }, false);
                    case "/":
                        return new SyntaxNode(delegate (int a, int b) { return a / b; }, false);
                    default:
                        throw new Exception("Invalid token");
                }
            }
        }
    }
}
