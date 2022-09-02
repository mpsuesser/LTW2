using System.Collections.Generic;

public static class BuffConstants {

    public static readonly Dictionary<BuffType, string> Name = new Dictionary<BuffType, string>() {
        { BuffType.EssenceOfNature, "Essence of Nature" },
        { BuffType.Devotion, "Devotion Aura" },
        { BuffType.Endurance, "Endurance Aura" },
        { BuffType.SpiderArmored, "Armored" },
        { BuffType.MudGolemResistance, "Solid Rock" },
        { BuffType.AcolyteUnholySacrifice, "Unholy Sacrifice" },
        { BuffType.SkeletonDeathPact, "Death Pact" },
    };

    public const double EarthTechnologyDiscAuraAttackSpeedMultiplier = 1.10;
    public const double EarthTechnologyDiscAuraRange = 235;

    public const double DevotionAuraRange = 700;
    public const int DevotionAuraArmorDiff = 1;

    public const double EnduranceAuraRange = 700;
    public const double EnduranceAuraSpeedMultiplier = 1.1;

    public const float SpiderArmoredPhysicalSplashDamageTakenMultiplier = 0.7f;

    public const float MudGolemHarmfulSpellEffectDurationMultiplier = 0.25f;

    public const float AcolyteUnholySacrificeRange = 155;
    public const int AcolyteUnholySacrificeHealAmount = 3;

    public const float SkeletonDeathPactReviveDelay = 1.5f;
    public const float SkeletonDeathPactReviveHealthMultiplier = 0.25f;

    public static readonly Dictionary<BuffType, string> Description = new Dictionary<BuffType, string>() {
        {
            BuffType.EssenceOfNature,
            $"All nearby friendly towers within a {EarthTechnologyDiscAuraRange} radius are infused with the essence of nature, increasing attack speed by {(EarthTechnologyDiscAuraAttackSpeedMultiplier - 1) * 100}%."
        },
        {
            BuffType.Devotion,
            $"Increases armor of allied creeps within a {DevotionAuraRange} radius of the swordsman by {DevotionAuraArmorDiff}."
        },
        {
            BuffType.Endurance,
            $"Increases the attack and movement speed of allied creeps within a{EnduranceAuraRange} radius of the succubus by {(EnduranceAuraSpeedMultiplier - 1) * 100}%."
        },
        {
            BuffType.SpiderArmored,
            $"Reduces damage taken from physical splash damage by {(1 - SpiderArmoredPhysicalSplashDamageTakenMultiplier) * 100}%."
        },
        {
            BuffType.MudGolemResistance,
            $"The durations of harmful spell effects are reduced by {(1 - MudGolemHarmfulSpellEffectDurationMultiplier) * 100}% and this unit is unphased by movement speed reduction effects."
        },
        {
            BuffType.AcolyteUnholySacrifice,
            $"Heals all creeps in a {AcolyteUnholySacrificeRange} radius for {AcolyteUnholySacrificeHealAmount} when killed."
        },
        {
            BuffType.SkeletonDeathPact,
            $"Returns to life with {SkeletonDeathPactReviveHealthMultiplier * 100}% health after {SkeletonDeathPactReviveDelay} seconds when killed."
        }
    };
}