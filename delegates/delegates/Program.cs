using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegates
{

    public delegate int BinaryOp(int x, int y);

    public class SimpleMath
    {
        public int Add(int x, int y) => x + y;
        public int Subtract(int x, int y) => x - y;
    }

    internal class Program
    {
        static void DisplayDelegateInfo(Delegate delObj)
        {
            foreach (Delegate d in delObj.GetInvocationList())
            {
                Console.WriteLine("Method Name: {0}", d.Method);
                Console.WriteLine("Type Name: {0}", d.Target);
            }
        }
        static void Main(string[] args)
        {
            SimpleMath m = new SimpleMath();
            BinaryOp b = new BinaryOp(m.Add);
            Console.WriteLine("***** Simple Delegate Example *****\n");
            DisplayDelegateInfo(b);
            Console.WriteLine("10 + 10 = {0}", b(10, 10));
        }
    }
}
