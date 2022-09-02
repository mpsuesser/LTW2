using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class BuffDisplayManager : MonoBehaviour {

    [SerializeField] private RectTransform buffsParent;

    private ClientEntity ActiveEntityBeingDisplayed { get; set; }
    
    private void Awake() {
        EventBus.OnTargetsUpdated += HandleTargetsUpdated;
    }

    private void OnDestroy() {
        EventBus.OnTargetsUpdated -= HandleTargetsUpdated;
    }

    private void HandleTargetsUpdated(List<ClientEntity> targets) {
        if (targets.Count != 1) {
            ClearBuffDisplay();
            return;
        }

        DisplayBuffsForEntity(targets[0]);
    }

    private void DisplayBuffsForEntity(ClientEntity entity) {
        ClearBuffDisplay();

        RegisterEntityAsTarget(entity);

        List<BuffState> buffStates = entity.Buffs.GetAll();
        foreach (
            BuffState bs 
                in buffStates.OrderBy(buffState => buffState.DurationRemaining)
        ) {
            DisplayBuff(bs);
        }
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(buffsParent);
    }

    private void DisplayBuff(BuffState bs) {
        BuffDisplay.Create(bs, buffsParent);
    }

    private void ClearBuffDisplay() {
        if (ActiveEntityBeingDisplayed != null) {
            DeregisterEntityAsTarget(ActiveEntityBeingDisplayed);
        }
        
        foreach (Transform tf in buffsParent) {
            Destroy(tf.gameObject);
        }
    }

    private void RegisterEntityAsTarget(ClientEntity entity) {
        entity.Buffs.OnBuffsUpdated += DisplayBuffsForEntity;
        
        ActiveEntityBeingDisplayed = entity;
    }

    private void DeregisterEntityAsTarget(ClientEntity entity) {
        entity.Buffs.OnBuffsUpdated -= DisplayBuffsForEntity;

        ActiveEntityBeingDisplayed = null;
    }
}