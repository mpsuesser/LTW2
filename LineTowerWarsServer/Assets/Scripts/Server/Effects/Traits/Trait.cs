public abstract class Trait : Effect {
    public abstract TraitType Type { get; }
    protected ServerEntity E { get; }
    
    protected Trait(ServerEntity entity) {
        RegisterHooks(entity);
        
        E = entity;
    }

    private void RegisterHooks(ServerEntity entity) {
        entity.OnSpawned += OnHolderSpawned;
        entity.OnDeath += OnHolderDeath;
        entity.OnDestroyed += OnHolderDestroyed;
    }

    private void UnregisterHooks(ServerEntity entity) {
        entity.OnSpawned -= OnHolderSpawned;
        entity.OnDeath -= OnHolderDeath;
        entity.OnDestroyed -= OnHolderDestroyed;
    }

    protected virtual void OnHolderSpawned(ServerEntity entity) { }
    protected virtual void OnHolderDeath(ServerEntity entity) { }
    protected virtual void OnHolderDestroyed(ServerEntity entity) {
        CleanUp();
        
        UnregisterHooks(entity);
    }
}