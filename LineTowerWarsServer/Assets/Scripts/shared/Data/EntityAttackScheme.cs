public class EntityAttackScheme {
    public AttackDeliveryModifiers Delivery { get; private set; }

    public double Range { get; private set; }
    public double Cooldown { get; private set; }
    public (double, double) DamageRange { get; private set; }
    public AttackType Modifier { get; private set; }

    public EntityAttackScheme(ServerTower tower) {
        TowerType towerType = tower.Type;
        
        Delivery = TowerConstants.AttackDelivery[towerType];
        Range = TowerConstants.Range[towerType];
        Cooldown = TowerConstants.Cooldown[towerType];
        DamageRange = TowerConstants.DamageRange[towerType];
        Modifier = TowerConstants.AttackModifier[towerType];
    }
    
    public EntityAttackScheme(ServerEnemy creep) {
        EnemyType creepType = creep.Type;
        if (!EnemyConstants.AttackingCreepTypes.Contains(creepType)) {
            throw new EntityIsNotConfiguredProperlyException(creep);
        }

        Delivery = EnemyConstants.AttackDelivery[creepType];
        Range = EnemyConstants.AttackRange[creepType];
        Cooldown = EnemyConstants.AttackCooldown[creepType];
        DamageRange = EnemyConstants.AttackDamageRange[creepType];
        Modifier = EnemyConstants.AttackModifier[creepType];
    }
    
    public EntityAttackScheme(ServerBuilder _builder) {
        Delivery = BuilderConstants.BuilderAttackDelivery;
        Range = BuilderConstants.BuilderAttackRange;
        Cooldown = BuilderConstants.BuilderAttackCooldown;
        DamageRange = BuilderConstants.BuilderAttackDamageRange;
        Modifier = BuilderConstants.BuilderAttackModifier;
    }
}