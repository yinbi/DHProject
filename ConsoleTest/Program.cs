using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int a1 = 1;
            int a2 = 2;
            int a3 = 4;
            //int a4 = 6;
            int a5 = 8;
            int a6 = 16;
            int a7 = 32;
            int a8 = 64;
            int a9 = 128;
            int b = a1 | a2 | a3 | a5 | a6 | a7 | a8 | a9;
            Console.WriteLine(b.ToString());
            //int b1 = 3;
            //int c = a & b;
            //int d = a | b;
            //int e = b | b1;
            //Console.WriteLine(c.ToString());
            //Console.WriteLine(d.ToString());
            //Console.WriteLine(e.ToString());
            Console.ReadKey();
        }
    }
}
