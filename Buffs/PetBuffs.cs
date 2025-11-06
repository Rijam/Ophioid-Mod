using Terraria;
using Terraria.ModLoader;
using OphioidMod.Projectiles;

namespace OphioidMod.Buffs
{
    public class BabyOphioflyBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Baby Ophioid Fly");
            // Description.SetDefault("Gross but, oddly cute");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            OphioidPlayer modPlayer = player.GetModPlayer<OphioidPlayer>();
            player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref modPlayer.PetBabyOphiofly, ModContent.ProjectileType<BabyFlyPet>());
        }
    }
    public class BabyOphiopedeBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Baby Ophioid Larva");
            // Description.SetDefault("Gross but, oddly cute");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            OphioidPlayer modPlayer = player.GetModPlayer<OphioidPlayer>();
            player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref modPlayer.PetBabyOphiopede, ModContent.ProjectileType<BabyOphiopedePet>());
        }
    }
}