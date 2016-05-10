using System;
using System.IO;
using System.Xml.Linq;
using RealmdumpCmd.library.xml;

namespace RealmdumpCmd.library
{
    public class Resources : IDisposable
    {
        public LanguageLibrary Language { get; set; }
        public ClassLibrary Classes { get; set; }
        public ObjectLibrary Objects { get; set; }
        public Settings Settings { get; set; }

        public Resources(string languagePath, string objectsPath, string classesPath, string settingsPath)
        {
            Language = new LanguageLibrary(File.ReadAllText(languagePath));
            Objects = new ObjectLibrary(XElement.Parse(File.ReadAllText(objectsPath)), Language);
            Classes = new ClassLibrary(XElement.Parse(File.ReadAllText(classesPath)), Language);
            Settings = new Settings(File.ReadAllText(settingsPath));
        }

        public void Dispose()
        {
            Language.Dispose();
            Objects.Dispose();
            Classes.Dispose();
            Settings.Dispose();
        }
    }
}
