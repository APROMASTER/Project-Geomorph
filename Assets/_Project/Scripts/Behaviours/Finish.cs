using UnityEngine;
using UnityEngine.Events;

public class Finish : InteractableObject
{
    [SerializeField] private GameLevelData _gameLevelData;
    [SerializeField] private UnityEvent<int> _onTeleport;

    public override void Interact(Transform interactor)
    {
        _gameLevelData.Levels[_gameLevelData.CurrentLevel].SetCompleted();
        _onTeleport?.Invoke(_gameLevelData.Levels[_gameLevelData.CurrentLevel].SceneBuildIndex);
    }
}
