using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace BoldAspect.CLI
{

    public enum ResourceType
    {
        RT_CURSOR = 1,
        RT_BITMAP = 2,
        RT_ICON = 3,
        RT_MENU = 4,
        RT_DIALOG = 5,
        RT_STRING = 6,
        RT_FONTDIR = 7,
        RT_FONT = 8,
        RT_ACCELERATOR = 9,
        RT_RCDATA = 10,
        RT_MESSAGETABLE = 11,
        RT_GROUP_CURSOR = 12,
        RT_GROUP_ICON = 14,
        RT_VERSION = 16,
        RT_DLGINCLUDE = 17,
        RT_PLUGPLAY = 19,
        RT_VXD = 20,
        RT_ANICURSOR = 21,
        RT_ANIICON = 22,
        RT_HTML = 23,
        RT_MANIFEST = 24,
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ResourceDirectoryTableHeader
    {
        public readonly uint Characteristics;
        public readonly uint TimeStamp;
        public readonly ushort MajorVersion;
        public readonly ushort MinorVersion;
        public readonly ushort NamedEntryCount;
        public readonly ushort IdEntryCount;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ResourceDirectoryEntry
    {
        public readonly uint NameOrIdRVA;
        public readonly uint DataOrSubDirectoryRVA;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ResourceDataEntryHeader
    {
        public readonly uint DataRVA;
        public readonly uint Size;
        public readonly uint CodePage;
        public readonly uint Reserved;
    }

    public sealed class ResourceNodeCollection : Collection<IResourceNode>
    {

    }

    public sealed class ResourceDataEntry : IResourceNode
    {
        private readonly ResourceDirectoryEntry _entry;
        private readonly IResourceNode _parent;
        private readonly ResourceNodeCollection _children;
        private readonly ResourceDataEntryHeader _header;
        private readonly Slice _resourceData;

        public ResourceDataEntry(Slice data, PortableExecutable pe, IResourceNode parent, ResourceDirectoryEntry entry)
        {
            _entry = entry;
            _parent = parent;
            using (var br = data.CreateReader())
            {
                _header = br.Read<ResourceDataEntryHeader>();
                _resourceData = pe.GetResourceData().GetSlice((int)_header.DataRVA, (int)_header.Size);
            }
            Id = (int)entry.NameOrIdRVA;
        }

        public int Id { get; set; }

        public IResourceNode Parent
        {
            get { return _parent; }
        }

        public ResourceNodeCollection Children
        {
            get { return _children; }
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }

    public interface IResourceNode
    {
        IResourceNode Parent { get; }
        ResourceNodeCollection Children { get; }
    }

    public class NamedResourceDirectory : ResourceDirectory
    {
        private readonly ResourceDirectoryEntry _entry;

        public string Name { get; set; }

        public NamedResourceDirectory(Slice data, PortableExecutable pe, IResourceNode parent, ResourceDirectoryEntry entry)
            : base(data, pe, parent)
        {
            _entry = entry;
            var offset = (int)(entry.NameOrIdRVA ^ 0x80000000);
            var rdata = pe.GetResourceData();
            var nameData = rdata.GetSlice(offset, rdata.Length - offset);
            using (var br = nameData.CreateReader())
            {
                var len = br.Read<ushort>();
                Name = br.ReadUTF16String(len * 2);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ResourceTypeDirectory : ResourceDirectory
    {
        private readonly ResourceDirectoryEntry _entry;

        public ResourceType TypeId { get; set; }

        public ResourceTypeDirectory(Slice data, PortableExecutable pe, IResourceNode parent, ResourceDirectoryEntry entry)
            : base(data, pe, parent)
        {
            _entry = entry;
            TypeId = (ResourceType)entry.NameOrIdRVA;
        }

        public override string ToString()
        {
            return TypeId.ToString();
        }
    }

    public class ResourceIdDirectory : ResourceDirectory
    {
        private readonly ResourceDirectoryEntry _entry;

        public int Id { get; set; }

        public ResourceIdDirectory(Slice data, PortableExecutable pe, IResourceNode parent, ResourceDirectoryEntry entry)
            : base(data, pe, parent)
        {
            _entry = entry;
            Id = (int)entry.NameOrIdRVA;
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }

    public class ResourceDirectory : IResourceNode
    {
        private readonly IResourceNode _parent;
        private readonly ResourceNodeCollection _children;
        private readonly ResourceDirectoryTableHeader _header;

        public ResourceDirectory(Slice data, PortableExecutable pe, IResourceNode parent = null)
        {
            _parent = parent;
            _children = new ResourceNodeCollection();
            using (var br = data.CreateReader())
            {
                _header = br.Read<ResourceDirectoryTableHeader>();
                ResourceDirectoryEntry[] entries = null;
                br.Read(ref entries, _header.NamedEntryCount + _header.IdEntryCount);
                
                var rdata = pe.GetResourceData();
                for (int i = 0; i < entries.Length; i++)
                {
                    var entry = entries[i];
                    if ((entry.DataOrSubDirectoryRVA & 0x80000000) == 0)
                    {
                        var offset =(int)entry.DataOrSubDirectoryRVA;
                        _children.Add(new ResourceDataEntry(rdata.GetSlice(offset, rdata.Length - offset), pe, this, entry));
                    }
                    else
                    {
                        var offset = (int)(entry.DataOrSubDirectoryRVA ^ 0x80000000);
                        var childData = rdata.GetSlice(offset, rdata.Length - offset);
                        if (i < _header.NamedEntryCount)
                        {
                            _children.Add(new NamedResourceDirectory(childData, pe, this, entry));
                        }
                        else if (parent == null)
                        {
                            _children.Add(new ResourceTypeDirectory(childData, pe, this, entry));
                        }
                        else
                        {
                            _children.Add(new ResourceIdDirectory(childData, pe, this, entry));
                        }
                    }
                }
            }
        }

        public IResourceNode Parent
        {
            get { return _parent; }
        }

        public ResourceNodeCollection Children
        {
            get { return _children; }
        }
    }
}
