public class TSkittering : Trait {
    public override TraitType Type => TraitType.Skittering;

    public TSkittering(ServerEntity entity) : base(entity) { }

    // TODO: Low priority autoattack
}