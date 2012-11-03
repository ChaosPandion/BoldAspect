using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata.Blobs
{
    public sealed class TypeDefOrRefOrSpecEncodedBlob
    {
        public readonly TypeDefOrRef TypeDefOrRef;
        public readonly uint Index;

        public TypeDefOrRefOrSpecEncodedBlob(TypeDefOrRef typeDefOrRef, uint index)
        {
            TypeDefOrRef = typeDefOrRef;
            Index = index;
        }
    }
}
