using System.Collections.Generic;

public class TArcanize2 : Trait {
    public override TraitType Type => TraitType.Arcanize2;

    public TArcanize2(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackFiredPre += AddManaToAttacker;
    }

    private static void AddManaToAttacker(
        ServerEntity attacker,
        List<ServerEntity> _targets
    ) {
        attacker.Status.GainMana(1);
    }

    public override float DamageDoneMultiplier =>
        1 + ((float)(E.Status.Mana / E.MaxMana) * (TraitConstants.Arcanize2MaximumManaDamageMultiplier - 1));
}