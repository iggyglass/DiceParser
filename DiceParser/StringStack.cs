using System;

namespace DiceParser
{
    public class StringStack
    {

        private int sp = 0;
        private string[] buffer;

        public StringStack(int maxStack)
        {
            buffer = new string[maxStack];
        }

        public void Push(string value)
        {
            if (sp == buffer.Length) throw new Exception("Stack Overflow.");

            buffer[sp] = value;
            sp++;
        }

        public string Pop()
        {
            if (sp == 0) throw new Exception("Stack Underflow.");

            sp--;
            return buffer[sp];
        }

        public string Peek()
        {
            if (sp == 0) return null;

            return buffer[sp - 1];
        }
    }
}
