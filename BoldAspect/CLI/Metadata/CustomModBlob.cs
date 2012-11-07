using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata.Blobs
{
    public sealed class CustomModBlob
    {
        public readonly ElementType Mod;
        public readonly TypeDefOrRefOrSpecEncodedBlob Token;

        public CustomModBlob(ElementType mod, TypeDefOrRefOrSpecEncodedBlob token)
        {
            Mod = mod;
            Token = token;
        }
    }
}
