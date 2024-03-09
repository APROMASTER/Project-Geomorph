using System;
using UnityEngine;

public class PauseManager
{
    private static PauseManager _instance;
    public static PauseManager Instance 
    {
        get
        {
            if (_instance == null) _instance = new();
            return _instance;
        }
    }

    public event Action OnPause;
    public event Action OnResume;

    public void Pause() => OnPause?.Invoke();
    public void Resume() => OnResume?.Invoke();
}
