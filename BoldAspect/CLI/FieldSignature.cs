using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public sealed class FieldSignature
    {
        private readonly TypeSignatureCollection _customMods = new TypeSignatureCollection();

        public TypeSignature TypeSignature { get; set; }

        public TypeSignatureCollection CustomMods
        {
            get { return _customMods; }
        }

        public override string ToString()
        {
            return TypeSignature == null ? "null" : TypeSignature.ToString();
        }
    }
}