using System;
using UnityEngine;

public class StatusSystem : IEntitySystem {
    public bool IsAlive => Health > 0;

    private ServerEntity E { get; }

    private int MaxHealth { get; }
    public double Health { get; private set; }
    public int MaxMana { get; }
    public double Mana { get; private set; }

    public StatusSystem(ServerEntity e, int startingHealth, int maxMana) {
        E = e;

        MaxHealth = startingHealth;
        Health = startingHealth;

        Mana = 0;
        MaxMana = maxMana;
    }

    public void TakeDamageFrom(ServerEntity dealer, double damage) {
        Health = Math.Max(0, Health - damage);
        if (Health == 0) {
            E.Die();
        }
        
        // TODO: Log damage event, send it in damage meter oriented message as batch

        ServerSend.EntityStatusSync(E);
    }

    public void HealForAmount(double amount) {
        // Bail if we're already at max health
        if (Health - MaxHealth > Mathf.Epsilon) {
            return;
        }

        amount += E.Effects.AggregateHealingReceivedDiff;
        amount *= E.Effects.AggregateHealingReceivedMultiplier;
        Health = Math.Min(Health + amount, MaxHealth);
        
        ServerSend.EntityStatusSync(E);
    }

    public void SetInitialHealthPercentage(float healthPercentage) {
        Health = MaxHealth * healthPercentage;
    }

    public void GainMana(double amount) {
        // Bail if we're already at max mana
        if (Mana - MaxMana > Mathf.Epsilon) {
            return;
        }
        
        Mana = Math.Min(Mana + amount, MaxMana);

        ServerSend.EntityStatusSync(E);
    }

    public void LoseMana(double amount) {
        // Bail if we're already at 0 mana
        if (Mana == 0) {
            return;
        }
        
        Mana = Math.Max(Mana - amount, 0);
        
        ServerSend.EntityStatusSync(E);
    }

    public void DumpAllMana() {
        LoseMana(Mana);
    }
}

// TODO: Figure out why the mana bar is not showing up on the client side.

// Checks:
// Is the status update message being sent for towers?
// Is the status update message being reflected for towers?
// Is the progress bar getting the update message?
