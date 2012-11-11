using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BoldAspect;

[assembly: AssemblyTitle("BoldAspect")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration(AssemblyInfo.Configuration)]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct(AssemblyInfo.Product)]
[assembly: AssemblyCopyright(AssemblyInfo.Copyright)]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("638da148-d9a5-4573-935f-163234e89ac0")]
[assembly: AssemblyVersion(AssemblyInfo.Version)]
[assembly: AssemblyFileVersion(AssemblyInfo.Version)]
[assembly: InternalsVisibleTo("BoldAspect.Test")]

namespace BoldAspect
{
    internal static class AssemblyInfo
    {
        public const string Copyright = "Copyright © Matthew O'Brien 2012";
        public const string Version = "0.0.3.0";
        public const string Product = "BoldAspect";

#if DEBUG
        public const string Configuration = "Debug";
#else
        public const string Configuration = "Release";
#endif

    }
}