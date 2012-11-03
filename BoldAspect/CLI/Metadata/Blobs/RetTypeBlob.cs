using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI.Metadata.Blobs
{
    public sealed class RetTypeBlob
    {
        public readonly IReadOnlyCollection<CustomModBlob> CustomMods;
        public readonly bool IsByRef;
        public readonly ElementType Type;

        private RetTypeBlob(IReadOnlyCollection<CustomModBlob> customMods, bool isByRef, ElementType type)
        {
            CustomMods = customMods;
            IsByRef = isByRef;
            Type = type;
        }

        public static RetTypeBlob CreateByRefRetType(IReadOnlyCollection<CustomModBlob> customMods, ElementType type)
        {
            return new RetTypeBlob(customMods, true, type);
        }

        public static RetTypeBlob CreateTypeByRefRetType(IReadOnlyCollection<CustomModBlob> customMods)
        {
            return new RetTypeBlob(customMods, false, ElementType.Typedbyref);
        }

        public static RetTypeBlob CreateVoidRetType(IReadOnlyCollection<CustomModBlob> customMods)
        {
            return new RetTypeBlob(customMods, false, ElementType.Void);
        }
    }
}