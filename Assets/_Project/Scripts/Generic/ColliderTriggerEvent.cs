using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collider2DTriggerEvent : MonoBehaviour
{
    public enum EventType {Enter, Stay, Exit, Noone}
    [SerializeField] private EventType _eventType;
    [SerializeField] private List<string> _detectedTags = new List<string>();
    //[SerializeField] private LayerMask _detectedLayers;
    [SerializeField] private UnityEvent _onRaised;
    bool _staying = false;

    void Update() 
    {
        if (_staying && _eventType != EventType.Noone) return;
        _onRaised?.Invoke();
    }
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (_eventType != EventType.Enter) return;
        if (!HasTags(other)) return;

        _onRaised?.Invoke();
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (_eventType != EventType.Stay) return;
        if (!HasTags(other)) return;

        _staying = true;
        _onRaised?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (_eventType != EventType.Exit) return;
        if (!HasTags(other)) return;

        _onRaised?.Invoke();
    }

    bool HasTags(Collider2D other)
    {
        bool tagFilterPass = false;
        if (_detectedTags.Count == 0)
        {
            tagFilterPass = true;
        }
        else foreach (var tag in _detectedTags)
        {
            if (other.CompareTag(tag))
            {
                tagFilterPass = true;
                break;
            }
        }
        return tagFilterPass;
    }
}