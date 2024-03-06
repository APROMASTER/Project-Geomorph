using UnityEngine;

public class PlayerPhysicsTemp : MonoBehaviour
{
    Rigidbody _rigidbody;
    public Vector3 BodyPosition { get => _rigidbody.position; set => _rigidbody.position = value; }
    public enum PlayerStates {Standing, Jumping, Falling, Stomping, OnInteraction, Dead}
    public enum PlayerModules {Sphere, Cube, Pyramid}
    [SerializeField] float _moveSpeed = 100, _jumpHeight = 30, _stompSpeed, _gravityScale = 1, _invincibleTime = 2;
    [SerializeField] int _jumpCount, _maxJumps = 1;
    [SerializeField] PlayerStates _currentState = PlayerStates.Standing;
    [SerializeField] PlayerModules _currentModule = PlayerModules.Sphere;
    [SerializeField] string killObstacleTag = "Untagged";
    bool _isGrounded, _isRoofed;
    float _playerInput, _jumpScale, _currentInvincibleTime;
    Vector3 _moveVelocity, _fallVelocity, _groundNormal;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _currentInvincibleTime = _invincibleTime;
    }

    void Update()
    {
        _playerInput = Input.GetAxisRaw("Horizontal");

        if (_currentInvincibleTime < _invincibleTime)
        {
            _currentInvincibleTime += Time.deltaTime;
        }
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

            case PlayerStates.Dead:
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

    void DieAction()
    {
        GameEvents.Instance.PlayerDies();
    }

    public void Revive()
    {
        _currentState = PlayerStates.Standing;
        _jumpCount = 0;
    }

    bool CheckGround()
    {
        if (Physics.Raycast(transform.position, -transform.up, 0.5f)) 
        {
            return true;
        }
        return false;
    }

    private void OnCollisionEnter(Collision other) 
    {
        OnValidateCollision(other);
    }

    private void OnCollisionStay(Collision other) 
    {
        OnValidateCollision(other);
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.CompareTag(killObstacleTag))
        {
            if (_currentState == PlayerStates.Dead || _currentInvincibleTime < _invincibleTime) return;

            DieAction();
            _currentInvincibleTime = 0;
            _currentState = PlayerStates.Dead;
        }
    }

    private void OnValidateCollision(Collision other)
    {
        _groundNormal = Vector2.zero;

        for (int i = 0; i < other.contactCount; i++) {
			Vector3 normal = other.GetContact(i).normal;
			if (normal.y >= 0.4f) {
				// groundContactCount += 1;
				_groundNormal += normal;
                _isGrounded = true;
			}
            else if (normal.y < -0.4f) {
				_isRoofed = true;
                // steepContactCount += 1;
				// steepNormal += normal;
			}
		}
        
        // if (Vector2.Angle(_groundNormal, Vector2.down) < 30f)
        // {
            
        //     //_isGrounded = true;
        // }
        if (_groundNormal == Vector3.zero) _groundNormal = Vector2.up;
    }

}
