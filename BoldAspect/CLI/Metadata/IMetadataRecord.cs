using System;
using System.Collections.Generic;
using System.IO;

using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata
{
    interface IMetadataRecord
    {
        void Read(BinaryReader reader, TableStream stream);
    }
}
