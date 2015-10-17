﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mina.Core.Buffer;

namespace FSO.Common.Serialization.TypeSerializers
{
    public class cTSOValueByteVector : ITypeSerializer
    {
        private readonly uint CLSID = 0x097608AB;

        public bool CanDeserialize(uint clsid)
        {
            return clsid == CLSID;
        }

        public bool CanSerialize(Type type)
        {
            return type.IsAssignableFrom(typeof(IList<byte>));
        }

        public object Deserialize(uint clsid, IoBuffer buffer, ISerializationContext serializer)
        {
            var result = new List<byte>();
            var count = buffer.GetUInt32();
            for(int i=0; i < count; i++){
                result.Add(buffer.Get());
            }
            return result;
        }

        public void Serialize(IoBuffer output, object value, ISerializationContext serializer)
        {
            IList<byte> list = (IList<byte>)value;
            output.PutUInt32((uint)list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                output.Put(list[i]);
            }
        }

        public uint? GetClsid(object value)
        {
            return CLSID;
        }
    }
}