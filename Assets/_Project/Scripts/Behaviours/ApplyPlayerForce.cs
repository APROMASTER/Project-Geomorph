using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ApplyPlayerForce : MonoBehaviour
{
    [SerializeField] private float _forceMagnitude;
    private Rigidbody2D _rigidbody2d;

    private void Start() 
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        _rigidbody2d.AddForce(Input.GetAxisRaw("Horizontal") * _forceMagnitude * Vector2.right);
    }
}
