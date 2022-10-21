using js;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jstest
{
    sealed class TestClassItem
    {
        public int Value { get; set; }
    }

    sealed class TestClassInner
    {
        public int Value { get; set; } = 42;

        public object? Null { get; set; } = null;

        public ICollection<int> Numbers { get; set; } = new int[] { 1, 2, 3, 4, 5 };
        public ICollection<int> EmptyCollection { get; set; } = Array.Empty<int>();
        public ICollection<int> NullCollection { get; set; } = null;
    }

    sealed class TestClassOuter
    {
        public string Message { get; set; } = "A Message.";

        public TestClassInner Inner { get; set; } = new TestClassInner();

        public ICollection<TestClassItem> Items { get; set; } = new List<TestClassItem> { 
            new TestClassItem() { Value = 56 }, 
            new TestClassItem() { Value = 59 } 
        };

        [NotSerialized]
        public int NotSerialized { get; set; } = -1;
    }

    sealed class ResultClassItem
    {
        public int Value { get; set; }
    }

    sealed class ResultClassInner
    {
        public int Value { get; set; } = -1;

        public object? Null { get; set; } = new object();
        public ICollection<int> Numbers { get; set; } = Array.Empty<int>();
        public ICollection<int> EmptyCollection { get; set; } = Array.Empty<int>();
        public ICollection<int> NullCollection { get; set; } = null;
    }

    sealed class ResultClassOuter
    {
        public string Message { get; set; } = String.Empty;

        public ResultClassInner Inner { get; set; } = null;

        public ICollection<ResultClassItem> Items { get; set; }

        [NotSerialized]
        public int NotSerialized { get; set; } = -2;
    }

    [TestClass]
    public class ObjectSerializerTests
    {
        [TestMethod]
        public void SerializeObjectDictionary()
        {
            var testClass = new TestClassOuter();
            var objectSerializer = new ObjectSerializer();
            var dic = objectSerializer.ObjectToDictionary(testClass);
            Assert.AreEqual(3, dic.Count);
            Assert.AreEqual("A Message.", dic["Message"]);
            var innerDictionary = dic["Inner"] as IDictionary<string, object?>;
            Assert.IsNotNull(innerDictionary);
            Assert.AreEqual(5, innerDictionary.Count);
            Assert.AreEqual(42, innerDictionary["Value"]);
            Assert.IsInstanceOfType(dic["Items"], typeof(Array));
            var itemArray = (object?[])dic["Items"];
            Assert.AreEqual(2, itemArray.Length);
            Assert.IsInstanceOfType(itemArray[0], typeof(IDictionary<string, object?>));
            var singleItemObject = (IDictionary<string, object?>)itemArray[0];
            Assert.AreEqual(56, singleItemObject["Value"]);
            Assert.IsNull(innerDictionary["Null"]);
            Assert.IsNull(innerDictionary["NullCollection"]);
            Assert.IsInstanceOfType(innerDictionary["EmptyCollection"], typeof(Array));
            var emptyArray = (Array)innerDictionary["EmptyCollection"];
            Assert.AreEqual(0, emptyArray.Length);
            Assert.IsInstanceOfType(innerDictionary["Numbers"], typeof(object[]));
            for (int i=0; i < 5; i++)
            {
                Assert.AreEqual(i+1, (int)((object[])innerDictionary["Numbers"])[i]);
            }
        }

        [TestMethod]
        public void SerializeObject()
        {
            var serializer = new ObjectSerializer();
            var objectStream = serializer.SerializeObject(new TestClassOuter());
            Assert.AreEqual(181, objectStream.Length);

            var deserializer = new Deserializer();
            var payload = deserializer.Load(objectStream);
            Assert.IsInstanceOfType(payload, typeof(IDictionary<string, object?>));
            var outerClass = payload as IDictionary<string, object?>;
            Assert.IsNotNull(outerClass);
            Assert.AreEqual(3, outerClass.Count);
            Assert.AreEqual("A Message.", outerClass["Message"]);
            Assert.IsInstanceOfType(outerClass["Inner"], typeof(IDictionary<string, object?>));
            var innerClass = outerClass["Inner"] as IDictionary<string, object?>;
            Assert.IsNotNull(innerClass);
            Assert.AreEqual(5, innerClass.Count);
            Assert.AreEqual(42, innerClass["Value"]);
            Assert.IsNull(innerClass["Null"]);
            Assert.IsNull(innerClass["NullCollection"]);
            Assert.IsInstanceOfType(innerClass["EmptyCollection"], typeof(Array));
            var emptyArray = (Array)innerClass["EmptyCollection"];
            Assert.AreEqual(0, emptyArray.Length);
            Assert.IsInstanceOfType(innerClass["Numbers"], typeof(object[]));
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(i + 1, (int)((object[])innerClass["Numbers"])[i]);
            }
            Assert.IsInstanceOfType(outerClass["Items"], typeof(Array));
            var itemArray = (object?[])outerClass["Items"];
            Assert.AreEqual(2, itemArray.Length);
            Assert.IsInstanceOfType(itemArray[0], typeof(IDictionary<string, object?>));
            var singleItemObject = (IDictionary<string, object?>)itemArray[0];
            Assert.AreEqual(56, singleItemObject["Value"]);

        }

        [TestMethod]
        public void DeserializeObject()
        {
            var serializer = new ObjectSerializer();
            var objectStream = serializer.SerializeObject(new TestClassOuter());
            Assert.AreEqual(181, objectStream.Length);

            var deserializer = new ObjectDeserializer();
            var result = deserializer.DeserializeObject<ResultClassOuter>(objectStream);

            Assert.IsNotNull(result);
            Assert.AreEqual("A Message.", result.Message);
            Assert.AreEqual(-2, result.NotSerialized);
            Assert.AreEqual(42, result.Inner.Value);
            Assert.AreEqual(null, result.Inner.Null);
            Assert.AreEqual(5, result.Inner.Numbers.Count);
            Assert.AreEqual(2, result.Items.Count);
            Assert.AreEqual(56, result.Items.First().Value);
            Assert.AreEqual(59, result.Items.Last().Value);
            Assert.IsNull(result.Inner.NullCollection);
            Assert.AreEqual(0, result.Inner.EmptyCollection.Count);
        }

    }
}
