using UnityEngine;

public class EnableOnInteract : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        gameObject.SetActive(true);
    }
}
