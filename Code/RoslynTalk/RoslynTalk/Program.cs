using System;
using System.IO;

namespace RoslynTalk
{
    class Program
    {
        static void Main(string[] args)
        {
            string code = File.ReadAllText("Customer.cs");

            var analyzer = new SimpleCodeAnalyzer();

            analyzer.Analyse();

            Console.ReadKey();
        }
    }
}
