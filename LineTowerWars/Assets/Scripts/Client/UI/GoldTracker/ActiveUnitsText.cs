using UnityEngine;
using TMPro;

public class ActiveUnitsText : MonoBehaviour {
    private int CurrentValue { get; set; }

    private TMP_Text Text { get; set; }

    // Animation
    [SerializeField] private Color CappedUnitsColor;
    private Color OriginalColor { get; set; }


    private void Awake() {
        Text = GetComponent<TMP_Text>();
        OriginalColor = Text.color;
    }

    public void SetValue(int val) {
        if (val >= LaneSystem.Singleton.MaxActiveUnits) {
            Text.color = CappedUnitsColor;
        } else {
            Text.color = OriginalColor;
        }

        Text.SetText(val.ToString() + " / " + LaneSystem.Singleton.MaxActiveUnits.ToString());
    }
}
