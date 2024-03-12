using UnityEngine;
using UnityEngine.Events;

public class PausableBody2D : MonoBehaviour
{
    private Rigidbody2D _body;
    private Vector3 _savedVelocity;
    [SerializeField] private bool _onlyEventDriven = false;
    [SerializeField] private UnityEvent _onSleep;
    [SerializeField] private UnityEvent _onWake;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        PauseManager.Instance.OnPause += Sleep;
        PauseManager.Instance.OnResume += WakeUp;
    }

    private void OnDestroy()
    {
        PauseManager.Instance.OnPause -= Sleep;
        PauseManager.Instance.OnResume -= WakeUp;
    }

    private void Sleep()
    {
        if (!_onlyEventDriven) SleepBody(); 
        _onSleep?.Invoke();
    }

    private void WakeUp()
    {
        if (!_onlyEventDriven) WakeBody();
        _onWake?.Invoke();
    }

    private void SleepBody()
    {
        if (_body == null) return;
        
        _savedVelocity = _body.velocity;
        _body.Sleep();
        _body.simulated = false;
        // Debug.Log(name + "'s rigidbody is asleep!");
    }

    private void WakeBody()
    {
        if (_body == null) return;

        _body.simulated = true;
        _body.WakeUp();
        _body.velocity = _savedVelocity;
        // Debug.Log(name + "'s rigidbody wake up!");
    }
}
