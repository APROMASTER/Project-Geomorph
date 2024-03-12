using UnityEngine;
using UnityEngine.Events;

public class OnStart : MonoBehaviour
{
    [SerializeField] private UnityEvent _onStart;
    void Start()
    {
        _onStart?.Invoke();
    }
}
