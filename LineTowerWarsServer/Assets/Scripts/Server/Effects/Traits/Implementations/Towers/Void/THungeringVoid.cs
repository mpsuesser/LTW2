public class THungeringVoid : Trait {
    public override TraitType Type => TraitType.HungeringVoid;

    public THungeringVoid(ServerEntity entity) : base(entity) {
        ProximityBuffApplicator_HungeringVoidDebuffAura.Create(entity);
        
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackLandedPost += ApplyBuff;
    }

    private static void ApplyBuff(
        ServerEntity attacker,
        ServerEntity target
    ) {
        BuffFactory.ApplyBuff(
            BuffType.HungeringVoidBuff,
            attacker,
            target
        );
    }
}