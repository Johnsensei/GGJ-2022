using UnityEngine;

public class GrowAndShrink : MonoBehaviour
{
    public float Speed = 1;
    public float MaxGrow = 2f;
    public float MaxShrink = .5f;

    private bool IsGrowing = false;

    void Update()
    {
        var change = Vector3.one * Speed * Time.deltaTime;
        if (IsGrowing)
        {
            if(transform.localScale.x + change.x >= MaxGrow) 
            {
                transform.localScale = Vector3.one * MaxGrow;
                IsGrowing = false;
            }
            else
                transform.localScale += change;
        }
        else
        {
            var shrink = Vector3.one * Speed * Time.deltaTime;
            if (transform.localScale.x + change.x <= MaxShrink)
            {
                transform.localScale = Vector3.one * MaxShrink;
                IsGrowing = true;
            }
            else
                transform.localScale -= change;
        }
    }
}
