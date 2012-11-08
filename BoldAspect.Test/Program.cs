using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BoldAspect.CLI.Metadata;
using BoldAspect.PE;

namespace BoldAspect.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //const string fileName = @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\mscorlib.dll";
            const string fileName = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll";
            //const string fileName = @"BoldAspect.dll";
            var pe = new PortableExecutable(fileName);
            var mod = pe.ReadModule();
           // EX();
            
        }

        //static void EX2()
        //{
        //    var sb = new StringBuilder();
        //    var dir = new DirectoryInfo("C:/Users/Matthew/Projects/BoldAspect/BoldAspect/CLI/Metadata/IndexTags");
        //    var files = dir.GetFiles();
        //    foreach (var file in files)
        //    {
        //        var fn = "BoldAspect.CLI.Metadata." + file.Name.Replace(".cs", "").Replace("Index", "");
        //        var type = Assembly.GetAssembly(typeof(PortableExecutable)).GetType(fn);
        //        sb.AppendFormat("new CodedIndex(typeof({0}), new [] {{ ", type.Name);
        //        var ts = Enum.GetNames(type);
        //        var first = true;
        //        foreach (var t in ts.Where(tn => !tn.StartsWith("Not")))
        //        {
        //            if (!first)
        //            {
        //                sb.Append(", ");
        //            }
        //            sb.AppendFormat("TableID.{0}", t);
        //            first = false;
        //        }
        //        sb.Append("}),\r\n");
        //    }
        //    Debug.Write(sb.ToString());
        //}


        //static void EX1()
        //{
        //    var sb = new StringBuilder();
        //    foreach (var n in Enum.GetNames(typeof(TableID)))
        //    {
        //        sb.AppendFormat("new SimpleIndex(TableID.{0}),\r\n", n);
        //    }
        //    Debug.Write(sb.ToString());
        //}
        //static void EX()
        //{
        //    var i = 0;
        //    var sb = new StringBuilder();
        //    foreach (var n in Enum.GetNames(typeof(TableID)))
        //    {
        //        sb.AppendFormat("new TableSchema(TableID.{0},\r\n", n);
        //        var t = Assembly.GetAssembly(typeof(PortableExecutable)).GetType("BoldAspect.CLI.Metadata." + n + "Record");
        //        var fs = t.GetFields();
        //        var first = true;
        //        foreach (var f in fs)
        //        {
        //            if (!first)
        //            {
        //                sb.Append(",\r\n");
        //            }
        //            sb.Append("        ");
        //            var ia = Attribute.GetCustomAttribute(f, typeof(ColumnAttribute), true);
        //            var cc = ia as ConstantColumnAttribute;
        //            if (cc != null)
        //            {
        //                sb.AppendFormat("typeof({0})", cc.Type.Name);
        //            }
        //            var si = ia as SimpleIndexAttribute;
        //            if (ia is SimpleIndexAttribute)
        //            {
        //                sb.AppendFormat("SimpleIndexes[{0}]", Enum.GetNames(typeof(TableID)).ToList().IndexOf(si.TableID.ToString()));
        //            }
        //            var ci = ia as CodedIndexAttribute;
        //            if (ci != null)
        //            {
        //                sb.AppendFormat("CodedIndexes.Single(c => c.EnumType == typeof({0}))", ci.EnumType.Name);
        //            }
        //            if (ia is StringHeapIndexAttribute)
        //            {
        //                sb.Append("typeof(StringsHeapIndex)");
        //            }
        //            if (ia is GuidHeapIndexAttribute)
        //            {
        //                sb.Append("typeof(GuidHeapIndex)");
        //            }
        //            if (ia is BlobHeapIndexAttribute)
        //            {
        //                sb.Append("typeof(BlobHeapIndex)");
        //            }
        //            first = false;
        //        }
        //        sb.Append("),\r\n");
        //    }
        //    Debug.Write(sb.ToString());
        //}

    }
}
