using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // [SerializeField] private List<Scene> _loadedScenes = new List<Scene>();

    public void AddScene(int sceneBuildIndex)
    {
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Additive);

        // _loadedScenes.Add();
    }

    public void UnloadScene(int sceneBuildIndex)
    {
        SceneManager.UnloadSceneAsync(sceneBuildIndex);
    }
}
