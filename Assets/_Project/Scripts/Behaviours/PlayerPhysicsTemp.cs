using UnityEngine;

public class PlayerPhysicsTemp : MonoBehaviour
{
    Rigidbody _rigidbody;
    public enum PlayerStates {Standing, Jumping, Falling, Stomping, OnInteraction, Dead}
    public enum PlayerModules {Sphere, Cube, Pyramid}
    [SerializeField] float _moveSpeed = 100, _jumpHeight = 30, _stompSpeed, _gravityScale = 1;
    [SerializeField] int _jumpCount, _maxJumps = 1;
    [SerializeField] PlayerStates _currentState = PlayerStates.Standing;
    [SerializeField] PlayerModules _currentModule = PlayerModules.Sphere;
    float _playerInput, _jumpScale;
    Vector3 _moveVelocity, _fallVelocity;   

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _playerInput = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate() 
    {
        float speedScale = _jumpScale = 1;

        Vector3 gravityCalculation = _fallVelocity + (Time.fixedDeltaTime * _gravityScale * Physics.gravity);
        _fallVelocity = CheckGround() ? Vector3.zero : gravityCalculation;

        switch (_currentModule)
        {
            case PlayerModules.Sphere:
            {

            } break;

            case PlayerModules.Cube:
            {
                speedScale = 0.85f;
                _jumpScale = 0;
            } break;

            case PlayerModules.Pyramid:
            {
                speedScale = 0.7f;
                _jumpScale = 0;
            } break;
        }
        
        switch (_currentState)
        {
            case PlayerStates.Standing:
            {
                if (!CheckGround())
                {
                    _currentState = PlayerStates.Falling;
                }
                JumpAction();
            } break;

            case PlayerStates.Jumping:
            {
                if (Vector3.Dot(_fallVelocity, Physics.gravity.normalized) > 0)
                {
                    _currentState = PlayerStates.Falling;
                }
                StompAction();
                JumpAction();
            } break;

            case PlayerStates.Falling:
            {
                if (CheckGround())
                {
                    _jumpCount = 0;
                    _currentState = PlayerStates.Standing;
                }
                StompAction();
                JumpAction();
            } break;

            case PlayerStates.Stomping:
            {
                speedScale = 0;
                if (CheckGround())
                {
                    _jumpCount = 0;
                    _currentState = PlayerStates.Standing;
                }
            } break;

            case PlayerStates.OnInteraction:
            {
                speedScale = 0;
            } break;
        }
        _moveVelocity = _playerInput * _moveSpeed * speedScale * Time.fixedDeltaTime * Vector3.right;
        
        _rigidbody.velocity = _moveVelocity + _fallVelocity;
    }

    void JumpAction()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _jumpCount < _maxJumps)
        {
            _fallVelocity = -_jumpHeight * _jumpScale * _gravityScale * Physics.gravity.normalized;
            _jumpCount ++;
            _currentState = PlayerStates.Jumping;
        }
    }

    void StompAction()
    {
        if (Input.GetAxisRaw("Vertical") < 0 && Input.GetKeyDown(KeyCode.Space))
        {
            _fallVelocity = _stompSpeed * Physics.gravity.normalized;
            _currentState = PlayerStates.Stomping;
        }
    }

    bool CheckGround()
    {
        if (Physics.Raycast(transform.position, -transform.up, 0.5f)) 
        {
            return true;
        }
        return false;
    }
}
