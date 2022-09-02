using System;
using System.Collections.Generic;

public class TraitSystem : IEntitySystem {
    public event Action OnActiveTraitsUpdated;
    
    public HashSet<Trait> ActiveTraits { get; private set; }
    
    private ServerEntity E { get; set; }
    
    public TraitSystem(ServerEntity entity) {
        E = entity;
        InstantiateActiveTraits();
    }
    
    private void InstantiateActiveTraits() {
        ActiveTraits = new HashSet<Trait>();

        foreach (TraitType traitType in E.AssociatedTraitTypes) {
            try {
                ActiveTraits.Add(
                    TraitFactory.InstantiateActiveTraitIfExists(traitType, E)
                );
            }
            catch (NotImplementedException) { }
        }

        OnActiveTraitsUpdated?.Invoke();
    }
    
    // This is really only made for purging spell resistance traits in the arcane
    // disc effect. If you're going to purge any other types, be careful of side
    // effects and make sure that the trait cleans itself up nicely, because
    // traits have generally been written under the assumption that they exist
    // for the full life span of the entity they're attached to.
    public void PurgeTraitType(TraitType traitType) {
        foreach (Trait trait in new HashSet<Trait>(ActiveTraits)) {
            if (trait.Type == traitType) {
                ActiveTraits.Remove(trait);
            }
        }

        E.AssociatedTraitTypes.Remove(traitType);

        OnActiveTraitsUpdated?.Invoke();
    }
}