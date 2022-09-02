using System.Collections.Generic;

public class TArcanize1 : Trait {
    public override TraitType Type => TraitType.Arcanize1;

    public TArcanize1(ServerEntity entity) : base(entity) {
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
        1 + ((float)(E.Status.Mana / E.MaxMana) * (TraitConstants.Arcanize1MaximumManaDamageMultiplier - 1));
}