using System.Collections.Generic;
using UnityEngine;

public class BuffEffects : SingletonBehaviour<BuffEffects> {
    private Dictionary<BuffType, BuffEffect> BuffEffectsByType { get; set; }
    
    private void Awake() {
        InitializeSingleton(this);

        LoadBuffEffects();
    }

    private void LoadBuffEffects() {
        BuffEffectsByType = new Dictionary<BuffType, BuffEffect>();
        
        BuffEffect[] buffEffects = Resources.LoadAll<BuffEffect>("client/BuffEffects");
        foreach (BuffEffect buffEffect in buffEffects) {
            BuffEffectsByType[buffEffect.Type] = buffEffect;
        }
    }

    public bool TryGetBuffEffectByType(BuffType buffType, out BuffEffect effect) {
        return BuffEffectsByType.TryGetValue(buffType, out effect);
    }
}