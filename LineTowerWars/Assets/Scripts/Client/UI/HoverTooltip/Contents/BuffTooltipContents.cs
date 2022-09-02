using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuffTooltipContents : MonoBehaviour {
    [SerializeField] private TMP_Text Name;
    [SerializeField] private TMP_Text Description;
    [SerializeField] private Image BuffImage;

    public void Load(BuffTooltip tooltip) {
        Name.SetText(tooltip.Name);
        Description.SetText(tooltip.Description);
        BuffImage.sprite = tooltip.BuffImageSprite;
    }
}