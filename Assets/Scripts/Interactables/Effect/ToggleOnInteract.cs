using UnityEngine;

public class ToggleOnInteract : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        gameObject.SetActive(gameObject.activeInHierarchy);
    }
}
