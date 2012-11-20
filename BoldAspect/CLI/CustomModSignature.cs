using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public sealed class CustomModSignature
    {
        public CustomModSignature(bool required, MetadataToken token)
        {
            Required = required;
            Token = token;
        }

        public bool Required { get; set; }
        public MetadataToken Token { get; set; }
        public CustomModSignature Next { get; set; }
    }
}