using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace js
{

    public sealed class Deserializer
    {
        public object? Load(Stream stream)
        {
            using BinaryReader reader = new BinaryReader(stream, Encoding.UTF8);

            return ReadValue(reader);
        }

        private object? ReadValue(BinaryReader reader)
        {
            var valueType = ReadValueType(reader);

            return ReadValue(valueType, reader);
        }

        private object? ReadValue(ValueTypeEnum valueType, BinaryReader reader)
        {
            return valueType switch
            {
                ValueTypeEnum.Null => ReadNull(valueType, reader),
                ValueTypeEnum.Int16 => ReadInt16(valueType, reader),
                ValueTypeEnum.Int32 => ReadInt32(valueType, reader),
                ValueTypeEnum.Int64 => ReadInt64(valueType, reader),
                ValueTypeEnum.UInt16 => ReadUInt16(valueType, reader),
                ValueTypeEnum.UInt32 => ReadUInt32(valueType, reader),
                ValueTypeEnum.UInt64 => ReadUInt64(valueType, reader),
                ValueTypeEnum.Byte => ReadByte(valueType, reader),
                ValueTypeEnum.SByte => ReadSByte(valueType, reader),
                ValueTypeEnum.Boolean => ReadBoolean(valueType, reader),
                ValueTypeEnum.Decimal => ReadDecimal(valueType, reader),
                ValueTypeEnum.Float32 => ReadFloat32(valueType, reader),
                ValueTypeEnum.Float64 => ReadFloat64(valueType, reader),
                ValueTypeEnum.Date => ReadDate(valueType, reader),
                ValueTypeEnum.Time => ReadTime(valueType, reader),
                ValueTypeEnum.DateTime => ReadDateTime(valueType, reader),
                ValueTypeEnum.Char => ReadChar(valueType, reader),
                ValueTypeEnum.String => ReadString(valueType, reader),
                ValueTypeEnum.Array => ReadArray(valueType, reader),
                ValueTypeEnum.Object => ReadObject(valueType, reader),
                _ => throw new Exception()
            };
        }

        private ValueTypeEnum ReadValueType(BinaryReader streamReader)
        {
            Int16 valueTypeInt = streamReader.ReadInt16();
            return (ValueTypeEnum)valueTypeInt;
        }

        private object? ReadNull(ValueTypeEnum valueType, BinaryReader reader)
        {
            Debug.Assert(valueType == ValueTypeEnum.Null);
            return null;
        }

        private Int16 ReadInt16(ValueTypeEnum valueType, BinaryReader reader)
        {
            Debug.Assert(valueType == ValueTypeEnum.Int16);
            Debug.Assert(reader.BaseStream.Length - reader.BaseStream.Position >= sizeof(Int16));
            var value = reader.ReadInt16();

            return value;
        }

        private Int32 ReadInt32(ValueTypeEnum valueType, BinaryReader reader)
        {
            Debug.Assert(valueType == ValueTypeEnum.Int32);
            Debug.Assert(reader.BaseStream.Length - reader.BaseStream.Position >= sizeof(Int32));
            var value = reader.ReadInt32();

            return value;
        }

        private Int64 ReadInt64(ValueTypeEnum valueType, BinaryReader reader)
        {
            Debug.Assert(valueType == ValueTypeEnum.Int64);
            Debug.Assert(reader.BaseStream.Length - reader.BaseStream.Position >= sizeof(Int64));
            var value = reader.ReadInt64();

            return value;
        }

        private UInt16 ReadUInt16(ValueTypeEnum valueType, BinaryReader reader)
        {
            Debug.Assert(valueType == ValueTypeEnum.UInt16);
            Debug.Assert(reader.BaseStream.Length - reader.BaseStream.Position >= sizeof(UInt16));
            var value = reader.ReadUInt16();

            return value;
        }

        private UInt32 ReadUInt32(ValueTypeEnum valueType, BinaryReader reader)
        {
            Debug.Assert(valueType == ValueTypeEnum.UInt32);
            Debug.Assert(reader.BaseStream.Length - reader.BaseStream.Position >= sizeof(UInt32));
            var value = reader.ReadUInt32();

            return value;
        }

        private UInt64 ReadUInt64(ValueTypeEnum valueType, BinaryReader reader)
        {
            Debug.Assert(valueType == ValueTypeEnum.UInt64);
            Debug.Assert(reader.BaseStream.Length - reader.BaseStream.Position >= sizeof(UInt64));
            var value = reader.ReadUInt64();

            return value;
        }

        private Byte ReadByte(ValueTypeEnum valueType, BinaryReader reader)
        {
            Debug.Assert(valueType == ValueTypeEnum.Byte);
            Debug.Assert(reader.BaseStream.Length - reader.BaseStream.Position >= sizeof(Byte));
            var value = reader.ReadByte();

            return value;
        }

        private SByte ReadSByte(ValueTypeEnum valueType, BinaryReader reader)
        {
            Debug.Assert(valueType == ValueTypeEnum.SByte);
            Debug.Assert(reader.BaseStream.Length - reader.BaseStream.Position >= sizeof(SByte));
            var value = reader.ReadSByte();

            return value;
        }

        private Boolean ReadBoolean(ValueTypeEnum valueType, BinaryReader reader)
        {
            Debug.Assert(valueType == ValueTypeEnum.Boolean);
            Debug.Assert(reader.BaseStream.Length - reader.BaseStream.Position >= sizeof(Boolean));
            var value = reader.ReadBoolean();

            return value;
        }

        private Decimal ReadDecimal(ValueTypeEnum valueType, BinaryReader reader)
        {
            Debug.Assert(valueType == ValueTypeEnum.Decimal);
            Debug.Assert(reader.BaseStream.Length - reader.BaseStream.Position >= sizeof(Decimal));
            var value = reader.ReadDecimal();

            return value;
        }

        private Single ReadFloat32(ValueTypeEnum valueType, BinaryReader reader)
        {
            Debug.Assert(valueType == ValueTypeEnum.Float32);
            Debug.Assert(reader.BaseStream.Length - reader.BaseStream.Position >= sizeof(Single));
            var value = reader.ReadSingle();

            return value;
        }

        private Double ReadFloat64(ValueTypeEnum valueType, BinaryReader reader)
        {
            Debug.Assert(valueType == ValueTypeEnum.Float64);
            Debug.Assert(reader.BaseStream.Length - reader.BaseStream.Position >= sizeof(Double));
            var value = reader.ReadDouble();

            return value;
        }

        private DateOnly ReadDate(ValueTypeEnum valueType, BinaryReader reader)
        {
            Debug.Assert(valueType == ValueTypeEnum.Date);
            Debug.Assert(reader.BaseStream.Length - reader.BaseStream.Position >= sizeof(Int32) * 3);
            var y = reader.ReadInt32();
            var m = reader.ReadInt32();
            var d = reader.ReadInt32();

            return new DateOnly(y, m, d);
        }

        private TimeOnly ReadTime(ValueTypeEnum valueType, BinaryReader reader)
        {
            Debug.Assert(valueType == ValueTypeEnum.Time);
            Debug.Assert(reader.BaseStream.Length - reader.BaseStream.Position >= sizeof(Int32) * 5);
            var hh = reader.ReadInt32();
            var mm = reader.ReadInt32();
            var ss = reader.ReadInt32();
            var mi = reader.ReadInt32();
            var ml = reader.ReadInt32();

            return new TimeOnly(hh, mm, ss, mi, ml);
        }

        private DateTimeOffset ReadDateTime(ValueTypeEnum valueType, BinaryReader reader)
        {
            Debug.Assert(valueType == ValueTypeEnum.DateTime);
            Debug.Assert(reader.BaseStream.Length - reader.BaseStream.Position >= sizeof(Int64));
            var value = reader.ReadInt64();
            var datetime = DateTimeOffset.FromUnixTimeMilliseconds(value);

            return datetime;
        }

        private Char ReadChar(ValueTypeEnum valueType, BinaryReader reader)
        {
            Debug.Assert(valueType == ValueTypeEnum.Char);
            var value = reader.ReadChar();

            return value;
        }

        private String ReadString(ValueTypeEnum valueType, BinaryReader reader)
        {
            Debug.Assert(valueType == ValueTypeEnum.String);
            var value = reader.ReadString();

            return value;
        }

        private object?[] ReadArray(ValueTypeEnum valueType, BinaryReader reader)
        {
            Debug.Assert(valueType == ValueTypeEnum.Array);

            var innerType = ReadValueType(reader);
            var count = reader.ReadInt32();
            var list = new List<object?>(count);
            for (int i = 0; i < count; i++)
            {
                list.Add(ReadValue(innerType, reader));
            }

            return list.ToArray();
        }

        private IDictionary<string, object?> ReadObject(ValueTypeEnum valueType, BinaryReader reader)
        {
            Debug.Assert(valueType == ValueTypeEnum.Object);
            var count = reader.ReadInt32();
            var list = new Dictionary<string, object?>(count);
            for (int i = 0; i < count; i++)
            {
                var key = reader.ReadString();
                var value = this.ReadValue(reader);
                list.Add(key, value);
            }

            return list;
        }
    }
}