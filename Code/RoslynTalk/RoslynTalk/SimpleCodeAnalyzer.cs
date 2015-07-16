using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;

namespace RoslynTalk
{
    public class SimpleCodeAnalyzer
    {
        public void Analyse()
        {
            var code = @"
using System;

namespace RoslynTalk 
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(""Hello World!"")
        }
    }
}";
            var tree = CSharpSyntaxTree.ParseText(code);

            var compilation = CSharpCompilation.Create("RoslynTalk")
                .AddReferences(MetadataReference.CreateFromFile(Assembly.GetAssembly(typeof (Console)).Location))
                .AddSyntaxTrees(tree);

            foreach (var diagnose in compilation.GetDiagnostics())
            {
                Console.WriteLine(diagnose);
            }

            compilation.Emit("text.exe"); 

        }

        public void Analyse(string code)
        {
            var tree = CSharpSyntaxTree.ParseText(code);

            var compilation = CSharpCompilation.Create("RoslynTalk")//AssemblyName
                .AddReferences(MetadataReference.CreateFromFile(Assembly.GetAssembly(typeof(int)).Location))
                .AddReferences(MetadataReference.CreateFromFile(Assembly.GetAssembly(typeof(string)).Location))
                .AddReferences(MetadataReference.CreateFromFile(Assembly.GetAssembly(typeof(DateTime)).Location))
                .AddSyntaxTrees(tree);


            foreach (var diagnose in compilation.GetDiagnostics())
            {
                Console.WriteLine(diagnose);
            }

            compilation.Emit("text.exe");
        }
    }
}
