using System;
using System.Collections.Generic;

public interface ITraitHolder<out T> {
    public event Action<T> OnSpawned;
    public event Action<T> OnDeath;
    public event Action<T> OnDestroyed;
    
    HashSet<TraitType> AssociatedTraitTypes { get; }
    
    TraitSystem Traits { get; }
}