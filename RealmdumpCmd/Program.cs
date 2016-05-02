using RealmdumpCmd.library.xml;
using System;
using System.IO;

namespace RealmdumpCmd
{
    internal class Program
    {
        private static XmlLibrary XmlLibrary { get; set; }
        private static LanguageLibrary LanguageLibrary { get; set; }

        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            LanguageLibrary = new LanguageLibrary(File.ReadAllText("stuff/json/strings.json"));
            XmlLibrary = new XmlLibrary(LanguageLibrary);

            Console.WriteLine($"Items: {XmlLibrary.ObjectLibrary.TypeToItem.Count}");
            Console.WriteLine($"Players: {XmlLibrary.ClassLibrary.TypeToPlayer.Count}");
            Console.WriteLine($"Language Strings: {LanguageLibrary.Names.Count}");

            Console.ReadLine();

            XmlLibrary.Dispose();
            LanguageLibrary.Dispose();
        }
    }
}