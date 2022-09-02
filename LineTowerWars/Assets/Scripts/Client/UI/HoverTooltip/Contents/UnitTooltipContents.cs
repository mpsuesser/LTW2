using UnityEngine;
using TMPro;
using System;

public class UnitTooltipContents : MonoBehaviour {
    [SerializeField] private TMP_Text Name;
    [SerializeField] private TMP_Text GoldCost;
    [SerializeField] private TMP_Text Income;
    [SerializeField] private TMP_Text Bounty;
    [SerializeField] private TMP_Text MaxHP;
    [SerializeField] private TMP_Text ArmorType;
    [SerializeField] private TMP_Text MoveSpeed;

    public void Load(UnitTooltip tooltip) {
        Name.SetText(tooltip.Name);
        GoldCost.SetText(tooltip.GoldCost.ToString());
        Income.SetText(tooltip.IncomeReward.ToString());
        Bounty.SetText(tooltip.Bounty.ToString());
        MaxHP.SetText(tooltip.MaxHP.ToString());
        ArmorType.SetText(tooltip.ArmorType);
        MoveSpeed.SetText($"{tooltip.MoveSpeed:0.#}");
    }
}
