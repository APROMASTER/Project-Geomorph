using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform _respawnPosition;
    public Transform RespawnPosition { get => _respawnPosition; }
    [SerializeField] private UnityEvent _onCheckpointReached;
    [SerializeField] private UnityEvent _onCheckPointReturn;
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.TryGetComponent(out TransformableEntity transformableEntity))
        {
            _onCheckpointReached?.Invoke();
        }
    }

    public void ReturnProgress()
    {
        _onCheckPointReturn?.Invoke();
    }
}
