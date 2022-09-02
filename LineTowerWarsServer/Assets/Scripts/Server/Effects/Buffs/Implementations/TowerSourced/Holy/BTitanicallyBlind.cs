public class BTitanicallyBlind : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.TitanicallyBlind;

    public BTitanicallyBlind(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float DamageDoneMultiplier =>
        TraitConstants.TitanicallyBlindDamageDealtMultiplier;
    
    public override float SpellResistDiff =>
        TraitConstants.TitanicallyBlindSpellResistDiff;
    
    public override float MovementSpeedMultiplier =>
        TraitConstants.TitanicallyBlindMovementSpeedMultiplier;
}