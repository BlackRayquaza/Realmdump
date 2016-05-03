using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;
using Newtonsoft.Json;
using RealmdumpCmd.library.xml;
using RealmdumpCmd.rotmg.api;
using RealmdumpCmd.util;

namespace RealmdumpCmd
{
    public class Realmdump
    {
        private XmlLibrary XmlLibrary { get; }
        private LanguageLibrary LanguageLibrary { get; }
        private Dictionary<string, string> accountsToLoad { get; set; }
        private List<Account> Accounts { get; }

        private WebClient WebClient { get; }
        private readonly string[] platforms = { "kongregate", "steamworks", "kabam" };

        public Realmdump()
        {
            LanguageLibrary = new LanguageLibrary(File.ReadAllText("stuff/json/strings.json"));
            XmlLibrary = new XmlLibrary(LanguageLibrary);
            Accounts = new List<Account>();
            WebClient = new WebClient();

            Console.WriteLine($"Items: {XmlLibrary.ObjectLibrary.TypeToItem.Count}");
            Console.WriteLine($"Players: {XmlLibrary.ClassLibrary.TypeToPlayer.Count}");
            Console.WriteLine($"Language Strings: {LanguageLibrary.Names.Count}");
        }

        public void Start()
        {
            checkAccountsFile("stuff/json/accounts.json");
            accountsToLoad = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("stuff/json/accounts.json"));
            setupAccounts();
            Console.ReadLine();
        }

        private void checkAccountsFile(string path)
        {
            if (File.Exists(path)) return;
            Console.WriteLine($"Cant find accounts file. Make sure {path} exists.");
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
            Environment.Exit(0);
        }

        private void setupAccounts()
        {
            var count = accountsToLoad.Count;

            foreach (var account in accountsToLoad)
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
        }
    }
}