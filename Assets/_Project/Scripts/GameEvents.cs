using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance { get; private set; }
    public event Action OnPlayerDead;
    public event Action<Checkpoint> OnCheckpointReached;
    public event Action<int> OnUnlockDoor;
    
    public void PlayerDies() => OnPlayerDead?.Invoke();
    public void CheckpointReach(Checkpoint checkpoint) => OnCheckpointReached?.Invoke(checkpoint);
    public void UnlockDoor(int doorId) => OnUnlockDoor?.Invoke(doorId);

    private void Awake() => Instance = this;
}
