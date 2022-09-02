using System.Collections.Generic;
using UnityEngine;

public class CrushingWave : MonoBehaviour {
    public static CrushingWave Create(
        ServerEntity sourceEntity,
        ServerEntity targetEntity,
        float damage,
        DamageSourceType damageSource
    ) {
        CrushingWave wave = Instantiate(
            ServerPrefabs.Singleton.pfCrushingWave,
            sourceEntity.transform.position,
            Quaternion.identity
        );

        wave.SourceEntity = sourceEntity;
        wave.SourceOrigin = sourceEntity.transform.position;
        Vector3 travelDirection = (targetEntity.transform.position - wave.SourceOrigin).normalized;
        wave.Destination = wave.SourceOrigin
                           + (travelDirection * TraitConstants.CrushingWaveTravelDistance);
        wave.Damage = damage;
        wave.DamageSource = damageSource;
        wave.CreationTime = Time.time;
        wave.EntitiesTouched = new HashSet<ServerEntity>();

        SphereCollider collider = wave.GetComponent<SphereCollider>();
        collider.radius = ServerUtil.ConvertGameRangeToUnityRange(
            TraitConstants.CrushingWaveGameRadius
        );

        return wave;
    }
    
    private ServerEntity SourceEntity { get; set; }
    private float Damage { get; set; }
    private DamageSourceType DamageSource { get; set; }
    
    private Vector3 SourceOrigin { get; set; }
    private Vector3 Destination { get; set; }
    private float CreationTime { get; set; }
    private HashSet<ServerEntity> EntitiesTouched { get; set; }

    private void Update() {
        float travelTime = Time.time - CreationTime;
        if (travelTime > TraitConstants.CrushingWaveTravelDuration) {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.Lerp(
            SourceOrigin,
            Destination,
            travelTime / TraitConstants.CrushingWaveTravelDuration
        );
    }
    
    private void OnTriggerEnter(Collider other) {
        ServerEnemy creep = other.gameObject.GetComponent<ServerEnemy>();
        if (!creep) {
            return;
        }
        
        if (EntitiesTouched.Contains(creep)) {
            return;
        }
        EntitiesTouched.Add(creep);

        if (SourceEntity != null) {
            SourceEntity.DealDamageTo(
                creep,
                Damage,
                DamageType.Spell,
                DamageSource
            );
        }
        else {
            creep.TakeDamageFrom(
                null,
                Damage,
                DamageType.Spell,
                DamageSource
            );
        }
    }
}