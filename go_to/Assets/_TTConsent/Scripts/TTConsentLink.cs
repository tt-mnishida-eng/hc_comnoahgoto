using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TTConsentLink : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData e)
    {
        var text = GetComponent<TextMeshProUGUI>();
        var pos = Input.mousePosition;
        int index = TMP_TextUtilities.FindIntersectingLink(text, pos, text.canvas.worldCamera);
        if (index == -1) return;
        var linkInfo = text.textInfo.linkInfo[index];
        var url = linkInfo.GetLinkID();
        Application.OpenURL(url);
    }
}