using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FadeText : Fader<TextMeshProUGUI>
{
    public override float GetComponentToFadeAlpha()
    {
        return ItemToFade.color.a;
    }

    public override void UpdateComponentToFadeAlpha(float newAlpha)
    {
        ItemToFade.color = new Color(ItemToFade.color.r, ItemToFade.color.g, ItemToFade.color.b, newAlpha);
    }
}
