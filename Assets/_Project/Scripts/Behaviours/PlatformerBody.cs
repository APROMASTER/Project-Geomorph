using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlatformerBody : MonoBehaviour
{
    private Rigidbody _body;
    private bool _onGround;
    private Vector3 _velocity, _desiredMovement;
    void Awake() => _body = GetComponent<Rigidbody>();

    void FixedUpdate()
    {
        _velocity = _desiredMovement;
        _body.velocity = _velocity;
    }

    public void Move(Vector3 moveVelocity) => _desiredMovement = moveVelocity;

    public void Jump(float jumpHeight) 
    {
        if (_onGround) {
			_velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
		}
    }
}
