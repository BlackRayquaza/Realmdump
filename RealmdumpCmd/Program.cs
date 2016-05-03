using Newtonsoft.Json;
using RealmdumpCmd.library.xml;
using RealmdumpCmd.rotmg.api;
using RealmdumpCmd.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;

namespace RealmdumpCmd
{
    internal class Program
    {
        private static XmlLibrary XmlLibrary { get; set; }
        private static LanguageLibrary LanguageLibrary { get; set; }
        private static Dictionary<string, string> AccountsToLoad { get; set; }
        private static List<Account> Accounts { get; set; }

        private static WebClient WebClient { get; set; }
        private static readonly string[] platforms = { "kongregate", "steamworks", "kabam" };

        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            LanguageLibrary = new LanguageLibrary(File.ReadAllText("stuff/json/strings.json"));
            XmlLibrary = new XmlLibrary(LanguageLibrary);

            Console.WriteLine($"Items: {XmlLibrary.ObjectLibrary.TypeToItem.Count}");
            Console.WriteLine($"Players: {XmlLibrary.ClassLibrary.TypeToPlayer.Count}");
            Console.WriteLine($"Language Strings: {LanguageLibrary.Names.Count}");

            if (!File.Exists("stuff/json/accounts.json"))
            {
                Console.WriteLine("Cant find accounts file.");
                Console.ReadLine();
                return;
            }

            AccountsToLoad =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(
                    File.ReadAllText("stuff/json/accounts.json"));
            Accounts = new List<Account>();
            WebClient = new WebClient();

            var count = AccountsToLoad.Count;

            foreach (var account in AccountsToLoad)
            {
                var resp =
                    XDocument.Parse(
                        WebClient.DownloadString(platforms.Contains(account.Key.Split(':')[0])
                            ? $"http://realmofthemadgodhrd.appspot.com/char/list?guid={HttpUtility.UrlEncode(account.Key)}&secret={account.Value}"
                            : $"http://realmofthemadgodhrd.appspot.com/char/list?guid={HttpUtility.UrlEncode(account.Key)}&password={account.Value}"),
                        LoadOptions.None);
                if (resp.HasElement("Error"))
                {
                    if (!LanguageLibrary.Names.ContainsKey(resp.Element("Error").Value))
                    {
                        Console.WriteLine($"{account.Key} => {resp.Element("Error").Value}");
                        continue;
                    }
                    Console.WriteLine($"{account.Key} => {LanguageLibrary.Names[resp.Element("Error").Value]}");
                    continue;
                }
                if (resp.Element("Chars").Element("Account").Element("AccountId").Value == "-1")
                {
                    Console.WriteLine($"{account.Key} => Not a valid account");
                    continue;
                }
                Accounts.Add(new Account(resp.Element("Chars")));
            }

            Console.WriteLine($"Loaded {Accounts.Count} out of {count} accounts");

            Console.ReadLine();
        }
    }
}