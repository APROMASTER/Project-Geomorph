using UnityEngine;

[ExecuteInEditMode]
public class SideScrollerCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector2 _offsetPosition;
    [SerializeField] private Vector2 _staticArea;
    [SerializeField, Range(0, 100f)] private float _damping, _maxDampSpeed;
    Vector2 _finalPosition;
    Vector3 deltaPosition;

    void Start()
    {
        _finalPosition = _target.position;
    }

    void Update()
    {
        if (_target == null) return;
        Vector2 targetPosition = _target.position;

        _finalPosition.x = Mathf.Clamp(_finalPosition.x, targetPosition.x - _staticArea.x, targetPosition.x + _staticArea.x);
        _finalPosition.y = Mathf.Clamp(_finalPosition.y, targetPosition.y - _staticArea.y, targetPosition.y + _staticArea.y);
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(_finalPosition.x + _offsetPosition.x, _finalPosition.y + _offsetPosition.y, transform.position.z), ref deltaPosition, _damping, _maxDampSpeed);
    }

    private void OnDrawGizmos() 
    {
        if (_target == null) return;
        Vector3 camPosition = transform.position;
        Gizmos.DrawWireCube(new Vector3(camPosition.x - _offsetPosition.x, camPosition.y - _offsetPosition.y, _target.position.z), _staticArea * 2);
    }
}
