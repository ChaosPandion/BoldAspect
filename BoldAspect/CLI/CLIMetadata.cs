using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public sealed class CLIMetadata
    {
        private readonly List<CLIAssembly> _assemblies = new List<CLIAssembly>();
        private readonly List<CLIModule> _modules = new List<CLIModule>();


        public CLIModule.Builder CreateModuleBuilder()
        {
            return new CLIModule.Builder(this);
        }

        public CLIModule FindModule(string name)
        {
            return null;
        }

        public CLIModule ReadModule(string filePath)
        {
            return ReadModule(File.OpenRead(filePath));
        }

        public CLIModule ReadModule(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (!stream.CanRead)
                throw new ArithmeticException();
            return null;
        }

        public CLIAssembly.Builder CreateAssemblyBuilder()
        {
            return new CLIAssembly.Builder(this);
        }

        public CLIAssembly FindAssembly(string name)
        {
            return null;
        }

        public CLIAssembly ReadAssembly(string filePath)
        {
            return ReadAssembly(File.OpenRead(filePath));
        }
        
        public CLIAssembly ReadAssembly(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (!stream.CanRead)
                throw new ArithmeticException();
            return null;
        }

        internal void Register(CLIAssembly assembly)
        {

        }

        internal void Register(CLIModule module)
        {

        }
    }
}