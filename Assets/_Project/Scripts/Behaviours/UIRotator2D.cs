using System.Collections;
using UnityEngine;

public class UIRotator2D : MonoBehaviour
{
    [SerializeField] private AnimationCurve _rotationCurve = AnimationCurve.Linear(0, 0, 1, 1); 
    [SerializeField] private float _duration = 1f;
    private Quaternion _previousRotation;
    private Coroutine _rotationCoroutine;
    public void RotateToInAngles(float angles)
    {
        _rotationCoroutine = StartCoroutine(Rotate(angles));
    }

    public void Reset()
    {
        StopRotation();
        transform.rotation = Quaternion.identity;
    }

    public void StopRotation()
    {
        if (_rotationCoroutine != null)
        {
            StopCoroutine(_rotationCoroutine);
            _rotationCoroutine = null;
        }
    }

    public void ReturnToPreviousRotation()
    {
        StopRotation();
        transform.rotation = _previousRotation;
    }

    IEnumerator Rotate(float angles)
    {
        Quaternion initialRotation = _previousRotation = transform.rotation;
        Quaternion finalRotation = Quaternion.AngleAxis(initialRotation.eulerAngles.z + angles, Vector3.forward);
        float progress = 0, deltaProgress = 1 / _duration;
        while (progress < 1)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, finalRotation, _rotationCurve.Evaluate(progress));
            progress += Time.deltaTime * deltaProgress;
            yield return null;
        }
        transform.rotation = finalRotation;
        _rotationCoroutine = null;
    }
}
