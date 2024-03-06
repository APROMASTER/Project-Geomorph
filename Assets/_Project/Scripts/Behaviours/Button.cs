using UnityEngine;
using UnityEngine.Events;

public class Button : InteractableObject
{
    [SerializeField] private UnityEvent _onPress;
    public override void Interact(Transform interactor)
    {
        _onPress?.Invoke();
    }
    
}
