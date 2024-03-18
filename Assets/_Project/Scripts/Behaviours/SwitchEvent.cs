using UnityEngine;
using UnityEngine.Events;

public class SwitchEvent : MonoBehaviour
{
    private bool _activated;
    [SerializeField] private UnityEvent _onActivate, _onDeactivate;

    public void Toggle()
    {
        _activated = !_activated;

        if (_activated) Activate();
        else Deactivate();
    }

    public void Activate() 
    {
        _activated = true;
        _onActivate?.Invoke();
    }

    public void Deactivate() 
    {
        _activated = false;
        _onDeactivate?.Invoke();
    }
}
