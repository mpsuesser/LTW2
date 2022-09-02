public static class BuffFactory {
    public static Buff ApplyBuff(
        BuffType type,
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity,
        int stackCount = 1
    ) {
        if (
            affectedEntity.Effects.AggregateIsImmuneToHarmfulSpellEffects
            && BuffConstants.HarmfulSpellEffectTypes.Contains(type)
        ) {
            return null;
        }
        
        if (affectedEntity.Buffs.TryGetBuffOfType(type, out Buff b)) {
            b.AddStacksFrom(stackCount, appliedByEntity);
            return b;
        }
        
        return CreateBuffOfType(
            type,
            affectedEntity,
            appliedByEntity,
            stackCount
        );
    }

    private static Buff CreateBuffOfType(
        BuffType type,
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity,
        int startingStackCount
    ) {
        Buff b = type switch {
            // Creeps
            BuffType.Devotion1 => new BDevotion1(affectedEntity, appliedByEntity),
            BuffType.Devotion2 => new BDevotion2(affectedEntity, appliedByEntity),
            BuffType.Devotion3 => new BDevotion3(affectedEntity, appliedByEntity),
            BuffType.Devotion4 => new BDevotion4(affectedEntity, appliedByEntity),
            BuffType.Endurance1 => new BEndurance1(affectedEntity, appliedByEntity),
            BuffType.Endurance2 => new BEndurance2(affectedEntity, appliedByEntity),
            BuffType.Endurance3 => new BEndurance3(affectedEntity, appliedByEntity),
            BuffType.Endurance4 => new BEndurance4(affectedEntity, appliedByEntity),
            BuffType.Ancient1 => new BAncient1(affectedEntity, appliedByEntity),
            BuffType.Ancient2 => new BAncient2(affectedEntity, appliedByEntity),
            BuffType.Geomancy1 => new BGeomancy1(affectedEntity, appliedByEntity),
            BuffType.Geomancy2 => new BGeomancy2(affectedEntity, appliedByEntity),
            BuffType.Ethereal => new BEthereal(affectedEntity, appliedByEntity),
            BuffType.EarthShield => new BEarthShield(affectedEntity, appliedByEntity),
            BuffType.ChaosEmpowerment => new BChaosEmpowerment(affectedEntity, appliedByEntity),
            BuffType.LingeringVoid => new BLingeringVoid(affectedEntity, appliedByEntity),
            BuffType.NecroticTransfusion => new BNecroticTransfusion(affectedEntity, appliedByEntity),
            BuffType.StaticOverload => new BStaticOverload(affectedEntity, appliedByEntity),
            BuffType.WarCry => new BWarCry(affectedEntity, appliedByEntity),
            BuffType.WarStance => new BWarStance(affectedEntity, appliedByEntity),
            BuffType.HardenedSkin => new BHardenedSkin(affectedEntity, appliedByEntity),
            
            // Towers
            BuffType.FireboltStun => new BFireboltStun(affectedEntity, appliedByEntity),
            BuffType.FireblastStun => new BFireblastStun(affectedEntity, appliedByEntity),
            BuffType.PyroblastStun => new BPyroblastStun(affectedEntity, appliedByEntity),
            BuffType.FrostboltSlow => new BFrostboltSlow(affectedEntity, appliedByEntity),
            BuffType.IceblastSlow => new BIceblastSlow(affectedEntity, appliedByEntity),
            BuffType.ChainsOfIceSlow => new BChainsOfIceSlow(affectedEntity, appliedByEntity),
            
            BuffType.ShatteredArmor1 => new BShatteredArmor1(affectedEntity, appliedByEntity),
            BuffType.ShatteredArmor2 => new BShatteredArmor2(affectedEntity, appliedByEntity),
            BuffType.Devastation1 => new BDevastation1(affectedEntity, appliedByEntity),
            BuffType.Devastation2 => new BDevastation2(affectedEntity, appliedByEntity),
            BuffType.NaturesForce => new BNaturesForce(affectedEntity, appliedByEntity),
            BuffType.Germination1 => new BGermination1(affectedEntity, appliedByEntity),
            BuffType.Germination2 => new BGermination2(affectedEntity, appliedByEntity),
            BuffType.LethalPreparation => new BLethalPreparation(affectedEntity, appliedByEntity),
            
            BuffType.FrostAttackSlow1 => new BFrostAttackSlow1(affectedEntity, appliedByEntity),
            BuffType.FrostAttackSlow2 => new BFrostAttackSlow2(affectedEntity, appliedByEntity),
            BuffType.FrostBlastSlow1 => new BFrostBlastSlow1(affectedEntity, appliedByEntity),
            BuffType.FrostBlastSlow2 => new BFrostBlastSlow2(affectedEntity, appliedByEntity),
            BuffType.ChillingDeathSlow => new BChillingDeathSlow(affectedEntity, appliedByEntity),
            BuffType.ChillingDeathAttackSpeedSlow => new BChillingDeathAttackSpeedSlow(affectedEntity, appliedByEntity),
            BuffType.Frostbite => new BFrostbite(affectedEntity, appliedByEntity),
            BuffType.IceLanced1 => new BIceLanced1(affectedEntity, appliedByEntity),
            BuffType.IceLanced2 => new BIceLanced2(affectedEntity, appliedByEntity),
            BuffType.CrystallizedPenetration => new BCrystallizedPenetration(affectedEntity, appliedByEntity),

            BuffType.TurbulentWeather1 => new BTurbulentWeather1(affectedEntity, appliedByEntity),
            BuffType.TurbulentWeather2 => new BTurbulentWeather2(affectedEntity, appliedByEntity),
            BuffType.TurbulentWeather3 => new BTurbulentWeather3(affectedEntity, appliedByEntity),
            
            BuffType.BlindedByTheLight1 => new BBlindedByTheLight1(affectedEntity, appliedByEntity),
            BuffType.BlindedByTheLight2 => new BBlindedByTheLight2(affectedEntity, appliedByEntity),
            BuffType.TitanicallyBlind => new BTitanicallyBlind(affectedEntity, appliedByEntity),
            
            BuffType.Ignite1 => new BIgnite1(affectedEntity, appliedByEntity),
            BuffType.Ignite2 => new BIgnite2(affectedEntity, appliedByEntity),
            BuffType.MeteoricVulnerability => new BMeteoricVulnerability(affectedEntity, appliedByEntity),
            BuffType.RisingHeat1 => new BRisingHeat1(affectedEntity, appliedByEntity),
            BuffType.RisingHeat2 => new BRisingHeat2(affectedEntity, appliedByEntity),
            BuffType.VolcanicExposure => new BVolcanicExposure(affectedEntity, appliedByEntity),
            BuffType.VolcanicOpportunity => new BVolcanicOpportunity(affectedEntity, appliedByEntity),
            
            BuffType.Corrupted1 => new BCorrupted1(affectedEntity, appliedByEntity),
            BuffType.Corrupted2 => new BCorrupted2(affectedEntity, appliedByEntity),
            BuffType.Plague => new BPlague(affectedEntity, appliedByEntity, startingStackCount),
            BuffType.UnendingPlague => new BUnendingPlague(affectedEntity, appliedByEntity, startingStackCount),
            
            BuffType.HurricaneStormParalysis => new BHurricaneStormParalysis(affectedEntity, appliedByEntity),
            BuffType.TorrentSlow1 => new BTorrentSlow1(affectedEntity, appliedByEntity),
            BuffType.TorrentSlow2 => new BTorrentSlow2(affectedEntity, appliedByEntity),
            BuffType.CripplingDecaySlow => new BCripplingDecaySlow(affectedEntity, appliedByEntity),
            BuffType.CripplingVulnerability => new BCripplingVulnerability(affectedEntity, appliedByEntity),
            
            BuffType.TemporalShift1 => new BTemporalShift1(affectedEntity, appliedByEntity),
            BuffType.TemporalShift2 => new BTemporalShift2(affectedEntity, appliedByEntity),
            BuffType.TemporalImplosion => new BTemporalImplosion(affectedEntity, appliedByEntity),
            BuffType.VoidLashingBuff1 => new BVoidLashingBuff1(affectedEntity, appliedByEntity),
            BuffType.VoidLashingBuff2 => new BVoidLashingBuff2(affectedEntity, appliedByEntity),
            BuffType.HungeringVoidBuff => new BHungeringVoidBuff(affectedEntity, appliedByEntity),
            BuffType.VoidLashingDebuff1 => new BVoidLashingDebuff1(affectedEntity, appliedByEntity),
            BuffType.VoidLashingDebuff2 => new BVoidLashingDebuff2(affectedEntity, appliedByEntity),
            BuffType.HungeringVoidDebuff => new BHungeringVoidDebuff(affectedEntity, appliedByEntity),

            // Discs
            BuffType.EssenceOfNature1 => new BEssenceOfNature1(affectedEntity, appliedByEntity),
            BuffType.EssenceOfNature2 => new BEssenceOfNature2(affectedEntity, appliedByEntity),
            BuffType.EssenceOfBlight1 => new BEssenceOfBlight1(affectedEntity, appliedByEntity),
            BuffType.EssenceOfBlight2 => new BEssenceOfBlight2(affectedEntity, appliedByEntity),
            BuffType.EssenceOfPower1 => new BEssenceOfPower1(affectedEntity, appliedByEntity),
            BuffType.EssenceOfPower2 => new BEssenceOfPower2(affectedEntity, appliedByEntity),
            BuffType.EssenceOfLight1 => new BEssenceOfLight1(affectedEntity, appliedByEntity),
            BuffType.EssenceOfLight2 => new BEssenceOfLight2(affectedEntity, appliedByEntity),
            BuffType.EssenceOfTheSea1 => new BEssenceOfTheSea1(affectedEntity, appliedByEntity),
            BuffType.EssenceOfTheSea2 => new BEssenceOfTheSea2(affectedEntity, appliedByEntity),
            BuffType.EssenceOfFrost1 => new BEssenceOfFrost1(affectedEntity, appliedByEntity),
            BuffType.EssenceOfFrost2 => new BEssenceOfFrost2(affectedEntity, appliedByEntity),
            BuffType.EssenceOfDarkness1 => new BEssenceOfDarkness1(affectedEntity, appliedByEntity),
            BuffType.EssenceOfDarkness2 => new BEssenceOfDarkness2(affectedEntity, appliedByEntity),
            BuffType.EssenceOfPowerStun => new BEssenceOfPowerStun(affectedEntity, appliedByEntity),
            BuffType.EssenceOfFrostSlow1 => new BEssenceOfFrostSlow1(affectedEntity, appliedByEntity),
            BuffType.EssenceOfFrostSlow2 => new BEssenceOfFrostSlow2(affectedEntity, appliedByEntity),
            _ => throw new NoImplementationForProvidedEnumException((int) type)
        };

        affectedEntity.Buffs.AddBuff(b);

        return b;
    }
}