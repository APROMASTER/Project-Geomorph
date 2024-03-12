using System;
using UnityEngine;

[CreateAssetMenu]
public class GameLevelData : ScriptableObject
{
    [Serializable]
    public struct GameLevel
    {
        public string Name;
        public bool Completed;
        public int SceneBuildIndex;

        public void SetCompleted() => Completed = true;
    }

    public GameLevel[] Levels;
    public int CurrentLevel = 0;

    public int GetNextLevel()
    {
        int lastUnlockedLevel = 0;

        for (int i = 0; i < Levels.Length; i++)
        {
            if (!Levels[i].Completed)
            {
                lastUnlockedLevel = i;
                break;
            }
        }

        return lastUnlockedLevel;
    }
}
