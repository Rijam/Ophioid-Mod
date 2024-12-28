using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using System;
using OphioidMod.NPCs;
using OphioidMod.Items;
using OphioidMod.Projectiles;

namespace OphioidMod
{
     public static class Vector2Extension {
         public static Vector2 Rotate(this Vector2 v, float degrees) {
             float radians = (float)(degrees + 2.0* Math.PI);
             float sin = (float)Math.Sin(radians);
             float cos = (float)Math.Cos(radians);
         
             float tx = (float)v.X;
             float ty = (float)v.Y;
 
             return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
         }
     }

    public interface ISinkyBoss
    {

    }

    public enum MusicPriority
    {
        None,
        BiomeLow,
        BiomeMedium,
        BiomeHigh,
        Environment,
        Event,
        BossLow,
        BossMedium,
        BossHigh
    }

    class OphioidMod : Mod
    {
        public static OphioidMod Instance;
        //public static Mod Idglib;
        public OphioidMod()
        {

        }

        public override void Load()
        {
            Instance = this;
            Ophiofly.CustomWorldEvilForDeathMessage = "";
            if (ModLoader.TryGetMod("Wikithis", out Mod wikithis) && !Main.dedServ)
            {
                // The wiki is a little outdated. It will need to be moved to wiki.gg and updated for the 1.4 version.
                wikithis.Call("AddModURL", this, "https://terrariamods.fandom.com/wiki/Ophioid{}");
            }
        }

        public override void Unload()
        {
            Instance = null;
            Ophiofly.CustomWorldEvilForDeathMessage = "";
        }
        
        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("BossChecklist", out Mod bossList))
            {
                //bossList.Call("AddBossWithInfo", "Ophiopede", 9.05f, (Func<bool>)(() => OphioidWorld.downedOphiopede), string.Format("Use a [i:{0}] or [i:{1}] anywhere, anytime", ItemType("Deadfungusbug"), ItemType("Livingcarrion")));
                //bossList.Call("AddBossWithInfo", "Ophioid", 11.50f, (Func<bool>)(() => OphioidWorld.downedOphiopede2), string.Format("Use a [i:{0}] anywhere, anytime", ItemType("Infestedcompost")));
                /*
                bossList.Call("AddBoss", 11.05f, ModContent.NPCType<OphiopedeHead>(), this, "Ophiopede", (Func<bool>)(() => (OphioidWorld.downedOphiopede)), ModContent.ItemType<Deadfungusbug>(), new List<int>() {ModContent.ItemType<Ophiopedetrophyitem>(), ModContent.ItemType<OphiopedeMask>() }, new List<int>() {ItemID.SoulofFright, ItemID.SoulofLight, ItemID.SoulofMight, ItemID.SoulofNight, ItemID.SoulofSight, ModContent.ItemType<Livingcarrion>(), ModContent.ItemType<Deadfungusbug>() }, 
                    "Use a [i:" + ModContent.ItemType<Deadfungusbug>() + "] or [i:" + ModContent.ItemType<Livingcarrion>() + "] at any time", "Ophiopede tunnels away", "OphioidMod/BCLPede", "OphioidMod/icon_small");
                bossList.Call("AddBoss", 13.50f, ModContent.NPCType<Ophiofly>(), this, "Ophiopede & Ophiofly", (Func<bool>)(() => (OphioidWorld.downedOphiopede2)), ModContent.ItemType<Infestedcompost>(), new List<int>() { ModContent.ItemType<Ophiopedetrophyitem>(), ModContent.ItemType<OphiopedeMask>(), ModContent.ItemType<SporeInfestedEgg>() }, 
                    new List<int>() { ItemID.SoulofFright, ItemID.SoulofLight, ItemID.SoulofMight, ItemID.SoulofNight, ItemID.SoulofSight,ItemID.SoulofFlight, ItemID.FragmentSolar, ItemID.FragmentNebula, ItemID.FragmentVortex, ItemID.FragmentStardust }, 
                    "Use an [i:" + ModContent.ItemType<Infestedcompost>() + "] at any time after beating Ophiopede", "Ophioid slinks back into its hidden nest", "OphioidMod/BCLFly");
                */
                /*
                bossList.Call
                (
                    "AddBoss",
                    this,
                    "Ophiopede",
                    ModContent.NPCType<OphiopedeHead>(),
                    11.05f,
                    (Func<bool>)(() => (OphioidWorld.downedOphiopede)),
                    (Func<bool>)(() => true),
                    new List<int>() { ModContent.ItemType<Ophiopedetrophyitem>(), ModContent.ItemType<OphiopedeMask>(), ItemID.SoulofFright, ItemID.SoulofLight, ItemID.SoulofMight, ItemID.SoulofNight, ItemID.SoulofSight, ModContent.ItemType<Livingcarrion>(), ModContent.ItemType<Deadfungusbug>() },
                    new List<int> { ModContent.ItemType<Deadfungusbug>(), ModContent.ItemType<Livingcarrion>() },
                    "Use a [i:" + ModContent.ItemType<Deadfungusbug>() + "] or [i:" + ModContent.ItemType<Livingcarrion>() + "] at any time",
                    "Ophiopede tunnels away",
                    (Action<SpriteBatch, Rectangle, Color>)((SpriteBatch sb, Rectangle rect, Color color) =>
                    {
                        Texture2D texture = ModContent.Request<Texture2D>("OphioidMod/BCLPede").Value;
                        Vector2 centered = new(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2) - 20);
                        sb.Draw(texture, centered, color);
                    })
                );
                bossList.Call
                (
                    "AddBoss",
                    this,
                    "Ophiopede & Ophiofly",
                    ModContent.NPCType<Ophiofly>(),
                    13.05f,
                    (Func<bool>)(() => (OphioidWorld.downedOphiopede2)),
                    (Func<bool>)(() => true),
                    new List<int>() { ModContent.ItemType<Ophiopedetrophyitem>(), ModContent.ItemType<OphiopedeMask>(), ModContent.ItemType<SporeInfestedEgg>(), ItemID.SoulofFright, ItemID.SoulofLight, ItemID.SoulofMight, ItemID.SoulofNight, ItemID.SoulofSight, ItemID.SoulofFlight, ItemID.FragmentSolar, ItemID.FragmentNebula, ItemID.FragmentVortex, ItemID.FragmentStardust },
                    ModContent.ItemType<Infestedcompost>(),
                    "Use an [i:" + ModContent.ItemType<Infestedcompost>() + "] at any time after beating Ophiopede",
                    "Ophioid slinks back into its hidden nest",
                    (Action<SpriteBatch, Rectangle, Color>)((SpriteBatch sb, Rectangle rect, Color color) =>
                    {
                        Texture2D texture = ModContent.Request<Texture2D>("OphioidMod/BCLFly").Value;
                        Vector2 centered = new(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                        sb.Draw(texture, centered, color);
                    })
                );
                */
                bossList.Call
                (
                    "LogBoss",
                    this,
                    nameof(OphiopedeHead),
                    11.05f,
                    () => OphioidWorld.downedOphiopede,
                    new List<int> { ModContent.NPCType<OphiopedeHead>(), ModContent.NPCType<OphiopedeBody>(), ModContent.NPCType<OphiopedeTail>() },
                    new Dictionary<string, object>()
                    { 
                        { "spawnItems", new List<int> { ModContent.ItemType<DeadFungusbug>(), ModContent.ItemType<LivingCarrion>() } },
                        { "collectibles", new List<int> { ModContent.ItemType<Ophiopedetrophyitem>(), ModContent.ItemType<OphiopedeMask>(), ModContent.ItemType<MusicBoxMetamorphosis>() } },
                        { "customPortrait", (SpriteBatch sb, Rectangle rect, Color color) =>
                            {
                                Texture2D texture = ModContent.Request<Texture2D>("OphioidMod/BCLPede").Value;
                                Vector2 centered = new(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2) - 20);
                                sb.Draw(texture, centered, color);
                            }
                        }
                    }
                );

                bossList.Call
                (
                    "LogBoss",
                    this,
                    nameof(Ophiofly),
                    16.05f,
                    () => OphioidWorld.downedOphiopede2,
                    new List<int> { ModContent.NPCType<Ophiofly>(), ModContent.NPCType<OphiopedeHead2>(), ModContent.NPCType<OphiopedeBody>(), ModContent.NPCType<OphiopedeTail>() },
                    new Dictionary<string, object>()
                    {
                        { "spawnItems", new List<int> { ModContent.ItemType<InfestedCompost>() } },
                        { "collectibles", new List<int> { ModContent.ItemType<Ophiopedetrophyitem>(), ModContent.ItemType<OphiopedeMask>(), ModContent.ItemType<MusicBoxMetamorphosis>(), ModContent.ItemType<MusicBoxTheFly>(), ModContent.ItemType<SporeInfestedEgg>(), ModContent.ItemType<OphioidLarva>() } },
                        { "customPortrait", (SpriteBatch sb, Rectangle rect, Color color) =>
                            {
                                Texture2D texture = ModContent.Request<Texture2D>("OphioidMod/BCLFly").Value;
                                Vector2 centered = new(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                                sb.Draw(texture, centered, color);
                            }
                        }
                    }
                );
            }

            if (ModLoader.TryGetMod("Fargowiltas", out Mod fargosMutantMod))
            {
                fargosMutantMod.Call("AddSummon", 11.1f, "OphioidMod", "DeadFungusbug", (Func<bool>)(() => OphioidWorld.downedOphiopede == true), Item.buyPrice(0, 65, 0, 0));
                fargosMutantMod.Call("AddSummon", 11.1f, "OphioidMod", "LivingCarrion", (Func<bool>)(() => OphioidWorld.downedOphiopede == true), Item.buyPrice(0, 65, 0, 0));
                fargosMutantMod.Call("AddSummon", 16.1f, "OphioidMod", "InfestedCompost", (Func<bool>)(() => OphioidWorld.downedOphiopede2 == true), Item.buyPrice(0, 95, 0, 0));
            }

            /*
            //Idglib = ModLoader.GetMod("Idglib");

            Mod yabhb = ModLoader.GetMod("FKBossHealthBar");
            if(yabhb != null)
            {
                 yabhb.Call("hbStart");
                 yabhb.Call("hbSetTexture",
                 GetTexture("healtbar_left"),
                 GetTexture("healtbar_frame"),
                 GetTexture("healtbar_right"),
                 GetTexture("healtbar_fill"));
                yabhb.Call("hbSetMidBarOffset", -32, 12);
                 yabhb.Call("hbSetBossHeadCentre", 80, 32);
                 yabhb.Call("hbSetFillDecoOffsetSmall", 20);
                 yabhb.Call("hbFinishSingle", NPCType("OphiopedeHead"));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                GetTexture("healtbar_left"),
                GetTexture("healtbar_frame"),
                GetTexture("healtbar_right"),
                GetTexture("healtbar_fill"));
                yabhb.Call("hbSetMidBarOffset", -32, 12);
                yabhb.Call("hbSetBossHeadCentre", 80, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 20);
                yabhb.Call("hbFinishSingle", NPCType("OphiopedeHead2"));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                GetTexture("healtbar_left"),
                GetTexture("healtbar_frame"),
                GetTexture("healtbar_right"),
                GetTexture("healtbar_fill"));
                yabhb.Call("hbSetMidBarOffset", -32, 12);
                yabhb.Call("hbSetBossHeadCentre", 80, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 20);
                yabhb.Call("hbFinishSingle", NPCType("Ophiofly"));

                yabhb.Call("hbStart");
                yabhb.Call("hbSetTexture",
                GetTexture("healtbar_left"),
                GetTexture("healtbar_frame"),
                GetTexture("healtbar_right"),
                GetTexture("healtbar_fill"));
                yabhb.Call("hbSetMidBarOffset", -32, 12);
                yabhb.Call("hbSetBossHeadCentre", 80, 32);
                yabhb.Call("hbSetFillDecoOffsetSmall", 20);
                yabhb.Call("hbFinishMultiple",NPCType("FlyMinionCacoon"),NPCType("FlyMinionCacoon"));

            }
            */
        }

        /*public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.myPlayer == -1 || Main.gameMenu || !Main.LocalPlayer.active)
            {
                return;
            }
            if (NPC.CountNPCS(ModContent.NPCType<OphiopedeHead2>()) > 0)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Centipede_Mod_-_Metamorphosis");
                priority = MusicPriority.BossMedium;
            }
        }*/


        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType type = (MessageType)reader.ReadByte();

            if (type == MessageType.OphioidMessage && Main.netMode == NetmodeID.MultiplayerClient)
            {
                int npcid = reader.ReadInt32();
                int time = reader.ReadInt32();
                Main.npc[npcid].GetGlobalNPC<OphioidNPC>().fallthrough = time;
                //Instance.Logger.DebugFormat("NPC ID is {0}", npcid);
            }
        }

        public override object Call(params object[] args)
        {
            if (args[0] is not string function)
            {
                Logger.Error("Call Error: Expected a function name for the first argument");
                return null;
            }
            switch (function)
            {
                /// Returns true/false if Ophiopede (1st fight) has been defeated.
                case "downedOphiopede":
                    return OphioidWorld.downedOphiopede;
                /// Returns true/false if Ophiofly (2nd fight) has been defeated.
                case "downedOphiofly":
                case "downedOphiopede2":
                    return OphioidWorld.downedOphiopede2;
                /// Returns true/false if Ophiopede's Head (1st fight), Ophiopede's Head (2nd fight), Ophiocoon, or Ophiofly are active.
                case "OphioidBossIsActive":
                case "OphioidBoss":
                    return OphioidWorld.OphioidBoss;
                /// Returns a string for the name of the world evil for Ophiopede (1st fight) and Ophiofly's death messages.
                /// Will be the CustomWorldEvilForDeathMessage if set.
                case "GetWorldEvilForDeathMessage":
                    return Ophiofly.GetWorldEvilForDeathMessage();
                /// Sets a custom name for the world evil for Ophiopede (1st fight) and Ophiofly's death messages.
                /// Pass the custom name as the second argument.
                case "SetCustomWorldEvilForDeathMessage":
                    return Ophiofly.CustomWorldEvilForDeathMessage = args[1].ToString();

                default:
                    Logger.Error($"Function \"{function}\" is not defined by Ophioid Mod");
                    return null;
            }
        }
    }

    public enum MessageType : byte
    {
        OphioidMessage
    }
}
