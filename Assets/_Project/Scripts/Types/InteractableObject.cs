using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField] private bool _interactable = true;
    public bool Interactable { get => _interactable; set => _interactable = value; }
    private bool _disabled = false;
    public bool Disabled { get => _disabled; set => _disabled = value; }
    
    abstract public void Interact(Transform interactor);
}
