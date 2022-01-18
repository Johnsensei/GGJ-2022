using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeImage : Fader<Image>
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
