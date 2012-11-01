using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.PE
{
    public sealed class DataDirectory
    {
        private readonly Section _section;

        public DataDirectory(Section section)
        {
            _section = section;
        }
    }
}