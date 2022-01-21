using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Fader<T> : MonoBehaviour, IFader where T : Component
{
    [Range(0.01f, 5)]
    public float FadeSpeedInSeconds = 1;

    internal T ItemToFade;
    Coroutine fadeCoroutine;

    public void Fade(bool fadeIn = true)
    {
        if (ItemToFade == null)
            ItemToFade = GetComponentToFade();

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeCoroutine(fadeIn));
    }

    IEnumerator FadeCoroutine(bool fadeIn)
    {
        var desiredAlpha = fadeIn ? 1 : 0;
        var newAlpha = (desiredAlpha - GetComponentToFadeAlpha()) * (1 / FadeSpeedInSeconds) * Time.deltaTime;

        while (Mathf.Abs(GetComponentToFadeAlpha() - desiredAlpha) > 0.01f)
        {
            UpdateComponentToFadeAlpha(GetComponentToFadeAlpha() + newAlpha);
            yield return new WaitForEndOfFrame();
        }

        UpdateComponentToFadeAlpha(desiredAlpha);
    }

    public virtual T GetComponentToFade()
    {
        return GetComponent<T>();
    }    

    public virtual float GetComponentToFadeAlpha() { return 0; }

    public virtual void UpdateComponentToFadeAlpha(float newAlpha) { }

    public void SetFadeSpeed(float fadeSpeed)
    {
        FadeSpeedInSeconds = fadeSpeed;
    }

}

public interface IFader
{
    public void Fade(bool fadeIn);

    public void SetFadeSpeed(float speed);
}
