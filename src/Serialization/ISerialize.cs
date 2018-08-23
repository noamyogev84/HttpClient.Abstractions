using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Http.Abstractions.Serialization
{
    public interface ISerialize
    {
        T DeserializeContent<T>(string content);
        string SerializeContent<T>(T content);
    }
}
