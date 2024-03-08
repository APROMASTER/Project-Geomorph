using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformerBody : MonoBehaviour
{
    [SerializeField] private float _gravityScale = 1;
    private Rigidbody2D _body;
    private bool _onGround, _onWall, _isAsleep;
    private Vector2 _velocity, _desiredMovement, _fallVelocity, _wallNormal;
    private void Awake() => _body = GetComponent<Rigidbody2D>();

    private void FixedUpdate()
    {
        if (_isAsleep)
        {
            _body.Sleep();
            ClearState();
            return;
        }

        _fallVelocity += _gravityScale * Time.fixedDeltaTime * Physics2D.gravity;
        // _fallVelocity -= _desiredMovement;

        if (_onGround)
        {
            //Debug.Log("Grounded");
            if (Vector2.Dot(_fallVelocity, Physics2D.gravity) > 0) _fallVelocity = Vector2.zero;
        }
        else if (_onWall)
        {
            if (Vector2.Dot(_fallVelocity, Physics2D.gravity) > 0)
            {
                
                _fallVelocity = Vector2.ClampMagnitude(_fallVelocity, 2f);
            }
        }
        
        _velocity = _fallVelocity;
        _velocity += Time.fixedDeltaTime * _desiredMovement;
        _body.velocity = _velocity;
        ClearState();
    }

    private void ClearState()
    {
        _onGround = false;
        _onWall = false;
    }

    private void OnCollisionEnter2D(Collision2D other) => EvaluateCollision(other);
    private void OnCollisionStay2D(Collision2D other) => EvaluateCollision(other);
    private void EvaluateCollision(Collision2D other)
    {
        for (int i = 0; i < other.contactCount; i++)
        {
            if (Vector2.Dot(other.GetContact(i).normal, Vector2.up) > 0.5f) _onGround = true;
            if (Mathf.Abs(Vector2.Dot(other.GetContact(i).normal, Vector2.up)) < 0.3f) 
            {
                _onWall = true;
                _wallNormal = other.GetContact(i).normal;
            }
        }
    }

    public void Move(Vector2 moveVelocity) => _desiredMovement = moveVelocity;

    public void Jump(float jumpHeight) 
    {
        if (_onGround) 
        {
			_fallVelocity = jumpHeight * _gravityScale * -Physics2D.gravity.normalized;
		}
        else if (_onWall)
        {
            _fallVelocity = jumpHeight * _gravityScale * (-Physics2D.gravity.normalized + (_wallNormal * 0.6f)).normalized;
        }
    }

    public void DoArtificialJump(float jumpHeight)
    {
        _fallVelocity = jumpHeight * _gravityScale * -Physics2D.gravity.normalized;
    }

    public void GravityCancel()
    {
        _fallVelocity = Vector2.zero;
    }

    public void Sleep()
    {
        _isAsleep = true;
        _body.isKinematic = true;
    }

    public void WakeUp()
    {
        _isAsleep = false;
        _body.isKinematic = false;
        _body.WakeUp();
    }
}
