using RealmdumpCmd.library.xml;
using System;
using System.IO;

namespace RealmdumpCmd
{
    internal class Program
    {
        public static XmlLibrary XmlLibrary { get; set; }
        public static LanguageLibrary LanguageLibrary { get; set; }

        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            LanguageLibrary = new LanguageLibrary(File.ReadAllText("stuff/json/strings.json"));
            XmlLibrary = new XmlLibrary(LanguageLibrary);

            Console.WriteLine($"Items: {XmlLibrary.ObjectLibrary.TypeToItem.Count}");
            Console.WriteLine($"Language Strings: {LanguageLibrary.Names.Count}");

            Console.ReadLine();

            XmlLibrary.Dispose();
            LanguageLibrary.Dispose();
        }
    }
}