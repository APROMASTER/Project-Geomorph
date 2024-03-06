using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [SerializeField] private PlayerPhysicsTemp _playerObject;
    [SerializeField] private Checkpoint _lastCheckpoint;
    [SerializeField] private float _respawnDelaySeconds = 0.5f;

    private void Start() 
    {
        GameEvents.Instance.OnCheckpointReached += SetLastCheckpoint;
        GameEvents.Instance.OnPlayerDead += RespawnPlayer;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnCheckpointReached -= SetLastCheckpoint;
        GameEvents.Instance.OnPlayerDead -= RespawnPlayer;
    }

    public void SetLastCheckpoint(Checkpoint checkpoint)
    {
        _lastCheckpoint = checkpoint;
    }

    public void RespawnPlayer() => StartCoroutine(RespawnProcess());
    public IEnumerator RespawnProcess()
    {
        yield return new WaitForSeconds(_respawnDelaySeconds);
        _playerObject.BodyPosition = _lastCheckpoint.RespawnPosition.position;
        _playerObject.Revive();
    }
}
