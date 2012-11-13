using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldAspect.CLI;

namespace BoldAspect.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Build();
        }

        static void Build()
        {
            const string fileName = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll";
            var module = CLIModule.Load(fileName);
        }
    }
}