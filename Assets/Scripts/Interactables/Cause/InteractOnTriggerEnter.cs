using System.Collections.Generic;
using UnityEngine;

public class InteractOnTriggerEnter : MonoBehaviour, IInteractable
{
    public string InteractableObjectTag;
    public LayerMask InteractableLayers;
    public List<GameObject> Interactables;

    public virtual void Interact()
    {
        foreach (var interactable in Interactables)
            interactable.GetComponent<IInteractable>().Interact();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var matchingTags = string.IsNullOrWhiteSpace(InteractableObjectTag) || collision.CompareTag(InteractableObjectTag);
        var otherLayer = collision.gameObject.layer;
        if (InteractableLayers.Contains(otherLayer) && matchingTags)
            Interact();
    }
}