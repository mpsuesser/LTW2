using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : Effect {
    public int ID { get; }
    public abstract BuffType Type { get; }

    public event Action<Buff> OnStacksUpdated;
    public event Action<Buff> OnRemoved;
    public event Action<Buff> OnAffectedUnitDied;

    protected ServerEntity AffectedEntity { get; }
    protected ServerEntity AppliedByEntity { get; }

    protected enum BuffExpirationType {
        DurationBased = 0,
        ProximityBased,
        Static,
    }

    protected abstract BuffExpirationType ExpirationType { get; }
    protected abstract bool IsStacking { get; }
    protected virtual bool IsDebuff => false;
    protected virtual int MaxStackCount => 5000;
    public int Stacks { get; private set; }

    protected Buff(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity,
        int startingStackCount
    ) {
        AffectedEntity = affectedEntity;
        AppliedByEntity = appliedByEntity;

        ID = UniqueID.NextBuffID;

        Stacks = startingStackCount;

        ServerSend.BuffAppliedToEntity(this, affectedEntity);

        affectedEntity.OnDestroyed += AffectedEntityDestroyed;
    }

    private void AffectedEntityDestroyed(ServerEntity ent) {
        OnAffectedUnitDied?.Invoke(this);
    }

    public abstract BuffTransitData PackageDataForTransit();

    public abstract void AddStacksFrom(int stacks, ServerEntity appliedByEntity);
    public virtual void Update() { }

    public void Purge() {
        Remove();
    }

    protected void Remove() {
        CleanUp();
        
        ServerSend.BuffRemovedFromEntity(this, AffectedEntity);

        // TODO: Have a buff-specific sync system subscribe to this and do the messaging
        // Added benefit of being able to batch process them in the future
        OnRemoved?.Invoke(this);
    }

    public void SetStacks(int stackCount) {
        if (stackCount <= 0) {
            Remove();
            return;
        }

        Stacks = Mathf.Clamp(stackCount, 1, MaxStackCount);

        OnStacksUpdated?.Invoke(this);
        CommunicateUpdateToClient();
    }

    protected void AddStack() => AddStacks(1);
    protected void AddStacks(int numStacksToAdd) {
        SetStacks(Stacks + numStacksToAdd);
    }

    protected void RemoveStack() => RemoveStacks(1);
    protected void RemoveStacks(int numStacksToRemove) {
        SetStacks(Stacks - numStacksToRemove);
    }

    // TODO: Split this out into a buff-specific sync system
    // Added benefit of being able to batch process them in the future
    private void CommunicateUpdateToClient() {
        ServerSend.BuffUpdated(this);
    }
}