using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.PE
{
    public sealed class Section
    {
        private readonly SectionHeader _sectionHeader;
        private readonly string _name;

        public Section(SectionHeader sectionHeader)
        {
            _sectionHeader = sectionHeader;
            var nameData = BitConverter.GetBytes(sectionHeader.Name);
            _name = Encoding.ASCII.GetString(nameData).TrimEnd('\0');
        }

        public string Name
        {
            get { return _name; }
        }

        public SectionCharacteristics Characteristics
        {
            get { return _sectionHeader.Characteristics; }
        }
    }
}
