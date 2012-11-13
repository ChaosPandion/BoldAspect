using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BoldAspect.Test.CLI
{
    [TestFixture]
    public sealed class CompressedIntegers
    {
        [Test]
        public void ReadCompressedUInt32()
        {
            //int length;

            //Assert.AreEqual(0x03, BinaryExtensions.GetBigEndianBytes(0x03).ReadCompressedUInt32(3, out length));
            //Assert.AreEqual(1, length);

            //Assert.AreEqual(0x7F, BinaryExtensions.GetBigEndianBytes(0x7F).ReadCompressedUInt32(3, out length));
            //Assert.AreEqual(1, length);

            //Assert.AreEqual(0x80, BinaryExtensions.GetBigEndianBytes(0x8080).ReadCompressedUInt32(2, out length));
            //Assert.AreEqual(2, length);

            //Assert.AreEqual(0x2E57, BinaryExtensions.GetBigEndianBytes(0xAE57).ReadCompressedUInt32(2, out length));
            //Assert.AreEqual(2, length);

            //Assert.AreEqual(0x3FFF, BinaryExtensions.GetBigEndianBytes(0xBFFF).ReadCompressedUInt32(2, out length));
            //Assert.AreEqual(2, length);

            //Assert.AreEqual(0x4000, BinaryExtensions.GetBigEndianBytes(0xC0004000).ReadCompressedUInt32(0, out length));
            //Assert.AreEqual(4, length);

            //Assert.AreEqual(0x1FFFFFFF, BinaryExtensions.GetBigEndianBytes(0xDFFFFFFF).ReadCompressedUInt32(0, out length));
            //Assert.AreEqual(4, length);
        }
    }
}
