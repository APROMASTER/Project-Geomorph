using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointController : MonoBehaviour
{
    [SerializeField] private Transform _playerInstance;
    [SerializeField] private Checkpoint _lastCheckpoint;
    [SerializeField] private float _respawnDelaySeconds = 0.5f;
    [SerializeField] private UnityEvent _onPlayerDead;
    [SerializeField] private UnityEvent _onPlayerRevive;

    public void SetLastCheckpoint(Checkpoint checkpoint)
    {
        _lastCheckpoint = checkpoint;
    }

    public void RespawnPlayer() => StartCoroutine(RespawnProcess());
    public IEnumerator RespawnProcess()
    {
        _onPlayerDead?.Invoke();
        _playerInstance.position = _lastCheckpoint.RespawnPosition.position;
        _lastCheckpoint.ReturnProgress();
        yield return new WaitForSeconds(_respawnDelaySeconds);
        
        _onPlayerRevive?.Invoke();
    }
}
