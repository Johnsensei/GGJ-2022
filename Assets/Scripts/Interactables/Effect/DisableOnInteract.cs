using UnityEngine;

public class DisableOnInteract : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        gameObject.SetActive(false);
    }
}
