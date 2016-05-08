using Newtonsoft.Json;
using RealmdumpCmd.library.xml;
using RealmdumpCmd.rotmg.api;
using RealmdumpCmd.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml.Linq;
using static System.Console;

namespace RealmdumpCmd
{
    public class Realmdump
    {
        private XmlLibrary XmlLibrary { get; }
        private LanguageLibrary LanguageLibrary { get; }
        private Dictionary<string, string> accountsToLoad { get; set; }
        private List<Account> Accounts { get; }

        private readonly string[] platforms = { "kongregate", "steamworks", "kabam" };

        public Realmdump()
        {
            LanguageLibrary = new LanguageLibrary(File.ReadAllText("resources/json/strings.json"));
            XmlLibrary = new XmlLibrary(LanguageLibrary);
            Accounts = new List<Account>();

            WriteLine($"Items: {XmlLibrary.ObjectLibrary.TypeToItem.Count}");
            WriteLine($"Players: {XmlLibrary.ClassLibrary.TypeToPlayer.Count}");
            WriteLine($"Language Strings: {LanguageLibrary.Names.Count}");
        }

        public void Start()
        {
            checkAccountsFile("resources/json/accounts.json");
            accountsToLoad = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("resources/json/accounts.json"));
            setupAccounts();
            showStats();
            ReadLine();
        }

        private void checkAccountsFile(string path)
        {
            if (File.Exists(path)) return;
            WriteLine($"Cant find accounts file. Make sure {path} exists.");
            WriteLine("Press enter to exit");
            ReadLine();
            Environment.Exit(0);
        }

        private void setupAccounts()
        {
            var webClient = new WebClient();
            var count = accountsToLoad.Count;
            foreach (var account in accountsToLoad)
            {
                var resp =
                    XDocument.Parse(
                       webClient.DownloadString(platforms.Contains(account.Key.Split(':')[0])
                            ? $"http://realmofthemadgodhrd.appspot.com/char/list?guid={HttpUtility.UrlEncode(account.Key)}&secret={account.Value}"
                            : $"http://realmofthemadgodhrd.appspot.com/char/list?guid={HttpUtility.UrlEncode(account.Key)}&password={account.Value}"),
                        LoadOptions.None);
                if (resp.HasElement("Error"))
                {
                    if (!LanguageLibrary.Names.ContainsKey(resp.Element("Error").Value))
                    {
                        WriteLine($"{account.Key} => {resp.Element("Error").Value}");
                        continue;
                    }
                    WriteLine($"{account.Key} => {LanguageLibrary.Names[resp.Element("Error").Value]}");
                    continue;
                }
                if (resp.Element("Chars").Element("Account").Element("AccountId").Value == "-1")
                {
                    WriteLine($"{account.Key} => Not a valid account");
                    continue;
                }
                Accounts.Add(new Account(resp.Element("Chars")));
            }
            accountsToLoad.Clear();
            WriteLine($"Loaded {Accounts.Count} out of {count} accounts");
        }

        private void showStats()
        {
            var sb = new StringBuilder();
            var iterations = 0;

            #region Account

            var accountsCount = Accounts.Count;

            var goldSum = Accounts.Sum(_ => _.Credits);
            var goldAverage = (int)Accounts.Average(_ => _.Credits);
            var accountsGold = Accounts.OrderByDescending(_ => _.Credits).ThenBy(_ => _.Name);
            var goldMost = Accounts.Where(x => x.Credits == accountsGold.First().Credits).ToList();
            var goldUseMost = goldMost.Count > 1;
            var goldLeast = Accounts.Where(x => x.Credits == accountsGold.Last().Credits).ToList();
            var goldUseLeast = goldLeast.Count > 1;

            var fameSum = Accounts.Sum(_ => _.Stats.Fame);
            var fameAverage = (int)Accounts.Average(_ => _.Stats.Fame);
            var accountsFame = Accounts.OrderByDescending(_ => _.Stats.Fame).ThenBy(_ => _.Name);

            var fortuneTokens = Accounts.All(_ => _.FortuneTokens > 0);
            var tokensSum = Accounts.Sum(_ => _.FortuneTokens);
            var tokensAverage = (int)Accounts.Average(_ => _.FortuneTokens);
            var accountsTokens = Accounts.OrderByDescending(_ => _.FortuneTokens).ThenBy(_ => _.Name);

            WriteLine($"Accounts: {accountsCount}");

            WriteLine($"Combined Gold: {goldSum}");
            WriteLine($"Average Gold: {goldAverage}");
            sb.Clear().Append("Most Gold: ");
            if (goldUseMost)
            {
                iterations = 0;
                foreach (var acc in goldMost)
                {
                    sb.Append(iterations == 0 ? $"{goldMost.First().Credits} (" : ", ");
                    sb.Append(acc.Name);
                    iterations++;
                }
                sb.Append(")");
                WriteLine(sb.ToString());
            }
            else
                WriteLine(sb.Append($"{accountsGold.First().Credits} ({accountsGold.First().Name})"));

            sb.Clear().Append("Least Gold: ");
            if (goldUseLeast)
            {
                iterations = 0;
                foreach (var acc in goldLeast)
                {
                    sb.Append(iterations == 0 ? $"{goldLeast.First().Credits} (" : ", ");
                    sb.Append(acc.Name);
                    iterations++;
                }
                sb.Append(")");
                WriteLine(sb.ToString());
            }
            else
                WriteLine(sb.Append($"{accountsGold.Last().Credits} ({accountsGold.Last().Name})"));

            WriteLine($"Combined Fame: {fameSum}");
            WriteLine($"Average Fame: {fameAverage}");
            WriteLine($"Most Fame: {accountsFame.First().Stats.Fame} ({accountsFame.First().Name})");
            WriteLine($"Least Fame: {accountsFame.Last().Stats.Fame} ({accountsFame.Last().Name})");

            if (fortuneTokens)
            {
                WriteLine($"Combined Fortune Tokens: {tokensSum}");
                WriteLine($"Average Fortune Tokens: {tokensAverage}");
                WriteLine($"Most Fortune Tokens: {accountsTokens.First().FortuneTokens} ({accountsTokens.First().Name})");
                WriteLine($"Least Fortune Tokens: {accountsTokens.Last().FortuneTokens} ({accountsTokens.Last().Name})");
            }

            #endregion Account

            //#region Total Fame

            //#region Most Total Fame

            //if (
            //    Accounts.Count(
            //        x => x.Stats.TotalFame == Accounts.OrderByDescending(y => y.Stats.TotalFame).First().Stats.TotalFame) >
            //    1)
            //{
            //    //var sb =
            //    new StringBuilder(
            //        $"Most Total Fame: {Accounts.OrderByDescending(_ => _.Stats.TotalFame).First().Stats.TotalFame} (");
            //    iterations = 0;
            //    foreach (var acc in Accounts.Where(
            //        x =>
            //            x.Stats.TotalFame ==
            //            Accounts.OrderByDescending(y => y.Stats.TotalFame)
            //                    .First()
            //                    .Stats.TotalFame))
            //    {
            //        if (iterations != 0)
            //            sb.Append(", ");
            //        sb.Append($"{acc.Name}");
            //        iterations++;
            //    }
            //    sb.Append(")");
            //    WriteLine(sb.ToString());
            //}
            //else
            //    WriteLine(
            //        $"Most Total Fame: {Accounts.OrderByDescending(_ => _.Stats.TotalFame).First().Stats.TotalFame} ({Accounts.OrderByDescending(_ => _.Stats.TotalFame).First().Name})");

            //#endregion Most Total Fame

            //#region Least Total Fame

            //if (
            //    Accounts.Count(
            //        x => x.Stats.TotalFame == Accounts.OrderBy(y => y.Stats.TotalFame).First().Stats.TotalFame) >
            //    1)
            //{
            //    //var sb =
            //    new StringBuilder(
            //        $"Least Total Fame: {Accounts.OrderBy(_ => _.Stats.TotalFame).First().Stats.TotalFame} (");
            //    iterations = 0;
            //    foreach (var acc in Accounts.Where(
            //        x =>
            //            x.Stats.TotalFame ==
            //            Accounts.OrderBy(y => y.Stats.TotalFame)
            //                    .First()
            //                    .Stats.TotalFame))
            //    {
            //        if (iterations != 0)
            //            sb.Append(", ");
            //        sb.Append($"{acc.Name}");
            //        iterations++;
            //    }
            //    sb.Append(")");
            //    WriteLine(sb.ToString());
            //}
            //else
            //    WriteLine(
            //        $"Least Total Fame: {Accounts.OrderBy(_ => _.Stats.TotalFame).First().Stats.TotalFame} ({Accounts.OrderBy(_ => _.Stats.TotalFame).First().Name})");

            //#endregion Least Total Fame

            //WriteLine($"Combined Total Fame: {Accounts.Sum(_ => _.Stats.TotalFame)}");
            //WriteLine($"Average Total Fame: {(int)Accounts.Average(_ => _.Stats.TotalFame)}");

            //#endregion Total Fame

            #region Stars

            WriteLine($"Combined Stars: {Accounts.Sum(_ => _.Stats.Stars)}");
            WriteLine($"Average Stars: {(int)Accounts.Average(_ => _.Stats.Stars)}");

            #endregion Stars

            //#region Characters

            //WriteLine($"Total Characters: {Accounts.Sum(_ => _.Characters.Count)}");
            //WriteLine($"Average Characters: {(int)Accounts.Average(_ => _.Characters.Count)}");
            //WriteLine($"Most Characters: {Accounts.OrderByDescending(_ => _.Characters.Count).First().Characters.Count} ({Accounts.OrderByDescending(_ => _.Characters.Count).First().Name})");
            //WriteLine(
            //    $"Most Character Expierence: {Accounts.OrderByDescending(_ => _.Characters.OrderByDescending(x => x.Experience).First().Experience).First().Characters.OrderByDescending(_ => _.Experience).First().Experience} ({Accounts.OrderByDescending(_ => _.Characters.OrderByDescending(x => x.Experience).First().Experience).First().Name})");

            //#endregion Characters
        }
    }
}