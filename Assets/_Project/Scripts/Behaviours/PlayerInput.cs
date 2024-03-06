using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 40f;
    [SerializeField] private float _jumpHeight = 10f;
    [SerializeField] private UnityEvent<Vector2> _onWalk;
    [SerializeField] private UnityEvent<float> _onJump;
    void Update()
    {
        _onWalk?.Invoke(_walkSpeed * Input.GetAxisRaw("Horizontal") * Vector3.right);
        if (Input.GetButtonDown("Jump")) _onJump?.Invoke(_jumpHeight);
    }
}
