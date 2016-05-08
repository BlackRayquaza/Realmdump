﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace RealmdumpCmd.library.xml
{
    public class LanguageLibrary : IDisposable
    {
        private Dictionary<string, string> values { get; }

        public string this[string name] => values.ContainsKey(name) ? values[name] : name;

        public int Count => values.Count;

        public LanguageLibrary(string json)
        {
            values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public void Dispose()
        {
            values.Clear();
        }
    }
}