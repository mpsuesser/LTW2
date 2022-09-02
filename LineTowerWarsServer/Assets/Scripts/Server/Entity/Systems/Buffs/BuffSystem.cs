using System;
using System.Collections.Generic;

public class BuffSystem : IEntitySystem {
    // TODO: This could be scaled if there are other similar usecases
    public event Action<double> OnMoveSpeedMultiplierUpdated;
    private const float MinimumPrecisionChangeForUpdate = 0.005f;

    public event Action OnActiveBuffsUpdated;
    
    private ServerEntity E { get; }
    
    public HashSet<Buff> ActiveBuffs { get; }
    private Dictionary<BuffType, Buff> ActiveBuffsByType { get; }

    public BuffSystem(ServerEntity e) {
        E = e;

        ActiveBuffs = new HashSet<Buff>();
        ActiveBuffsByType = new Dictionary<BuffType, Buff>();
    }

    public void Update() {
        foreach (Buff b in ActiveBuffs) {
            b.Update();
        }
    }

    public void AddBuff(Buff b) {
        ActiveBuffs.Add(b);
        ActiveBuffsByType.Add(b.Type, b);

        b.OnStacksUpdated += HandleBuffStacksUpdated;
        b.OnRemoved += HandleBuffRemoval;

        OnActiveBuffsUpdated?.Invoke();
    }

    private void HandleBuffRemoval(Buff b) {
        ActiveBuffs.Remove(b);
        ActiveBuffsByType.Remove(b.Type);

        OnActiveBuffsUpdated?.Invoke();
    }

    private void HandleBuffStacksUpdated(Buff b) {
        OnActiveBuffsUpdated?.Invoke();
    }

    public bool TryGetBuffOfType(BuffType type, out Buff b) {
        if (!HasBuffOfType(type)) {
            b = null;
            return false;
        }
        
        b = ActiveBuffsByType[type];
        return true;
    }

    public bool HasBuffOfType(BuffType type) {
        return ActiveBuffsByType.ContainsKey(type);
    }
}
