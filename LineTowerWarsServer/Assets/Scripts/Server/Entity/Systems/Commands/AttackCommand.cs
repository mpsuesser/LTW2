using UnityEngine;

public abstract class AttackCommand : ICommand {
    public CommandState State { get; protected set; }
    
    protected ServerEntity CommandingEntity { get; }
    protected IAttacker CommandingAttacker { get; }
    
    protected AttackCommand(
        ServerEntity attackingEntity
    ) {
        CommandingEntity = attackingEntity;

        if (!(CommandingEntity is IAttacker attacker)) {
            throw new EntityIsNotConfiguredProperlyException(CommandingEntity);
        }
        
        CommandingAttacker = attacker;
    }

    public abstract void PrepareForExecution();
    public abstract void Update();
    public abstract void CleanUp();

    protected Vector3 GetClosestNavigablePointTo(ServerEntity target) {
        if (CommandingEntity.AssociatedTraitTypes.Contains(TraitType.Flying)) {
            return target.transform.position;
        }
        
        Vector3 targetPos = target.transform.position;
        Vector3 commandingPos = CommandingEntity.transform.position;
        Vector3 direction = (targetPos - commandingPos).normalized;
        float dist = Vector3.Distance(targetPos, commandingPos);

        RaycastHit[] hits = Physics.RaycastAll(
            CommandingEntity.transform.position,
            direction,
            dist,
            LayerMaskConstants.EntityLayerMask
        );
        foreach (RaycastHit hit in hits) {
            if (hit.collider.gameObject != target.gameObject) {
                continue;
            }

            // 1 unit off of the hit point, in the direction of the commanding unit
            return commandingPos + direction * (hit.distance - 1);
        }

        throw new NotFoundException();
    }
}