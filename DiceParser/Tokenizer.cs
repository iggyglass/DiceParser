using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DiceParser
{
    public static class Tokenizer
    {
        public static string[] Tokenize(string input)
        {
            input = input.Replace(" ", "");
            string[] temp = Regex.Split(input, @"(\+)|((?<=[0-9])\-)|(\*)|(\/)|(\()|(\))|(d)");

            return temp;
        }

        public static Queue<string> ToRPN(string[] tokens)
        {
            Queue<string> output = new Queue<string>();
            StringStack operators = new StringStack(1024);

            for (int i = 0; i < tokens.Length; i++)
            {
                if (Regex.IsMatch(tokens[i], @"[0-9]+"))
                {
                    output.Enqueue(tokens[i]);
                }
                else if (Regex.IsMatch(tokens[i], @"(\+)|(-)|(\*)|(\/)|(d)"))
                {
                    while (operators.Peek() != null && Regex.IsMatch(operators.Peek(), @"(\+)|(-)|(\*)|(\/)|(d)") && GetPrecedence(tokens[i]) <= GetPrecedence(operators.Peek()))
                    {
                        output.Enqueue(operators.Pop());
                    }

                    operators.Push(tokens[i]);
                }
                else if (tokens[i] == "(")
                {
                    operators.Push(tokens[i]);
                }
                else if (tokens[i] == ")")
                {
                    while (operators.Peek() != "(")
                    {
                        output.Enqueue(operators.Pop());
                    }

                    operators.Pop();
                }
                else if (tokens[i].Trim() == "")
                {
                    continue;
                }
                else
                {
                    throw new Exception($"Unable to parse token {tokens[i]}.");
                }
            }

            while (operators.Peek() != null)
            {
                output.Enqueue(operators.Pop());
            }

            return output;
        }

        public static SyntaxNode ToAST(string[] tokens)
        {
            Stack<SyntaxNode> output = new Stack<SyntaxNode>();
            StringStack operators = new StringStack(1024);

            for (int i = 0; i < tokens.Length; i++)
            {
                if (Regex.IsMatch(tokens[i], @"[0-9]+"))
                {
                    output.Push(SyntaxNode.FromToken(tokens[i]));
                }
                else if (Regex.IsMatch(tokens[i], @"(\+)|(-)|(\*)|(\/)|(d)"))
                {
                    while (operators.Peek() != null && Regex.IsMatch(operators.Peek(), @"(\+)|(-)|(\*)|(\/)|(d)") && GetPrecedence(tokens[i]) <= GetPrecedence(operators.Peek()))
                    {
                        SyntaxNode node = SyntaxNode.FromToken(operators.Pop());

                        node.Right = output.Pop();
                        node.Left = output.Pop();
                        output.Push(node);
                    }

                    operators.Push(tokens[i]);
                }
                else if (tokens[i] == "(")
                {
                    operators.Push(tokens[i]);
                }
                else if (tokens[i] == ")")
                {
                    while (operators.Peek() != "(")
                    {
                        SyntaxNode node = SyntaxNode.FromToken(operators.Pop());

                        node.Right = output.Pop();
                        node.Left = output.Pop();
                        output.Push(node);
                    }

                    operators.Pop();
                }
                else if (tokens[i].Trim() == "")
                {
                    continue;
                }
                else
                {
                    throw new Exception($"Unable to parse token {tokens[i]}.");
                }
            }

            while (operators.Peek() != null)
            {
                SyntaxNode node = SyntaxNode.FromToken(operators.Pop());

                node.Right = output.Pop();
                node.Left = output.Pop();
                output.Push(node);
            }

            return output.Pop();
        }

        public static int GetPrecedence(string op)
        {
            switch (op)
            {
                case "+":
                    return 2;
                case "-":
                    return 2;
                case "*":
                    return 3;
                case "/":
                    return 3;
                case "d":
                    return 2;
                default:
                    throw new InvalidOperationException($"Operator {op} not found.");
            }
        }
    }
}
