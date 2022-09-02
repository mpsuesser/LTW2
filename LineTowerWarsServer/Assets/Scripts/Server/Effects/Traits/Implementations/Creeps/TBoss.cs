public class TBoss : Trait {
    public override TraitType Type => TraitType.Boss;

    public TBoss(ServerEntity entity) : base(entity) { }

    public override int LivesStolenDiff => 1;
}