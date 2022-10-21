using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace js
{
    public sealed class Serializer
    {

        public void Write(object? value, Stream stream)
        {
            using var writer = new BinaryWriter(stream, encoding: Encoding.UTF8, leaveOpen: true);
            Write(value, writer);
        }

        public ValueTypeEnum ValueType(object? value)
        {
            if (value == null)
            {
                return ValueTypeEnum.Null;
            }

            else if (value is Int16)
            {
                return ValueTypeEnum.Int16;
            }

            else if (value is Int32)
            {
                return ValueTypeEnum.Int32;
            }

            else if (value is Int64)
            {
                return ValueTypeEnum.Int64;
            }

            else if (value is UInt16)
            {
                return ValueTypeEnum.UInt16;
            }

            else if (value is UInt32)
            {
                return ValueTypeEnum.UInt32;
            }

            else if (value is UInt64)
            {
                return ValueTypeEnum.UInt64;
            }

            else if (value is Byte)
            {
                return ValueTypeEnum.Byte;
            }

            else if (value is SByte)
            {
                return ValueTypeEnum.SByte;
            }

            else if (value is Boolean)
            {
                return ValueTypeEnum.Boolean;
            }

            else if (value is Decimal)
            {
                return ValueTypeEnum.Decimal;
            }

            else if (value is Single)
            {
                return ValueTypeEnum.Float32;
            }

            else if (value is Double)
            {
                return ValueTypeEnum.Float64;
            }

            else if (value is DateOnly)
            {
                return ValueTypeEnum.Date;
            }

            else if (value is TimeOnly)
            {
                return ValueTypeEnum.Time;
            }

            else if (value is DateTimeOffset)
            {
                return ValueTypeEnum.DateTime;
            }

            else if (value is Char)
            {
                return ValueTypeEnum.Char;
            }

            else if (value is String)
            {
                return ValueTypeEnum.String;
            }

            else if (value is object?[])
            {
                return ValueTypeEnum.Array;
            }

            else if (value is IDictionary<string, object?>)
            {
                return ValueTypeEnum.Object;
            }

            else throw new Exception($"Could not serialize type {value.GetType()?.Name ?? "<null>"}.");
        }

        public void Write(object? value, BinaryWriter writer)
        {
            var valueType = ValueType(value);
            writer.Write((Int16)valueType);
            Write(valueType, value, writer);
        }

        public void Write(ValueTypeEnum valueType, object? value, BinaryWriter writer)
        {
            switch (valueType)
            {
                case ValueTypeEnum.Null:
                    {
                        WriteNull(writer);
                        break;
                    }

                case ValueTypeEnum.Int16:
                    {
                        WriteInt16((Int16)value, writer);
                        break;
                    }

                case ValueTypeEnum.Int32:
                    {
                        WriteInt32((Int32)value, writer);
                        break;
                    }

                case ValueTypeEnum.Int64:
                    {
                        WriteInt64((Int64)value, writer);
                        break;
                    }

                case ValueTypeEnum.UInt16:
                    {
                        WriteUInt16((UInt16)value, writer);
                        break;
                    }

                case ValueTypeEnum.UInt32:
                    {
                        WriteUInt32((UInt32)value, writer);
                        break;
                    }

                case ValueTypeEnum.UInt64:
                    {
                        WriteUInt64((UInt64)value, writer);
                        break;
                    }

                case ValueTypeEnum.Byte:
                    {
                        WriteByte((Byte)value, writer);
                        break;
                    }

                case ValueTypeEnum.SByte:
                    {
                        WriteSByte((SByte)value, writer);
                        break;
                    }

                case ValueTypeEnum.Boolean:
                    {
                        WriteBoolean((Boolean)value, writer);
                        break;
                    }

                case ValueTypeEnum.Decimal:
                    {
                        WriteDecimal((Decimal)value, writer);
                        break;
                    }

                case ValueTypeEnum.Float32:
                    {
                        WriteFloat32((Single)value, writer);
                        break;
                    }

                case ValueTypeEnum.Float64:
                    {
                        WriteFloat64((Double)value, writer);
                        break;
                    }

                case ValueTypeEnum.Date:
                    {
                        WriteDate((DateOnly)value, writer);
                        break;
                    }

                case ValueTypeEnum.Time:
                    {
                        WriteTime((TimeOnly)value, writer);
                        break;
                    }

                case ValueTypeEnum.DateTime:
                    {
                        WriteDateTime((DateTimeOffset)value, writer);
                        break;
                    }

                case ValueTypeEnum.Char:
                    {
                        WriteChar((Char)value, writer);
                        break;
                    }

                case ValueTypeEnum.String:
                    {
                        WriteString((String)value, writer);
                        break;
                    }

                case ValueTypeEnum.Array:
                    {
                        WriteArray((object?[])value, writer);
                        break;
                    }

                case ValueTypeEnum.Object:
                    {
                        WriteObject((IDictionary<string, object?>)value, writer);
                        break;
                    }

                default: throw new Exception($"Could not serialize type {value.GetType()?.Name ?? "<null>"}.");
            }
        }

        private void WriteNull(BinaryWriter writer)
        {
        }

        private void WriteInt16(Int16 value, BinaryWriter writer)
        {
            writer.Write((Int16)value);
        }

        private void WriteInt32(Int32 value, BinaryWriter writer)
        {
            writer.Write((Int32)value);
        }

        private void WriteInt64(Int64 value, BinaryWriter writer)
        {
            writer.Write((Int64)value);
        }

        private void WriteUInt16(UInt16 value, BinaryWriter writer)
        {
            writer.Write((UInt16)value);
        }

        private void WriteUInt32(UInt32 value, BinaryWriter writer)
        {
            writer.Write((UInt32)value);
        }

        private void WriteUInt64(UInt64 value, BinaryWriter writer)
        {
            writer.Write((UInt64)value);
        }

        private void WriteByte(Byte value, BinaryWriter writer)
        {
            writer.Write((Byte)value);
        }

        private void WriteSByte(SByte value, BinaryWriter writer)
        {
            writer.Write((SByte)value);
        }

        private void WriteBoolean(Boolean value, BinaryWriter writer)
        {
            writer.Write((Boolean)value);
        }

        private void WriteDecimal(Decimal value, BinaryWriter writer)
        {
            writer.Write((Decimal)value);
        }

        private void WriteFloat32(Single value, BinaryWriter writer)
        {
            writer.Write((Single)value);
        }

        private void WriteFloat64(Double value, BinaryWriter writer)
        {
            writer.Write((Double)value);
        }

        private void WriteDate(DateOnly value, BinaryWriter writer)
        {
            writer.Write((Int32)value.Year);
            writer.Write((Int32)value.Month);
            writer.Write((Int32)value.Day);
        }

        private void WriteTime(TimeOnly value, BinaryWriter writer)
        {
            writer.Write((Int32)value.Hour);
            writer.Write((Int32)value.Minute);
            writer.Write((Int32)value.Second);
            writer.Write((Int32)value.Millisecond);
            writer.Write((Int32)value.Microsecond);
        }

        private void WriteDateTime(DateTimeOffset value, BinaryWriter writer)
        {
            writer.Write((Int64)value.ToUnixTimeMilliseconds());
        }

        private void WriteChar(Char value, BinaryWriter writer)
        {
            writer.Write((Char)value);
        }

        private void WriteString(String value, BinaryWriter writer)
        {
            writer.Write((String)value);
        }

        private void WriteArray(object?[] array, BinaryWriter writer)
        {
            if (array.Length > 0)
            {
                var valueType = ValueType(array[0]);
                writer.Write((Int16)valueType);
                writer.Write((Int32)array.Length);
                foreach (var item in array)
                {
                    Write(valueType, item, writer);
                }
            } else
            {
                writer.Write((Int16)ValueTypeEnum.Null);
                writer.Write((Int32)0);
            }
        }

        private void WriteObject(IDictionary<string, object?> obj, BinaryWriter writer)
        {
            writer.Write((Int32)obj.Count);
            foreach (var item in obj)
            {
                WriteString(item.Key, writer);
                Write(item.Value, writer);
            }
        }

    }
}
