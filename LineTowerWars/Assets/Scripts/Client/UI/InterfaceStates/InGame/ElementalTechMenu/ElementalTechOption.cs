using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ElementalTechOption : HoverTooltip
{
    public event Action<ElementalTechType> OnPushed;

    [SerializeField] private ElementalTechType TechType;
    [SerializeField] private Button Btn;
    [SerializeField] private TMP_Text ButtonText;
    [SerializeField] private Image Img;
    private KeyCode Hotkey { get; set; } // For tooltip purposes only

    protected override void Awake() {
        Btn.onClick.AddListener(OnPush);
        Img.sprite = ClientResources.Singleton.GetSpriteForTechType(TechType);

        base.Awake();
    }

    public void OnPush() {
        OnPushed?.Invoke(TechType);
    }

    public void UpdateStateByLane(Lane lane) {
        if (lane.HasTech(TechType) || (ElementalTech.Prerequisite.ContainsKey(TechType) && !lane.HasTech(ElementalTech.Prerequisite[TechType]))) {
            Btn.interactable = false;
        } else {
            Btn.interactable = true;
        }
    }

    // For tooltip purposes only
    public void SetHotkey(KeyCode kc) {
        Hotkey = kc;
        ButtonText.SetText($"({Hotkey.ToString()}) {TechType.ToString()}");
    }

    protected override Tooltip GetTooltipContent() {
        Lane lane = ClientLaneTracker.Singleton.MyLane;
        if (lane == null) {
            return new StandardTooltip();
        }

        return new ResearchElementTooltip(TechType);
    }
}
