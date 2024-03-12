using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private List<LearnableObjectData> _targetFilters = new();
    [SerializeField] private bool _followWhenTarget;
    [SerializeField] private AnimationCurve _rotationAnimation = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private float _animationDuration = 1, _checkDistance = 5, _checkAmplittude = 1;
    [SerializeField, Min(3)] private int _checkSamples;
    [SerializeField] private Color _idleColor = Color.green;
    [SerializeField] private Color _foundColor = Color.red;
    [SerializeField] private UnityEvent<Color> _onFindTarget;
    [SerializeField] private UnityEvent<Color> _onLoseTarget;
    private float _delta, _progress;
    private Transform _target;

    private void Start() 
    {
        _delta = 1 / _animationDuration;
    }

    void Update()
    {
        CheckTarget();

        if (_target == null) 
        {
            DoIdleAnimation();
            //return;
        }
        else
        {
            if (!_target.gameObject.activeSelf) LoseTarget();
        }
        
        if (_target != null && _followWhenTarget) TrackTarget();
    }

    private void CheckTarget()
    {
        Transform newTarget = null;
        for (int i = 0; i < _checkSamples; i++)
        {
            Vector3 direction = Quaternion.AngleAxis((i - _checkSamples * 0.5f) * (_checkAmplittude / _checkSamples), Vector3.forward) * transform.right;

            Ray ray = new Ray(transform.position, direction);
            RaycastHit2D result = Physics2D.Raycast(transform.position, direction, _checkDistance);
            if (result.transform != null)
            {
                // if (result.transform.CompareTag("Player")) newTarget = result.transform;
                if (result.transform.TryGetComponent(out TransformableEntity transformableEntity))
                {
                    foreach (var filter in _targetFilters)
                    {
                        if (transformableEntity.LearnableData == filter)
                        {
                            newTarget = result.transform;
                            break;
                        }
                    }
                }
                
                if (_target == null)
                {
                    if (newTarget != null)
                    {
                        SetTarget(result.transform);
                        // break;
                        Debug.DrawRay(ray.origin, ray.direction * result.distance, Color.green);
                    }
                    else
                    {
                        Debug.DrawRay(ray.origin, ray.direction * result.distance, Color.yellow);
                    }
                }
                else
                {
                    if (newTarget != null)
                    {
                        Debug.DrawRay(ray.origin, ray.direction * result.distance, Color.green);
                    }
                    else 
                    {
                        Debug.DrawRay(ray.origin, ray.direction * result.distance, Color.yellow);
                    }
                }
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * _checkDistance, Color.red);
            }
            
        }
        if (newTarget == null) LoseTarget();
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            LoseTarget();
        }

    }

    void DoIdleAnimation()
    {
        if (_progress >= 1)
        {
            _progress = 0;
            _delta = 1 / _animationDuration;
        }
        _progress += Time.deltaTime * _delta;

        transform.rotation = Quaternion.AngleAxis(_rotationAnimation.Evaluate(_progress), Vector3.forward);
    }

    void TrackTarget()
    {
        transform.right = _target.position - transform.position;
    }

    public void SetTarget(Transform newtarget)
    {
        _target = newtarget;
        _onFindTarget?.Invoke(_foundColor);
    }

    public void LoseTarget()
    {
        _target = null;
        _onLoseTarget?.Invoke(_idleColor);
    }
}
