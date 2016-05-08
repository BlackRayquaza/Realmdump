using Newtonsoft.Json;
using RealmdumpCmd.library;
using RealmdumpCmd.rotmg.api.account;
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
        public Resources Resources { get; set; }
        private Dictionary<string, string> accountsToLoad { get; set; }
        private List<Account> Accounts { get; }

        private readonly string[] platforms = { "kongregate", "steamworks", "kabam" };

        public Realmdump()
        {
            Resources = new Resources("resources/json/strings.json", "resources/xml/equip.xml",
                "resources/xml/players.xml", "resources/json/settings.json");
            Accounts = new List<Account>();

            WriteLine($"Items: {Resources.Objects.TypeToItem.Count}");
            WriteLine($"Players: {Resources.Classes.TypeToPlayer.Count}");
            WriteLine($"Language Strings: {Resources.Language.Count}");
        }

        public void Start()
        {
            if (Resources.Settings.GetValue<bool>("debug"))
                WriteLine("debug!");
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
                    if (Resources.Settings.GetValue<bool>("displayAccError"))
                        WriteLine($"{account.Key} => {Resources.Language[resp.Element("Error").Value]}");
                    continue;
                }
                if (resp.Element("Chars").Element("Account").Element("AccountId").Value == "-1")
                {
                    if (Resources.Settings.GetValue<bool>("displayAccError"))
                        WriteLine($"{account.Key} => Not a valid account");
                    continue;
                }
                Accounts.Add(new Account(resp.Element("Chars"), Resources.Language));
            }
            accountsToLoad.Clear();
            WriteLine($"Loaded {Accounts.Count} out of {count} accounts");
        }

        private void showStats()
        {
            var sb = new StringBuilder();
            int iterations;

            #region init

            var accountsCount = Accounts.Count;

            var goldSum = Accounts.Sum(_ => _.Credits);
            var goldAverage = (int)Accounts.Average(_ => _.Credits);
            var accountsGold = Accounts.OrderByDescending(_ => _.Credits).ThenBy(_ => _.Name);
            var goldMost =
                Accounts.Where(x => x.Credits == accountsGold.First().Credits).OrderBy(_ => _.Name).ToList();
            var goldUseMost = goldMost.Count > 1;
            var goldLeast =
                Accounts.Where(x => x.Credits == accountsGold.Last().Credits).OrderBy(_ => _.Name).ToList();
            var goldUseLeast = goldLeast.Count > 1;

            var fameSum = Accounts.Sum(_ => _.Stats.Fame);
            var fameAverage = (int)Accounts.Average(_ => _.Stats.Fame);
            var accountsFame = Accounts.OrderByDescending(_ => _.Stats.Fame).ThenBy(_ => _.Name);
            var fameMost =
                Accounts.Where(x => x.Stats.Fame == accountsFame.First().Stats.Fame)
                        .OrderBy(_ => _.Name)
                        .ToList();
            var fameUseMost = fameMost.Count > 1;
            var fameLeast =
                Accounts.Where(x => x.Stats.Fame == accountsFame.Last().Stats.Fame)
                        .OrderBy(_ => _.Name)
                        .ToList();
            var fameUseLeast = fameLeast.Count > 1;

            var fortuneTokens = Accounts.All(_ => _.FortuneTokens > 0);
            var tokensSum = Accounts.Sum(_ => _.FortuneTokens);
            var tokensAverage = (int)Accounts.Average(_ => _.FortuneTokens);
            var accountsTokens = Accounts.OrderByDescending(_ => _.FortuneTokens).ThenBy(_ => _.Name);

            var totalFameSum = Accounts.Sum(_ => _.Stats.TotalFame);
            var totalFameAverage = (int)Accounts.Average(_ => _.Stats.TotalFame);
            var accountsTotalFame = Accounts.OrderByDescending(_ => _.Stats.TotalFame).ThenBy(_ => _.Name);
            var totalFameMost =
                Accounts.Where(x => x.Stats.TotalFame == accountsTotalFame.First().Stats.TotalFame)
                        .OrderBy(_ => _.Name)
                        .ToList();
            var useTotalFameMost = totalFameMost.Count > 1;
            var totalFameLeast =
                Accounts.Where(x => x.Stats.TotalFame == accountsTotalFame.Last().Stats.TotalFame)
                        .OrderBy(_ => _.Name)
                        .ToList();
            var useTotalFameLeast = totalFameLeast.Count > 1;

            #endregion init

            #region Account

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
                WriteLine(sb.Append(")").ToString());
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
                WriteLine(sb.Append(")").ToString());
            }
            else
                WriteLine(sb.Append($"{accountsGold.Last().Credits} ({accountsGold.Last().Name})"));

            WriteLine($"Combined Fame: {fameSum}");
            WriteLine($"Average Fame: {fameAverage}");
            sb.Clear().Append("Most Fame: ");
            if (fameUseMost)
            {
                iterations = 0;
                foreach (var acc in fameMost)
                {
                    sb.Append(iterations == 0 ? $"{fameMost.First().Stats.Fame} (" : ", ");
                    sb.Append(acc.Name);
                    iterations++;
                }
                WriteLine(sb.Append(")").ToString());
            }
            else
                WriteLine(sb.Append($"{fameMost.Last().Credits} ({fameMost.Last().Name})"));

            sb.Clear().Append("Least Fame: ");
            if (fameUseLeast)
            {
                iterations = 0;
                foreach (var acc in fameLeast)
                {
                    sb.Append(iterations == 0 ? $"{fameLeast.First().Stats.Fame} (" : ", ");
                    sb.Append(acc.Name);
                    iterations++;
                }
                WriteLine(sb.Append(")").ToString());
            }
            else
                WriteLine(sb.Append($"{fameLeast.Last().Credits} ({fameLeast.Last().Name})"));

            if (fortuneTokens)
            {
                WriteLine($"Combined Fortune Tokens: {tokensSum}");
                WriteLine($"Average Fortune Tokens: {tokensAverage}");
                WriteLine($"Most Fortune Tokens: {accountsTokens.First().FortuneTokens} ({accountsTokens.First().Name})");
                WriteLine($"Least Fortune Tokens: {accountsTokens.Last().FortuneTokens} ({accountsTokens.Last().Name})");
            }

            #endregion Account

            #region Total Fame

            WriteLine($"Combined Total Fame: {totalFameSum}");
            WriteLine($"Average Total Fame: {totalFameAverage}");

            sb.Clear().Append("Most Total Fame: ");
            if (useTotalFameMost)
            {
                iterations = 0;
                foreach (var acc in totalFameMost)
                {
                    sb.Append(iterations == 0 ? $"{totalFameMost.First().Stats.TotalFame} (" : ", ");
                    sb.Append(acc.Name);
                    iterations++;
                }
                WriteLine(sb.Append(")").ToString());
            }
            WriteLine(sb.Append($"{totalFameMost.First().Stats.TotalFame} ({totalFameMost.First().Name})"));

            #region Least Total Fame

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

            #endregion Least Total Fame

            #endregion Total Fame

            #region Stars

            WriteLine($"Combined Stars: {Accounts.Sum(_ => _.Stats.Stars)}");
            WriteLine($"Average Stars: {(int)Accounts.Average(_ => _.Stats.Stars)}");

            #endregion Stars

            #region Characters

            WriteLine($"Total Characters: {Accounts.Sum(_ => _.Characters.Count)}");
            WriteLine($"Average Characters: {(int)Accounts.Average(_ => _.Characters.Count)}");
            //WriteLine($"Most Characters: {Accounts.OrderByDescending(_ => _.Characters.Count).First().Characters.Count} ({Accounts.OrderByDescending(_ => _.Characters.Count).First().Name})");
            //WriteLine(
            //    $"Most Character Experience: {Accounts.OrderByDescending(_ => _.Characters.OrderByDescending(x => x.Experience).First().Experience).First().Characters.OrderByDescending(_ => _.Experience).First().Experience} ({Accounts.OrderByDescending(_ => _.Characters.OrderByDescending(x => x.Experience).First().Experience).First().Name})");

            #endregion Characters
        }
    }
}