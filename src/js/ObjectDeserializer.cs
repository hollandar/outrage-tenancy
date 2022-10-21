using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace js
{
    public sealed class ObjectDeserializer
    {
        Deserializer deserializer = new Deserializer();
        public ObjectDeserializer()
        {

        }

        public TResultType? DeserializeObject<TResultType>(Stream stream) where TResultType : class, new()
        {
            if (!stream.CanRead)
            {
                throw new Exception("Stream is unreadable.");
            }

            var payload = deserializer.Load(stream);
            if (payload is not IDictionary<string, object?>)
            {
                throw new Exception("Stream did not contain an object structure.");
            }

            return (TResultType?)DictionaryToObject((IDictionary<string, object?>)payload, typeof(TResultType));

        }

        private static HashSet<Type> AsValueTypes = new HashSet<Type>() { typeof(String) };

        private object? DictionaryToObject(IDictionary<string, object?> dictionary, Type type)
        {
            var payloadConstructor = type.GetConstructor(Type.EmptyTypes);
            if (payloadConstructor == null)
                throw new Exception($"Payload object {type.FullName} must have a default constructor.");

            var payloadObject = payloadConstructor.Invoke(null);
            var properties = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var property in properties)
            {
                var propertyType = property.PropertyType;
                if (property.GetCustomAttributes(typeof(NotSerializedAttribute), true)?.Any() ?? false)
                    continue;

                if (dictionary.ContainsKey(property.Name))
                {
                    var value = dictionary[property.Name];
                    property.SetValue(payloadObject, MapProperty(property.PropertyType, value));
                }
            }

            return payloadObject;
        }

        public object? MapProperty(Type propertyType, object? value)
        {
            if (value is IDictionary<string, object?> && propertyType.IsClass && !AsValueTypes.Contains(propertyType))
            {
                var innerDictionary = (IDictionary<string, object?>)value;
                var innerObject = DictionaryToObject(innerDictionary, propertyType);
                return innerObject;
            }

            else if (value is Array && !AsValueTypes.Contains(propertyType))
            {
                var enumerable = (Array)value;
                if (propertyType.IsGenericType)
                {

                    var genericType = propertyType.GetGenericTypeDefinition();
                    if (genericType == typeof(ICollection<>) || genericType == typeof(IEnumerable<>))
                    {
                        var innerTypes = propertyType.GetGenericArguments();
                        if (innerTypes.Length > 1)
                        {
                            throw new Exception($"Collection types only support one type parameter. {propertyType.ToString()} appears invalid.");
                        }

                        var innerType = innerTypes[0];
                        var collectionType = typeof(List<>).MakeGenericType(innerType);
                        var collectionConstructor = collectionType.GetConstructor(new Type[] { typeof(int) });
                        if (collectionConstructor == null) throw new Exception("Unable to find capacity constructor on list.");
                        var collectionInstance = collectionConstructor.Invoke(new object[] { enumerable.Length });
                        if (collectionInstance == null) throw new Exception("A List<> could not be created.");
                        var addMethod = collectionType.GetMethod("Add", BindingFlags.Public | BindingFlags.Instance, new Type[] { innerType });
                        if (addMethod == null) throw new Exception("Could not find Add method on List<>.");
                        foreach (var item in enumerable)
                        {
                            addMethod.Invoke(collectionInstance, new object[] { MapProperty(innerType, item) });
                        }

                        return collectionInstance;
                    }
                    else
                        throw new Exception("Only descendents of IEnumerable<> and ICollection<> are supported.");
                }
                else
                {
                    return value;
                }
            }

            else
            {
                return value;
            }
        }
    }
}
