using UnityEngine;
using UnityEngine.Events;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerStandingState _standingState = new();
    private PlayerState _currentState;
    [SerializeField] private float _maxAcceleration = 20;
    [SerializeField] private UnityEvent<Vector3> _onMove;
    [SerializeField] private UnityEvent<float> _onJump;
    public float MoveSpeed { get; set; }
    private Vector3 _velocity;

    void Start()
    {
        SwitchState(_standingState);
    }

    void Update()
    {
        Vector2 playerInput = Vector2.zero;
		playerInput.x = Input.GetAxis("Horizontal");
		//playerInput.y = Input.GetAxis("Vertical");
		//playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        
        _currentState.UpdateState(this);

        Accelerate(MoveSpeed * playerInput);
        _onMove?.Invoke(_velocity);
        // _onJump?.Invoke();
    }

    public void Accelerate(Vector3 desiredVelocity)
    {
        float maxSpeedChange = _maxAcceleration * Time.deltaTime;
		_velocity.x =
			Mathf.MoveTowards(_velocity.x, desiredVelocity.x, maxSpeedChange);
		_velocity.z =
			Mathf.MoveTowards(_velocity.z, desiredVelocity.z, maxSpeedChange);

    }

    public void SwitchState(PlayerState newState)
	{
		if (_currentState != null) _currentState.ExitState(this);
		_currentState = newState;
		newState.EnterState(this);
	}
}
