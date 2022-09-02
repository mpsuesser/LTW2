using System.Collections.Generic;
using UnityEngine;

public class CommandIssuanceEventSystem : MonoBehaviour {
    private void Awake() {
        ServerEventBus.OnRequestEntitiesMove += HandleEntitiesMoveCommandRequest;
        ServerEventBus.OnRequestEntitiesAttackTarget += HandleEntitiesAttackTargetCommandRequest;
        ServerEventBus.OnRequestEntitiesAttackLocation += HandleEntitiesAttackLocationCommandRequest;
    }

    private void OnDestroy() {
        ServerEventBus.OnRequestEntitiesMove -= HandleEntitiesMoveCommandRequest;
        ServerEventBus.OnRequestEntitiesAttackTarget -= HandleEntitiesAttackTargetCommandRequest;
        ServerEventBus.OnRequestEntitiesAttackLocation -= HandleEntitiesAttackLocationCommandRequest;
    }

    private static void HandleEntitiesMoveCommandRequest(
        HashSet<ServerEntity> movingEntities,
        Vector3 location,
        bool isQueuedAction
    ) {
        foreach (ServerEntity movingEntity in movingEntities) {
            if (
                !(movingEntity is ICommandable commandable)
                || !(movingEntity is INavigable)
            ) {
                continue;
            }
            
            MoveCommand command = new MoveCommand(
                movingEntity,
                location
            );
            commandable.Commands.ProcessNewCommand(command, isQueuedAction);
        }
    }

    private static void HandleEntitiesAttackTargetCommandRequest(
        HashSet<ServerEntity> attackingEntities,
        ServerEntity target,
        bool isQueuedAction
    ) {
        foreach (ServerEntity attackingEntity in attackingEntities) {
            if (!(attackingEntity is ICommandable commandable)) {
                continue;
            }

            if (!IsValidAttackTarget(attackingEntity, target)) {
                continue;
            }

            AttackTargetCommand command = new AttackTargetCommand(
                attackingEntity,
                target
            );
            commandable.Commands.ProcessNewCommand(command, isQueuedAction);
        }
    }

    private static void HandleEntitiesAttackLocationCommandRequest(
        HashSet<ServerEntity> attackingEntities,
        Vector3 location,
        bool isQueuedAction
    ) {
        foreach (ServerEntity attackingEntity in attackingEntities) {
            if (
                !(attackingEntity is ICommandable commandable)
                || !(attackingEntity is INavigable)
            ) {
                continue;
            }
            
            AttackLocationCommand command = new AttackLocationCommand(
                attackingEntity,
                location
            );
            commandable.Commands.ProcessNewCommand(command, isQueuedAction);
        }
    }

    private static bool IsValidAttackTarget(ServerEntity attacker, ServerEntity target) {
        switch (attacker) {
            case ServerEnemy _:
                return target is ServerTower;
            case ServerTower _:
            case ServerBuilder _:
                return target is ServerEnemy;
        }

        return false;
    }
}