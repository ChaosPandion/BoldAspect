using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldAspect.PE;

namespace BoldAspect.CLI.PE
{
    public sealed class PeImage
    {
        private readonly Dictionary<string, Section> _sections = new Dictionary<string, Section>();

        public PeImage()
        {

        }

        public MachineType Machine { get; set; }

        public FileCharacteristics Characteristics { get; set; }

        public int SectionCount 
        {
            get { return _sections.Count; }
        }

        public Section CreateSection(string name)
        {
            return null;
        }

        public Section GetSection(string name)
        {
            return null;
        }

        public bool HasSection(string name)
        {
            return false;
        }
    }
}
