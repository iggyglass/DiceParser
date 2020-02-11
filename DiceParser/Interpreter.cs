namespace DiceParser
{
    public static class Interpreter
    {
        public static int Interpret(string data)
        {
            string[] tokens = Tokenizer.Tokenize(data.ToLower());

            SyntaxNode root = Tokenizer.ToAST(tokens);

            return root.Evaluate();
        }
    }
}
