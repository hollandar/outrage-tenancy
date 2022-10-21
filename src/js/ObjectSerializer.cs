using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace js
{
    public sealed class ObjectSerializer
    {
        private readonly Serializer serializer = new Serializer();

        public ObjectSerializer()
        {

        }

        public Stream SerializeObject<TPayloadType>(TPayloadType payload)
        {
            var stream = new MemoryStream();
            SerializeObjectToStream(payload, stream);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        public void SerializeObjectToStream(object? payload, Stream stream)
        {
            var objectDictionary = ObjectToDictionary(payload);
            serializer.Write(objectDictionary, stream);
        }

        private static HashSet<Type> AsValueTypes = new HashSet<Type>() { typeof(String) };

        public IDictionary<string, object?> ObjectToDictionary(object payload)
        {
            var type = payload.GetType();
            var properties = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var propertyValues = new Dictionary<string, object?>();
            foreach (var property in properties.Where(r => r.CanRead))
            {
                var serializable = (NotSerializedAttribute?)property.GetCustomAttributes(typeof(NotSerializedAttribute), false).FirstOrDefault();
                if (serializable is not null)
                {
                    continue;
                }

                var key = property.Name;

                propertyValues[key] = GetPropertyValue(property.PropertyType, property.GetValue(payload));
            }

            return propertyValues;
        }

        public object? GetPropertyValue(Type type, object? value)
        {
            if (type.IsAssignableTo(typeof(IEnumerable)) && !AsValueTypes.Contains(type))
            {
                var collectionList = new List<object?>();
                var collection = value as IEnumerable;
                if (collection != null)
                {
                    foreach (var item in collection)
                    {
                        collectionList.Add(GetPropertyValue(item.GetType(), item));
                    }
                    return collectionList.ToArray();
                }
                else
                {
                    return null;
                }
            }
            else if (type.IsClass && !AsValueTypes.Contains(type))
            {
                var innerObject = value;
                if (innerObject is not null)
                {
                    return ObjectToDictionary(innerObject);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return value;
            }
        }
    }
}
