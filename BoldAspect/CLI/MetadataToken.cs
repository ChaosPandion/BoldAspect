using System;

namespace BoldAspect.CLI
{
    public partial struct MetadataToken : IEquatable<MetadataToken>, IComparable<MetadataToken>, IComparable
    {
        private readonly uint Value;

        public MetadataToken(uint value)
        {
            Value = value;
        }

        public MetadataToken(TableID table, uint value)
        {
            Value = ((uint)table << 24) | value;
        }

        public TableID Table
        {
            get { return (TableID)((Value & 0xFF000000) >> 24); }
        }

        public uint Key
        {
            get { return (Value & 0x00FFFFFF); }
        }

        public bool IsNull
        {
            get { return Key == 0; }
        }

        public override string ToString()
        {
            return IsNull ? "null" : string.Format("{0}(0x{1:X8})", Table, Key);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is MetadataToken && Equals((MetadataToken)obj);
        }

        public bool Equals(MetadataToken other)
        {
            return this.Value == other.Value;
        }

        public int CompareTo(object obj)
        {
            return CompareTo((obj as MetadataToken?) ?? default(MetadataToken));
        }

        public int CompareTo(MetadataToken other)
        {
            return this.Value.CompareTo(other.Value);
        }

        public static bool operator ==(MetadataToken left, MetadataToken right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(MetadataToken left, MetadataToken right)
        {
            return !left.Equals(right);
        }
    }
}