using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform _respawnPosition; 
    [SerializeField] private string _playerTag = "Player";
    public Transform RespawnPosition { get => _respawnPosition; }
    
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag(_playerTag))
        {
            GameEvents.Instance.CheckpointReach(this);
        }
    }
}
