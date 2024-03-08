using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LearnObject : MonoBehaviour
{
    [SerializeField] private LearnableObjectData _learnableData;
    public LearnableObjectData LearnableData { get => _learnableData; }
    public List<LearnableObjectData> LearnedObjects = new List<LearnableObjectData>();
    [SerializeField] private float _enteringTime = 0.5f;
    [SerializeField] private float _learningTime = 1f;
    [SerializeField] private float _exitingTime = 0.15f;
    [SerializeField] private float _endImpulseMagnitude = 5f;
    [SerializeField] private LayerMask _learnableLayer;
    [SerializeField] private string _conversionLayer;
    [SerializeField] private Transform _conversionStencil;
    [SerializeField] private UnityEvent _onLearnStart;
    [SerializeField] private UnityEvent<Vector3> _onLearning;
    [SerializeField] private UnityEvent<Vector3> _onLearnComplete;
    [SerializeField] private UnityEvent _onLearnEnd;
    private bool _canLearn = true;

    public void Learn(LearnableObject learnableObject)
    {
        // Check the LearnableData
        if (LearnedObjects.Find(x => x == learnableObject.LearnableData) != null 
        || !_canLearn) 
            return;

        StartCoroutine(LearnTransition(learnableObject));
    }

    IEnumerator LearnTransition(LearnableObject objectToLearn)
    {
        _canLearn = false;
        _onLearnStart?.Invoke();

        Vector3 targetSize = objectToLearn.Collider2D.bounds.extents.magnitude * Vector3.one;

        Vector3 initialPosition = transform.position;
        float progress = 0;
        float enteringDelta = 1 / _enteringTime;

        Transform targetTransform = objectToLearn.transform;
        GameObject _fakeConverted = Instantiate(objectToLearn.LearnableData.LearnedEntity.gameObject, targetTransform.position, objectToLearn.transform.rotation);
        _fakeConverted.transform.SetParent(targetTransform);
        _fakeConverted.layer = LayerMask.NameToLayer(_conversionLayer);
        if (_fakeConverted.TryGetComponent(out Collider2D collider2D))
        {
            collider2D.enabled = false;
        }
        if (_fakeConverted.TryGetComponent(out Rigidbody2D rigidbody2D))
        {
            rigidbody2D.simulated = false;
        }
        if (_fakeConverted.TryGetComponent(out MeshRenderer meshRenderer))
        {
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
        _conversionStencil.localScale = Vector3.zero;

        // Mover el objeto hacia el centro del objeto Target
        while (Vector3.Distance(transform.position, objectToLearn.transform.position) > 0.001f)
        {
            progress += Time.deltaTime * enteringDelta;
            Vector3 newPosition = Vector3.Lerp(initialPosition, objectToLearn.transform.position, progress);
            _conversionStencil.localScale = Vector3.Lerp(Vector3.zero, targetSize, progress);
            _onLearning?.Invoke(newPosition);
            yield return null;
        }
        // Aprender el objeto
        yield return new WaitForSeconds(_learningTime);

        progress = 0;
        LearnedObjects.Add(objectToLearn.LearnableData);
        initialPosition = transform.position;
        float exitingDelta = 1 / _exitingTime;
        // while (Physics2D.CircleCast(transform.position, 0.5f, Vector3.up, 0.5f, _learnableLayer.value))
        while (progress < 1)
        {
            // Debug.Log("Colliding");
            _conversionStencil.localScale = Vector3.Lerp(targetSize, Vector3.zero, progress);
            Vector3 newPosition = Vector3.Lerp(initialPosition, initialPosition + (objectToLearn.Collider2D.bounds.extents.y * Vector3.up), progress);
            _onLearnComplete?.Invoke(newPosition);
            progress += Time.deltaTime * exitingDelta;
            yield return null;
        }
        Destroy(_fakeConverted);
        // Debug.Log("END");
        _onLearnEnd?.Invoke();
        _canLearn = true;
        yield return null;
    }

    private void OnDrawGizmos() 
    {
        // Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
    
}
