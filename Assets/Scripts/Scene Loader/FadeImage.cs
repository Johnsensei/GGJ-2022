using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeImage : MonoBehaviour
{
    [Range(0.01f, 5)]
    public float FadeSpeedInSeconds = 1;

    Image image;
    Coroutine fadeCoroutine;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Fade(bool fadeIn = true)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeCoroutine(fadeIn? 1 : 0));
    }

    IEnumerator FadeCoroutine(float desiredAlpha)
    {
        var newAlpha = (desiredAlpha - image.color.a) * (1 / FadeSpeedInSeconds) * Time.deltaTime;

        while (Mathf.Abs(image.color.a - desiredAlpha) > 0.01f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + newAlpha);
            yield return new WaitForEndOfFrame();
        }
    }

}
