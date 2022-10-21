using js;
using System.Runtime.Serialization;

namespace jstest
{
    [TestClass]
    public class SerializeTests
    {
        Serializer serializer = new();
        Deserializer deserializer = new();

        [TestMethod]
        public void WriteNullValue()
        {
            using MemoryStream stream = new MemoryStream();
            serializer.Write(null, stream);

            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.AreEqual(null, value);
        }

        [TestMethod]
        public void WriteInt16Value()
        {
            using MemoryStream stream = new MemoryStream();
            serializer.Write((Int16)64, stream);

            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.IsInstanceOfType(value, typeof(Int16));
            Assert.AreEqual((Int16)64, value);
        }

        [TestMethod]
        public void WriteInt32Value()
        {
            using MemoryStream stream = new MemoryStream();
            serializer.Write((Int32)990, stream);

            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.IsInstanceOfType(value, typeof(Int32));
            Assert.AreEqual((Int32)990, value);
        }

        [TestMethod]
        public void WriteInt64Value()
        {
            using MemoryStream stream = new MemoryStream();
            serializer.Write((Int64)6549084, stream);

            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.IsInstanceOfType(value, typeof(Int64));
            Assert.AreEqual((Int64)6549084, value);
        }

        [TestMethod]
        public void WriteUInt16Value()
        {
            using MemoryStream stream = new MemoryStream();
            serializer.Write((UInt16)64, stream);

            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.IsInstanceOfType(value, typeof(UInt16));
            Assert.AreEqual((UInt16)64, value);
        }

        [TestMethod]
        public void WriteUInt32Value()
        {
            using MemoryStream stream = new MemoryStream();
            serializer.Write((UInt32)990, stream);

            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.IsInstanceOfType(value, typeof(UInt32));
            Assert.AreEqual((UInt32)990, value);
        }

        [TestMethod]
        public void WriteUInt64Value()
        {
            using MemoryStream stream = new MemoryStream();
            serializer.Write((UInt64)6549084, stream);

            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.IsInstanceOfType(value, typeof(UInt64));
            Assert.AreEqual((UInt64)6549084, value);
        }

        [TestMethod]
        public void WriteByteValue()
        {
            using MemoryStream stream = new MemoryStream();
            serializer.Write((Byte)29, stream);

            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.IsInstanceOfType(value, typeof(Byte));
            Assert.AreEqual((Byte)29, value);
        }

        [TestMethod]
        public void WriteSByteValue()
        {
            using MemoryStream stream = new MemoryStream();
            serializer.Write((SByte)127, stream);

            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.IsInstanceOfType(value, typeof(SByte));
            Assert.AreEqual((SByte)127, value);
        }

        [TestMethod]
        public void WriteBooleanValue()
        {
            using MemoryStream stream = new MemoryStream();
            serializer.Write((Boolean)true, stream);

            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.IsInstanceOfType(value, typeof(Boolean));
            Assert.AreEqual((Boolean)true, value);
        }

        [TestMethod]
        public void WriteDecimalValue()
        {
            using MemoryStream stream = new MemoryStream();
            serializer.Write((Decimal)3.14159, stream);

            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.IsInstanceOfType(value, typeof(Decimal));
            Assert.AreEqual((Decimal)3.14159, value);
        }

        [TestMethod]
        public void WriteFlat32Value()
        {
            using MemoryStream stream = new MemoryStream();
            serializer.Write((Single)3.14159, stream);

            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.IsInstanceOfType(value, typeof(Single));
            Assert.AreEqual((Single)3.14159, value);
        }

        [TestMethod]
        public void WriteFloat64Value()
        {
            using MemoryStream stream = new MemoryStream();
            serializer.Write((Double)3.14159, stream);

            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.IsInstanceOfType(value, typeof(Double));
            Assert.AreEqual((Double)3.14159, value);
        }

        [TestMethod]
        public void WriteDateValue()
        {
            using MemoryStream stream = new MemoryStream();
            serializer.Write(new DateOnly(1990, 2, 1), stream);

            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.IsInstanceOfType(value, typeof(DateOnly));
            Assert.AreEqual((DateOnly)new DateOnly(1990, 2, 1), value);
        }

        [TestMethod]
        public void WriteTimeValue()
        {
            using MemoryStream stream = new MemoryStream();
            serializer.Write(new TimeOnly(13, 5, 2, 0, 0), stream);

            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.IsInstanceOfType(value, typeof(TimeOnly));
            Assert.AreEqual((TimeOnly)new TimeOnly(13, 5, 2), value);
        }

        [TestMethod]
        public void WriteDateTimeValue()
        {
            using MemoryStream stream = new MemoryStream();
            serializer.Write(new DateTimeOffset(1990, 2, 1, 13, 5, 2, TimeSpan.FromSeconds(0)), stream);

            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.IsInstanceOfType(value, typeof(DateTimeOffset));
            Assert.AreEqual(new DateTimeOffset(1990, 2, 1, 13, 5, 2, TimeSpan.FromSeconds(0)), value);
        }

        [TestMethod]
        public void WriteCharValue()
        {
            using MemoryStream stream = new MemoryStream();
            serializer.Write((Char)'&', stream);

            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.IsInstanceOfType(value, typeof(Char));
            Assert.AreEqual((Char)'&', value);
        }

        [TestMethod]
        public void WriteStringValue()
        {
            using MemoryStream stream = new MemoryStream();
                serializer.Write((String)"The quick brown fox jumped over the lazy dog.", stream);
            
            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.IsInstanceOfType(value, typeof(String));
            Assert.AreEqual("The quick brown fox jumped over the lazy dog.", value);
        }

        [TestMethod]
        public void WriteInt32Array()
        {
            using MemoryStream stream = new MemoryStream();
            serializer.Write(new object?[] { 1, 2, 3, 4, 5 }, stream);
            
            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.IsInstanceOfType(value, typeof(object?[]));
            Assert.IsTrue(Enumerable.SequenceEqual(new object?[] { 1, 2, 3, 4, 5 }, (object?[])value));
        }

        [TestMethod]
        public void WriteEmptyArray()
        {
            using MemoryStream stream = new MemoryStream();
            serializer.Write(new object?[] { }, stream);
            
            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.IsInstanceOfType(value, typeof(object?[]));
            var array = value as object?[];
            Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void WriteObject()
        {
            using MemoryStream stream = new MemoryStream();
            serializer.Write(new Dictionary<string, object?> { { "Name", "Jack" }, { "Age", (Int32)59 } }, stream);
            
            stream.Seek(0, SeekOrigin.Begin);

            var value = deserializer.Load(stream);

            Assert.IsInstanceOfType(value, typeof(IDictionary<string, object?>));

            var dictionary = (IDictionary<string, object?>)value;
            Assert.AreEqual(2, dictionary.Count);
            Assert.AreEqual("Jack", dictionary["Name"]);
            Assert.AreEqual((Int32)59, dictionary["Age"]);
        }
    }
}