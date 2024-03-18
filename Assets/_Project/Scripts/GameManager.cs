using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private bool _gameOver;
    [SerializeField] private UnityEvent _onGameOver;

    public bool GameOver 
    {
        set 
        { 
            _gameOver = value; 
            if (value == true) _onGameOver?.Invoke();
        }
    }
}
