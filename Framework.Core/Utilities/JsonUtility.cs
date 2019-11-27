using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Framework.Core.Utilities
{
    public static class JsonUtility
    {
        public static string ToJson(this object obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }
}
