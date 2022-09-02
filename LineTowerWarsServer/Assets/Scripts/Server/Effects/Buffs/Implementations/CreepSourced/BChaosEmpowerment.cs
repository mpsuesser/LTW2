public class BChaosEmpowerment : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.ChaosEmpowerment;
    
    public BChaosEmpowerment(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }
    
    public override float SpellResistDiff =>
        TraitConstants.ChaosEmpowermentSpellResist;
    
    public override float ManaRegenPerSecondDiff =>
        TraitConstants.ChaosEmpowermentManaGainPerSecond;
}