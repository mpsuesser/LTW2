using UnityEngine;
using TMPro;

public class StandardTwoBodiesTooltipContents : MonoBehaviour
{
    [SerializeField] private TMP_Text Title;
    [SerializeField] private TMP_Text CornerNote;
    [SerializeField] private TMP_Text Body;
    [SerializeField] private TMP_Text SecondBody;

    public void Load(StandardTwoBodiesTooltip tooltip) {
        Title.SetText(tooltip.Title);
        CornerNote.SetText(tooltip.CornerNote);
        Body.SetText(tooltip.Body);
        SecondBody.SetText(tooltip.SecondBody);
    }
}