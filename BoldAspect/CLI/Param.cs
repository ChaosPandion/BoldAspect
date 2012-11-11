using System;
using System.Collections.ObjectModel;
using System.IO;


namespace BoldAspect.CLI
{
    [Flags]
    public enum ParamAttributes : ushort
    {
        In = 0x0001,
        Out = 0x0002,
        Optional = 0x0010,
        HasDefault = 0x1000,
        HasFieldMarshal = 0x2000,
        Unused = 0xcfe0,
    }

    public interface IParam
    {
        ParamAttributes Flags { get; set; }
        int Sequence { get; set; }
        string Name { get; set; }
        IModule DeclaringModule { get; set; }
    }

    public sealed class CLIParam : IParam
    {
        public ParamAttributes Flags { get; set; }
        public int Sequence { get; set; }
        public string Name { get; set; }
        public IModule DeclaringModule { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public sealed class ParamCollection : Collection<IParam>
    {

    }
}