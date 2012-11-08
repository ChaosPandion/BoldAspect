using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoldAspect.CLI.Metadata.MetadataStreams;

namespace BoldAspect.CLI.Metadata
{
    public static class MetadataReader
    {
        public static IModule ReadModule(MetadataRoot root)
        {
            var data = root._tables._tableMap[TableID.Module][0];
            var module = new CLIModule();
            var offset = 2;
            if (root._tables._stringsHeapIndexWidth == 2)
            {
                module.Name = root._stringHeap.Get((uint)data.GetInt16(offset));
                offset += 2;
            }
            else
            {
                module.Name = root._stringHeap.Get((uint)data.GetInt32(offset));
                offset += 4;
            }
            if (root._tables._guidHeapIndexWidth == 2)
            {
                module.Guid = root._guidHeap.Get((uint)data.GetInt16(offset));
            }
            else
            {
                module.Guid = root._guidHeap.Get((uint)data.GetInt32(offset));
            }
            module.Assembly = ReadAssembly(root, module);
            return module;
        }

        private static IAssembly ReadAssembly(MetadataRoot root, IModule manifestModule)
        {
            List<byte[]> entries;
            if (!root._tables._tableMap.TryGetValue(TableID.Assembly, out entries) || entries.Count == 0)
                return null;
            var ts = root._tables.Tables.Single(t => t.TableID == TableID.Assembly);
            var data = entries[0];

            Debug.WriteLine(string.Join("", data.Select(b => b.ToString("X2"))));

            var assembly = new CLIAssembly();
            assembly.HashAlgorithm = (AssemblyHashAlgorithm)data.GetInt32(0);
            assembly.Version = new Version(data.GetInt16(4), data.GetInt16(6), data.GetInt16(8), data.GetInt16(10));
            assembly.Flags = (AssemblyFlags)data.GetInt32(12);

            var offset = 16;
            var pk = default(Slice);
            if (root._tables._blobHeapIndexWidth == 2)
            {
                pk = root._blobHeap.Get((uint)data.GetInt16(offset));
                offset += 2;
            }
            else
            {
                pk = root._blobHeap.Get((uint)data.GetInt32(offset));
                offset += 4;
            }
            assembly.PublicKey = BitConverter.ToString(pk.Data, pk.Offset, pk.Length); //Encoding.ASCII.GetString(pk.Data, pk.Offset, pk.Length); //Convert.ToBase64String(pk.Data, pk.Offset, pk.Length);


            var cul = default(string);
            if (root._tables._stringsHeapIndexWidth == 2)
            {
                assembly.Name = root._stringHeap.Get((uint)data.GetInt16(offset));
                offset += 2;
                cul = root._stringHeap.Get((uint)data.GetInt16(offset));
            }
            else
            {
                assembly.Name = root._stringHeap.Get((uint)data.GetInt32(offset));
                offset += 4;
                cul = root._stringHeap.Get((uint)data.GetInt32(offset));
            }

            assembly.Culture = CultureInfo.CreateSpecificCulture(cul);
            assembly.ManifestModule = manifestModule;
            return assembly;
        }

    }
}