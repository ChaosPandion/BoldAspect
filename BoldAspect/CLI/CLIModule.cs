using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoldAspect.CLI
{
    public sealed class CLIModule
    {
        private readonly CLIMetadata _owner;


        public CLIMetadata Owner
        {
            get { return _owner; }
        }

        public sealed class Builder
        {


            internal Builder(CLIMetadata owner)
            {

            }
        }
    }

    public sealed class CLIModuleReference
    {

    }
}
