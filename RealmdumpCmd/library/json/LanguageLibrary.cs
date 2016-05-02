using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RealmdumpCmd.library.xml
{
    public class LanguageLibrary : IDisposable
    {
        public Dictionary<string, string> Names { get; set; }

        public LanguageLibrary(string json)
        {
            Names = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public void Dispose()
        {
            Names.Clear();
        }
    }
}