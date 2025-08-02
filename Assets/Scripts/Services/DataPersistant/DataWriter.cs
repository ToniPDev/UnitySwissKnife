using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Services.DataPersistant
{
    /// <summary>
    /// Clase que amplia y simplifica el uso de BinaryWriter.
    /// </summary>
    public class DataWriter
    {
        private readonly BinaryWriter _writer;

        public DataWriter (BinaryWriter writer)
        {
            _writer = writer;
        }

        public void Write(IEnumerable<object> value)
        {
            foreach (var o in value)
            {
                Action callback = Type.GetTypeCode(o.GetType()) switch
                {
                    TypeCode.Boolean => () => Write((bool)o),
                    TypeCode.Byte    => () => Write((byte)o),
                    TypeCode.Char    => () => Write((char)o),
                    TypeCode.Decimal => () => Write((decimal)o),
                    TypeCode.Double  => () => Write((double)o),
                    TypeCode.Int16   => () => Write((short)o),
                    TypeCode.Int32   => () => Write((int)o),
                    TypeCode.Int64   => () => Write((long)o),
                    TypeCode.SByte   => () => Write((sbyte)o),
                    TypeCode.Single  => () => Write((float)o),
                    TypeCode.String  => () => Write((string)o),
                    TypeCode.UInt16  => () => Write((ushort)o),
                    TypeCode.UInt32  => () => Write((uint)o),
                    TypeCode.UInt64  => () => Write((ulong)o),
                    _ => throw new ArgumentOutOfRangeException()
                };
                
                callback();
            }
        }

        public void Write (float value) => _writer.Write(value);
        
        public void Write (double value) => _writer.Write(value);

        public void Write (short value) => _writer.Write(value);
        
        public void Write (int value) => _writer.Write(value);
        
        public void Write (uint value) => _writer.Write(value);
        
        public void Write (ushort value) => _writer.Write(value);
        
        public void Write (ulong value) => _writer.Write(value);
        
        public void Write (long value) => _writer.Write(value);
        
        public void Write (decimal value) => _writer.Write(value);
        
        public void Write (sbyte value) => _writer.Write(value);
        
        public void Write (byte value) => _writer.Write(value);
        
        public void Write (bool value) => _writer.Write(value);
        
        public void Write (char value) => _writer.Write(value);
        
        public void Write (string value) => _writer.Write(value);

        public void Write (Color value) 
        {
            _writer.Write(value.r);
            _writer.Write(value.g);
            _writer.Write(value.b);
            _writer.Write(value.a);
        }

        public void Write (Quaternion value) 
        {
            _writer.Write(value.x);
            _writer.Write(value.y);
            _writer.Write(value.z);
            _writer.Write(value.w);
        }

        public void Write (Vector3 value) 
        {
            _writer.Write(value.x);
            _writer.Write(value.y);
            _writer.Write(value.z);
        }

        public void Write(DateTime time) => _writer.Write(time.ToBinary());

        public void Write (Random.State value) => _writer.Write(JsonUtility.ToJson(value));
        
        public void Write<T> (T value) => _writer.Write(JsonUtility.ToJson(value));
        
        public void Write (byte[] value) => _writer.Write(value);
    }
}