using System;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynTalk
{
    public static class SyntaxExamples
    {
        public static void Example()
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(
@"using System;
using System.Collections;
using System.Linq;
using System.Text;
 
namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(""Hello, World!"");
        }
    }
}");

            var root = (CompilationUnitSyntax)tree.GetRoot();

            MemberDeclarationSyntax firstMember = root.Members[0];
            
            var helloWorldDeclaration = (NamespaceDeclarationSyntax)firstMember;

            var programDeclaration = (ClassDeclarationSyntax)helloWorldDeclaration.Members[0];

            var mainDeclaration = (MethodDeclarationSyntax)programDeclaration.Members[0];

            var argsParameter = mainDeclaration.ParameterList.Parameters[0];

            
        }

        public static void QueryMethodExample()
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(
   @"using System;
using System.Collections;
using System.Linq;
using System.Text;
 
namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(""Hello, World!"");
        }
    }
}");

            var root = (CompilationUnitSyntax)tree.GetRoot();

            var firstParameters = from methodDeclaration in root.DescendantNodes()
                                                    .OfType<MethodDeclarationSyntax>()
                                  where methodDeclaration.Identifier.ValueText == "Main"
                                  select methodDeclaration.ParameterList.Parameters.First();

            var argsParameter2 = firstParameters.Single();

        }

        public static void SyntaxWalkerExample()
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(
@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
 
namespace TopLevel
{
    using Microsoft;
    using System.ComponentModel;
 
    namespace Child1
    {
        using Microsoft.Win32;
        using System.Runtime.InteropServices;
 
        class Foo { }
    }
 
    namespace Child2
    {
        using System.CodeDom;
        using Microsoft.CSharp;
 
        class Bar { }
    }
}");

            //NameSyntax name = IdentifierName("System");
            //name = QualifiedName(name, IdentifierName("Collections"));
            //name = QualifiedName(name, IdentifierName("Generic"));


            //var root = (CompilationUnitSyntax)tree.GetRoot();

            //var oldUsing = root.Usings[1];
            //var newUsing = oldUsing.WithName(name);

            //root = root.ReplaceNode(oldUsing, newUsing);

            //root.FindNode()

            //root.ToFullString();
        }

        /// <summary>
        /// (deprecates) Use some thing else instead
        /// </summary>
        public static void Test()
        {
            var code = @"
using System;
namespace RoslynTalk 
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(""Hello World!"");
            Console.ReadKey();
        }
    }
}";
            //Parser
            var tree = CSharpSyntaxTree.ParseText(code);

            //Symbol, Metadata, Binding
            var t = Assembly.GetAssembly(typeof (Console)).Location;

            var compilation = CSharpCompilation.Create("RoslynTalk")
                .AddReferences(MetadataReference.CreateFromFile(
                    Assembly.GetAssembly(typeof(Console)).Location))
                .AddSyntaxTrees(tree);


            foreach (var diagnose in compilation.GetDiagnostics())
            {
                Console.WriteLine(diagnose);
            }

            //IL Emitter
            compilation.Emit("text.exe");
            Console.ReadKey();
        }
    }
}
