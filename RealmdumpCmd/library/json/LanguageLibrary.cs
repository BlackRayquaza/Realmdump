using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RealmdumpCmd.library.xml
{
    public class LanguageLibrary : IDisposable
    {
        private Dictionary<string, string> strings { get; }

        public string this[string key]
        {
            get
            {
                key = key.Trim('{', '}');
                return strings.ContainsKey(key) ? strings[key] : key;
            }
        }

        public LanguageLibrary(string json)
        {
            strings = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public int Count => strings.Count;

        public string GetValue(string key) => this[key];

        public Dictionary<string, string> Values() => strings;

        public void Dispose()
        {
            strings.Clear();
        }
    }
}