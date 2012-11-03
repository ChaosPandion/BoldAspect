using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldAspect.PE;

namespace BoldAspect.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            const string fileName = @"C:\Users\Matthew\Projects\MData\MData\bin\Debug\MData.dll";
            var pe = new PortableExecutable(fileName);
        }
    }
}
