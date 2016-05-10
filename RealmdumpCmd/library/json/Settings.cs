using Newtonsoft.Json;
using RealmdumpCmd.util;
using System;
using System.Collections.Generic;

namespace RealmdumpCmd.library.xml
{
    public class Settings : IDisposable
    {
        private Dictionary<string, string> values { get; }

        public int Count => values.Count;

        public Settings(string json)
        {
            values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public Dictionary<string, string> Values => values; 

        public T GetValue<T>(string key)
        {
            return values.ContainsKey(key) ? values[key].Convert<T>() : default(T);
        }

        public void Dispose()
        {
            values.Clear();
        }
    }
}