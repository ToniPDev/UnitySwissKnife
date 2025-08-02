using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Services.DataPersistant
{
    /// <summary>
    /// Clase que amplia y simplifica el uso de BinaryReader.
    /// </summary>
    public class DataReader
    {
        public int Version { get; }

        private readonly BinaryReader _reader;

        public DataReader (BinaryReader reader, int version) 
        {
            _reader = reader;
            Version = version;
        }

        public T Read<T>(IEnumerable<object> value) where T : struct
        {
            foreach (var o in value)
            {
                object callback = Type.GetTypeCode(o.GetType()) switch
                {
                    TypeCode.Boolean => ReadBool,
                    TypeCode.Byte    => ReadByte,
                    TypeCode.Char    => ReadChar,
                    TypeCode.Decimal => ReadDecimal,
                    TypeCode.Double  => ReadDouble,
                    TypeCode.Int16   => ReadShort,
                    TypeCode.Int32   => ReadInt,
                    TypeCode.Int64   => ReadLong,
                    TypeCode.SByte   => ReadSByte,
                    TypeCode.Single  => ReadFloat,
                    TypeCode.String  => ReadString,
                    TypeCode.UInt16  => ReadUShort,
                    TypeCode.UInt32  => ReadUInt,
                    TypeCode.UInt64  => ReadULong,
                    _ => throw new ArgumentOutOfRangeException()
                };
                
                return (T)callback;
            }

            return default;
        }

        public float ReadFloat => _reader.ReadSingle();
        
        public double ReadDouble =>_reader.ReadDouble();

        public short ReadShort => _reader.ReadInt16();
        
        public int ReadInt => _reader.ReadInt32();
        
        public ushort ReadUShort => _reader.ReadUInt16();
        
        public uint ReadUInt => _reader.ReadUInt32();

        public ulong ReadULong => _reader.ReadUInt64();
        
        public long ReadLong => _reader.ReadInt64();
        
        public decimal ReadDecimal => _reader.ReadDecimal();
        
        public sbyte ReadSByte => _reader.ReadSByte();
        
        public byte ReadByte => _reader.ReadByte();
        
        public byte[] ReadBytes(int byteCount) => _reader.ReadBytes(byteCount);

        public bool ReadBool => _reader.ReadBoolean();
        
        public char ReadChar => _reader.ReadChar();
        
        public string ReadString => _reader.ReadString();

        public Color ReadColor () 
        {
            Color value;
            value.r = _reader.ReadSingle();
            value.g = _reader.ReadSingle();
            value.b = _reader.ReadSingle();
            value.a = _reader.ReadSingle();
            return value;
        }

        public Quaternion ReadQuaternion () 
        {
            Quaternion value;
            value.x = _reader.ReadSingle();
            value.y = _reader.ReadSingle();
            value.z = _reader.ReadSingle();
            value.w = _reader.ReadSingle();
            return value;
        }

        public Random.State ReadRandomState () => JsonUtility.FromJson<Random.State>(_reader.ReadString());
        
        public T ReadJSon<T> () => JsonUtility.FromJson<T>(_reader.ReadString());
        
        public Vector3 ReadVector3 () 
        {
            Vector3 value;
            value.x = _reader.ReadSingle();
            value.y = _reader.ReadSingle();
            value.z = _reader.ReadSingle();
            return value;
        }
    }
}