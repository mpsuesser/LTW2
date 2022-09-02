using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
public class ProximityBuffApplicator : MonoBehaviour {
    protected static ProximityBuffApplicator Create(
        BuffType buffType,
        ServerEntity auraProvider,
        float gameRange,
        EntityFilter filter
    ) {
        ProximityBuffApplicator applicator = Instantiate(
            ServerPrefabs.Singleton.pfProximityBuffApplicator,
            auraProvider.transform.position,
            Quaternion.identity
        );

        applicator.SetBuffType(buffType);
        applicator.SetAuraProvider(auraProvider);
        applicator.SetRange(ServerUtil.ConvertGameRangeToUnityRange(gameRange));
        applicator.SetFilter(filter);
        
        return applicator;
    }
    
    private ServerEntity AuraProvider { get; set; }
    private EntityFilter Filter { get; set; }
    private BuffType Type { get; set; }
    
    protected HashSet<ServerEntity> EntitiesAppliedTo { get; private set; }
    private SphereCollider myCollider;

    protected virtual void Awake() {
        myCollider = GetComponent<SphereCollider>();
        
        EntitiesAppliedTo = new HashSet<ServerEntity>();
    }

    private void LateUpdate() {
        transform.position = AuraProvider.transform.position;
    }

    private void SetAuraProvider(ServerEntity auraProvider) {
        AuraProvider = auraProvider;

        auraProvider.OnDestroyed += OnAuraProviderDestroyed;
    }

    private void OnAuraProviderDestroyed(ServerEntity ent) {
        Destroy(gameObject);
    }

    private void SetBuffType(BuffType buffType) {
        Type = buffType;
    }
    
    private void SetRange(float range) {
        myCollider.radius = range;
    }

    private void SetFilter(EntityFilter filter) {
        Filter = filter;
    }

    private void OnTriggerEnter(Collider other) {
        ServerEntity entity = other.gameObject.GetComponent<ServerEntity>();
        if (!entity) {
            return;
        }

        if (EntitiesAppliedTo.Contains(entity)) {
            return;
        }

        if (!Filter.PassesFilter(entity)) {
            return;
        }
        
        ApplyBuffToEntity(entity);
    }

    private void OnTriggerExit(Collider other) {
        ServerEntity entity = other.GetComponent<ServerEntity>();
        if (!entity) {
            return;
        }

        if (!EntitiesAppliedTo.Contains(entity)) {
            return;
        }

        RemoveAppliedBuffFromEntity(entity);
    }

    private void RemoveBuffFromEntity(ServerEntity entity) {
        EntitiesAppliedTo.Remove(entity);
        EntitiesAppliedToUpdatedPost();
    }

    protected virtual void EntitiesAppliedToUpdatedPost() {}

    protected virtual void ApplyBuffToEntity(
        ServerEntity entity
    ) {
        try {
            BuffFactory.ApplyBuff(Type, entity, AuraProvider);
        }
        catch (NoImplementationForProvidedEnumException e) {
            LTWLogger.Log($"Could not apply buff {Type} from applicator because its BuffFactory implementation has not yet been implemented: {e.Message}");
            return;
        }

        EntitiesAppliedTo.Add(entity);
        EntitiesAppliedToUpdatedPost();

        entity.OnDestroyed += RemoveBuffFromEntity;
    }

    protected virtual void RemoveAppliedBuffFromEntity(
        ServerEntity entity
    ) {
        if (!entity.Buffs.TryGetBuffOfType(Type, out Buff b)) {
            return;
        }

        if (!(b is Buff_ProximityBased pbb)) {
            return;
        }
        
        pbb.RemoveStackFrom(AuraProvider);

        RemoveBuffFromEntity(entity);
    }
}