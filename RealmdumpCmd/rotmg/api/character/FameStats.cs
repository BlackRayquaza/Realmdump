using System;
using System.IO;
using System.Net;
using System.Text;

namespace RealmdumpCmd.rotmg.api.character
{
    public class FameStats
    {
        public int Shots { get; set; }
        public int ShotsThatDamage { get; set; }
        public int SpecialAbilityUses { get; set; }
        public int TilesUncovered { get; set; }
        public int Teleports { get; set; }
        public int PotionsDrunk { get; set; }
        public int MonsterKills { get; set; }
        public int MonsterAssists { get; set; }
        public int GodKills { get; set; }
        public int GodAssists { get; set; }
        public int CubeKills { get; set; }
        public int OryxKills { get; set; }
        public int QuestsCompleted { get; set; }
        public int PirateCavesCompleted { get; set; }
        public int UndeadLairsCompleted { get; set; }
        public int AbyssOfDemonsCompleted { get; set; }
        public int SnakePitsCompleted { get; set; }
        public int SpiderDensCompleted { get; set; }
        public int SpriteWorldsCompleted { get; set; }
        public int LevelUpAssists { get; set; }
        public int MinutesActive { get; set; }
        public int TombsCompleted { get; set; }
        public int TrenchesCompleted { get; set; }
        public int JunglesCompleted { get; set; }
        public int ManorsCompleted { get; set; }

        public FameStats(string pcstats)
        {
            var reader = new NReader(new MemoryStream(Convert.FromBase64String(pcstats.Replace('-', '+').Replace('_', '/'))));
            do
            {
                var id = reader.ReadByte();
                switch (id)
                {
                    case 0: Shots = reader.ReadInt32(); break;
                    case 1: ShotsThatDamage = reader.ReadInt32(); break;
                    case 2: SpecialAbilityUses = reader.ReadInt32(); break;
                    case 3: TilesUncovered = reader.ReadInt32(); break;
                    case 4: Teleports = reader.ReadInt32(); break;
                    case 5: PotionsDrunk = reader.ReadInt32(); break;
                    case 6: MonsterKills = reader.ReadInt32(); break;
                    case 7: MonsterAssists = reader.ReadInt32(); break;
                    case 8: GodKills = reader.ReadInt32(); break;
                    case 9: GodAssists = reader.ReadInt32(); break;
                    case 10: CubeKills = reader.ReadInt32(); break;
                    case 11: OryxKills = reader.ReadInt32(); break;
                    case 12: QuestsCompleted = reader.ReadInt32(); break;
                    case 13: PirateCavesCompleted = reader.ReadInt32(); break;
                    case 14: UndeadLairsCompleted = reader.ReadInt32(); break;
                    case 15: AbyssOfDemonsCompleted = reader.ReadInt32(); break;
                    case 16: SnakePitsCompleted = reader.ReadInt32(); break;
                    case 17: SpiderDensCompleted = reader.ReadInt32(); break;
                    case 18: SpriteWorldsCompleted = reader.ReadInt32(); break;
                    case 19: LevelUpAssists = reader.ReadInt32(); break;
                    case 20: MinutesActive = reader.ReadInt32(); break;
                    case 21: TombsCompleted = reader.ReadInt32(); break;
                    case 22: TrenchesCompleted = reader.ReadInt32(); break;
                    case 23: JunglesCompleted = reader.ReadInt32(); break;
                    case 24: ManorsCompleted = reader.ReadInt32(); break;
                }
            } while (reader.PeekChar() != -1);
        }
    }

    public class NReader : BinaryReader
    {
        public NReader(Stream s) : base(s, Encoding.UTF8)
        {
        }

        public override short ReadInt16() => IPAddress.NetworkToHostOrder(base.ReadInt16());

        public override int ReadInt32() => IPAddress.NetworkToHostOrder(base.ReadInt32());

        public override long ReadInt64() => IPAddress.NetworkToHostOrder(base.ReadInt64());

        public override ushort ReadUInt16() => (ushort)IPAddress.NetworkToHostOrder((short)base.ReadUInt16());

        public override uint ReadUInt32() => (uint)IPAddress.NetworkToHostOrder((int)base.ReadUInt32());

        public override ulong ReadUInt64() => (ulong)IPAddress.NetworkToHostOrder((long)base.ReadUInt64());

        public override float ReadSingle()
        {
            var arr = ReadBytes(4);
            Array.Reverse(arr);
            return BitConverter.ToSingle(arr, 0);
        }

        public override double ReadDouble()
        {
            var arr = ReadBytes(8);
            Array.Reverse(arr);
            return BitConverter.ToDouble(arr, 0);
        }

        public string ReadNullTerminatedString()
        {
            var ret = new StringBuilder();
            var b = ReadByte();
            while (b != 0)
            {
                ret.Append((char)b);
                b = ReadByte();
            }
            return ret.ToString();
        }

        public string ReadUTF() => Encoding.UTF8.GetString(ReadBytes(ReadInt16()));

        public string Read32UTF() => Encoding.UTF8.GetString(ReadBytes(ReadInt32()));
    }
}