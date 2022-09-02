using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class ServerEntity : MonoBehaviour,
                                     ITraitHolder<ServerEntity>,
                                     IResourceful, IEffectHolder, IBuffable, IRegennable {
    public event Action<ServerEntity> OnSpawned;
    public event Action<ServerEntity> OnDeath;
    public event Action<ServerEntity> OnDestroyed;
    public event Action<ServerEntity, double, DamageType, DamageSourceType> OnDamageTaken;
    public event Action<ServerEntity> OnLaneSet;
    
    public abstract HashSet<TraitType> AssociatedTraitTypes { get; protected set; }
    
    public abstract int Armor { get; protected set; }
    public abstract int SpellResist { get; protected set; }
    public abstract int MaxHealth { get; protected set; }
    public abstract int MaxMana { get; protected set; }

    public virtual ArmorType ArmorModifier => ArmorType.None;
    public virtual AttackType AttackModifier => AttackType.None;
    
    public int ID { get; private set; }
    public Lane ActiveLane { get; protected set; }
    
    public int HP => (int) Math.Ceiling(Status.Health);
    public int MP => (int) Math.Ceiling(Status.Mana);
    public bool IsAlive => Status.IsAlive;
    public bool IsAtMaxHealth => HP == MaxHealth;
    public double HealthPercentage => HealthRatio * 100;
    public double ManaPercentage => ManaRatio * 100;
    public double HealthRatio => Status.Health / MaxHealth;
    public double ManaRatio => Status.Mana / MaxMana;

    public StatusSystem Status { get; private set; }
    public EffectSystem Effects { get; private set; }
    public BuffSystem Buffs { get; private set; }
    public RegenSystem Regen { get; private set; }
    public TraitSystem Traits { get; private set; }

    protected virtual void Awake() {
        ID = UniqueID.NextEntityID;

        Status = new StatusSystem(this, MaxHealth, MaxMana);
        Regen = new RegenSystem(this);
        Buffs = new BuffSystem(this);
        Traits = new TraitSystem(this);
        Effects = new EffectSystem(this);
    }

    protected void SetActiveLane(Lane lane) {
        ActiveLane = lane;

        OnLaneSet?.Invoke(this);
    }

    protected void OnSpawningCompleted() {
        OnSpawned?.Invoke(this);
    }

    private void Update() {
        Buffs.Update();
    }

    protected virtual void OnDestroy() {
        OnDestroyed?.Invoke(this);
        
        ServerEventBus.EntityDestroyed(this);
    }

    public double DealDamageTo(
        ServerEntity target,
        double damage,
        DamageType damageType,
        DamageSourceType sourceType
    ) {
        DamageEngine.AdjustOutboundDamage(
            this,
            target,
            ref damage,
            damageType,
            sourceType
        );
        
        return target.TakeDamageFrom(
            this, 
            damage,
            damageType,
            sourceType
        );
    }

    public double TakeDamageFrom(
        ServerEntity dealer,
        double damage,
        DamageType damageType,
        DamageSourceType sourceType
    ) {
        DamageEngine.AdjustInboundDamage(
          this,
          dealer,
          ref damage,
          damageType,
          sourceType
        );

        Status.TakeDamageFrom(
            dealer, 
            damage
        );

        OnDamageTaken?.Invoke(dealer, damage, damageType, sourceType);
        return damage;
    }

    public void DoHealingTo(
        ServerEntity entityBeingHealed,
        double amount
    ) {
        // TODO: HealingEngine? Use DamageEngine?
        // TODO: Adjust amount by Effects.AggregateHealingDoneMultiplier
        
        entityBeingHealed.ReceiveHealingFrom(this, amount);
    }

    private void ReceiveHealingFrom(
        ServerEntity healer,
        double amount
    ) {
        // TODO: HealingEngine? Use DamageEngine?
        amount *= Effects.AggregateHealingReceivedMultiplier;
        
        Status.HealForAmount(amount);
    }

    public virtual void Die() {
        OnDeath?.Invoke(this);
        
        ServerSend.EntityDied(this);
        
        Destroy(gameObject);
    }

    public virtual void Despawn() {
        ServerSend.EntityDespawned(this);
        
        Destroy(gameObject);
    }
}