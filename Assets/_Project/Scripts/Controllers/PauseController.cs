using UnityEngine;

public class PauseController : MonoBehaviour
{
    private bool _isPaused = false;

    private void Update() // POSSIBLE CHANGE IF USING Input System
    {
        // if (!Input.GetKeyDown(KeyCode.Escape)) return;

        // if (_isPaused) Resume();
        // else Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        PauseManager.Instance.Pause();
        _isPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PauseManager.Instance.Resume();
        _isPaused = false;
    }
}
