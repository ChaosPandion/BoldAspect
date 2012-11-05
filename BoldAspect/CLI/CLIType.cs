using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public sealed class CLIType
    {
        public CLIMetadata Metadata { get; private set; }
        public string Name { get; set; }
        public string NameSpace { get; set; }
    }

    public sealed class CLITypeRef
    {
        public CLIMetadata Metadata { get; private set; }
        public string Name { get; set; }
        public string NameSpace { get; set; }
    }

    public sealed class CLIExportedType
    {
        public CLIMetadata Metadata { get; private set; }
    }
}
