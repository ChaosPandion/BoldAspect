using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public sealed class Section
    {
        private readonly Slice _data;

        public Section(Slice data)
        {
            _data = data;
        }

        public string Name { get; set; }

        public SectionCharacteristics Characteristics { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
