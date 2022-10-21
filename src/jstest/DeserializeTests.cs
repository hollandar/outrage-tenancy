using js;

namespace jstest
{
    [TestClass]
    public class DeserializeTests
    {
        [TestMethod]
        public void ReadNullValue()
        {
            using MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream, encoding:System.Text.Encoding.UTF8, leaveOpen: true))
            {
                writer.Write((Int16)ValueTypeEnum.Null);
            }
            stream.Seek(0, SeekOrigin.Begin);

            var loader = new Deserializer();
            var value = loader.Load(stream);

            Assert.AreEqual(null, value);
        }

        [TestMethod]
        public void ReadInt16Value()
        {
            using MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream, encoding:System.Text.Encoding.UTF8, leaveOpen: true))
            {
                writer.Write((Int16)ValueTypeEnum.Int16);
                writer.Write((Int16)64);
            }
            stream.Seek(0, SeekOrigin.Begin);

            var loader = new Deserializer();
            var value = loader.Load(stream);

            Assert.IsInstanceOfType(value, typeof(Int16));
            Assert.AreEqual((Int16)64, value);
        }

        [TestMethod]
        public void ReadInt32Value()
        {
            using MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream, encoding:System.Text.Encoding.UTF8, leaveOpen: true))
            {
                writer.Write((Int16)ValueTypeEnum.Int32);
                writer.Write((Int32)990);
            }
            stream.Seek(0, SeekOrigin.Begin);

            var loader = new Deserializer();
            var value = loader.Load(stream);

            Assert.IsInstanceOfType(value, typeof(Int32));
            Assert.AreEqual((Int32)990, value);
        }

        [TestMethod]
        public void ReadInt64Value()
        {
            using MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream, encoding:System.Text.Encoding.UTF8, leaveOpen: true))
            {
                writer.Write((Int16)ValueTypeEnum.Int64);
                writer.Write((Int64)6549084);
            }
            stream.Seek(0, SeekOrigin.Begin);

            var loader = new Deserializer();
            var value = loader.Load(stream);

            Assert.IsInstanceOfType(value, typeof(Int64));
            Assert.AreEqual((Int64)6549084, value);
        }

        [TestMethod]
        public void ReadUInt16Value()
        {
            using MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream, encoding:System.Text.Encoding.UTF8, leaveOpen: true))
            {
                writer.Write((Int16)ValueTypeEnum.UInt16);
                writer.Write((UInt16)64);
            }
            stream.Seek(0, SeekOrigin.Begin);

            var loader = new Deserializer();
            var value = loader.Load(stream);

            Assert.IsInstanceOfType(value, typeof(UInt16));
            Assert.AreEqual((UInt16)64, value);
        }

        [TestMethod]
        public void ReadUInt32Value()
        {
            using MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream, encoding:System.Text.Encoding.UTF8, leaveOpen: true))
            {
                writer.Write((Int16)ValueTypeEnum.UInt32);
                writer.Write((UInt32)990);
            }
            stream.Seek(0, SeekOrigin.Begin);

            var loader = new Deserializer();
            var value = loader.Load(stream);

            Assert.IsInstanceOfType(value, typeof(UInt32));
            Assert.AreEqual((UInt32)990, value);
        }

        [TestMethod]
        public void ReadUInt64Value()
        {
            using MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream, encoding:System.Text.Encoding.UTF8, leaveOpen: true))
            {
                writer.Write((Int16)ValueTypeEnum.UInt64);
                writer.Write((UInt64)6549084);
            }
            stream.Seek(0, SeekOrigin.Begin);

            var loader = new Deserializer();
            var value = loader.Load(stream);

            Assert.IsInstanceOfType(value, typeof(UInt64));
            Assert.AreEqual((UInt64)6549084, value);
        }

        [TestMethod]
        public void ReadByteValue()
        {
            using MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream, encoding:System.Text.Encoding.UTF8, leaveOpen: true))
            {
                writer.Write((Int16)ValueTypeEnum.Byte);
                writer.Write((Byte)29);
            }
            stream.Seek(0, SeekOrigin.Begin);

            var loader = new Deserializer();
            var value = loader.Load(stream);

            Assert.IsInstanceOfType(value, typeof(Byte));
            Assert.AreEqual((Byte)29, value);
        }

        [TestMethod]
        public void ReadSByteValue()
        {
            using MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream, encoding:System.Text.Encoding.UTF8, leaveOpen: true))
            {
                writer.Write((Int16)ValueTypeEnum.SByte);
                writer.Write((SByte)127);
            }
            stream.Seek(0, SeekOrigin.Begin);

            var loader = new Deserializer();
            var value = loader.Load(stream);

            Assert.IsInstanceOfType(value, typeof(SByte));
            Assert.AreEqual((SByte)127, value);
        }

        [TestMethod]
        public void ReadBooleanValue()
        {
            using MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream, encoding:System.Text.Encoding.UTF8, leaveOpen: true))
            {
                writer.Write((Int16)ValueTypeEnum.Boolean);
                writer.Write((Boolean)true);
            }
            stream.Seek(0, SeekOrigin.Begin);

            var loader = new Deserializer();
            var value = loader.Load(stream);

            Assert.IsInstanceOfType(value, typeof(Boolean));
            Assert.AreEqual((Boolean)true, value);
        }

        [TestMethod]
        public void ReadDecimalValue()
        {
            using MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream, encoding:System.Text.Encoding.UTF8, leaveOpen: true))
            {
                writer.Write((Int16)ValueTypeEnum.Decimal);
                writer.Write((Decimal)3.14159);
            }
            stream.Seek(0, SeekOrigin.Begin);

            var loader = new Deserializer();
            var value = loader.Load(stream);

            Assert.IsInstanceOfType(value, typeof(Decimal));
            Assert.AreEqual((Decimal)3.14159, value);
        }

        [TestMethod]
        public void ReadFlat32Value()
        {
            using MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream, encoding:System.Text.Encoding.UTF8, leaveOpen: true))
            {
                writer.Write((Int16)ValueTypeEnum.Float32);
                writer.Write((Single)3.14159);
            }
            stream.Seek(0, SeekOrigin.Begin);

            var loader = new Deserializer();
            var value = loader.Load(stream);

            Assert.IsInstanceOfType(value, typeof(Single));
            Assert.AreEqual((Single)3.14159, value);
        }

        [TestMethod]
        public void ReadFloat64Value()
        {
            using MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream, encoding:System.Text.Encoding.UTF8, leaveOpen: true))
            {
                writer.Write((Int16)ValueTypeEnum.Float64);
                writer.Write((Double)3.14159);
            }
            stream.Seek(0, SeekOrigin.Begin);

            var loader = new Deserializer();
            var value = loader.Load(stream);

            Assert.IsInstanceOfType(value, typeof(Double));
            Assert.AreEqual((Double)3.14159, value);
        }

        [TestMethod]
        public void ReadDateValue()
        {
            using MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream, encoding: System.Text.Encoding.UTF8, leaveOpen: true))
            {
                writer.Write((Int16)ValueTypeEnum.Date);
                writer.Write((Int32)1990);
                writer.Write((Int32)2);
                writer.Write((Int32)1);
            }
            stream.Seek(0, SeekOrigin.Begin);

            var loader = new Deserializer();
            var value = loader.Load(stream);

            Assert.IsInstanceOfType(value, typeof(DateOnly));
            Assert.AreEqual((DateOnly)new DateOnly(1990, 2, 1), value);
        }

        [TestMethod]
        public void ReadTimeValue()
        {
            using MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream, encoding: System.Text.Encoding.UTF8, leaveOpen: true))
            {
                writer.Write((Int16)ValueTypeEnum.Time);
                writer.Write((Int32)13);
                writer.Write((Int32)5);
                writer.Write((Int32)2);
                writer.Write((Int32)0);
                writer.Write((Int32)0);
            }
            stream.Seek(0, SeekOrigin.Begin);

            var loader = new Deserializer();
            var value = loader.Load(stream);

            Assert.IsInstanceOfType(value, typeof(TimeOnly));
            Assert.AreEqual((TimeOnly)new TimeOnly(13, 5, 2), value);
        }

        [TestMethod]
        public void ReadDateTimeValue()
        {
            using MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream, encoding: System.Text.Encoding.UTF8, leaveOpen: true))
            {
                writer.Write((Int16)ValueTypeEnum.DateTime);
                writer.Write((Int64)new DateTimeOffset(1990, 2, 1, 13, 5, 2, TimeSpan.FromSeconds(0)).ToUnixTimeMilliseconds());
            }
            stream.Seek(0, SeekOrigin.Begin);

            var loader = new Deserializer();
            var value = loader.Load(stream);

            Assert.IsInstanceOfType(value, typeof(DateTimeOffset));
            Assert.AreEqual(new DateTimeOffset(1990, 2, 1, 13, 5, 2, TimeSpan.FromSeconds(0)), value);
        }

        [TestMethod]
        public void ReadCharValue()
        {
            using MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream, encoding: System.Text.Encoding.UTF8, leaveOpen: true))
            {
                writer.Write((Int16)ValueTypeEnum.Char);
                writer.Write((Char)'&');
            }
            stream.Seek(0, SeekOrigin.Begin);

            var loader = new Deserializer();
            var value = loader.Load(stream);

            Assert.IsInstanceOfType(value, typeof(Char));
            Assert.AreEqual((Char)'&', value);
        }

        [TestMethod]
        public void ReadStringValue()
        {
            using MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream, encoding: System.Text.Encoding.UTF8, leaveOpen: true))
            {
                writer.Write((Int16)ValueTypeEnum.String);
                writer.Write((String)"The quick brown fox jumped over the lazy dog.");
            }
            stream.Seek(0, SeekOrigin.Begin);

            var loader = new Deserializer();
            var value = loader.Load(stream);

            Assert.IsInstanceOfType(value, typeof(String));
            Assert.AreEqual("The quick brown fox jumped over the lazy dog.", value);
        }

        [TestMethod]
        public void ReadInt32Array()
        {
            using MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream, encoding: System.Text.Encoding.UTF8, leaveOpen: true))
            {
                writer.Write((Int16)ValueTypeEnum.Array);
                writer.Write((Int16)ValueTypeEnum.Int32);
                writer.Write((Int32)5);
                writer.Write((Int32)1);
                writer.Write((Int32)2);
                writer.Write((Int32)3);
                writer.Write((Int32)4);
                writer.Write((Int32)5);
            }
            stream.Seek(0, SeekOrigin.Begin);

            var loader = new Deserializer();
            var value = loader.Load(stream);

            Assert.IsInstanceOfType(value, typeof(object?[]));
            Assert.IsTrue(Enumerable.SequenceEqual(new object?[] { 1, 2, 3, 4, 5 }, (object?[])value));
        }
 
        [TestMethod]
        public void ReadObject()
        {
            using MemoryStream stream = new MemoryStream();
            using (BinaryWriter writer = new BinaryWriter(stream, encoding: System.Text.Encoding.UTF8, leaveOpen: true))
            {
                writer.Write((Int16)ValueTypeEnum.Object);
                writer.Write((Int32)2);
                writer.Write((String)"Name");
                writer.Write((Int16)ValueTypeEnum.String);
                writer.Write((String)"Jack");
                writer.Write((String)"Age");
                writer.Write((Int16)ValueTypeEnum.Int64);
                writer.Write((Int64)59);
            }
            stream.Seek(0, SeekOrigin.Begin);

            var loader = new Deserializer();
            var value = loader.Load(stream);

            Assert.IsInstanceOfType(value, typeof(IDictionary<string, object?>));

            var dictionary = (IDictionary<string, object?>)value;
            Assert.AreEqual(2, dictionary.Count);
            Assert.AreEqual("Jack", dictionary["Name"]);
            Assert.AreEqual((Int64)59, dictionary["Age"]);
        }
    }
}