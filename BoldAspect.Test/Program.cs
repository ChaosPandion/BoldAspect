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
            var t = typeof(int);
            //const string fileName = @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\mscorlib.dll";
            const string fileName = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll";
            //const string fileName = @"C:\Windows\Microsoft.Net\assembly\GAC_MSIL\System\v4.0_4.0.0.0__b77a5c561934e089\System.dll";
            //const string fileName = @"BoldAspect.dll";
            var assembly = MetadataStorage.ReadAssembly(fileName);
        }
    }
}