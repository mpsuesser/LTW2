using System.Collections.Generic;
using UnityEngine;

public class BuffDescriptors : SingletonBehaviour<BuffDescriptors> {
    private Dictionary<BuffType, BuffDescriptor> BuffDescriptorsByType { get; set; }
    
    private void Awake() {
        InitializeSingleton(this);

        LoadBuffDescriptors();
    }

    private void LoadBuffDescriptors() {
        BuffDescriptorsByType = new Dictionary<BuffType, BuffDescriptor>();
        
        BuffDescriptor[] buffDescriptors = Resources.LoadAll<BuffDescriptor>("client/BuffDescriptors");
        foreach (BuffDescriptor buffDescriptor in buffDescriptors) {
            BuffDescriptorsByType[buffDescriptor.Type] = buffDescriptor;
        }
    }

    public BuffDescriptor GetBuffDescriptorByType(BuffType buffType) {
        if (!BuffDescriptorsByType.TryGetValue(buffType, out BuffDescriptor bd)) {
            throw new BuffDescriptorNotFoundException(buffType);
        }

        return bd;
    }
}