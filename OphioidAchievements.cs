using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OphioidMod
{
    public class OphiopedeKilled : ModAchievement
    {
        public override string TextureName => "OphioidMod/OphioidAchievements";
        public override int Index => 0;

        public override void SetStaticDefaults()
        {
            AddNPCKilledCondition(ModContent.NPCType<NPCs.OphiopedeHead>());
        }
        public override Position GetDefaultPosition() => new After("BUCKETS_OF_BOLTS");
    }
    public class OphioflyKilled : ModAchievement
    {
        public override string TextureName => "OphioidMod/OphioidAchievements";
        public override int Index => 1;

        public override void SetStaticDefaults()
        {
            AddNPCKilledCondition(ModContent.NPCType<NPCs.Ophiofly>());
        }
        public override Position GetDefaultPosition() => new After("DEFEAT_OLD_ONES_ARMY_TIER3");
    }
}